using System;
using System.Net.Http;

namespace LuKaSo.Mintos.Exceptions
{
    public class ServerErrorException : Exception
    {
        public ServerErrorException(HttpResponseMessage message) : base($"Unexpected server responce retrieved. \r\n {message.ToString()}")
        { }
    }
}
