using Api.Data.UnitOfWork;
using Api.Dto.Common;
using Api.Dto.Students;
using Api.Services.Students;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService _service;
        private readonly IUnitOfWork _unitOfWork;

        public StudentsController(IStudentsService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutStudent(Guid id, Student student)
        //{
        //    if (id != student.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(student).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StudentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [HttpPost]
        public async Task<ActionResult<CreationReturnDto>> PostStudentAsync(CreateStudentDto dto)
        {
            Guid id = await _service.CreateAsync(dto);
            await _unitOfWork.CommitAsync();

            return new CreationReturnDto { Id = id };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllStudentsDto>>> GetAllStudentsAsync()
        {
            IEnumerable<GetAllStudentsDto> results = await _service.GetAllAsync();

            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetOneStudentDto>> GetOneStudentAsync(Guid id)
        {
            GetOneStudentDto result = await _service.GetOneAsync(id);

            return Ok(result);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteStudent(Guid id)
        //{
        //    var student = await _context.Students.FindAsync(id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Students.Remove(student);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool StudentExists(Guid id)
        //{
        //    return _context.Students.Any(e => e.Id == id);
        //}
    }
}
