namespace CursoProgressao.Api.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}
