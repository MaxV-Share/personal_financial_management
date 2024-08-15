using System.ComponentModel.DataAnnotations;

namespace PersonalFinancialManagement.Models.Dtos.Users
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bạn cần nhập tài khoản")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Bạn cần nhập tên.")]
        public required string FullName { set; get; }

        //[Required(ErrorMessage = "Bạn cần nhập tên đăng nhập.")]
        public required string? UserName { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public required string Password { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public required string ConfirmPassword { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập email.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng.")]
        public required string Email { set; get; }

        public string? Address { set; get; }

        //[Required(ErrorMessage = "Bạn cần nhập số điện thoại.")]
        public required string? PhoneNumber { set; get; }

    }
    public class UserViewModel
    {
        public string? Id { set; get; }
        public string? UserName { set; get; }
        public string? FullName { set; get; }
        public string? Email { set; get; }
        public string? Address { set; get; }
        public string? PhoneNumber { set; get; }
    }
    public class UserPasswordChangeRequest
    {
        public string? Id { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu cũ")]
        [DataType(DataType.Password)]
        public required string CurrentPassword { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu mới")]
        [DataType(DataType.Password)]
        public required string NewPassword { set; get; }
    }
}
