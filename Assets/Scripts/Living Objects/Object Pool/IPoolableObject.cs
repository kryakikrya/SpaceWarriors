public interface IPoolableObject<T> where T : IObjectSettings
{
    void InitializeInfo(T settings);
}
