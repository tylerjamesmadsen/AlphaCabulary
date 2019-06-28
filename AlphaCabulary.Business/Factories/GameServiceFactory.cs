using System;
using AlphaCabulary.ApplicationCore.Catalog.Interfaces;
using AlphaCabulary.Business.Game;
using AlphaCabulary.Business.Services;
using AlphaCabulary.Business.WordLookup;

namespace AlphaCabulary.Business.Factories
{
    public class GameServiceFactory : IFactory<IGameService>
    {
        private readonly IFactory<ILetterPairGenerator> _letterPairGeneratorFactory;
        private readonly IFactory<ITimer> _timerFactory;

        public GameServiceFactory(IFactory<ILetterPairGenerator> letterPairGeneratorFactory, IFactory<ITimer> timerServiceFactory)
        {
            _letterPairGeneratorFactory = letterPairGeneratorFactory ?? throw new ArgumentNullException(nameof(letterPairGeneratorFactory));
            _timerFactory = timerServiceFactory ?? throw new ArgumentNullException(nameof(timerServiceFactory));
        }

        public IGameService Create()
        {
            ILetterPairGenerator letterPairGenerator = _letterPairGeneratorFactory.Create();
            ITimer timerService = _timerFactory.Create();


            return new GameService(letterPairGenerator, timerService, /*TODO*/ new ScoreCalculator(new DatamuseWordLookup()));
        }
    }
}