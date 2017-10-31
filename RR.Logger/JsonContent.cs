using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace RR.Logger
{
    public class JsonContent<T> : StringContent
    {
        public JsonContent(T obj) :
            base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }
    }
    }
