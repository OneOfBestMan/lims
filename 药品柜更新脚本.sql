ALTER TABLE dbo.tb_DrugLock ADD
	pic nvarchar(256) NULL
ALTER TABLE dbo.tb_DrugLock ADD
	ischem int NULL
	ALTER TABLE dbo.tb_DrugLock ADD
	isdanger int NULL
	ALTER TABLE dbo.tb_DrugLock ADD
	locktypeid int NULL


update  dbo.tb_DrugLock set locktypeid=2
 where lockType='������3_4'
 
 update  dbo.tb_DrugLock set locktypeid=4
 where lockType='��͸����3_4'
 
 update  dbo.tb_DrugLock set locktypeid=3
 where lockType='��͸����2_4'
 
 
 update  dbo.tb_DrugLock set pic=a.lockType+a.mark+'.png'
from
dbo.tb_DrugLock a


update tb_DrugLock set isdanger=1 where mark='Σ��Ʒ'
update tb_DrugLock set ischem=1 where mark='��ѧƷ'
update tb_DrugLock set ischem=1,isdanger=1 where mark='����'
