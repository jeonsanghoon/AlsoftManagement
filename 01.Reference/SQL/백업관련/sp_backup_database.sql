
USE [master]
GO
/****** Object:  StoredProcedure [dbo].[sp_BackUP_Database]    Script Date: 2017-03-16 ���� 9:19:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************/
/* ��� ��ũ��Ʈ                      */
/* �ۼ��� : ������                    */
/* �ۼ��� : 2016-10-13                */
/* ����   : ��¥���� ����             */
/*          15�� �̻� ������ ���� ����*/
/**************************************/
/** ��� ��) EXEC sp_BackUP_Database  N'D:\database\',  N'ALTSOFT_ONLINESERVICE' **/
ALTER PROC [dbo].[sp_BackUP_Database] @DIR NVARCHAR(100),    @DATABASE_NAME NVARCHAR(100)
                        
AS
BEGIN 
	
	DECLARE @BACKUPNAME NVARCHAR(100) = @DATABASE_NAME + N'_backup_' +  convert(varchar(20), getdate(), 112) + N'.bak'

	
	/* ���� ���� ����  15�� ������ ������ ��*/
	DECLARE @NOW_DATE NVARCHAR(30) = CONVERT(VARCHAR(20), DATEADD(day, - 15, GETDATE()), 120);
	EXECUTE master.dbo.xp_delete_file 0, @DIR,N'bak', @NOW_DATE,1
	SET @DIR = @DIR + @BACKUPNAME
	BACKUP DATABASE @DATABASE_NAME TO DISK = @DIR WITH NOFORMAT
										, NOINIT, NAME = @BACKUPNAME
										, SKIP, NOREWIND, NOUNLOAD, STATS = 10

END

