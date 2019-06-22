using AlphaCabulary.ApplicationCore.Catalog.Interfaces;
using AlphaCabulary.ApplicationCore.Catalog.Utilities;
using AlphaCabulary.Business.Services;

namespace AlphaCabulary.Business.Factories
{
    public class TimerFactory : IFactory<ITimer>
    {
        public ITimer Create()
        {
            return new Timer();
        }
    }
}