-- ==============================================================================
-- File: 08_backup.sql
-- Dự án: PhanHe2 - Hệ thống Quản lý Y tế Bệnh viện
-- Mô tả: Yêu cầu 4 - Sao lưu và Phục hồi dữ liệu
--        Flashback + Data Pump + LogMiner + RMAN + DBMS_SCHEDULER (tự động)
-- Chạy với quyền: SYSTEM trong XEPDB1 cho phần demo SQL/Data Pump/Scheduler.
-- LogMiner/RMAN là phần hướng dẫn chạy thủ công bằng SYSDBA ở CDB$ROOT/OS.
-- ==============================================================================
SET DEFINE OFF;
SET ECHO ON;
SET SERVEROUTPUT ON;


-- ==============================================================================
-- PHẦN 1: TÌM HIỂU CÁC PHƯƠNG PHÁP SAO LƯU VÀ PHỤC HỒI
-- ==============================================================================
/*
1. RMAN (Recovery Manager)
   Ưu điểm  : Công cụ chính thức và mạnh nhất của Oracle. Hỗ trợ full backup,
               incremental backup (chỉ backup các block thay đổi), nén và mã hoá.
               Phục hồi chính xác đến thời điểm (Point-in-Time Recovery).
   Khuyết điểm: Yêu cầu cấu hình Archive Log Mode, kiến thức DBA chuyên sâu.
               Phục hồi phức tạp hơn khi môi trường thay đổi.

2. Data Pump (EXPDP / IMPDP)
   Ưu điểm  : Trích xuất dữ liệu ở mức logic (Table, Schema, Database).
               Dễ chuyển dữ liệu giữa các môi trường, phiên bản Oracle khác nhau.
               Có thể chạy tự động bằng DBMS_SCHEDULER.
   Khuyết điểm: Không phục hồi được đến thời điểm chính xác (point-in-time).
               Tốn thời gian với CSDL lớn, không backup được ở mức vật lý.

3. Flashback Technology
   Ưu điểm  : Phục hồi cực nhanh lùi về thời điểm trong quá khứ mà không cần
               restore từ file backup (dùng UNDO data hoặc Flashback Log).
               Phù hợp phục hồi lỗi người dùng (lỡ UPDATE, DELETE sai dòng).
   Khuyết điểm: Bị giới hạn bởi undo_retention và dung lượng UNDO tablespace.
               Không dùng được khi mất datafile (physical failure).

4. LogMiner
   Ưu điểm  : Phân tích Redo Log / Archive Log để lấy lại SQL_UNDO của từng
               thao tác. Giúp xác định và hoàn tác lỗi nghiệp vụ chính xác.
   Khuyết điểm: Thao tác phức tạp, phù hợp phục hồi có chọn lọc, không phù hợp
               phục hồi quy mô lớn.

KẾT LUẬN:
   - Sao lưu định kỳ        : RMAN (full hàng tuần + incremental hàng ngày)
   - Sao lưu tự động logic  : Data Pump + DBMS_SCHEDULER (hàng ngày)
   - Phục hồi lỗi người dùng: Flashback Table / Flashback Query (nhanh nhất)
   - Phân tích lỗi chi tiết : LogMiner (khi cần xác định chính xác câu SQL gây lỗi)
*/


-- ==============================================================================
-- PHẦN 2: CẤU HÌNH NỀN TẢNG (chạy với SYSDBA một lần)
-- ==============================================================================

-- Kiểm tra trạng thái Archive Log và Flashback
SELECT LOG_MODE, FLASHBACK_ON FROM V$DATABASE;

-- Bật Archive Log Mode (nếu chưa bật):
--   SHUTDOWN IMMEDIATE;
--   STARTUP MOUNT;
--   ALTER DATABASE ARCHIVELOG;
--   ALTER DATABASE OPEN;

-- Bật Flashback Database (nếu chưa bật):
--   SHUTDOWN IMMEDIATE;
--   STARTUP MOUNT;
--   ALTER DATABASE FLASHBACK ON;
--   ALTER DATABASE OPEN;

-- Bật Supplemental Logging (cần cho LogMiner ghi đầy đủ thông tin).
-- Trên Oracle CDB/PDB, lệnh này phải chạy ở CDB$ROOT với SYSDBA.
-- Khi script đang chạy trong XEPDB1, chỉ ghi chú và bỏ qua để không làm hỏng quy trình demo.
DECLARE
    v_con_name VARCHAR2(128);
BEGIN
    v_con_name := SYS_CONTEXT('USERENV', 'CON_NAME');
    IF v_con_name = 'CDB$ROOT' THEN
        EXECUTE IMMEDIATE 'ALTER DATABASE ADD SUPPLEMENTAL LOG DATA';
        DBMS_OUTPUT.PUT_LINE('[OK] Supplemental logging da bat trong CDB$ROOT.');
    ELSE
        DBMS_OUTPUT.PUT_LINE('[INFO] Dang o ' || v_con_name || '; bo qua ALTER DATABASE ADD SUPPLEMENTAL LOG DATA.');
        DBMS_OUTPUT.PUT_LINE('[INFO] Neu can LogMiner that, chay lenh nay bang SYSDBA tai CDB$ROOT.');
    END IF;
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('[INFO] Khong the bat supplemental logging tu session hien tai: ' || SQLERRM);
END;
/

-- Cho phép Flashback trên các bảng quan trọng (cần ROW MOVEMENT)
ALTER TABLE SYSTEM."BỆNHNHÂN" ENABLE ROW MOVEMENT;
ALTER TABLE SYSTEM."HSBA"     ENABLE ROW MOVEMENT;
ALTER TABLE SYSTEM."ĐƠNTHUỐC" ENABLE ROW MOVEMENT;
ALTER TABLE SYSTEM."HSBA_DV"  ENABLE ROW MOVEMENT;

PROMPT >> [OK] Cau hinh nen tang hoan tat.


-- ==============================================================================
-- PHẦN 3: FLASHBACK - PHỤC HỒI NHANH LỖI NGƯỜI DÙNG
-- ==============================================================================

-- 3A. Flashback Query: Xem dữ liệu tại một thời điểm (không thay đổi dữ liệu thật).
-- Sau khi reset/create lại schema, không truy vấn lùi 30 phút trực tiếp vì bảng vừa đổi DDL
-- có thể gây ORA-01466. Khi demo lỗi người dùng thật, đổi SYSTIMESTAMP thành:
--   SYSTIMESTAMP - INTERVAL '30' MINUTE
SELECT "MÃHSBA", "CHẨNĐOÁN", "ĐIỀUTRỊ", "KẾTLUẬN"
FROM SYSTEM."HSBA"
AS OF TIMESTAMP SYSTIMESTAMP
ORDER BY "MÃHSBA";

-- 3B. Kiểm tra dữ liệu ĐƠNTHUỐC bằng Flashback Query.
-- Khi cần demo phục hồi thật, có thể đổi SYSTIMESTAMP thành:
--   SYSTIMESTAMP - INTERVAL '60' MINUTE
SELECT "MÃHSBA", "TÊNTHUỐC", "LIỀUDÙNG"
FROM SYSTEM."ĐƠNTHUỐC"
AS OF TIMESTAMP SYSTIMESTAMP
ORDER BY "MÃHSBA";

-- 3C. Flashback Table: Khôi phục bảng về thời điểm cụ thể
-- Kịch bản: Bác sĩ lỡ UPDATE sai hàng loạt CHẨNĐOÁN → khôi phục bảng HSBA
-- Bước 1: Xem thời điểm trước khi xảy ra lỗi (thay đổi timestamp phù hợp)
-- SELECT * FROM SYSTEM."HSBA" AS OF TIMESTAMP TO_TIMESTAMP('2025-01-15 10:00:00','YYYY-MM-DD HH24:MI:SS');

-- Bước 2: Thực hiện flashback (thay timestamp phù hợp)
-- FLASHBACK TABLE SYSTEM."HSBA" TO TIMESTAMP (SYSTIMESTAMP - INTERVAL '30' MINUTE);

/* ============================================================================
   3D. Demo: Tạo tình huống lỗi và phục hồi bằng Flashback
   CẢNH BÁO: Đoạn này CHỈ CHẠY THỦ CÔNG từng bước trong môi trường demo.
   KHÔNG chạy toàn bộ trong cùng một lần — UPDATE sẽ xóa dữ liệu thật!
   ============================================================================

-- Bước 1: Lưu timestamp trước khi thao tác
CREATE TABLE SYSTEM.FLASHBACK_DEMO_LOG (
    DEMO_TIME TIMESTAMP DEFAULT SYSTIMESTAMP,
    NOTE      VARCHAR2(200)
);
INSERT INTO SYSTEM.FLASHBACK_DEMO_LOG (NOTE)
VALUES ('Truoc khi gia lap loi - HSBA');
COMMIT;

-- Bước 2: Giả lập lỗi (chạy riêng, có chủ đích)
UPDATE SYSTEM."HSBA" SET "CHẨNĐOÁN" = N'[LỖI] Dữ liệu bị ghi đè nhầm';
COMMIT;

-- Bước 3: Kiểm tra dữ liệu bị lỗi
SELECT "MÃHSBA", "CHẨNĐOÁN" FROM SYSTEM."HSBA";

-- Bước 4: Phục hồi bằng Flashback Table về 5 phút trước
FLASHBACK TABLE SYSTEM."HSBA"
TO TIMESTAMP (SYSTIMESTAMP - INTERVAL '5' MINUTE);

-- Bước 5: Xác nhận đã phục hồi
SELECT "MÃHSBA", "CHẨNĐOÁN" FROM SYSTEM."HSBA";

-- Bước 6: Dọn dẹp bảng demo
DROP TABLE SYSTEM.FLASHBACK_DEMO_LOG;
============================================================================ */


-- ==============================================================================
-- PHẦN 4: DATA PUMP - SAO LƯU LOGIC (CHỦ ĐỘNG)
-- ==============================================================================
-- Data Pump chạy bằng lệnh OS (expdp/impdp), không phải SQL.
-- Tạo thư mục Oracle Directory để lưu file dump.

-- Script ưu tiên dùng đường dẫn thật của DATA_PUMP_DIR do Oracle XE tạo sẵn,
-- rồi ánh xạ lại thành BACKUP_DIR để các câu lệnh bên dưới thống nhất tên.
DECLARE
    v_path VARCHAR2(4000);
    v_user VARCHAR2(128);
BEGIN
    SELECT directory_path
    INTO v_path
    FROM all_directories
    WHERE directory_name = 'DATA_PUMP_DIR'
    FETCH FIRST 1 ROW ONLY;

    EXECUTE IMMEDIATE
        'CREATE OR REPLACE DIRECTORY BACKUP_DIR AS ''' || REPLACE(v_path, '''', '''''') || '''';

    v_user := SYS_CONTEXT('USERENV', 'SESSION_USER');
    IF v_user <> 'SYSTEM' THEN
        EXECUTE IMMEDIATE 'GRANT READ, WRITE ON DIRECTORY BACKUP_DIR TO SYSTEM';
    END IF;

    DBMS_OUTPUT.PUT_LINE('[OK] BACKUP_DIR tro den: ' || v_path);
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('[INFO] Khong tim thay DATA_PUMP_DIR. Tao OS folder truoc roi sua BACKUP_DIR cho phu hop.');
        RAISE;
END;
/

PROMPT >> [OK] Oracle Directory BACKUP_DIR da tao.

/*
-- Chạy Export toàn bộ schema SYSTEM (lệnh OS, không phải SQL):
--   expdp 'system/Welcome1#@localhost:1521/XEPDB1' \
--         schemas=SYSTEM \
--         directory=BACKUP_DIR \
--         dumpfile=phanhe2_full_%DATE%.dmp \
--         logfile=phanhe2_export_%DATE%.log \
--         compression=ALL

-- Chạy Import để phục hồi:
--   impdp 'system/Welcome1#@localhost:1521/XEPDB1' \
--         schemas=SYSTEM \
--         directory=BACKUP_DIR \
--         dumpfile=phanhe2_full_20250115.dmp \
--         logfile=phanhe2_import.log \
--         table_exists_action=REPLACE
*/


-- ==============================================================================
-- PHẦN 5: LOGMINER - PHÂN TÍCH LOG VÀ PHỤC HỒI CÓ CHỌN LỌC
-- ==============================================================================

PROMPT >> [INFO] LogMiner requires SYSDBA in CDB$ROOT, so executable demo is documented below.

/*
-- Run manually as SYSDBA in CDB$ROOT, not in XEPDB1:
--   sqlplus / as sysdba

-- 1. Find the real redo log path on this machine:
SELECT MEMBER FROM V$LOGFILE;

-- 2. Start LogMiner with one real redo log file:
BEGIN
    BEGIN DBMS_LOGMNR.END_LOGMNR; EXCEPTION WHEN OTHERS THEN NULL; END;

    DBMS_LOGMNR.ADD_LOGFILE(
        LogFileName => 'C:\APP\ADMIN\PRODUCT\21C\ORADATA\XE\REDO01.LOG',
        Options     => DBMS_LOGMNR.NEW
    );

    DBMS_LOGMNR.START_LOGMNR(
        Options => DBMS_LOGMNR.DICT_FROM_ONLINE_CATALOG +
                   DBMS_LOGMNR.COMMITTED_DATA_ONLY
    );
END;
/

-- 3. Read recent business-table changes and their undo SQL:
SELECT
    TO_CHAR(TIMESTAMP, 'DD/MM/YYYY HH24:MI:SS') AS "Thoi gian",
    SEG_OWNER     AS "Schema",
    TABLE_NAME    AS "Bang",
    OPERATION     AS "Thao tac",
    SQL_REDO      AS "SQL thuc hien",
    SQL_UNDO      AS "SQL hoan tac"
FROM V$LOGMNR_CONTENTS
WHERE SEG_OWNER   = 'SYSTEM'
  AND TABLE_NAME  IN ('HSBA', 'ĐƠNTHUỐC', 'BỆNHNHÂN', 'HSBA_DV')
  AND OPERATION   IN ('INSERT','UPDATE','DELETE')
ORDER BY TIMESTAMP DESC
FETCH FIRST 30 ROWS ONLY;

BEGIN
    DBMS_LOGMNR.END_LOGMNR;
END;
/
*/


-- ==============================================================================
-- PHẦN 6: SAO LƯU TỰ ĐỘNG - DBMS_SCHEDULER + DATA PUMP
-- ==============================================================================
-- Tạo job tự động export schema SYSTEM mỗi ngày lúc 02:00 sáng

-- Bước 1: Tạo stored procedure thực hiện backup
CREATE OR REPLACE PROCEDURE SP_AUTO_BACKUP AS
    v_filename  VARCHAR2(200);
    v_logfile   VARCHAR2(200);
    v_datestamp VARCHAR2(20);
BEGIN
    v_datestamp := TO_CHAR(SYSDATE, 'YYYYMMDD_HH24MI');
    v_filename  := 'phanhe2_auto_' || v_datestamp || '.dmp';
    v_logfile   := 'phanhe2_auto_' || v_datestamp || '.log';

    -- Ghi log bắt đầu backup
    DBMS_OUTPUT.PUT_LINE('[' || TO_CHAR(SYSDATE,'DD/MM/YYYY HH24:MI:SS') || '] Bat dau backup: ' || v_filename);

    -- Chạy Data Pump Export qua DBMS_DATAPUMP
    DECLARE
        v_job_handle NUMBER;
    BEGIN
        -- Mở job export
        v_job_handle := DBMS_DATAPUMP.OPEN(
            operation   => 'EXPORT',
            job_mode    => 'SCHEMA',
            job_name    => 'AUTO_BACKUP_' || v_datestamp,
            version     => 'COMPATIBLE'
        );

        -- Chỉ định file dump và log
        DBMS_DATAPUMP.ADD_FILE(
            handle    => v_job_handle,
            filename  => v_filename,
            directory => 'BACKUP_DIR',
            filetype  => DBMS_DATAPUMP.KU$_FILE_TYPE_DUMP_FILE
        );
        DBMS_DATAPUMP.ADD_FILE(
            handle    => v_job_handle,
            filename  => v_logfile,
            directory => 'BACKUP_DIR',
            filetype  => DBMS_DATAPUMP.KU$_FILE_TYPE_LOG_FILE
        );

        -- Chỉ định schema cần backup
        DBMS_DATAPUMP.METADATA_FILTER(
            handle     => v_job_handle,
            name       => 'SCHEMA_EXPR',
            value      => 'IN (''SYSTEM'')'
        );

        -- Bắt đầu chạy và đợi hoàn tất
        DECLARE
            v_job_state VARCHAR2(30);
        BEGIN
            DBMS_DATAPUMP.START_JOB(v_job_handle);
            DBMS_DATAPUMP.WAIT_FOR_JOB(v_job_handle, v_job_state);
            IF v_job_state != 'COMPLETED' THEN
                RAISE_APPLICATION_ERROR(-20001, 'Backup ket thuc voi trang thai: ' || v_job_state);
            END IF;
        END;
        DBMS_DATAPUMP.DETACH(v_job_handle);

        DBMS_OUTPUT.PUT_LINE('[OK] Backup hoan tat: ' || v_filename);
    EXCEPTION
        WHEN OTHERS THEN
            BEGIN DBMS_DATAPUMP.STOP_JOB(v_job_handle, 1, 0); EXCEPTION WHEN OTHERS THEN NULL; END;
            DBMS_OUTPUT.PUT_LINE('[LOI] Backup that bai: ' || SQLERRM);
            RAISE;
    END;
END SP_AUTO_BACKUP;
/

PROMPT >> [OK] Procedure SP_AUTO_BACKUP da tao.

-- Bước 2: Xóa job cũ nếu tồn tại
BEGIN
    DBMS_SCHEDULER.DROP_JOB(job_name => 'JOB_DAILY_BACKUP', force => TRUE);
EXCEPTION
    WHEN OTHERS THEN NULL;
END;
/

-- Bước 3: Tạo job chạy mỗi ngày lúc 02:00 sáng
BEGIN
    DBMS_SCHEDULER.CREATE_JOB(
        job_name        => 'JOB_DAILY_BACKUP',
        job_type        => 'STORED_PROCEDURE',
        job_action      => 'SP_AUTO_BACKUP',
        start_date      => TRUNC(SYSDATE + 1) + INTERVAL '2' HOUR,  -- 02:00 ngày mai
        repeat_interval => 'FREQ=DAILY; BYHOUR=2; BYMINUTE=0; BYSECOND=0',
        end_date        => NULL,           -- Chạy mãi mãi
        enabled         => TRUE,
        auto_drop       => FALSE,
        comments        => 'Backup tu dong hang ngay luc 02:00 - PhanHe2 Y te'
    );
    DBMS_OUTPUT.PUT_LINE('[OK] Job JOB_DAILY_BACKUP da tao - chay moi ngay luc 02:00.');
END;
/

-- Bước 4: Tạo thêm job backup cuối tuần (full backup mỗi chủ nhật lúc 01:00)
BEGIN
    DBMS_SCHEDULER.DROP_JOB(job_name => 'JOB_WEEKLY_BACKUP', force => TRUE);
EXCEPTION
    WHEN OTHERS THEN NULL;
END;
/

BEGIN
    DBMS_SCHEDULER.CREATE_JOB(
        job_name        => 'JOB_WEEKLY_BACKUP',
        job_type        => 'STORED_PROCEDURE',
        job_action      => 'SP_AUTO_BACKUP',
        start_date      => TRUNC(SYSDATE + 1) + INTERVAL '1' HOUR,
        repeat_interval => 'FREQ=WEEKLY; BYDAY=SUN; BYHOUR=1; BYMINUTE=0; BYSECOND=0',
        end_date        => NULL,
        enabled         => TRUE,
        auto_drop       => FALSE,
        comments        => 'Backup toan bo cuoi tuan - Chu nhat 01:00 - PhanHe2 Y te'
    );
    DBMS_OUTPUT.PUT_LINE('[OK] Job JOB_WEEKLY_BACKUP da tao - chay moi Chu nhat luc 01:00.');
END;
/

-- Kiểm tra các job đã tạo
SELECT
    JOB_NAME            AS "Ten Job",
    JOB_TYPE            AS "Loai",
    STATE               AS "Trang thai",
    ENABLED             AS "Bat?",
    TO_CHAR(NEXT_RUN_DATE, 'DD/MM/YYYY HH24:MI:SS') AS "Lan chay tiep theo",
    REPEAT_INTERVAL     AS "Lich lap lai"
FROM DBA_SCHEDULER_JOBS
WHERE JOB_NAME IN ('JOB_DAILY_BACKUP', 'JOB_WEEKLY_BACKUP')
ORDER BY JOB_NAME;

PROMPT >> [OK] Sao luu tu dong da thiet lap (hang ngay 02:00 + Chu nhat 01:00).


-- ==============================================================================
-- PHẦN 7: RMAN - SAO LƯU VẬT LÝ (LỆNH OS)
-- ==============================================================================
/*
Chạy các lệnh RMAN sau trên Terminal của hệ điều hành:

-- Kết nối RMAN
  rman target /

-- Full backup hàng tuần (chủ nhật):
  RMAN> BACKUP AS COMPRESSED BACKUPSET DATABASE PLUS ARCHIVELOG
        FORMAT 'C:\oracle_backup\rman\full_%T_%U.bkp'
        TAG 'WEEKLY_FULL';

-- Incremental backup hàng ngày:
  RMAN> BACKUP INCREMENTAL LEVEL 1
        DATABASE FORMAT 'C:\oracle_backup\rman\incr_%T_%U.bkp'
        TAG 'DAILY_INCR';

-- Phục hồi khi mất toàn bộ database:
  RMAN> SHUTDOWN IMMEDIATE;
  RMAN> STARTUP MOUNT;
  RMAN> RESTORE DATABASE;
  RMAN> RECOVER DATABASE;
  RMAN> ALTER DATABASE OPEN RESETLOGS;

-- Phục hồi đến thời điểm cụ thể (Point-in-Time Recovery):
  RMAN> SHUTDOWN IMMEDIATE;
  RMAN> STARTUP MOUNT;
  RMAN> SET UNTIL TIME "TO_DATE('2025-01-15 10:00:00','YYYY-MM-DD HH24:MI:SS')";
  RMAN> RESTORE DATABASE;
  RMAN> RECOVER DATABASE;
  RMAN> ALTER DATABASE OPEN RESETLOGS;
*/


-- ==============================================================================
-- PHẦN 8: KIỂM TRA LỊCH SỬ SAO LƯU VÀ PHỤC HỒI
-- ==============================================================================

-- Xem lịch sử chạy các job tự động
SELECT
    JOB_NAME            AS "Ten Job",
    TO_CHAR(ACTUAL_START_DATE, 'DD/MM/YYYY HH24:MI:SS') AS "Thoi gian chay",
    STATUS              AS "Trang thai",
    RUN_DURATION        AS "Thoi gian chay"
FROM DBA_SCHEDULER_JOB_RUN_DETAILS
WHERE JOB_NAME IN ('JOB_DAILY_BACKUP', 'JOB_WEEKLY_BACKUP')
ORDER BY ACTUAL_START_DATE DESC
FETCH FIRST 20 ROWS ONLY;

-- Xem các Data Pump job còn được Oracle ghi nhận
SELECT
    OWNER_NAME          AS "Owner",
    JOB_NAME            AS "Job",
    OPERATION           AS "Operation",
    JOB_MODE            AS "Mode",
    STATE               AS "State",
    DEGREE              AS "Degree",
    ATTACHED_SESSIONS   AS "Attached sessions",
    DATAPUMP_SESSIONS   AS "Datapump sessions"
FROM DBA_DATAPUMP_JOBS
ORDER BY OWNER_NAME, JOB_NAME;

PROMPT >> 08_backup.sql hoan tat.
