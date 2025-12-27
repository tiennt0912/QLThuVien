using QuanLyThuVien.DAO;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class FormChiTietPhieuMuon : Form
    {
        private CTPhieuMuonDAO ctPhieuMuonDAO;
        private string soPhieu;

        public FormChiTietPhieuMuon(string soPhieu)
        {
            InitializeComponent();
            this.ctPhieuMuonDAO =  new CTPhieuMuonDAO();
            this.soPhieu = soPhieu;
        }

        private void FormChiTietPhieuMuon_Load(object sender, EventArgs e)
        {
            LoadThongTinPhieuMuon();
            LoadChiTietPhieuMuon();
        }

        private void LoadThongTinPhieuMuon()
        {
            try
            {

                DataTable dt = ctPhieuMuonDAO.LoadThongTinPM(soPhieu);
                if (dt.Rows.Count == 0) {
                    MessageBox.Show("Không tìm thấy kết quả " , "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                DataRow firstRow = dt.Rows[0];

                /*                lblSoPhieu.Text = pmct.SoPhieu.ToString();
                                lblSinhVien.Text = pmct["TenSinhVien"].ToString();
                                lblLop.Text = reader["Lop"].ToString();
                                lblThuThu.Text = reader["TenThuThu"].ToString();
                                lblNgayMuon.Text = Convert.ToDateTime(reader["NgayMuon"]).ToString("dd/MM/yyyy");
                                lblNgayHenTra.Text = Convert.ToDateTime(reader["NgayHenTra"]).ToString("dd/MM/yyyy");
                                lblTrangThai.Text = reader["TrangThai"].ToString();*/


                lblSoPhieu.Text = firstRow["SoPhieu"].ToString();
                lblSinhVien.Text = firstRow["TenSinhVien"].ToString();
                lblLop.Text = firstRow["Lop"].ToString();
                lblThuThu.Text = firstRow["TenThuThu"].ToString();
                lblNgayMuon.Text = Convert.ToDateTime(firstRow["NgayMuon"]).ToString("dd/MM/yyyy");
                lblNgayHenTra.Text = Convert.ToDateTime(firstRow["NgayHenTra"]).ToString("dd/MM/yyyy");
                lblTrangThai.Text = firstRow["TrangThai"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin phiếu mượn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChiTietPhieuMuon()
        {
            try
            {
/*                connection.Open();
                string query = @"SELECT s.MaSach, s.TenSach, ct.NgayMuon, ct.NgayTra, ct.TinhTrang, ct.GhiChu
                                FROM tblPhieumuonchitiet ct
                                JOIN tblSach s ON ct.MaSach = s.MaSach
                                WHERE ct.SoPhieu = @SoPhieu";
                adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@SoPhieu", soPhieu);

                DataTable dt = new DataTable();
                adapter.Fill(dt);*/
                DataTable dt = ctPhieuMuonDAO.LoadChiTietPM(soPhieu);
                dgvChiTietPhieuMuon.DataSource = dt;
                FormatDgvChiTiet();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết phiếu mượn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDgvChiTiet()
        {
            dgvChiTietPhieuMuon.Columns["MaSach"].HeaderText = "Mã sách";
            dgvChiTietPhieuMuon.Columns["TenSach"].HeaderText = "Tên sách";
            dgvChiTietPhieuMuon.Columns["NgayMuon"].HeaderText = "Ngày mượn";
            dgvChiTietPhieuMuon.Columns["NgayTra"].HeaderText = "Ngày trả";
            dgvChiTietPhieuMuon.Columns["TinhTrang"].HeaderText = "Tình trạng";
            dgvChiTietPhieuMuon.Columns["GhiChu"].HeaderText = "Ghi chú";

            dgvChiTietPhieuMuon.Columns["MaSach"].Width = 80;
            dgvChiTietPhieuMuon.Columns["TenSach"].Width = 250;
            dgvChiTietPhieuMuon.Columns["NgayMuon"].Width = 100;
            dgvChiTietPhieuMuon.Columns["NgayTra"].Width = 100;
            dgvChiTietPhieuMuon.Columns["TinhTrang"].Width = 100;
            dgvChiTietPhieuMuon.Columns["GhiChu"].Width = 150;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
