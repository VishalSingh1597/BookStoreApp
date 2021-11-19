using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class ResetPasswordModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
