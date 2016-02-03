 
/********************************
  call : dbo.fn_SplitString('1,2,3,4,5',',',1) year_car
 ********************************/

 ALTER FUNCTION [dbo].[fn_SplitString] 
(
	@string NVARCHAR(MAX), 
    @delimiter CHAR(1) ,
    @index int

)
RETURNS VARCHAR(MAX)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @returnResult as varchar(max)

    DECLARE @start INT, @end INT 
    SELECT @start = 1, @end = CHARINDEX(@delimiter, @string) 

	declare @val int set @val = 0
    WHILE @start < LEN(@string) + 1 BEGIN  
        IF @end = 0  
            SET @end = LEN(@string) + 1   

		if(@index = @val)
			set	@returnResult =  SUBSTRING(@string, @start, @end - @start)  
		  
        SET @start = @end + 1 
        SET @end = CHARINDEX(@delimiter, @string, @start)
        set @val = @val +1 
        
    END 

	-- Return the result of the function
	RETURN @returnResult

END