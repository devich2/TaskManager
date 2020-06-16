using System.Collections.Generic;

namespace TaskManager.Models.Email
{
    public class EmailStringAttachment: EmailMessage
    {
        public List<string> AttachmentsFilePath { get; set; }
    }
}