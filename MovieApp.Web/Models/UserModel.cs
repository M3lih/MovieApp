﻿using Microsoft.AspNetCore.Mvc;
using MovieApp.Web.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Web.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "UserName için 10 karakterden fazla olamaz")]
        [Remote(action:"VerifyUserName", controller:"User")]
        public string UserName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "{0} karakter uzunlugu {2}--{1} arasında olmalıdır. ", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [EmailProviders]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }
        
        [Url]
        public string Url { get; set; }

        //[Range(1900,2010)]
        //public int BirthYear { get; set; }

        [BirthDate(ErrorMessage = "Doğum tarihiniz şimdi ya da ileri tarih olamaz.")]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

    }
}
