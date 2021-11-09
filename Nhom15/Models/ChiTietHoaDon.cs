namespace Nhom15.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDon")]
    public partial class ChiTietHoaDon
    {
        [Key]
        [StringLength(50)]
        public string MaCTHoaDon { get; set; }

        [Required]
        [StringLength(10)]
        public string MaSP { get; set; }

        public int? DonGia { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string MaHoaDon { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
