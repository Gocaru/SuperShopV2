﻿using System.ComponentModel.DataAnnotations;

namespace SuperShopV2.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name ="Current password")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "Current password")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        public string Confirm { get; set; }
    }
}
