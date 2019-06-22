namespace AlphaCabulary.ApplicationCore.Catalog.Interfaces
{
    public interface ILetterPairGenerator
    {
        string GetLetterPair();
        string GetLetterPair(int index);
    }
}
