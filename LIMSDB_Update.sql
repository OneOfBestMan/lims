--样品表更新
alter table [dbo].[tb_Sample] 
add 
storcondition nvarchar(256),
checkdepar nvarchar(36),
urgentlevel int,
AreaID int
go

alter table [dbo].[tb_Sample] 
alter column handleUser int
go

alter table [dbo].[tb_Sample] 
alter column InspectCompany int
go

--调整字段顺序********************************************************************

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'AreaID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'测试部门', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'checkdepar';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'createDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'createUser';
GO
EXEC sp_dropextendedproperty 'MS_Description',@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'tb_Sample', @level2type=N'COLUMN', @level2name=N'handleDate';
GO
EXEC sp_dropextendedproperty 'MS_Description',@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'tb_Sample', @level2type=N'COLUMN', @level2name=N'handleUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'销毁日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'handleDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'销毁人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'handleUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'批次', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'protNum';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'样品编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'sampleNum';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储条件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'storcondition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'updateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'updateUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'紧急程度（1：普通、2：紧急）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'urgentlevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'样品表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample';
GO


--项目表
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否农药残留', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Project', @level2type = N'COLUMN', @level2name = N'IsPesCheck';
GO

--修改视图:View_Drug
ALTER VIEW [dbo].[View_Drug]
AS
SELECT     dbo.tb_Drug.id, dbo.tb_Drug.drugName, dbo.tb_Drug.drugCode, dbo.tb_Drug.drugType, dbo.tb_Drug.unit, dbo.tb_Drug.productDate, dbo.tb_Drug.validDate, dbo.tb_Drug.amount, 
                      dbo.tb_Drug.manufacturers, dbo.tb_Drug.registrant, dbo.tb_Drug.riskLevel, dbo.tb_Drug.remark, dbo.tb_Drug.createUser, dbo.tb_Drug.createDate, dbo.tb_Drug.updateUser, dbo.tb_Drug.updateDate, 
                      dbo.tb_Drug.temp1, dbo.tb_Drug.temp2, dbo.tb_Base.baseName AS danwei, tb_Base_1.baseName AS leibie, tb_Base_2.baseName AS weixiandengji, tb_Base_3.baseName AS cabinet, 
                      dbo.tb_Drug.cabinet AS cabinetId, dbo.tb_Drug.isMSDS
FROM         dbo.tb_Drug INNER JOIN
                      dbo.tb_Base ON dbo.tb_Drug.unit = dbo.tb_Base.id LEFT OUTER JOIN
                      dbo.tb_Base AS tb_Base_1 ON dbo.tb_Drug.drugType = tb_Base_1.id LEFT OUTER JOIN
                      dbo.tb_Base AS tb_Base_2 ON dbo.tb_Drug.riskLevel = tb_Base_2.id LEFT OUTER JOIN
                      dbo.tb_Base AS tb_Base_3 ON dbo.tb_Drug.cabinet = tb_Base_3.id

GO

--检验报告
EXEC sp_dropextendedproperty 'MS_Description',@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'tb_TestReport', @level2type=N'COLUMN', @level2name=N'IssuedTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'批准日期（签发日期）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_TestReport', @level2type = N'COLUMN', @level2name = N'IssuedTime';
GO

--原始记录对应数据
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'原始记录文件名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_RecordSample', @level2type = N'COLUMN', @level2name = N'RecordFilePath';
GO

--检验报告
CREATE NONCLUSTERED INDEX [IK_TaskNo]
    ON [dbo].[tb_OriginalRecord]([TaskNo] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'检验标准', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_OriginalRecord', @level2type = N'COLUMN', @level2name = N'InsStand';
GO

--仪器设备
alter table [dbo].[tb_Measure] 
add IsCall int
go

--实验计划
CREATE NONCLUSTERED INDEX [IK_TaskNo]
    ON [dbo].[tb_ExpePlan]([TaskNo] ASC);
GO

--易耗品
alter table [dbo].[tb_Device] 
add netvalue decimal(18,2),
explain nvarchar(500)
go

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'设备操作规程', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Device', @level2type = N'COLUMN', @level2name = N'explain';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'净值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Device', @level2type = N'COLUMN', @level2name = N'netvalue';
GO

--实验任务
CREATE TABLE [dbo].[task] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [taskname]    NVARCHAR (250) NULL,
    [director]    NVARCHAR (50)  NULL,
    [publishid]   INT            NULL,
    [remark]      NCHAR (10)     NULL,
    [status]      INT            NULL,
    [tasktime]    SMALLDATETIME  NULL,
    [publishtime] SMALLDATETIME  NULL,
    [finishtime]  SMALLDATETIME  NULL,
    CONSTRAINT [PK_task] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'负责人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'task', @level2type = N'COLUMN', @level2name = N'director';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'完成日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'task', @level2type = N'COLUMN', @level2name = N'finishtime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发布人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'task', @level2type = N'COLUMN', @level2name = N'publishid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发布时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'task', @level2type = N'COLUMN', @level2name = N'publishtime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'待完成、完成、延期、延期完成(1,2,3,4)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'task', @level2type = N'COLUMN', @level2name = N'status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计划完成时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'task', @level2type = N'COLUMN', @level2name = N'tasktime';
GO

--数据处理
update tb_Sample set AreaID=B.AreaID  from tb_Sample as A left join tb_InPersonnel as B on A.createUser=B.PersonnelID;
GO

update tb_Sample  set handleUser=0 where handleUser='' or handleUser is null
GO
