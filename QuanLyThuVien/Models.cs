using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Models
{
    public class Sach
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public int SoLuong { get; set; }
        public string MaLoai { get; set; }
        public string TacGia { get; set; }
        public string NhaXuatBan { get; set; }
        public int NamXuatBan { get; set; }
        public DateTime NgayNhap { get; set; }
        public string TrangThai { get; set; }
    }

    public class TheLoai
    {
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
    }

    public class SinhVien
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public string Lop { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public DateTime NgayDangKy { get; set; }
        public DateTime NgayHetHan { get; set; }
    }

    public class ThuThu
    {
        public string MaThuThu { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string Quyen { get; set; }
    }

    public class PhieuMuon
    {
        public string SoPhieu { get; set; }
        public string MaSV { get; set; }
        public string MaThuThu { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime NgayHenTra { get; set; }
        public string TrangThai { get; set; }
    }

    public class PhieuMuonChiTiet
    {
        public int ID { get; set; }
        public string SoPhieu { get; set; }
        public string MaSach { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime? NgayTra { get; set; }
        public string GhiChu { get; set; }
        public string TinhTrang { get; set; }
    }
}