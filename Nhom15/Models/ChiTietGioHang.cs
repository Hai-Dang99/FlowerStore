namespace Nhom15.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietGioHang")]
    public partial class ChiTietGioHang
    {
        [Required]
        [StringLength(10)]
        public string MaSP { get; set; }

        [Key]
        [StringLength(50)]
        public string GioHangID { get; set; }

        public int? SoLuong { get; set; }

        public int? ThanhTien { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        public virtual SanPham SanPham { get; set; }

        public virtual TaiKhoanKhachHang TaiKhoanKhachHang { get; set; }
    }
}
