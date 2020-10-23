using Newtonsoft.Json;
using RestSharp;

namespace Catcher_WebAPI.Models.Areas.APITester
{
    public class TestAPIViewModel
    {
        [JsonProperty(ReferenceLoopHandling = ReferenceLoopHandling.Ignore)]
        public IRestResponse restResponse { get; set; }
        [JsonProperty(ReferenceLoopHandling = ReferenceLoopHandling.Ignore)]
        public RestRequest restRequest { get; set; }
    }
}
