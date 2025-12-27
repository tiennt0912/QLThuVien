CREATE DATABASE QLThuVien
GO

USE QLThuVien
GO

-- Bảng Thể loại
CREATE TABLE tblTheloai (
    MaLoai VARCHAR(10) PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL,
    MoTa NTEXT
)

-- Bảng Sách
CREATE TABLE tblSach (
    MaSach VARCHAR(10) PRIMARY KEY,
    TenSach NVARCHAR(200) NOT NULL,
    SoLuong INT NOT NULL DEFAULT 0,
    MaLoai VARCHAR(10) REFERENCES tblTheloai(MaLoai),
    TacGia NVARCHAR(100),
    NhaXuatBan NVARCHAR(100),
    NamXuatBan INT,
    NgayNhap DATE DEFAULT GETDATE(),
    TrangThai NVARCHAR(50) DEFAULT N'Có sẵn'
)

-- Bảng Sinh viên
CREATE TABLE tblSinhvien (
    MaSV VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    Lop NVARCHAR(20),
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(200),
    SDT VARCHAR(15),
    Email VARCHAR(100),
    NgayDangKy DATE DEFAULT GETDATE(),
    NgayHetHan DATE
)

-- Bảng Thủ thư
CREATE TABLE tblThuthu (
    MaThuThu VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(200),
    SDT VARCHAR(15),
    Email VARCHAR(100),
    TaiKhoan VARCHAR(50) UNIQUE,
    MatKhau VARCHAR(100),
    Quyen NVARCHAR(20) DEFAULT N'Thủ thư'
)

-- Bảng Phiếu mượn
CREATE TABLE tblPhieumuon (
    SoPhieu VARCHAR(10) PRIMARY KEY,
    MaSV VARCHAR(10) REFERENCES tblSinhvien(MaSV),
    MaThuThu VARCHAR(10) REFERENCES tblThuthu(MaThuThu),
    NgayMuon DATE DEFAULT GETDATE(),
    NgayHenTra DATE,
    TrangThai NVARCHAR(50) DEFAULT N'Đang mượn'
)

-- Bảng Phiếu mượn chi tiết
CREATE TABLE tblPhieumuonchitiet (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    SoPhieu VARCHAR(10) REFERENCES tblPhieumuon(SoPhieu),
    MaSach VARCHAR(10) REFERENCES tblSach(MaSach),
    NgayMuon DATE DEFAULT GETDATE(),
    NgayTra DATE,
    GhiChu NTEXT,
    TinhTrang NVARCHAR(50)
)

-- Thêm dữ liệu vào bảng Thể loại
INSERT INTO tblTheloai (MaLoai, TenLoai, MoTa) VALUES
('TL001', N'Văn học', N'Sách văn học các thể loại'),
('TL002', N'Khoa học', N'Sách khoa học các lĩnh vực'),
('TL003', N'Lịch sử', N'Sách về lịch sử Việt Nam và thế giới'),
('TL004', N'Công nghệ thông tin', N'Sách về CNTT và lập trình'),
('TL005', N'Kinh tế', N'Sách về kinh tế, tài chính, quản trị'),
('TL006', N'Tiểu thuyết', N'Sách tiểu thuyết hiện đại'),
('TL007', N'Tâm lý học', N'Sách về tâm lý, phát triển bản thân');

-- Thêm dữ liệu vào bảng Sách
INSERT INTO tblSach (MaSach, TenSach, SoLuong, MaLoai, TacGia, NhaXuatBan, NamXuatBan) VALUES
('S001', N'Chí Phèo', 10, 'TL001', N'Nam Cao', N'NXB Kim Đồng', 2018),
('S002', N'Dac Nhan Tam', 5, 'TL005', N'Dale Carnegie', N'NXB Trẻ', 2019),
('S003', N'Lập trình C++ cơ bản', 7, 'TL004', N'Nguyễn Văn A', N'NXB Giáo Dục', 2020),
('S004', N'Lịch sử Việt Nam', 8, 'TL003', N'Trần Hữu Tài', N'NXB Chính Trị', 2017),
('S005', N'Vật lý đại cương', 6, 'TL002', N'Ngô Bảo Châu', N'NXB Đại học Quốc Gia', 2021),
('S006', N'Trí tuệ cảm xúc', 9, 'TL007', N'Daniel Goleman', N'NXB Lao Động', 2022),
('S007', N'Chiến tranh và hòa bình', 4, 'TL006', N'Lev Tolstoy', N'NXB Văn Học', 2016);

-- Thêm dữ liệu vào bảng Sinh viên
INSERT INTO tblSinhvien (MaSV, HoTen, Lop, NgaySinh, GioiTinh, DiaChi, SDT, Email, NgayHetHan) VALUES
('SV001', N'Nguyễn Văn An', N'K15CNTT', '2002-06-15', N'Nam', N'Hà Nội', '0912345678', 'an.nguyen@gmail.com', '2026-06-15'),
('SV002', N'Trần Thị Bình', N'K16QTKD', '2003-08-22', N'Nữ', N'Bắc Ninh', '0987654321', 'binh.tran@yahoo.com', '2027-08-22'),
('SV003', N'Phạm Hữu Cường', N'K15KTPM', '2002-09-10', N'Nam', N'Hải Phòng', '0911122233', 'cuong.pham@gmail.com', '2026-09-10'),
('SV004', N'Lê Hoàng Dũng', N'K17CNTT', '2004-02-05', N'Nam', N'Đà Nẵng', '0933344556', 'dung.le@gmail.com', '2028-02-05'),
('SV005', N'Ngô Thanh Hương', N'K16KT', '2003-12-12', N'Nữ', N'Quảng Ninh', '0977788990', 'huong.ngo@gmail.com', '2027-12-12');

-- Thêm dữ liệu vào bảng Thủ thư
INSERT INTO tblThuthu (MaThuThu, HoTen, NgaySinh, GioiTinh, DiaChi, SDT, Email, TaiKhoan, MatKhau) VALUES
('TT001', N'Lê Thanh Hà', '1990-03-05', N'Nữ', N'Hà Nội', '0909090909', 'ha.le@gmail.com', 'thuthu1', '123456'),
('TT002', N'Vũ Đình Nam', '1988-11-20', N'Nam', N'Hải Dương', '0988888888', 'nam.vu@gmail.com', 'thuthu2', 'abcdef'),
('TT003', N'Hoàng Minh Đức', '1992-07-15', N'Nam', N'TP Hồ Chí Minh', '0966667777', 'duc.hoang@gmail.com', 'thuthu3', '123abc');

-- Thêm dữ liệu vào bảng Phiếu mượn
INSERT INTO tblPhieumuon (SoPhieu, MaSV, MaThuThu, NgayHenTra) VALUES
('PM001', 'SV001', 'TT001', '2025-03-10'),
('PM002', 'SV002', 'TT002', '2025-03-15'),
('PM003', 'SV003', 'TT003', '2025-03-20'),
('PM004', 'SV004', 'TT001', '2025-03-25');

-- Thêm dữ liệu vào bảng Phiếu mượn chi tiết
INSERT INTO tblPhieumuonchitiet (SoPhieu, MaSach, NgayMuon, NgayTra, GhiChu, TinhTrang) VALUES
('PM001', 'S001', '2025-03-01', NULL, N'Không có ghi chú', N'Đang mượn'),
('PM001', 'S003', '2025-03-01', NULL, N'Không có ghi chú', N'Đang mượn'),
('PM002', 'S002', '2025-03-02', NULL, N'Bìa sách hơi cũ', N'Đang mượn'),
('PM003', 'S004', '2025-03-05', NULL, N'Không có ghi chú', N'Đang mượn'),
('PM003', 'S006', '2025-03-05', NULL, N'Sách còn mới', N'Đang mượn'),
('PM004', 'S005', '2025-03-10', NULL, N'Bìa sách bị quăn', N'Đang mượn'),
('PM004', 'S007', '2025-03-10', NULL, N'Sách quý, cần giữ gìn', N'Đang mượn');
