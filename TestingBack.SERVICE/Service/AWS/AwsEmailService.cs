
using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace TestingBack.SERVICE.Service.AWS
{
    public class AwsEmailService
    {
        private readonly IConfiguration _configuration;

        public AwsEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string _EmailFrom => _configuration["AwsS3EMail:EmailFrom"];
        private string _SmtpUser => _configuration["AwsS3EMail:SmtpUser"];
        private string _SmtpPass => _configuration["AwsS3EMail:SmtpPass"];

        private static BodyBuilder GetMessageBody(string html)
        {
            var body = new BodyBuilder()
            {
                HtmlBody = html,
            };
            
            return body;
        }

        public MimeMessage GetMessage(string[] to, string subject, string html, string from = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from, _EmailFrom));

            foreach(var emailDestinatario in to)
            {
                message.To.Add(new MailboxAddress(string.Empty, emailDestinatario));
            }

            message.Subject = subject;
            message.Body = GetMessageBody(html).ToMessageBody();
            return message;
        }

        public MemoryStream GetMessageStream(string[] to, string subject, string html, string from = null)
        {
            var stream = new MemoryStream();
            GetMessage(to, subject, html).WriteTo(stream);
            return stream;
        }
        
        public async Task<bool> EnviarCorreo(string[] to, string subject, string html, string from = null)
        {
            var credentals = new BasicAWSCredentials(_SmtpUser, _SmtpPass);
            using (var client = new AmazonSimpleEmailServiceClient(credentals, RegionEndpoint.USEast1))
            {
                var sendRequest = new SendRawEmailRequest { RawMessage = new RawMessage(GetMessageStream(to, subject, html)) };
                try
                {
                    Console.WriteLine("Enviando correo con Amazon SES...");
                    var response = client.SendRawEmailAsync(sendRequest);
                    Console.WriteLine("El correo se ha enviado correctamente.");
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("El correo no se ha enviado.");
                    Console.WriteLine("Error message: " + e.Message);
                    return false;
                }
            }
        }
    }
}
