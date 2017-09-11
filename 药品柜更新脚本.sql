ALTER TABLE dbo.tb_DrugLock ADD
	pic nvarchar(256) NULL
ALTER TABLE dbo.tb_DrugLock ADD
	ischem int NULL
	ALTER TABLE dbo.tb_DrugLock ADD
	isdanger int NULL
	ALTER TABLE dbo.tb_DrugLock ADD
	locktypeid int NULL


update  dbo.tb_DrugLock set locktypeid=2
 where lockType='²£Á§¹ñ3_4'
 
 update  dbo.tb_DrugLock set locktypeid=4
 where lockType='²»Í¸Ã÷¹ñ3_4'
 
 update  dbo.tb_DrugLock set locktypeid=3
 where lockType='²»Í¸Ã÷¹ñ2_4'
 
 
 update  dbo.tb_DrugLock set pic=a.lockType+a.mark+'.png'
from
dbo.tb_DrugLock a


