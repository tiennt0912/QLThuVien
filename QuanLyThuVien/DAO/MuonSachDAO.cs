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
    public class MuonSachDAO
    {
        public string SearchTicketID(string soPhieu)
        {
            string query = "SELECT TOP 1 SoPhieu FROM tblPhieumuon WHERE SoPhieu LIKE @SoPhieu + '%' ORDER BY SoPhieu DESC";
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
                            return result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return "";
        }

        public DataTable TimSach(string maSach)
        {
            string query = "SELECT TOP 1 SoPhieu FROM tblPhieumuon WHERE SoPhieu LIKE @MaPhieu + '%' ORDER BY SoPhieu DESC";
            DataTable dt = new DataTable();
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

        public DataTable LoadAllSinhVien()
        {
            DataTable dt = new DataTable();
            string query = "SELECT MaSV, HoTen FROM tblSinhvien";
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

        public DataTable LoadAllThuThu()
        {
            DataTable dt = new DataTable();
            string query = "SELECT MaThuThu, HoTen FROM tblThuthu";
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

        public DataTable LoadAllSach()
        {
            DataTable dt = new DataTable();
            string query = "SELECT MaSach, TenSach, SoLuong, TrangThai FROM tblSach WHERE SoLuong > 0 AND TrangThai = N'Có sẵn'";
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
        public DataTable LoadAllPhieuMuon()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT pm.SoPhieu, sv.HoTen AS TenSinhVien, tt.HoTen AS TenThuThu, 
                                pm.NgayMuon, pm.NgayHenTra, pm.TrangThai
                                FROM tblPhieumuon pm
                                JOIN tblSinhvien sv ON pm.MaSV = sv.MaSV
                                JOIN tblThuthu tt ON pm.MaThuThu = tt.MaThuThu
                                ORDER BY pm.NgayMuon DESC";
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
        public DataTable LoadSachByState(string additionCon = "")
        {

            DataTable dt = new DataTable();
            string query = "SELECT MaSach, TenSach, SoLuong, TrangThai FROM tblSach WHERE SoLuong > 0 AND TrangThai = N'Có sẵn'";
            if (!string.IsNullOrEmpty(additionCon))
            {
                query += $"AND(MaSach LIKE N'%{additionCon}%' OR TenSach LIKE N'%{additionCon}%')";
            }
            try
            {
                using (SqlConnection conn = DBHelper.GetSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
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
        public bool AddPhieuMuon(PhieuMuon pm)
        {
            string query = @"INSERT INTO tblPhieumuon (SoPhieu, MaSV, MaThuThu, NgayMuon, NgayHenTra, TrangThai)
                                            VALUES (@SoPhieu, @MaSV, @MaThuThu, @NgayMuon, @NgayHenTra, @TrangThai)";
            try
            {
                using (SqlConnection conn = DBHelper.GetSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.AddWithValue("@SoPhieu", pm.SoPhieu);
                            cmd.Parameters.AddWithValue("@MaSV", pm.MaSV);
                            cmd.Parameters.AddWithValue("@MaThuThu", pm.MaThuThu);
                            cmd.Parameters.AddWithValue("@NgayMuon", pm.NgayMuon);
                            cmd.Parameters.AddWithValue("@NgayHenTra", pm.NgayHenTra);
                            cmd.Parameters.AddWithValue("@TrangThai", pm.TrangThai);
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
        public bool AddPhieuMuonCT(PhieuMuonChiTiet pmct)
        {
            string query = @"INSERT INTO tblPhieumuonchitiet (SoPhieu, MaSach, NgayMuon, TinhTrang, GhiChu)
                                                VALUES (@SoPhieu, @MaSach, @NgayMuon, @TinhTrang, @GhiChu)";
            try
            {
                using (SqlConnection conn = DBHelper.GetSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.AddWithValue("@SoPhieu", pmct.SoPhieu);
                            cmd.Parameters.AddWithValue("@MaSach", pmct.MaSach);
                            cmd.Parameters.AddWithValue("@NgayMuon", pmct.NgayMuon);
                            cmd.Parameters.AddWithValue("@TinhTrang", pmct.TinhTrang);
                            cmd.Parameters.AddWithValue("@GhiChu", pmct.GhiChu);
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

        public void UpdateSoLuongSach(string maSach)
        {
            string query = @"UPDATE tblSach SET SoLuong = SoLuong - 1,
                                                TrangThai = CASE WHEN SoLuong - 1 <= 0 THEN N'Đã hết' ELSE N'Có sẵn' END
                                                WHERE MaSach = @MaSach";
            try
            {
                using (SqlConnection conn = DBHelper.GetSqlConnection())
                {

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@MaSach", maSach);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }

}
