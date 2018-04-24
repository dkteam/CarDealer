using System;
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Web.Models
{
    public class FeedbackViewModel
    {
        public int ID { set; get; }

        [MaxLength(100, ErrorMessage ="Họ tên không vượt quá 100 ký tự")]
        [Required(ErrorMessage ="Cần phải nhập tên")]
        public string Name { set; get; }

        [MaxLength(20, ErrorMessage = "Số điện thoại không vượt quá 20 ký tự")]
        [Required(ErrorMessage = "Cần phải nhập số điện thoại")]
        public string Mobile { set; get; }

        [StringLength(50, ErrorMessage = "Email không vượt quá 20 ký tự")]
        [Required(ErrorMessage = "Cần phải nhập email")]
        public string Email { set; get; }

        [StringLength(500, ErrorMessage = "Nội dung không vượt quá 500 ký tự")]
        public string Message { set; get; }

        public DateTime CreatedDate { set; get; }

        [Required(ErrorMessage = "Cần phải nhập trạng thái")]
        public bool Status { set; get; }
    }
}