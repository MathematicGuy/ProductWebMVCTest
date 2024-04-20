using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProductWebMVC.Models
{
    public class HangHoa
    {
        //[Required(ErrorMessage = "Mã hàng hóa is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã hàng hóa is required")]
        public int ma_hang_hoa { get; set; }

        [Required(ErrorMessage = "Tên hàng hóa is required")]
        public string ten_hang_hoa { get; set; }
        
        [Required(ErrorMessage = "Số lượng is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng must be 0 or greater")]
        public int so_luong { get; set; }

        //[Required(ErrorMessage = "Tên hàng hóa is required")]
        public string? ghi_chu { get; set; }

    }
}

