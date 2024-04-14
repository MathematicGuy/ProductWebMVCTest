using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductWebMVC.Models
{
    public class HangHoa
    {
        public int Id { get; set; }
        public int ma_hang_hoa { get; set; }
        public string ten_hang_hoa { get; set; }
        public int so_luong { get; set; }
        public string ghi_chu { get; set; }

    }
}

