using System.IO;
using System.Text.Json;
using Flurl.Http.Configuration;

namespace MtgApiManager.Lib.Core
{
    /// <inheritdoc />
    public class SystemTextJsonSerializer : ISerializer
    {
        /// <inheritdoc />
        public T Deserialize<T>(string s)
        {
            return JsonSerializer.Deserialize<T>(s);
        }

        /// <inheritdoc />
        public T Deserialize<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return JsonSerializer.Deserialize<T>(reader.ReadToEnd());
            }
        }

        /// <inheritdoc />
        public string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}