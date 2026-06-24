ATBM 2026 - CQ2026-18 - Database scripts

Run order for Oracle setup:

IMPORTANT: On Oracle 21c XE, connect to the PDB service XEPDB1, not the CDB root service XE.
Use: sqlplus system/Welcome1#@localhost:1521/XEPDB1
Running these scripts on localhost:1521/XE can raise ORA-65095 for Vietnamese object names.
Do not manually run ALTER SESSION SET "_ORACLE_SCRIPT"=true before setup. The reset script temporarily uses it only for cleanup, then turns it off.


0. Log in as SYSTEM for normal setup scripts unless a note says SYSDBA is required.

1. 01_schema.sql
   Creates schema objects for PH2: BENHNHAN, NHANVIEN, HSBA, HSBA_DV, DONTHUOC, THONGBAO.

2. 02_data.sql
   Inserts sample hospital data.

3. 03_accounts.sql
   Creates Oracle users and links them through ORAUSER.

4. 04_rbac.sql
   Creates RBAC roles, secure views, and grants role privileges.

5. 05_vpd.sql
   Creates VPD functions/policies and audit trigger table/logging.

6. 06_ols.sql
   Requires Oracle Label Security to be enabled first.
   As SYS AS SYSDBA, run:
     EXEC LBACSYS.CONFIGURE_OLS;
     EXEC LBACSYS.OLS_ENFORCEMENT.ENABLE_OLS;
     SHUTDOWN IMMEDIATE;
     STARTUP;
   Then reconnect as SYSTEM and run 06_ols.sql.

7. 07_audit.sql
   Configures Standard Audit, FGA, and Unified Audit examples.

8. 08_backup.sql
   Contains backup/restore implementation notes and PL/SQL scheduler procedures.

PH1 support scripts:

- 00_ph1_script_csdl.sql
- 00_ph1_sample_database.sql

These two PH1 files are identical sample-data scripts for the PH1 Oracle administration demo.
Run one of them only if you want the extra sample users NV01, NV02, TP01, GD01 and sample tables NHANVIEN_DATA, DUAN_DATA.

Default sample password used by PH2 accounts: Welcome1#
Important demo users: DPV001, DPV002, BS001, BS002, BS003, KTV001, KTV002, BN001..BN005.
