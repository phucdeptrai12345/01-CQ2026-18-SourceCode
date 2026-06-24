-- Kịch bản dữ liệu mẫu (Sample Database) cho ứng dụng Oracle Management System
-- Hỗ trợ chạy trên PDB (ví dụ: FREEPDB1) hoặc bản Oracle 21c trở lên.

-- Run this script in PDB service XEPDB1. Do not enable _ORACLE_SCRIPT here;
-- local PDB users and roles do not require C## prefixes.

-- Xóa dữ liệu cũ nếu có (Tránh lỗi tạo lại)
BEGIN
   EXECUTE IMMEDIATE 'DROP TABLE NHANVIEN_DATA CASCADE CONSTRAINTS';
   EXECUTE IMMEDIATE 'DROP TABLE DUAN_DATA CASCADE CONSTRAINTS';
   EXECUTE IMMEDIATE 'DROP ROLE ROLE_NHANVIEN';
   EXECUTE IMMEDIATE 'DROP ROLE ROLE_QUANLY';
   EXECUTE IMMEDIATE 'DROP ROLE ROLE_GIAMDOC';
   EXECUTE IMMEDIATE 'DROP USER NV01 CASCADE';
   EXECUTE IMMEDIATE 'DROP USER NV02 CASCADE';
   EXECUTE IMMEDIATE 'DROP USER TP01 CASCADE';
   EXECUTE IMMEDIATE 'DROP USER GD01 CASCADE';
EXCEPTION
   WHEN OTHERS THEN
      IF SQLCODE != -942 AND SQLCODE != -1918 AND SQLCODE != -1919 AND SQLCODE != -1920 THEN
         RAISE;
      END IF;
END;
/

-- ==========================================
-- 1. Tạo một số Bảng (Table) mẫu để kiểm tra chức năng Phân Quyền
-- ==========================================
CREATE TABLE NHANVIEN_DATA (
    MANV VARCHAR2(50) PRIMARY KEY,
    TENNV VARCHAR2(100),
    LUONG NUMBER
);

CREATE TABLE DUAN_DATA (
    MADA VARCHAR2(50) PRIMARY KEY,
    TENDA VARCHAR2(100),
    NGANSACH NUMBER
);

-- Chèn một số dòng dữ liệu mẫu
INSERT INTO NHANVIEN_DATA VALUES ('NV01', 'Nguyen Van A', 5000);
INSERT INTO NHANVIEN_DATA VALUES ('NV02', 'Tran Thi B', 4500);
INSERT INTO DUAN_DATA VALUES ('DA01', 'Du an Bao Mat Oracle', 100000);
COMMIT;


-- ==========================================
-- 2. Tạo một số Role mẫu
-- ==========================================
CREATE ROLE ROLE_NHANVIEN;
CREATE ROLE ROLE_QUANLY;
CREATE ROLE ROLE_GIAMDOC;

-- (Cấp sẵn vài quyền cơ bản cho Role để test chức năng "Xem Quyền")
GRANT SELECT ON NHANVIEN_DATA TO ROLE_NHANVIEN;
GRANT SELECT, INSERT, UPDATE, DELETE ON DUAN_DATA TO ROLE_QUANLY;
-- Cấp quyền lên một cột cụ thể để test (Tab 2: Column-level Privileges)
GRANT UPDATE (LUONG) ON NHANVIEN_DATA TO ROLE_QUANLY;


-- ==========================================
-- 3. Tạo một số User mẫu
-- ==========================================
CREATE USER NV01 IDENTIFIED BY password123;
CREATE USER NV02 IDENTIFIED BY password123;
CREATE USER TP01 IDENTIFIED BY password123;
CREATE USER GD01 IDENTIFIED BY password123;

-- Cấp quyền đăng nhập chung (để user test login cũng được)
GRANT CREATE SESSION TO NV01, NV02, TP01, GD01;

-- Cấp Role tương ứng cho User (Test Tab 3: User-Role)
GRANT ROLE_NHANVIEN TO NV01;
GRANT ROLE_NHANVIEN TO NV02;
GRANT ROLE_QUANLY TO TP01;
GRANT ROLE_GIAMDOC TO GD01 WITH ADMIN OPTION;

-- Hoàn tất
PROMPT ==============================================
PROMPT DA TAO DU KIEU MAU THI NGIEM THANH CONG!
PROMPT User: NV01, NV02, TP01, GD01 (Password chung: password123)
PROMPT ==============================================
