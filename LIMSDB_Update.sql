--��Ʒ�����
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

--�����ֶ�˳��********************************************************************

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'��λ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'AreaID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'���Բ���', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'checkdepar';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'����ʱ��', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'createDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'������', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'createUser';
GO
EXEC sp_dropextendedproperty 'MS_Description',@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'tb_Sample', @level2type=N'COLUMN', @level2name=N'handleDate';
GO
EXEC sp_dropextendedproperty 'MS_Description',@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'tb_Sample', @level2type=N'COLUMN', @level2name=N'handleUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'��������', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'handleDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'������', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'handleUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'����', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'protNum';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'��Ʒ���', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'sampleNum';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'�洢����', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'storcondition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'�޸�ʱ��', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'updateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'�޸���', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'updateUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'�����̶ȣ�1����ͨ��2��������', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample', @level2type = N'COLUMN', @level2name = N'urgentlevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'��Ʒ��', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Sample';
GO


--��Ŀ��
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'�Ƿ�ũҩ����', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Project', @level2type = N'COLUMN', @level2name = N'IsPesCheck';
GO

--�޸���ͼ:View_Drug
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

--���鱨��
EXEC sp_dropextendedproperty 'MS_Description',@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'tb_TestReport', @level2type=N'COLUMN', @level2name=N'IssuedTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'��׼���ڣ�ǩ�����ڣ�', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_TestReport', @level2type = N'COLUMN', @level2name = N'IssuedTime';
GO

--ԭʼ��¼��Ӧ����
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ԭʼ��¼�ļ�����', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_RecordSample', @level2type = N'COLUMN', @level2name = N'RecordFilePath';
GO

--���鱨��
CREATE NONCLUSTERED INDEX [IK_TaskNo]
    ON [dbo].[tb_OriginalRecord]([TaskNo] ASC);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'�����׼', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_OriginalRecord', @level2type = N'COLUMN', @level2name = N'InsStand';
GO

--�����豸
alter table [dbo].[tb_Measure] 
add IsCall int
go

--ʵ��ƻ�
CREATE NONCLUSTERED INDEX [IK_TaskNo]
    ON [dbo].[tb_ExpePlan]([TaskNo] ASC);
GO

--�׺�Ʒ
alter table [dbo].[tb_Device] 
add netvalue decimal(18,2),
explain nvarchar(500)
go

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'�豸�������', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Device', @level2type = N'COLUMN', @level2name = N'explain';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'��ֵ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tb_Device', @level2type = N'COLUMN', @level2name = N'netvalue';
GO

--ʵ������
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

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'������', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'task', @level2type = N'COLUMN', @level2name = N'director';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'�������', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'task', @level2type = N'COLUMN', @level2name = N'finishtime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'������', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'task', @level2type = N'COLUMN', @level2name = N'publishid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'����ʱ��', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'task', @level2type = N'COLUMN', @level2name = N'publishtime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'����ɡ���ɡ����ڡ��������(1,2,3,4)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'task', @level2type = N'COLUMN', @level2name = N'status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'�ƻ����ʱ��', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'task', @level2type = N'COLUMN', @level2name = N'tasktime';
GO

--���ݴ���
update tb_Sample set AreaID=B.AreaID  from tb_Sample as A left join tb_InPersonnel as B on A.createUser=B.PersonnelID;
GO

update tb_Sample  set handleUser=0 where handleUser='' or handleUser is null
GO
