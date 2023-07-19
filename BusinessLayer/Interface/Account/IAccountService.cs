
using EntityLayer.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface.Account
{
    public interface IAccountService
    {
        UserLoginResponse Login(UserLogin inputModel);
    }
}
