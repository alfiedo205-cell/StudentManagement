using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models;

public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ tên")]
    [Display(Name = "Họ tên")]
    public string Name { get; set; } = string.Empty;

    [Range(16, 60, ErrorMessage = "Tuổi phải từ 16 đến 60")]
    [Display(Name = "Tuổi")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập ngành học")]
    [Display(Name = "Ngành học")]
    public string Major { get; set; } = string.Empty;
}
