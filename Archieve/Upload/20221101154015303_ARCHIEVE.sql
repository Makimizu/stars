jajal','t.txt','system',getdate(),'system',getdate(),null) 

update a set
THEDATE = convert(date, convert(varchar(4),YEAR(a.THEDATE)) + '-' + convert(varchar(4),MONTH(a.THEDATE)) + '-' + convert(varchar(4),DAY(b.START_DATE)-1))
from		LIFE.dbo.APPLICATION_BENEFIT_CYCLE a
inner join	LIFE.dbo.APPLICATION_MASTER b on a.REGNO = b.REGNO
where
a.REGNO in ('HC01744','HC01745')


(select 'tes