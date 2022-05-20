using CursoProgressao.Server.Data;
using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Models;
using CursoProgressao.Server.Services.Contracts;
using CursoProgressao.Shared.Dto.Payments;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Server.Services.Payments;

public class PaymentsService : IPaymentsService
{
    private readonly SchoolContext _context;
    private readonly IContractsService _contractsService;
    private readonly NotFoundException _notFoundException = new("PaymentNotFound");

    public PaymentsService(SchoolContext context, IContractsService contractsService)
    {
        _context = context;
        _contractsService = contractsService;
    }

    public async Task<Guid> CreateAsync(Guid contractId, CreatePaymentDto dto)
    {
        await _contractsService.CheckExistenceAsync(contractId);

        await _contractsService.GetAndCheckDatesRange(contractId, dto.ReferenceDate);
        await AssessReferenceDateUniquenessAsync(contractId, dto.ReferenceDate);

        Payment payment = new(contractId, dto.Value, dto.PaymentDate, dto.ReferenceDate);

        while (await DoesExistAsync(payment.Id))
            payment = new(contractId, dto.Value, dto.PaymentDate, dto.ReferenceDate);

        _context.Payments.Add(payment);

        return payment.Id;
    }

    public async Task UpdateAsync(Guid contractId, Guid id, UpdatePaymentDto dto)
    {
        await _contractsService.CheckExistenceAsync(contractId);

        Payment payment = await GetModelAsync(id);

        if (dto.ReferenceDate is not null)
        {
            DateTime referenceDate = (DateTime)dto.ReferenceDate;

            await _contractsService.GetAndCheckDatesRange(contractId, referenceDate);
            await AssessReferenceDateUniquenessAsync(contractId, referenceDate);

            payment.ReferenceDate = referenceDate;
        }

        if (dto.Value is not null) payment.Value = (float)dto.Value;
        if (dto.PaymentDate is not null) payment.PaymentDate = (DateTime)dto.PaymentDate;
    }

    public async Task DeleteAsync(Guid id)
    {
        Payment payment = await GetModelAsync(id);

        _context.Payments.Remove(payment);
    }

    public async Task<IEnumerable<GetAllPaymentsDto>> GetAllAsync(Guid contractId)
    {
        await _contractsService.CheckExistenceAsync(contractId);

        return await _context.Payments
            .AsNoTracking()
            .Where(payment => payment.ContractId == contractId)
            .Select(payment => new GetAllPaymentsDto()
            {
                Id = payment.Id,
                Value = payment.Value,
                PaymentDate = payment.PaymentDate,
                ContractReferenceDate = payment.ReferenceDate
            })
            .ToListAsync();
    }

    private async Task<Payment> GetModelAsync(Guid id)
    {
        Payment? payment = await _context.Payments
            .FirstOrDefaultAsync(payment => payment.Id == id);

        if (payment is null) throw _notFoundException;

        return payment;
    }

    private async Task AssessReferenceDateUniquenessAsync(Guid contractId, DateTime referenceDate)
    {
        bool doesExist = await _context.Payments
            .Where(payment => payment.ContractId == contractId)
            .AnyAsync(payment =>
                payment.ReferenceDate.Year == referenceDate.Year &&
                payment.ReferenceDate.Month == referenceDate.Month);

        if (doesExist) throw new ConflictException("ReferenceDateAlreadyExists");
    }

    private async Task<bool> DoesExistAsync(Guid id)
        => await _context.Payments.AnyAsync(payment => payment.Id == id);
}
