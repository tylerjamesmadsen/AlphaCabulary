using AlphaCabulary.ApplicationCore.Interfaces;
using System;

namespace AlphaCabulary.Business.Game
{
    public class InternalLetterPairGenerator : ILetterPairGenerator
    {
        private static readonly string[] _pairs =
        {
            "AL", "AR", "BA", "BL", "BO", "BR", "BU", "CA", "CH", "CL", "CO", "DE", "DI", "DO", "EN", "EX", "FA",
            "FI", "FL", "FO", "FR", "GA", "IN", "LE", "LO", "MA", "NE", "PA", "PE", "PI", "PL", "PR", "PU", "QU",
            "RA", "RE", "RO", "SE", "SI", "SK", "SO", "ST", "SU", "TA", "TR", "UN", "VI", "VO", "WH", "WI"
        };

        public string GetLetterPair()
        {
            var randomIndex = new Random().Next(0, _pairs.Length - 1);

            return _pairs[randomIndex];
        }

        public string GetLetterPair(int index)
        {
            if (index < 0 || index > _pairs.Length - 1) { throw new ArgumentOutOfRangeException(nameof(index)); }

            return _pairs[index];
        }
    }
}
