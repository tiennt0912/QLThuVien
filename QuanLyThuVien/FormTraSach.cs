using QuanLyThuVien.DAO;
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
    public partial class FormTraSach : Form
    {
        // Chuỗi kết nối với cơ sở dữ liệu
        private TraSachDAO traSachDAO;

        public FormTraSach()
        {
            traSachDAO = new TraSachDAO();
            InitializeComponent();
        }

        private void FormTraSach_Load(object sender, EventArgs e)
        {
            // Load dữ liệu cho combobox phiếu mượn
            LoadPhieuMuon();

            // Thiết lập DateTimePicker ngày trả là ngày hiện tại
            dtpNgayTra.Value = DateTime.Now;

            // Thiết lập các ComboBox tình trạng sách
            cboTinhTrang.Items.Add("Bình thường");
            cboTinhTrang.Items.Add("Hư hỏng nhẹ");
            cboTinhTrang.Items.Add("Hư hỏng nặng");
            cboTinhTrang.Items.Add("Mất sách");
            cboTinhTrang.SelectedIndex = 0;
        }

        private void LoadPhieuMuon()
        {
            try
            {

                /*                    conn.Open();
                                    string sql = "SELECT p.SoPhieu, sv.HoTen, p.NgayMuon " +
                                                "FROM tblPhieumuon p " +
                                                "INNER JOIN tblSinhvien sv ON p.MaSV = sv.MaSV " +
                                                "WHERE p.TrangThai = N'Đang mượn'";

                                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                                    DataTable dt = new DataTable();
                                    adapter.Fill(dt);*/
                DataTable dt = traSachDAO.LoadPhieuMuon();
                
                cboPhieuMuon.DisplayMember = "SoPhieu";
                cboPhieuMuon.ValueMember = "SoPhieu";
                cboPhieuMuon.DataSource = dt;
            }


            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboPhieuMuon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPhieuMuon.SelectedValue != null)
            {
                LoadThongTinPhieuMuon(cboPhieuMuon.SelectedValue.ToString());
                LoadDanhSachSachMuon(cboPhieuMuon.SelectedValue.ToString());
            }
        }

        private void LoadThongTinPhieuMuon(string soPhieu)
        {
            try
            {
                /*                using (SqlConnection conn = new SqlConnection(connectionString))
                                {
                                    conn.Open();
                                    string sql = "SELECT p.SoPhieu, sv.MaSV, sv.HoTen, sv.Lop, p.NgayMuon, p.NgayHenTra " +
                                                "FROM tblPhieumuon p " +
                                                "INNER JOIN tblSinhvien sv ON p.MaSV = sv.MaSV " +
                                                "WHERE p.SoPhieu = @SoPhieu";

                                    SqlCommand cmd = new SqlCommand(sql, conn);
                                    cmd.Parameters.AddWithValue("@SoPhieu", soPhieu);

                                    SqlDataReader reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        txtMaSV.Text = reader["MaSV"].ToString();
                                        txtHoTen.Text = reader["HoTen"].ToString();
                                        txtLop.Text = reader["Lop"].ToString();
                                        txtNgayMuon.Text = Convert.ToDateTime(reader["NgayMuon"]).ToString("dd/MM/yyyy");
                                        txtNgayHenTra.Text = Convert.ToDateTime(reader["NgayHenTra"]).ToString("dd/MM/yyyy");
                                    }
                                    reader.Close();
                                }*/
                DataTable dt = traSachDAO.LoadThongTinPhieuMuon(soPhieu);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


                txtMaSV.Text = firstRow["MaSV"].ToString();
                txtHoTen.Text = firstRow["HoTen"].ToString();
                txtLop.Text = firstRow["Lop"].ToString();
                txtNgayMuon.Text = Convert.ToDateTime(firstRow["NgayMuon"]).ToString("dd/MM/yyyy");
                txtNgayHenTra.Text = Convert.ToDateTime(firstRow["NgayHenTra"]).ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachSachMuon(string soPhieu)
        {
            try
            {
                DataTable dt = traSachDAO.LoadDsSachMuon(soPhieu);
                dgvDanhSachSach.DataSource = dt;
                // Định dạng lại DataGridView
                dgvDanhSachSach.Columns["ID"].Visible = false;
                dgvDanhSachSach.Columns["MaSach"].HeaderText = "Mã sách";
                dgvDanhSachSach.Columns["TenSach"].HeaderText = "Tên sách";
                dgvDanhSachSach.Columns["TenLoai"].HeaderText = "Thể loại";
                dgvDanhSachSach.Columns["TacGia"].HeaderText = "Tác giả";
                dgvDanhSachSach.Columns["NgayMuon"].HeaderText = "Ngày mượn";
                dgvDanhSachSach.Columns["NgayMuon"].DefaultCellStyle.Format = "dd/MM/yyyy";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDanhSachSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachSach.Rows[e.RowIndex];
                txtMaSach.Text = row.Cells["MaSach"].Value.ToString();
                txtTenSach.Text = row.Cells["TenSach"].Value.ToString();
                txtTheLoai.Text = row.Cells["TenLoai"].Value.ToString();
                txtTacGia.Text = row.Cells["TacGia"].Value.ToString();

                // Lưu ID của chi tiết phiếu mượn
                lblPhieuMuonChiTietID.Text = row.Cells["ID"].Value.ToString();
            }
        }

        private void btnTraSach_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblPhieuMuonChiTietID.Text))
            {
                MessageBox.Show("Vui lòng chọn sách cần trả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {


                // Cập nhật thông tin trả sách trong chi tiết phiếu mượn
/*                string updateChiTiet = "UPDATE tblPhieumuonchitiet SET " +
                                      "NgayTra = @NgayTra, " +
                                      "GhiChu = @GhiChu, " +
                                      "TinhTrang = @TinhTrang " +
                                      "WHERE ID = @ID";*/

                /*                    SqlCommand cmd = new SqlCommand(updateChiTiet, conn);
                                    cmd.Parameters.AddWithValue("@NgayTra", dtpNgayTra.Value);
                                    cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                                    cmd.Parameters.AddWithValue("@TinhTrang", cboTinhTrang.Text);
                                    cmd.Parameters.AddWithValue("@ID", int.Parse(lblPhieuMuonChiTietID.Text));

                                    cmd.ExecuteNonQuery();*/
                DateTime ngayTra = dtpNgayTra.Value;
                string ghiChu = txtGhiChu.Text;
                string tinhTrang = cboTinhTrang.Text;
                int id = int.Parse(lblPhieuMuonChiTietID.Text);
                bool res = traSachDAO.UpdatePhieuMuonCT(id, ngayTra, ghiChu, tinhTrang);
                if (!res)
                {
                    MessageBox.Show("Lỗi update trả sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }



                // Tăng số lượng sách trong kho
                string maSach = txtMaSach.Text;
                bool updateSach = traSachDAO.UpdateSoLuongSach(maSach);
                if (!updateSach)
                {
                    MessageBox.Show("Lỗi update số lượng sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                /*                    string updateSoLuongSach = "UPDATE tblSach SET SoLuong = SoLuong + 1 WHERE MaSach = @MaSach";
                                    cmd = new SqlCommand(updateSoLuongSach, conn);
                                    cmd.Parameters.AddWithValue("@MaSach", );
                                    cmd.ExecuteNonQuery();*/

                // Kiểm tra nếu không còn sách nào chưa trả thì cập nhật trạng thái phiếu mượn
                string soPhieu = cboPhieuMuon.SelectedValue.ToString();
                int conSach = traSachDAO.KiemTraConSach(soPhieu);
                if (conSach == 0)
                {
                    bool traSachBool =  traSachDAO.UpdateTrangThaiTraSach(soPhieu);
                    if (!traSachBool)
                    {
                        MessageBox.Show("Lỗi update trạng thái trả sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                        
                }
                if (conSach == -1)
                {
                        MessageBox.Show("Lỗi kiểm tra số lượng sách trong phiếu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }
                MessageBox.Show("Trả sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Làm mới danh sách sách mượn
                LoadDanhSachSachMuon(cboPhieuMuon.SelectedValue.ToString());

                // Xóa thông tin sách đã trả
                txtMaSach.Text = "";
                txtTenSach.Text = "";
                txtTheLoai.Text = "";
                txtTacGia.Text = "";
                txtGhiChu.Text = "";
                cboTinhTrang.SelectedIndex = 0;
                lblPhieuMuonChiTietID.Text = "";

                // Nếu không còn sách nào chưa trả, làm mới danh sách phiếu mượn
                if (conSach == 0)
                {
                    LoadPhieuMuon();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Xóa thông tin sách đã trả
            txtMaSach.Text = "";
            txtTenSach.Text = "";
            txtTheLoai.Text = "";
            txtTacGia.Text = "";
            txtGhiChu.Text = "";
            cboTinhTrang.SelectedIndex = 0;
            lblPhieuMuonChiTietID.Text = "";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormTraSach_Load_1(object sender, EventArgs e)
        {

        }
    }
}
