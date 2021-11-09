namespace Nhom15.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPhamKhuyenMai")]
    public partial class SanPhamKhuyenMai
    {
        [Required]
        [StringLength(10)]
        public string MaSP { get; set; }

        [Required]
        [StringLength(10)]
        public string MaChuongTrinh { get; set; }

        public int? PhanTramKhuyenMai { get; set; }

        [Key]
        public int MaSPKM { get; set; }

        public virtual ChuongTrinhKhuyenMai ChuongTrinhKhuyenMai { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
