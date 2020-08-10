using System;
using Newtonsoft.Json;

namespace GenerateMock.Utilities.Exceptions
{
    public class BaseNotFoundException : Exception
    {
        public BaseNotFoundException(int code, string message)
            : base(message)
        {
            Code = code;
            ExMessage = message;
        }

        public int Code { get; set; }
        public string ExMessage { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(new { Code, Message = ExMessage });
        }
    }
}
