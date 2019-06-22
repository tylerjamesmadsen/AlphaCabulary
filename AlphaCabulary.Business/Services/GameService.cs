using System;
using AlphaCabulary.ApplicationCore.Catalog.Interfaces;

namespace AlphaCabulary.Business.Services
{
    public class GameService : IGameService
    {
        private readonly ILetterPairGenerator _letterPairGenerator;
        private readonly ITimerService _timerService;

        public bool IsRunning { get; set; }

        public GameService(ILetterPairGenerator letterPairGenerator, ITimerService timerService)
        {
            _letterPairGenerator = letterPairGenerator ?? throw new ArgumentNullException(nameof(letterPairGenerator));
            _timerService = timerService ?? throw new ArgumentNullException(nameof(timerService));
        }

        public void Start()
        {


            _timerService.Start(TODO);
        }

        public void Stop(bool isCancelled)
        {
            _timerService.Stop();
        }
    }
}