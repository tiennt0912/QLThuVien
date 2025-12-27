using AmateurProject.Db;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DAO
{
    public class CTPhieuMuonDAO
    {
        public DataTable LoadThongTinPM(string soPhieu)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT pm.SoPhieu, sv.HoTen AS TenSinhVien, sv.Lop, tt.HoTen AS TenThuThu, 
                                               pm.NgayMuon, pm.NgayHenTra, pm.TrangThai
                                               FROM tblPhieumuon pm
                                               JOIN tblSinhvien sv ON pm.MaSV = sv.MaSV
                                               JOIN tblThuthu tt ON pm.MaThuThu = tt.MaThuThu
                                               WHERE pm.SoPhieu = @SoPhieu";
            try
            {
                using (SqlConnection conn = DBHelper.GetSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SoPhieu",soPhieu);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd)) {
                            conn.Open();
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return dt;
        }
        public DataTable LoadChiTietPM(string soPhieu)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT s.MaSach, s.TenSach, ct.NgayMuon, ct.NgayTra, ct.TinhTrang, ct.GhiChu
                                FROM tblPhieumuonchitiet ct
                                JOIN tblSach s ON ct.MaSach = s.MaSach
                                WHERE ct.SoPhieu = @SoPhieu";
            try
            {
                using (SqlConnection conn = DBHelper.GetSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {   
                        cmd.Parameters.AddWithValue("@SoPhieu", soPhieu);  
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return dt;
        }
    }
}
