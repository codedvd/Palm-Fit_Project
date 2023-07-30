using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.AuthRepository
{
    public interface IAuthRepo
    {
        string SendOTPByEmail(string email, string otp);
        void SaveOTPInUserData(string email, string otp);
        string GenerateOTP();

    }
}
