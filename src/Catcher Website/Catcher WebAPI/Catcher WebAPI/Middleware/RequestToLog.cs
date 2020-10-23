using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Catcher_WebAPI.Data.CanvasLogging;
using Catcher_WebAPI.Models.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Catcher_WebAPI.Middleware
{
    public partial class RequestResponseLogger
    {
        public class RequestToLog
        {
            public  RequestToLog(HttpContext context)
            {
                BodyStream = new MemoryStream();
                Request = context.Request;
                InitLogger().Wait();
                StreamReader = new StreamReader(BodyStream);
            }
            
            public async Task InitLogger()
            {
                if (Request.ContentType.Contains("xml"))
                {
                    HasXMLBody = true;
                    await Request.Body.CopyToAsync(BodyStream);
                    BodyStream.Seek(0, SeekOrigin.Begin);
                    Request.Body = BodyStream;
                }
            }

            public bool HasXMLBody { get; set; }
            public HttpRequest Request { get; set; }
            public StreamReader StreamReader { get; set; }
            public Stream BodyStream { get; set; }
            
            public int ResponseType { get; set; } = 0;
            public string RequestBody
            {
                get
                {
                    StreamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    string bodyString = StreamReader.ReadToEnd();
                    return bodyString;
                }
            }

            public override string ToString()
            {
                return RequestBody;
            }

            public void Save(GoCanvasLoggingDbContext dbContext)
            {
                if (HasXMLBody)
                {
                    var record = new ResponseLog() { ResponseBody = $"{this.ToString()}" };
                    try
                    {
                        MemoryStream ms = new MemoryStream();
                        this.Request.Body.Seek(0, SeekOrigin.Begin);
                        this.Request.Body.CopyTo(ms);
                        ms.Seek(0, SeekOrigin.Begin);

                        var xmlSerializer = new XmlSerializer(typeof(SubmissionNotification));
                        SubmissionNotification submission_notification = (SubmissionNotification)xmlSerializer.Deserialize(ms);

                    }
                    catch (Exception e)
                    {
                        record.ResponseType = 1;
                    }
                    
                    try
                    {
                        dbContext.ResponseLogs.Add(record);
                        dbContext.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        using (dbContext.Database.BeginTransaction(IsolationLevel.Unspecified))
                        {
                            dbContext.Database.ExecuteSqlRaw($"INSERT INTO [tblResponseLog]([ResponseBody])VALUES('ResponseBody Logger FAILED::{e}|{this.ToString()}');");
                        }

                        try
                        {
                            using (dbContext.Database.BeginTransaction(IsolationLevel.Unspecified))
                            {
                                dbContext.Database.ExecuteSqlRaw($"INSERT INTO [tblResponseLog]([ResponseBody])VALUES('{this.ToString()}');");
                            }
                        }
                        catch (Exception exception)
                        {
                            using (dbContext.Database.BeginTransaction(IsolationLevel.Unspecified))
                            {
                                dbContext.Database.ExecuteSqlRaw($"INSERT INTO [tblResponseLog]([ResponseBody])VALUES('ResponseBody Logger FAILED::{e}|{this.ToString()}');");
                            }
                        }
                    }
                }

            }
        }
    }
}
