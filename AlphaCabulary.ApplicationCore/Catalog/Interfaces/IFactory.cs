namespace AlphaCabulary.ApplicationCore.Catalog.Interfaces
{
    public interface IFactory<out T>
    {
        T Create();
    }
}