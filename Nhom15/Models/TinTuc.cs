namespace Nhom15.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinTuc")]
    public partial class TinTuc
    {
        [Key]
        [StringLength(50)]
        public string MaTinTuc { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        public string NoiDung { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(100)]
        public string Anh { get; set; }

        public string TomTat { get; set; }

        public DateTime? AddTime { get; set; }

        public virtual TaiKhoanAdmin TaiKhoanAdmin { get; set; }
    }
}
