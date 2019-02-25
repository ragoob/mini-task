using Data.Context;
using Domain.Interfaces;

namespace Data.UnityOfWork
{
    public class UntitOfWork : IUnitOfWork
    {
        private readonly MiniTaskContext _miniTaskContext;
        
        public UntitOfWork(MiniTaskContext miniTaskContext)
        {
            _miniTaskContext = miniTaskContext;
        }

        public bool Commit()
        {
            try
            {
                return _miniTaskContext.SaveChanges() > 0;
            }
            catch (System.Exception ex)
            {

                return false;
            }
        }

        public void Dispose()
        {
            _miniTaskContext.Dispose();
        }
    }
}
