 
/********************************
  call : dbo.fnc_GetQueryStringUrl(page_name, 'year') year_car
 ********************************/

ALTER FUNCTION [dbo].[fnc_GetQueryStringUrl] 
( 
	@Allurl varchar(max),
	@valueSplit varchar(100) 

)
RETURNS VARCHAR(MAX)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @returnResult as varchar(max)

	-- Add the T-SQL statements to compute the return value here
	 
	 /*	declare @url varchar(1000)  set @url= 'http://www.silkspan.com/v2/insurance/default.aspx?typedealer=th.hao123&bname=hoticon&dfdfdf=122'  */
		
		  
		DECLARE @xml xml , @delimiter varchar(10) ,@url varchar(max)
		set @url = substring( @Allurl, charindex('?',@Allurl,1 )+1 ,len(@Allurl))
		SET @delimiter = '&'
		SET @xml = cast(('<X>'+replace(@url, @delimiter, '</X><X>')+'</X>') as xml) 

		SELECT   
		 @returnResult = substring(
			C.value('.', 'varchar(1000)')
			,    charindex('=',C.value('.', 'varchar(1000)'),1 )+1 
			,len(C.value('.', 'varchar(1000)'))
		) 
		FROM @xml.nodes('X') as X(C) 
		where   C.value('.', 'varchar(1000)') like '%'+@valueSplit+'%'
		  

		set @returnResult  = case when  charindex('#',@returnResult) >  0 then   substring(@returnResult,0, charindex('#',@returnResult)) else @returnResult   end


	-- Return the result of the function
	RETURN @returnResult

END

