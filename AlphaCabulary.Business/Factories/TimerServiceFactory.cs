using AlphaCabulary.ApplicationCore.Catalog.Interfaces;
using AlphaCabulary.ApplicationCore.Catalog.Utilities;
using AlphaCabulary.Business.Services;

namespace AlphaCabulary.Business.Factories
{
    public class TimerServiceFactory : IFactory<ITimerService>
    {
        public ITimerService Create()
        {
            return new TimerService(new Timer());
        }
    }
}