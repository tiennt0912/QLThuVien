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
    public class TraSachDAO
    {
        public DataTable LoadPhieuMuon()
        {
            DataTable dt = new DataTable();
            string query = "SELECT p.SoPhieu, sv.HoTen, p.NgayMuon " +
                                "FROM tblPhieumuon p " +
                                "INNER JOIN tblSinhvien sv ON p.MaSV = sv.MaSV " +
                                "WHERE p.TrangThai = N'Đang mượn'";

            try
            {
                using (SqlConnection conn = DBHelper.GetSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
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
        public DataTable LoadDsSachMuon(string soPhieu)
        {
            DataTable dt = new DataTable();
            string query = "SELECT pct.ID, s.MaSach, s.TenSach, tl.TenLoai, s.TacGia, pct.NgayMuon " +
               "FROM tblPhieumuonchitiet pct " +
               "INNER JOIN tblSach s ON pct.MaSach = s.MaSach " +
               "INNER JOIN tblTheloai tl ON s.MaLoai = tl.MaLoai " +
               "WHERE pct.SoPhieu = @SoPhieu AND pct.NgayTra IS NULL";

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
        public DataTable LoadThongTinPhieuMuon(string soPhieu)
        {
            DataTable dt = new DataTable();
            string query = "SELECT p.SoPhieu, sv.MaSV, sv.HoTen, sv.Lop, p.NgayMuon, p.NgayHenTra " +
                                                "FROM tblPhieumuon p " +
                                                "INNER JOIN tblSinhvien sv ON p.MaSV = sv.MaSV " +
                                                "WHERE p.SoPhieu = @SoPhieu";
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

        public bool UpdatePhieuMuonCT(int id, DateTime ngayTra, string ghiChu, string tinhTrang) {
            string query = "UPDATE tblPhieumuonchitiet SET " +
                                         "NgayTra = @NgayTra, " +
                                         "GhiChu = @GhiChu, " +
                                         "TinhTrang = @TinhTrang " +
                                         "WHERE ID = @ID";
            try
            {
                using (SqlConnection conn = DBHelper.GetSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.AddWithValue("@NgayTra", ngayTra);
                            cmd.Parameters.AddWithValue("@GhiChu", ghiChu);
                            cmd.Parameters.AddWithValue("@TinhTrang", tinhTrang);

                            cmd.Parameters.AddWithValue("@ID", id);
                            conn.Open();
                            int res = cmd.ExecuteNonQuery();
                            return res > 0;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public bool UpdateSoLuongSach(string maSach)
        {
            string query = "UPDATE tblSach SET SoLuong = SoLuong + 1 WHERE MaSach = @MaSach";
            try
            {
                using (SqlConnection conn = DBHelper.GetSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.AddWithValue("@MaSach", maSach);
                            conn.Open();
                            int res = cmd.ExecuteNonQuery();
                            return res > 0;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public bool UpdateTrangThaiTraSach(string soPhieu)
        {
            string query = "UPDATE tblPhieumuon SET TrangThai = N'Đã trả' WHERE SoPhieu = @SoPhieu";
            try
            {
                using (SqlConnection conn = DBHelper.GetSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.AddWithValue("@SoPhieu", soPhieu);
                            conn.Open();
                            int res = cmd.ExecuteNonQuery();
                            return res > 0;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public int KiemTraConSach(string soPhieu)
        {
            string query = "SELECT COUNT(*) FROM tblPhieumuonchitiet WHERE SoPhieu = @SoPhieu AND NgayTra IS NULL";
            try
            {
                using (SqlConnection conn = DBHelper.GetSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@SoPhieu", soPhieu);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return -1;
        }
    }
}
