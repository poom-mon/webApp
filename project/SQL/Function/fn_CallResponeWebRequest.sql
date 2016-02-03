 
/********************************
  call : dbo.Encoding('decrypt','1234556') year_car
  (A) : parameter type
  (B) : Parameter
 ********************************/

 ALTER FUNCTION [dbo].[Encoding] (@process varchar(50), @data VARCHAR(MAX))
RETURNS VARCHAR(MAX)
AS  
BEGIN

-- @process : decrypt
--			: encrypt 

--ถ้า Sql server  ส่วน configure ยังไม่ถูกเปิดใช้งาน ให้ run command นี้ก่อน----------------
--exec sp_configure 'show advanced options', 1
--go
--reconfigure
--go
--exec sp_configure 'Ole Automation Procedures', 1 -- Enable
---- exec sp_configure 'Ole Automation Procedures', 0 -- Disable
--go
--reconfigure
--go
--exec sp_configure 'show advanced options', 0
--go
--reconfigure
--go
--end------------------------------------------------------------------------
	Declare @Object as Int
	Declare @ResponseText as Varchar(8000)
	declare @LinkWeb varchar(400)
	set @LinkWeb = 'http://www.xxx.com?process='+ @process +'&data=' + REPLACE(@data,'+','%2B')

	Exec sp_OACreate 'MSXML2.XMLHTTP', @Object OUT
	Exec sp_OAMethod @Object, 'open', NULL, 'get', @LinkWeb , 'false'
	Exec sp_OAMethod @Object, 'send'
	Exec sp_OAMethod @Object, 'responseText', @ResponseText OUTPUT
	 
	RETURN @ResponseText
END

