﻿using Data.Entities;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Services
{
    public interface IAuthRepository
    {
        string GenerateJwtToken(AppUser user);
        Task<UserOTP?> FindMatchingValidOTP(string otpFromUser);
        string SendOTPByEmail(string email, string otp);
    }
}
