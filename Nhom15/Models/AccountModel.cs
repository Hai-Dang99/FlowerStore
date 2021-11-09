using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nhom15.Models
{
    public class AccountModel
    {
        [StringLength(50)]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string Username { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }

        [StringLength(20)]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không chính xác")]
        public string ConfirmPassword { get; set; }

        public bool? FullControl { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string HoTen { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(12)]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string SDT { get; set; }
    }
}