using System;
using AlphaCabulary.ApplicationCore.Catalog.Interfaces;
using AlphaCabulary.Business.Services;

namespace AlphaCabulary.Business.Factories
{
    public class GameServiceFactory : IFactory<IGameService>
    {
        private readonly IFactory<ILetterPairGenerator> _letterPairGeneratorFactory;
        private readonly IFactory<ITimerService> _timerServiceFactory;

        public GameServiceFactory(IFactory<ILetterPairGenerator> letterPairGeneratorFactory, IFactory<ITimerService> timerServiceFactory)
        {
            _letterPairGeneratorFactory = letterPairGeneratorFactory ?? throw new ArgumentNullException(nameof(letterPairGeneratorFactory));
            _timerServiceFactory = timerServiceFactory ?? throw new ArgumentNullException(nameof(timerServiceFactory));
        }

        public IGameService Create()
        {

            ILetterPairGenerator letterPairGenerator = _letterPairGeneratorFactory.Create();
            ITimerService timerService = _timerServiceFactory.Create();

            return new GameService(letterPairGenerator, timerService);
        }
    }
}