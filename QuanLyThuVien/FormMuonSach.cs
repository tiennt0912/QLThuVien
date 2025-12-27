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
    public partial class FormMuonSach : Form
    {
        // Chuỗi kết nối đến SQL Server
        public MuonSachDAO muonSachDAO;

        public FormMuonSach()
        {
            muonSachDAO = new MuonSachDAO();
            InitializeComponent();
        }

        private void FormMuonSach_Load(object sender, EventArgs e)
        {
            dtpNgayMuon.Value = DateTime.Now;
            // Đặt ngày hẹn trả mặc định là 14 ngày sau ngày mượn
            dtpNgayHenTra.Value = DateTime.Now.AddDays(14);

            // Load dữ liệu cho combobox sinh viên
            LoadSinhVien();
            // Load dữ liệu cho combobox thủ thư
            LoadThuThu();
            // Load danh sách sách
            LoadSach();
            // Load danh sách phiếu mượn
            LoadPhieuMuon();

            // Tạo mã phiếu mượn mới
            txtSoPhieu.Text = TaoMaPhieuMuon();
        }

        private void LoadSinhVien()
        {
            try
            {
                
                DataTable dt = muonSachDAO.LoadAllSinhVien();
                cboMaSV.DisplayMember = "HoTen";
                cboMaSV.ValueMember = "MaSV";
                cboMaSV.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThuThu()
        {
            try
            {
                DataTable dt = muonSachDAO.LoadAllThuThu();
                cboMaThuThu.DisplayMember = "HoTen";
                cboMaThuThu.ValueMember = "MaThuThu";
                cboMaThuThu.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách thủ thư: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSach()
        {
            try
            {
                DataTable dt = muonSachDAO.LoadAllSach();
                dgvSach.DataSource = dt;
                FormatDgvSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDgvSach()
        {
            // Cấu hình hiển thị cho DataGridView sách
            dgvSach.Columns["MaSach"].HeaderText = "Mã sách";
            dgvSach.Columns["TenSach"].HeaderText = "Tên sách";
            dgvSach.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvSach.Columns["TrangThai"].HeaderText = "Trạng thái";

            dgvSach.Columns["MaSach"].Width = 80;
            dgvSach.Columns["TenSach"].Width = 250;
            dgvSach.Columns["SoLuong"].Width = 80;
            dgvSach.Columns["TrangThai"].Width = 100;
        }

        private void LoadPhieuMuon()
        {
            try
            {   
                DataTable dt = muonSachDAO.LoadAllPhieuMuon();
                dgvPhieuMuon.DataSource = dt;
                FormatDgvPhieuMuon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phiếu mượn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDgvPhieuMuon()
        {
            // Cấu hình hiển thị cho DataGridView phiếu mượn
            dgvPhieuMuon.Columns["SoPhieu"].HeaderText = "Số phiếu";
            dgvPhieuMuon.Columns["TenSinhVien"].HeaderText = "Sinh viên";
            dgvPhieuMuon.Columns["TenThuThu"].HeaderText = "Thủ thư";
            dgvPhieuMuon.Columns["NgayMuon"].HeaderText = "Ngày mượn";
            dgvPhieuMuon.Columns["NgayHenTra"].HeaderText = "Ngày hẹn trả";
            dgvPhieuMuon.Columns["TrangThai"].HeaderText = "Trạng thái";

            dgvPhieuMuon.Columns["SoPhieu"].Width = 80;
            dgvPhieuMuon.Columns["TenSinhVien"].Width = 150;
            dgvPhieuMuon.Columns["TenThuThu"].Width = 150;
            dgvPhieuMuon.Columns["NgayMuon"].Width = 100;
            dgvPhieuMuon.Columns["NgayHenTra"].Width = 100;
            dgvPhieuMuon.Columns["TrangThai"].Width = 100;
        }

        private string TaoMaPhieuMuon()
        {
            // Tạo mã phiếu mượn mới theo định dạng PM + năm + tháng + ngày + số thứ tự
            string maPhieu = "PM" + DateTime.Now.ToString("yyMMdd");
            try
            {
                //connection.Open();
                //string query = "SELECT TOP 1 SoPhieu FROM tblPhieumuon WHERE SoPhieu LIKE @MaPhieu + '%' ORDER BY SoPhieu DESC";
                //command = new SqlCommand(query, connection);
                //command.Parameters.AddWithValue("@MaPhieu", maPhieu);

                //object result = command.ExecuteScalar();
                string result = muonSachDAO.SearchTicketID(maPhieu);
                if (result != null)
                {
                    string lastCode = result.ToString();
                    // Lấy số thứ tự cuối cùng và tăng lên 1
                    int lastNumber = int.Parse(lastCode.Substring(8));
                    return maPhieu + (lastNumber + 1).ToString("D2");
                }
                else
                {
                    // Nếu chưa có phiếu mượn nào trong ngày, bắt đầu từ 01
                    return maPhieu + "01";
                }
            }
            catch
            {
                return maPhieu + "01";
            }
        }

        private void btnTimSach_Click(object sender, EventArgs e)
        {
            try
            {
                string keyWord = txtTimSach.Text;
                DataTable dt = muonSachDAO.LoadSachByState(keyWord);
                dgvSach.DataSource = dt;
                FormatDgvSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemSach_Click(object sender, EventArgs e)
        {
            if (dgvSach.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sách cần mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maSach = dgvSach.SelectedRows[0].Cells["MaSach"].Value.ToString();
            string tenSach = dgvSach.SelectedRows[0].Cells["TenSach"].Value.ToString();

            // Kiểm tra xem sách đã được thêm vào danh sách mượn chưa
            foreach (DataGridViewRow row in dgvSachMuon.Rows)
            {
                // Kiểm tra null trước khi so sánh
                if (row.Cells["MaSachMuon"].Value != null &&
                    row.Cells["MaSachMuon"].Value.ToString() == maSach)
                {
                    MessageBox.Show("Sách này đã được thêm vào danh sách mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }


            // Thêm sách vào DataGridView sách mượn
            int index = dgvSachMuon.Rows.Add();
            dgvSachMuon.Rows[index].Cells["MaSachMuon"].Value = maSach;
            dgvSachMuon.Rows[index].Cells["TenSachMuon"].Value = tenSach;
            dgvSachMuon.Rows[index].Cells["TinhTrangMuon"].Value = "Tốt";
            dgvSachMuon.Rows[index].Cells["GhiChuMuon"].Value = "";

            // Cập nhật số lượng sách được chọn để mượn
            lblSoSachMuon.Text = dgvSachMuon.Rows.Count.ToString();
        }

        private void btnXoaSach_Click(object sender, EventArgs e)
        {
            if (dgvSachMuon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sách cần xóa khỏi danh sách mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvSachMuon.Rows.RemoveAt(dgvSachMuon.SelectedRows[0].Index);

            // Cập nhật số lượng sách được chọn để mượn
            lblSoSachMuon.Text = dgvSachMuon.Rows.Count.ToString();
        }

        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoPhieu.Text))
            {
                MessageBox.Show("Vui lòng nhập số phiếu mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoPhieu.Focus();
                return;
            }

            if (cboMaSV.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn sinh viên mượn sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaSV.Focus();
                return;
            }

            if (cboMaThuThu.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn thủ thư lập phiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaThuThu.Focus();
                return;
            }

            if (dgvSachMuon.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một sách để mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpNgayHenTra.Value <= dtpNgayMuon.Value)
            {
                MessageBox.Show("Ngày hẹn trả phải sau ngày mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayHenTra.Focus();
                return;
            }

            try
            {


                try
                {
                    // Thêm phiếu mượn
/*                    command.CommandText = @"INSERT INTO tblPhieumuon (SoPhieu, MaSV, MaThuThu, NgayMuon, NgayHenTra, TrangThai)
                                            VALUES (@SoPhieu, @MaSV, @MaThuThu, @NgayMuon, @NgayHenTra, @TrangThai)";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@SoPhieu", txtSoPhieu.Text);
                    command.Parameters.AddWithValue("@MaSV", cboMaSV.SelectedValue);
                    command.Parameters.AddWithValue("@MaThuThu", cboMaThuThu.SelectedValue);
                    command.Parameters.AddWithValue("@NgayMuon", dtpNgayMuon.Value);
                    command.Parameters.AddWithValue("@NgayHenTra", dtpNgayHenTra.Value);
                    command.Parameters.AddWithValue("@TrangThai", "Đang mượn");
                    command.ExecuteNonQuery();*/

                    PhieuMuon pm =  new PhieuMuon();
                    pm.SoPhieu = txtSoPhieu.Text;
                    pm.MaSV = cboMaSV.SelectedValue.ToString();
                    pm.MaThuThu = cboMaThuThu.SelectedValue.ToString();
                    pm.NgayMuon = Convert.ToDateTime(dtpNgayMuon.Value);
                    pm.NgayHenTra = Convert.ToDateTime(dtpNgayHenTra.Value);
                    pm.TrangThai = "Đang mượn";
                    bool res = muonSachDAO.AddPhieuMuon(pm);   
/*                    if (!res)
                    {
                        return;
                    }*/


                    // Thêm chi tiết phiếu mượn và cập nhật số lượng sách
                    foreach (DataGridViewRow row in dgvSachMuon.Rows)
                    {
                        string maSach = row.Cells["MaSachMuon"].Value.ToString();
                        string tinhTrang = row.Cells["TinhTrangMuon"].Value.ToString();
                        string ghiChu = row.Cells["GhiChuMuon"].Value != null ? row.Cells["GhiChuMuon"].Value.ToString() : "";

                        // Thêm chi tiết phiếu mượn
/*                        command.CommandText = @"INSERT INTO tblPhieumuonchitiet (SoPhieu, MaSach, NgayMuon, TinhTrang, GhiChu)
                                                VALUES (@SoPhieu, @MaSach, @NgayMuon, @TinhTrang, @GhiChu)";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@SoPhieu", txtSoPhieu.Text);
                        command.Parameters.AddWithValue("@MaSach", maSach);
                        command.Parameters.AddWithValue("@NgayMuon", dtpNgayMuon.Value);
                        //command.Parameters.AddWithValue("@NgayTra", dtpNgayHenTra.Value);
                        command.Parameters.AddWithValue("@TinhTrang", tinhTrang);
                        command.Parameters.AddWithValue("@GhiChu", ghiChu);
                        command.ExecuteNonQuery();*/
                        PhieuMuonChiTiet pmct =  new PhieuMuonChiTiet();
                        pmct.SoPhieu = txtSoPhieu.Text;
                        pmct.MaSach = maSach;
                        pmct.NgayMuon = Convert.ToDateTime(dtpNgayMuon.Value);
                        pmct.TinhTrang = tinhTrang;
                        pmct.GhiChu = ghiChu;
                        bool result = muonSachDAO.AddPhieuMuonCT(pmct);
/*                        if (!result)
                        {
                            return;
                        }*/



                        // Cập nhật số lượng sách giảm 1
                        muonSachDAO.UpdateSoLuongSach(maSach);
                        
                    }

                    // Hoàn tất giao dịch

                    MessageBox.Show("Lập phiếu mượn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Làm mới form sau khi lập phiếu
                    dgvSachMuon.Rows.Clear();
                    lblSoSachMuon.Text = "0";
                    txtSoPhieu.Text = TaoMaPhieuMuon();
                    dtpNgayMuon.Value = DateTime.Now;
                    dtpNgayHenTra.Value = DateTime.Now.AddDays(14);

                    // Làm mới danh sách phiếu mượn và danh sách sách
                    LoadPhieuMuon();
                    LoadSach();
                }
                catch (Exception ex)
                {
                    // Hủy giao dịch nếu có lỗi
                    MessageBox.Show("Lỗi khi lập phiếu mượn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dgvSachMuon.Rows.Clear();
            lblSoSachMuon.Text = "0";
            txtSoPhieu.Text = TaoMaPhieuMuon();
            dtpNgayMuon.Value = DateTime.Now;
            dtpNgayHenTra.Value = DateTime.Now.AddDays(14);
            txtTimSach.Clear();
            LoadSach();
            LoadPhieuMuon();
        }

        private void dgvPhieuMuon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string soPhieu = dgvPhieuMuon.Rows[e.RowIndex].Cells["SoPhieu"].Value.ToString();

                // Hiển thị thông tin chi tiết phiếu mượn
                FormChiTietPhieuMuon formChiTiet = new FormChiTietPhieuMuon(soPhieu);
                formChiTiet.ShowDialog();

                // Làm mới danh sách phiếu mượn sau khi đóng form chi tiết
                LoadPhieuMuon();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
