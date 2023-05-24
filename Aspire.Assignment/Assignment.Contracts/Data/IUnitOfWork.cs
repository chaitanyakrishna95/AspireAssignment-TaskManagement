using Assignment.Contracts.Data.Repositories;

namespace Assignment.Contracts.Data
{
    public interface IUnitOfWork
    {
        IAppRepository App { get; }
        IUserRepository User { get; }

        ITaskDetailRepository TaskDetail { get; }
        Task CommitAsync();
    }
}