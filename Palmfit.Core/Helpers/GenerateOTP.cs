using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class GenerateOTP
    {
        public static string GenerateAnOTP()
        {
            Random random = new Random();
            int otpValue = random.Next(100000, 999999);
            return otpValue.ToString();
        }
    }
}
