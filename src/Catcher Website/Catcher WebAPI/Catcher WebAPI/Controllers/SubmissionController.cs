using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Catcher_WebAPI.Data.CanvasData;
using Catcher_WebAPI.Models.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace Catcher_WebAPI.Controllers
{
    [Route("api/submission")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private GoCanvasDbContext dbContext;
        public SubmissionController(GoCanvasDbContext context)
        {
            dbContext = context;
        }
        public override BadRequestObjectResult BadRequest(object error)
        {
            Log($"Error: {error}");
            return base.BadRequest(error);
        }

        public override BadRequestResult BadRequest()
        {
            Log($"Error");
            return base.BadRequest();
        }

        public override BadRequestObjectResult BadRequest(ModelStateDictionary modelState)
        {
            Log($"Error: ModelState is valid: {modelState.IsValid}");
            return base.BadRequest(modelState);
        }

        public override NotFoundResult NotFound()
        {
            Log($"Not Found");
            
            return base.NotFound();
        }

        [HttpPost(template: "new", Name = "New Submission", Order = 0)]
        [Consumes("application/xml")]
        
        public async Task<IActionResult> New()
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                this.Request.Body.Seek(0, SeekOrigin.Begin);
                await this.Request.Body.CopyToAsync(ms);
                //await this.ControllerContext.HttpContext.Request.BodyReader.CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);

                var xmlSerializer = new XmlSerializer(typeof(SubmissionNotification));
                SubmissionNotification submission_notification = (SubmissionNotification)xmlSerializer.Deserialize(ms);
                Log($"Submission XML: {submission_notification.ToString()}");
                
                submission_notification.Save(dbContext);
                return Ok();
            }
            catch (Exception e)
            {
                return Ok();
            }
        }

        private void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            System.Console.WriteLine(message);
        }
    }
}
