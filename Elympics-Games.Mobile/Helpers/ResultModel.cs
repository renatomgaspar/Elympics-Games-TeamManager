using System.Net;

namespace Elympics_Games.Mobile.Helpers
{
    public class ResultModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
