﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class UserNewPassword
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
