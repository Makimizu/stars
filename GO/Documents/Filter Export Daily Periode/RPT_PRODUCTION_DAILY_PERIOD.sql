USE [GLIFE]
GO

/****** Object:  StoredProcedure [dbo].[RPT_PRODUCTION_DAILY_PERIOD]    Script Date: 16-12-2021 09:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
CREATE procedure [dbo].[RPT_PRODUCTION_DAILY_PERIOD]  
  @start_date date,
  @end_date date,
  --@policy_no varchar(50),
  --@lembaga varchar(200),
  @status varchar(300)
AS  
  

  if(@status = '') set @status = null

	

select aa.*  
	, bb.DOCNO NOMOR_SERTIFIKAT,   
	cc.REMARK PENDING_REMARK_DOCUMENT,   
	cd.REMARK PENDING_REMARK_FACULTATIVE,  
	dd.USER_ENDDATE TGL_INFORCE, dd.USER_ENDBY USER_INFORCE  
	, ee.FULLNAME NAMA_PASANGAN, ee.DOB DOB_PASANGAN  
	, dbo.UFN_AGE_CALC(ee.DOB, aa.START_DATE, convert(int, isnull(at.VAL, '1'))) START_AGE_SPOUSE  
	,ar.REMARK REMARK_UW  
	--,hh.POST_DATE TGL_BAYAR, gg.USERDATE TGL_SETTLE  
	,REM.EM RATE_EM  
	,REP.EP RATE_EP  
from (
	SELECT   
		a.REGNO,  
		CONVERT(DATE,b.DOB) AS DOB,  
		b.START_AGE,  
		a.USERDATE TGL_INPUT,  
		--a.USERBY USER_INPUT,  
		mi.CREATEBY USER_INPUT,  
		G.USER_STARTDATE TGL_VERIF,   
		G.USER_STARTBY USER_VERIF,  
		b.START_DATE,  
		b.END_DATE,  
		datediff(month, b.START_DATE, b.END_DATE)/12 TENOR_TAHUN,  
		datediff(month, b.START_DATE, b.END_DATE)%12 TENOR_BULAN,  
		b.FULLNAME,  
		b.SUMINS,  
		b.RATE,  
		b.PREMIUM,  
		b.NETT_PREMIUM,  
		b.POLICY_NO,  
		b.COMPANY_NAME,  
		d.NAMA_CABANG,  
		--max(e.seq) seq,  
		case when max(e.seq) = '1' then 'VERIFICATION'  
		when max(e.seq) = '2' then 'APPROVAL'  
		when max(e.seq) = '3' then 'REJECTED'  
		when max(e.seq) = '4' then 'INFORCE'  
		when max(e.seq) = '5' then 'CANCELLED'  
		else 'LAPSED' end STATUS_UW,  
		b.AGING,  
		f.PENDINGDATE PENDINGDATE_DOC,  
		ff.PENDINGDATE PENDINGDATE_FAC,  
		b.AGENT_NAME  
		,b.UW_CODE  
		,a.TC_ID  
		,b.PRODUCT_GROUP  
	FROM APPLICATION_MASTER a  
	inner join APPLICATION_MAIN_INFO mi on a.REGNO = mi.REGNO  
	inner join V_APPLICATION_MASTER b on a.REGNO = b.REGNO  
	inner join POLICY c on a.POLICY_ID = c.ID  
	left join CLIENT_BASE.dbo.BRANCH d on c.COMPANY_CODE = d.COMPANY_CODE and a.BRANCH_CODE = d.BRANCH_CODE  
	inner join TRACK_DATA e with(INDEX(IDXTRACK_DATA_001)) on a.REGNO = e.OWNER collate SQL_Latin1_General_CP1_CI_AS  
	left join APPLICATION_MASTER_PENDING f on a.REGNO = f.REGNO  and f.PENDING_CODE = '001'  
	left join APPLICATION_MASTER_PENDING ff on a.REGNO = ff.REGNO  and ff.PENDING_CODE = '002'  
	LEFT JOIN (select OWNER, USER_STARTDATE=MIN(USER_STARTDATE), USER_STARTBY from TRACK_DATA where TIPE_CODE = 'UW' and SEQ = 1 group by OWNER, USER_STARTBY) G on G.OWNER = a.REGNO collate database_default  
	where  
	--a.USERBY <> 'MIGRASIATK2'  
	--a.USERBY not like '%MIGRASI%'  
	mi.CREATEBY not like '%MIGRASI%'  
	and e.TIPE_CODE = 'UW' 
	and e.seq in (
		SELECT Split.a.value('.', 'NVARCHAR(MAX)') DATA
		FROM
		(
			SELECT CAST('<X>'+REPLACE(@status, ',', '</X><X>')+'</X>' AS XML) AS String
		) AS A
		CROSS APPLY String.nodes('/X') AS Split(a)
	)
	and e.user_startdate between @start_date and @end_date
	and c.PRODUCT_GROUP in ('CL','GTL', 'GTLR')  
	--and b.policy_no = isnull(@policy_no, b.POLICY_NO)
	--and b.END_DATE >= getdate()  
	group by   
		a.REGNO,  
		b.DOB,  
		b.START_AGE,  
		a.USERDATE,  
		--a.USERBY,  
		mi.CREATEBY,  
		G.USER_STARTDATE,  
		G.USER_STARTBY,  
		b.START_DATE,  
		b.END_DATE,  
		b.FULLNAME,  
		b.SUMINS,  
		b.RATE,  
		b.PREMIUM,  
		b.NETT_PREMIUM,  
		b.POLICY_NO,  
		b.COMPANY_NAME,  
		d.NAMA_CABANG,  
		b.AGING,  
		f.PENDINGDATE,  
		ff.PENDINGDATE,  
		b.AGENT_NAME  
		,b.UW_CODE  
		,a.TC_ID  
		,b.PRODUCT_GROUP  
) aa   
left join (SELECT * FROM APPLICATION_DOCUMENT_LETTER where CODE = '002') bb on aa.REGNO = bb.REGNO  
left join APPLICATION_MASTER_PENDING cc on aa.REGNO = cc.REGNO and cc.PENDING_CODE = '001' --and cc.PENDINGDATE = aa.PENDINGDATE  
left join APPLICATION_MASTER_PENDING cd on aa.REGNO = cd.REGNO and cd.PENDING_CODE = '002' --and cd.PENDINGDATE = aa.PENDINGDATE  
--INNER join PARAM_PENDING_TYPE ff on cc.PENDING_CODE = ff.CODE  
left join TRACK_DATA dd on aa.REGNO = dd.OWNER collate SQL_Latin1_General_CP1_CI_AS and dd.SEQ = '4'  
left join APPLICATION_JOIN_ACCOUNT ee on aa.REGNO = ee.REGNO  
left join V_LINK_UB_TC_ITEMS at on aa.TC_ID = at.TC_CODE and at.TC_ITEM = '13'  
left join APPLICATION_REMARK ar on aa.REGNO = ar.REGNO AND ar.SEQ = (select max(seq) from APPLICATION_REMARK where REGNO = aa.REGNO)  
left join (SELECT 100 * isnull(a.AMOUNT,0) / isnull(b.SUMINS * b.RATE /1000,0) EM, a.REGNO FROM APPLICATION_PREMIUM_COMPONENT a join APPLICATION_BENEFIT b on a.REGNO = b.REGNO WHERE a.CODE = 'EM') REM ON REM.REGNO = aa.REGNO  
left join (SELECT (1 - isnull(0.55,0)) * 1000 * isnull(AMOUNT,0) / b.SUMINS EP, a.REGNO FROM APPLICATION_PREMIUM_COMPONENT a join APPLICATION_MAIN_INFO b on a.REGNO = b.REGNO WHERE CODE = 'EP') REP ON REP.REGNO = aa.REGNO  
--left join FINANCE.DBO.INVOICE_DETAIL ff ON aa.REGNO = ff.DOC_NO  
----left join FINANCE.DBO.REKENING_JURNAL_DEBET gg on gg.INVOICENO = ff.INVOICENO and replace(gg.ROWID,' ','') = ff.INVOICENO+'-'+ff.DOC_NO collate SQL_Latin1_General_CP1_CI_AS --kondisi untuk INVOICE BATCH  
--left join FINANCE.DBO.REKENING_JURNAL_DEBET gg on gg.INVOICENO = ff.INVOICENO  --kondisi untuk INVOICE SATUAN  
--left join FINANCE.DBO.REKENING_JURNAL hh on hh.TRXID = gg.TRXID  
--where aa.REGNO  = '20190904134317853'  
where STATUS_UW <> '99'  

  

GO


