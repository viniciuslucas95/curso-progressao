using CursoProgressao.Server.Data;
using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Models;
using CursoProgressao.Server.Services.Classes;
using CursoProgressao.Shared.Dto.Classes;
using CursoProgressao.Shared.Dto.Contracts;
using CursoProgressao.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Server.Services.Contracts;

public class ContractsService : IContractsService
{
    private readonly SchoolContext _context;
    private readonly IClassesService _classesService;
    private readonly NotFoundException _notFoundException = new("ContractNotFound");

    public ContractsService(SchoolContext context, IClassesService classesService)
    {
        _context = context;
        _classesService = classesService;
    }

    public async Task<Guid> CreateAsync(Guid studentId, CreateContractDto dto, Func<Guid, Task> checkStudentExistenceAsync)
    {
        await checkStudentExistenceAsync(studentId);
        await _classesService.CheckExistenceAsync(dto.ClassId);

        Contract contract = new(studentId, dto.ClassId, dto.DueDateDay, dto.PaymentValue, dto.StartDate, dto.EndDate);
        _context.Contracts.Add(contract);

        return contract.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateContractDto dto)
    {
        Contract contract = await GetModelAsync(id);

        if (dto.DueDateDay is not null) contract.DueDateDay = (int)dto.DueDateDay;
        if (dto.PaymentValue is not null) contract.PaymentValue = (float)dto.PaymentValue;

        if (dto.CancelDate is not null)
        {
            CheckDatesRange(contract.StartDate, contract.EndDate, (DateTime)dto.CancelDate);

            contract.CancelDate = dto.CancelDate;
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        Contract contract = await GetCompleteModelAsync(id);

        _context.Contracts.Remove(contract);
    }

    public async Task<IEnumerable<GetAllContractsDto>> GetAllAsync(Guid studentId)
    {
        IQueryable<GetAllClassesDto> classes = _classesService.QueryAll();

        return await _context.Contracts
            .Where(contract => contract.StudentId == studentId)
            .Include(contract => contract.Payments)
            .Join(
                classes,
                contract => contract.ClassId,
                classObj => classObj.Id,
                (contract, classObj) => new GetAllContractsDto()
                {
                    Id = contract.Id,
                    StartDate = contract.StartDate,
                    EndDate = contract.EndDate,
                    CancelDate = contract.CancelDate,
                    DueDateDay = contract.DueDateDay,
                    PaymentValue = contract.PaymentValue,
                    Class = classObj.Name,
                    IsOwing = IsOwing(new()
                    {
                        StartDate = contract.StartDate,
                        EndDate = contract.EndDate,
                        CancelDate = contract.CancelDate,
                        DueDateDay = contract.DueDateDay,
                        ReferenceDates = contract.Payments.Select(payment => payment.ReferenceDate)
                    }),
                    IsActive = IsActive(new()
                    {
                        StartDate = contract.StartDate,
                        EndDate = contract.EndDate,
                        CancelDate = contract.CancelDate
                    })
                })
            .ToListAsync();
    }

    public async Task GetAndCheckDatesRange(Guid contractId, DateTime date)
    {
        var result = await (from contract in _context.Contracts
                            where contract.Id == contractId
                            select new
                            {
                                contract.StartDate,
                                contract.EndDate,
                                contract.CancelDate
                            }
                            )
                            .FirstOrDefaultAsync();

        if (result is null) throw _notFoundException;

        CheckDatesRange(result.StartDate, result.EndDate, date, result.CancelDate);
    }

    public IQueryable<GetAllContractsSummaryDto> QueryAllSummary()
    {
        IQueryable<GetAllClassesDto> classes = _classesService.QueryAll();

        return _context.Contracts
            .Include(contract => contract.Payments)
            .Join(
                classes,
                contract => contract.ClassId,
                classObj => classObj.Id,
                (contract, classObj) => new GetAllContractsSummaryDto
                {
                    Id = contract.Id,
                    Class = classObj.Name,
                    IsOwing = IsOwing(new()
                    {
                        StartDate = contract.StartDate,
                        EndDate = contract.EndDate,
                        CancelDate = contract.CancelDate,
                        DueDateDay = contract.DueDateDay,
                        ReferenceDates = contract.Payments.Select(payment => payment.ReferenceDate)
                    }),
                    IsActive = IsActive(new()
                    {
                        StartDate = contract.StartDate,
                        EndDate = contract.EndDate,
                        CancelDate = contract.CancelDate
                    })
                });
    }

    private async Task<Contract> GetModelAsync(Guid id)
    {
        Contract? contract = await _context.Contracts
            .FirstOrDefaultAsync(contract => contract.Id == id);

        if (contract is null) throw _notFoundException;

        return contract;
    }

    private async Task<Contract> GetCompleteModelAsync(Guid id)
    {
        Contract? contract = await _context.Contracts
            .Where(contract => contract.Id == id)
            .Include(contract => contract.Payments)
            .FirstOrDefaultAsync();

        if (contract is null) throw _notFoundException;

        return contract;
    }

    public async Task CheckExistenceAsync(Guid id)
    {
        bool doesExist = await _context.Contracts.AnyAsync(contract => contract.Id == id);

        if (!doesExist) throw _notFoundException;
    }

    public static void CheckDatesRange(DateTime startDate, DateTime endDate, DateTime date, DateTime? cancelDate = null)
    {
        DateTime finalDate = cancelDate ?? endDate;

        if (date < startDate || date > finalDate)
            throw new BadRequestException("DateNotInContractDatesRange");
    }

    private static bool IsActive(ContractDatesDto contract)
    {
        DateTime now = DateTime.Now.GetUtcTime();
        DateTime end = contract.CancelDate is not null ?
            (DateTime)contract.CancelDate :
            contract.EndDate;

        if (now > end) return false;

        return true;
    }

    private static bool IsOwing(ContractFinanceDto contract)
    {
        if (!contract.ReferenceDates.Any()) return true;

        DateTime now = DateTime.Now.GetUtcTime();
        DateTime dueDate;

        List<DateTime> referenceDates =
            contract.ReferenceDates
            .OrderBy(referenceDate => referenceDate)
            .ToList();

        try
        {
            dueDate = new(contract.StartDate.Year, contract.StartDate.Month, contract.DueDateDay);
        }
        catch (ArgumentOutOfRangeException)
        {
            dueDate = new(contract.StartDate.Year, contract.StartDate.Month, 1);
            dueDate = dueDate.AddMonths(1);
        }

        dueDate = dueDate.AddMonths(1);
        DateTime referenceDate = new(contract.StartDate.Year, contract.StartDate.Month, 1);
        DateTime endReferenceDate = new(contract.EndDate.Year, contract.EndDate.Month, 1);
        endReferenceDate = endReferenceDate.AddDays(-1);
        endReferenceDate = endReferenceDate.AddMonths(1);

        while (now > dueDate)
        {
            if (referenceDate > endReferenceDate) break;

            if (contract.CancelDate is not null)
            {
                DateTime cancelDate = (DateTime)contract.CancelDate;
                DateTime cancelDateReference = new(cancelDate.Year, cancelDate.Month, 1);
                cancelDate = cancelDate.AddDays(-1);
                cancelDate = cancelDate.AddMonths(1);

                if (referenceDate > cancelDateReference) break;
            }

            bool shouldReturn = true;

            for (int i = 0; i < referenceDates.Count; i++)
            {
                if (referenceDates[i].Year == referenceDate.Year && referenceDates[i].Month == referenceDate.Month)
                {
                    dueDate = dueDate.AddMonths(1);
                    referenceDate = referenceDate.AddMonths(1);
                    referenceDates.RemoveAt(i);
                    shouldReturn = false;
                    break;
                }
            }

            if (shouldReturn) return true;
        }

        return false;
    }
}
