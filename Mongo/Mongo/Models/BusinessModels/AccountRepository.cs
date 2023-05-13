using Mongo.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Models.BusinessModels
{
    public interface AccountRepository
    {
        Account checkLogin(string email, string password);
        Account register(Account data);
        bool forgotPassword(string email);
        Logined getInforLogin();

    }
}
