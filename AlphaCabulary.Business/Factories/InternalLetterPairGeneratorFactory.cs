using AlphaCabulary.ApplicationCore.Catalog.Interfaces;
using AlphaCabulary.Business.Game;

namespace AlphaCabulary.Business.Factories
{
    public class InternalLetterPairGeneratorFactory : IFactory<ILetterPairGenerator>
    {
        public ILetterPairGenerator Create()
        {
            return new InternalLetterPairGenerator();
        }
    }
}