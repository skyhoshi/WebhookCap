using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Catcher_WebAPI.Data.CanvasData;
using Catcher_WebAPI.Models.Areas.APITester;
using Catcher_WebAPI.Models.Tables;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RestSharp;

namespace Catcher_WebAPI.Areas.APITester
{
    [Area("APITester")]
    public class APITesterController : Controller
    {
        private readonly GoCanvasDbContext _context;
        //[Microsoft.AspNetCore.Mvc.ModelBinder(typeof(TestAPIViewModel), Name = "a")]

        public APITesterController(GoCanvasDbContext context)
        {
            _context = context;
        }

        // GET: APITester/APITester
        public async Task<IActionResult> Index()
        {
            return View("Index");
        }

        [HttpPost("test")]
        public async Task<IActionResult> Test()
        {
            TestAPIViewModel vm = new TestAPIViewModel();
            string testHost = Request.Host.ToString();
            string uri = $"{Request.Scheme}://{testHost}/api/submission/new";
            RestClient? client = new RestClient(uri);
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/xml");
            var preTests = Request.Form["preTests"];
            if (preTests.Contains("0"))
            {
                //request.AddParameter("application/xml", preTests.First(), ParameterType.RequestBody);
            }
            else if (preTests.Contains("1"))
            {
                //request.AddParameter("application/xml", preTests.First(), ParameterType.RequestBody);
            }
            else
            {
                Guid submissionGuid = Guid.NewGuid();
                string formName = "";
                formName = Request.Form["submissionId"];
                string tags = "";
                //tags = this.Request.RouteValues[""];
                request.AddParameter("application/xml", $"<?xml version=\"1.0\" encoding=\"UTF-8\"?><submission-notification><form><id type=\"integer\">1</id><name>{formName}</name><guid>{Guid.NewGuid()}</guid><tag>{tags}</tag></form><submission><guid>{Guid.NewGuid()}</guid></submission></submission-notification>", ParameterType.RequestBody);
            }

            IRestResponse response = client.Execute(request);
            vm.restRequest = request;
            vm.restResponse = response;
            Console.WriteLine(response.Content);
            return View("Index", vm);
        }
    }
}
