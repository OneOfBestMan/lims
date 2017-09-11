ALTER TABLE dbo.tb_DrugLock ADD
	pic nvarchar(256) NULL
ALTER TABLE dbo.tb_DrugLock ADD
	ischem int NULL
	ALTER TABLE dbo.tb_DrugLock ADD
	isdanger int NULL
	ALTER TABLE dbo.tb_DrugLock ADD
	locktypeid int NULL


update  dbo.tb_DrugLock set locktypeid=2
 where lockType='玻璃柜3_4'
 
 update  dbo.tb_DrugLock set locktypeid=4
 where lockType='不透明柜3_4'
 
 update  dbo.tb_DrugLock set locktypeid=3
 where lockType='不透明柜2_4'
 
 
 update  dbo.tb_DrugLock set pic=a.lockType+a.mark+'.png'
from
dbo.tb_DrugLock a


update tb_DrugLock set isdanger=1 where mark='危险品'
update tb_DrugLock set ischem=1 where mark='化学品'
update tb_DrugLock set ischem=1,isdanger=1 where mark='都是'
