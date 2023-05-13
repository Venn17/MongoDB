using Mongo.Models.BusinessModels;
using Mongo.Models.DataModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Mongo.Models.Services
{
    public class AccountService : AccountRepository
    {
        TestDBContext context;
        private static Random random = new Random();

        public AccountService(TestDBContext context)
        {
            this.context = context;
        }

        public Account checkLogin(string email, string password)
        {
            var acccounts = context.Accounts.Find(FilterDefinition<Account>.Empty).ToList();
            Account check = null;
            foreach (var item in acccounts)
            {
                if(item.email == email && item.password == password)
                {
                    check = item;
                    Logined _new = new Logined();
                    _new._id = item._id;
                    _new.name = item.name;
                    _new.email = item.email;
                    context.Logined.InsertOne(_new);
                }
            }
            return check;
        }

        public bool forgotPassword(string email)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string new_pass = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            try
            {
                var account = context.Accounts.Find(p => p.email == email).FirstOrDefault();
                    if (account == null)
                    {
                        return false;
                    }
                    else
                    {
                        var senderEmail = new MailAddress("vienavtb@gmail.com", "CongVien");
                        var receiverEmail = new MailAddress(email, "Receiver");
                        var password = "rwpezipvxnciwrij";
                        var subject = "Here's the link to reset your password";
                        var body = "<p>Hello,</p>" + "<p>You have requested to reset your password.</p>"
                        + "<p> below to change your password:</p>"
                        + "<h4>Your new password is : <b>" + new_pass + "</b></h4>"
                        + "<h3><p>Please don't share this email for everyone !</p></h3>"
                        + "<br><p>This link will expire within the next hour . "
                        + "<b>(If this is a spam message, please click  it is not spam)<b>";
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = subject,
                            Body = body,
                            IsBodyHtml = true
                        })
                        {
                            smtp.Send(mess);
                        }
                        account.password = new_pass;
                        var data = Builders<Account>.Update
                        .Set("name", account.name)
                        .Set("email", account.email)
                        .Set("password",new_pass)
                        .Set("role",account.role)
                        .Set("status",account.status);
                    context.Accounts.UpdateOne(x => x._id == account._id, data);
                    }
                    return true;
                }
            catch (Exception)
            {

            }
            return false;
        }

        public Logined getInforLogin()
        {
            return context.Logined.Find(FilterDefinition<Logined>.Empty).FirstOrDefault();
        }

        public Account register(Account data)
        {
            try
            {
                context.Accounts.InsertOne(data);
            }
            catch (Exception)
            {
                return null;
            }
            return data;
        }
    }
}
