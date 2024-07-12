﻿using System.ComponentModel.DataAnnotations;

namespace CCISBookIT.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
