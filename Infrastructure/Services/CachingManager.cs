using Microsoft.Extensions.Caching.Memory;

/// <summary>
/// Implementacion de un mecanismo de cache para datos.  Permite economizar el numero de consultas que se hacen contra repositorios de datos
/// bien sea relacionales como no relacionales, economizando utilizacion de recursos especialmente en nube.
/// Este mecanismo permite mejorar la concurencia de la aplicacion y la velocidad respuesta al usuario se incrementa significativamente
/// a costa de consumir un recurso que es memoria ram que es mas barata.
///
/// Implementaciones adicionales en la infraestructura podria caber caches en REDIS, Azure table storages etc.
/// Por efectos de la prueba tecnica y como prototipo se uso uno muy sencillo como lo es cache en memoria.
/// </summary>
public class CachingManager : ICachingManager
{
    /// <summary>
    ///
    /// </summary>
    private readonly IMemoryCache _repository;

    /// <summary>
    ///
    /// </summary>
    public CachingManager()
    {
        //Se puede configurar la manera como el cache opera a partir de las opciones
        MemoryCacheOptions options = new MemoryCacheOptions();

        _repository = new MemoryCache(options);
    }

    /// <summary>
    /// Cantidad de cache para opciones estaticas o
    /// que jamas cambian EN SEGUNDOS (1 hora)
    /// </summary>
    public const int LongCachingTimeInSeconds = 60 * 60;

    /// <summary>
    /// Cache para opciones que deben estar cacheadas
    /// y evitar exesivo consumo de consultas y recursos en
    /// desarrollo esta deshabilitado y en produccion esta en 2
    /// MINUTOS
    /// </summary>
    public const int StandardCachingTimeInSeconds = 2 * 60;

    /// <summary>
    /// Cache para la lista de productos
    /// </summary>
    public const string ProductList = "Product.List";

    /// <summary>
    /// Indica si el caching esta habilitado por defecto habilitado, con logica desde program podria establecerse
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    ///
    /// </summary>
    /// <param name="key"></param>
    public bool Clear(string key)
    {
        bool result = false;
        if (Exist(key))
        {
            _repository.Remove(key);
            result = true;
        }

        return result;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    internal bool Exist(string key)
    {
        return Enabled && _repository.TryGetValue(key, out _);
    }

    /// <summary>
    /// Obtiene datos desde el cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public T? GetData<T>(string key)
    {
        if (Enabled)
        {
            _repository.TryGetValue(key, out T? value);
            return value;
        }
        else return default;
    }

    /// <summary>
    /// Guarda en cache elementos de larga duracion
    /// </summary>
    /// <param name="key">Llave del cache</param>
    /// <param name="data">Datos a guardar en memoria</param>
    internal void SetLongTime(string key, object data)
    {
        if (Enabled) SetData(key, data, LongCachingTimeInSeconds);
    }

    /// <summary>
    /// Adiciona un elemento al cache de corta duracion
    /// </summary>
    /// <param name="key">Llave del cache</param>
    /// <param name="data">Datos a guardar en memoria</param>
    public void SetStandard(string key, object data)
    {
        if (Enabled) SetData(key, data, StandardCachingTimeInSeconds);
    }

    /// <summary>
    /// Carga un parametro en el cache
    /// </summary>
    /// <param name="key">Llave del cache</param>
    /// <param name="data">Datos a guardar en el cache</param>
    /// <param name="duration">Duracion en segundos</param>
    private void SetData(string key, object data, int duration)
    {
        if (data != null)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(duration)
            };

            _repository.Set(key, data, cacheEntryOptions);
        }
    }
}