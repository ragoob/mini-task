using Data.Context;
using Domain.Interfaces;

namespace Data.UnityOfWork
{
    public class UntityOfWork : IUnitOfWork
    {
        private readonly MiniTaskContext _miniTaskContext;
        
        public UntityOfWork(MiniTaskContext miniTaskContext)
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
