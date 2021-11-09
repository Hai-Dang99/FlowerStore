using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Nhom15.Models
{
    public partial class Nhom15DbContext : DbContext
    {
        public Nhom15DbContext()
            : base("name=Nhom15DbContext")
        {
        }

        public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<ChuongTrinhKhuyenMai> ChuongTrinhKhuyenMais { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KHO> KHOes { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoanAdmin> TaiKhoanAdmins { get; set; }
        public virtual DbSet<TaiKhoanKhachHang> TaiKhoanKhachHangs { get; set; }
        public virtual DbSet<TinTuc> TinTucs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietGioHang>()
                .Property(e => e.MaSP)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietGioHang>()
                .Property(e => e.GioHangID)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietGioHang>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.MaCTHoaDon)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.MaSP)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.MaHoaDon)
                .IsUnicode(false);

            modelBuilder.Entity<ChuongTrinhKhuyenMai>()
                .Property(e => e.MaChuongTrinh)
                .IsUnicode(false);

            modelBuilder.Entity<ChuongTrinhKhuyenMai>()
                .HasMany(e => e.SanPhamKhuyenMais)
                .WithRequired(e => e.ChuongTrinhKhuyenMai)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaHoaDon)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<KHO>()
                .Property(e => e.MaKho)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaSP)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaKho)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietGioHangs)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.SanPhamKhuyenMais)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPhamKhuyenMai>()
                .Property(e => e.MaSP)
                .IsUnicode(false);

            modelBuilder.Entity<SanPhamKhuyenMai>()
                .Property(e => e.MaChuongTrinh)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoanAdmin>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoanAdmin>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoanKhachHang>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoanKhachHang>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoanKhachHang>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<TinTuc>()
                .Property(e => e.MaTinTuc)
                .IsUnicode(false);

            modelBuilder.Entity<TinTuc>()
                .Property(e => e.Username)
                .IsUnicode(false);
        }
    }
}
