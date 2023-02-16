using SendGrid;
using SendGrid.Helpers.Mail;

namespace MoviesNow.LK.Services
{
    public static class SendGridAPI
    {
        //public static async Task<bool> Execute(string userEmail, string userName, string subject, string plainTextContent, string htmlContent)
        //{
        //    //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
        //    //var apiKey = "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";
        //    //var client = new SendGridClient(apiKey);
        //    //var from = new EmailAddress("jpkasunbuddika@gmail.com", "MoviesNow.LK");
        //    //var to = new EmailAddress(userEmail, userName);
        //    //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    //var response = await client.SendEmailAsync(msg);
        //    //return await Task.FromResult(true);
        //}
    }
}
