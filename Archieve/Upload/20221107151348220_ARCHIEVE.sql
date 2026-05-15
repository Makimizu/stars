jajal','t.txt','system',getdate(),'system',getdate(),null) 

update b set
THEDATE			= dateadd(day, -1, convert(date, convert(varchar(4), YEAR(b.THEDATE)) + (case	when RIGHT(convert(varchar(20), a.START_DATE, 112), 4) = '0229' then '0228' else RIGHT(convert(varchar(20), a.START_DATE, 112), 4) end)))
from			LIFE.dbo.APPLICATION_MASTER a
inner join		LIFE.dbo.APPLICATION_BENEFIT_CYCLE b on a.REGNO = b.REGNO
where
a.REGNO			in ('DC59291','DC59290','DC59348')


(select 'tes