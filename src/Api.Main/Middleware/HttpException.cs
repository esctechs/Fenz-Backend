using Newtonsoft.Json;

namespace Api.Main.Middleware
{
    public class HttpException
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}