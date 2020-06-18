namespace TaskManager.Configuration
{
    public class SmtpSendConfiguration
    {
        public string DefaultEmailFrom {get; set;}
        public string DefaultNameFrom {get; set;}
        public string Password {get; set;}
        public bool EnableSsl {get; set;}
        public int Port {get; set;}
        public string Host {get; set;}
    }
}