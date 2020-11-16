
select device_code, count(1) cnt from T_AD_DEVICE
group by device_code
order by cnt desc

select * from T_MEMBER


begin tran
INSERT INTO T_AD_DEVICE

SELECT 
   A.AD_CODE
, 40 DEVICE_CODE
, A.FR_DATE
, A.TO_DATE
, A.FR_TIME
, A.TO_TIME
, A.FR_UTC_DATE
, A.TO_UTC_DATE
, A.FR_UTC_TIME
, A.TO_UTC_TIME
, A.CLICK_CNT
, A.HIDE
, A.REMARK
, 1
, GETDATE()
,1
, GETDATE()

 FROM
( 
select * from T_AD_DEVICE
where DEVICE_CODE = 32) A
LEFT JOIN 
 (
select B.* from T_DEVICE A
INNER JOIN T_AD_DEVICE B
   ON A.DEVICE_CODE = B.DEVICE_CODE
WHERE A.DEVICE_CODE = 40
) B
ON  A.AD_CODE = B.AD_CODE
where b.AD_DEVICE_CODE IS NULL
commit tran