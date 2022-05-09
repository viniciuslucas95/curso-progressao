using Api.Data.Contexts;

namespace Api.Data.UnitOfWork
{
    public class SchoolUnitOfWork : IUnitOfWork
    {
        private readonly SchoolContext _context;

        public SchoolUnitOfWork(SchoolContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task RollbackAsync()
        {
            throw new NotImplementedException();
        }
    }
}
