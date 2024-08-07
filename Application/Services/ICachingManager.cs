/// <summary>
/// Modulo de cache para aplicaciones
/// </summary>
public interface ICachingManager
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="key"></param>
    /// <param name="data"></param>
    void SetStandard(string key, object data);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    T? GetData<T>(string key);

    /// <summary>
    ///
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool Clear(string key);
}