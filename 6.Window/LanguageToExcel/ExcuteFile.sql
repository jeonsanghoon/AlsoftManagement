
/*usms 언어변환*/
select  FileUrl
        ,'<Code CodeId="' + RIGHT('0000' + convert(varchar(4), code) ,4) + '">' + char(13)
        +'     <en>'+ en + '<abbr>' + en + '</abbr></en>' + char(13)
		+'     <ko>'+ Ko + '<abbr>' + Ko + '</abbr></ko>' + char(13)
		+'     <ja>'+ ja + '<abbr>' + ja + '</abbr></ja>' + char(13)
		+'     <zh>'+ zh + '<abbr>' + zh + '</abbr></zh>' + char(13)
		+'     <th>'+ th + '<abbr>' + th + '</abbr></th>' + char(13)
		+'</Code>'
from dbo.XLS_USMS_LANGUAGE



/*UPCloud*/
select  FileUrl
        ,'<data name="' + RIGHT('0000' + convert(varchar(4), code) ,4) + '"" xml:space="preserve">' + char(13)
        +'     <value>'+ en + '</value>' + char(13)
		+'</data>' 
from dbo.XLS_USMS_LANGUAGE
select * from [dbo].[XLS_USMS_LANGUAGE]
