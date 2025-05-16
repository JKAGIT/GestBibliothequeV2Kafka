using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEvenements.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize<T>(T data)
        {
            return System.Text.Json.JsonSerializer.Serialize(data);
        }

        public T Deserialize<T>(string data)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(data);
        }
    }
}


