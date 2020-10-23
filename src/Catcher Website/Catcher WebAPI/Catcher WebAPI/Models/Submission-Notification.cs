using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Catcher_WebAPI.Data.CanvasData;
using Catcher_WebAPI.Models.Tables;
using Microsoft.AspNetCore.Html;
using Microsoft.EntityFrameworkCore;

namespace Catcher_WebAPI.Models.Xml
{
    [XmlRoot(ElementName = "submission-notification", Namespace = "")]
    public class SubmissionNotification
    {
        [XmlElement(ElementName = "form")]
        public SubmissionNotification.NotificationForm Form { get; set; }
        [XmlElement(ElementName = "submission")]
        public SubmissionNotification.NotificationSubmission Submission { get; set; }

        public override string ToString()
        {
            return $"<?xml version=\"1.0\" encoding=\"UTF-8\"?><{nameof(SubmissionNotification)}><submission-notification><form>" +
                   $"<id type=\"integer\">{Form.Id}</id><name>{Form.Name}</name><guid>{Form.FormGuid}</guid><tag>{Form.Tag}</tag></form>" +
                   $"<submission><guid>{Submission.SubmissionGuid}</guid></submission></submission-notification>";

            return base.ToString();
        }

        public class NotificationForm
        {
            [XmlElement(ElementName = "id")]
            public int Id { get; set; }
            [XmlElement(ElementName = "name")]
            public string Name { get; set; }
            [XmlElement(ElementName = "guid")]
            public string FormGuid { get; set; }

            [XmlElement(ElementName = "Tag")] 
            public string Tag { get; set; } = "";
        }

        public class NotificationSubmission
        {
            [XmlElement(ElementName = "guid")]
            public string SubmissionGuid { get; set; }
        }

        public void Save(GoCanvasDbContext dbContext)
        {
            try
            {
                try
                {
                    dbContext.Database.ExecuteSqlRaw($"INSERT INTO [tblMySubmissions]([FormName],[GoCanSubmissionGUID])VALUES ('{this.Form.Name}', '{this.Submission.SubmissionGuid.ToString()}');");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            catch (Exception e)
            {
                dbContext.MySubmissions.Load();
                dbContext.MySubmissions.Add(new MySubmissions() { FormName = this.Form.Name, GoCanSubmissionGUID = this.Submission.SubmissionGuid.ToString() });
                dbContext.SaveChanges();
            }
        }
    }

}
