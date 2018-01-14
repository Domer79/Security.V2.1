use ItisOmsSecurity
go

alter table sec.SecObjects
drop constraint [FK_sec.SecObjects_sec.AccessTypes_IdAccessType]

/****** Object:  Index [UQ_SecObject_ObjectName]    Script Date: 13.01.2018 16:17:59 ******/
DROP INDEX [UQ_SecObject_ObjectName] ON [sec].[SecObjects]

alter table sec.SecObjects
drop column idAccessType




alter table sec.Grants
drop constraint [FK_Grants_AccessTypes]

declare @name nvarchar(100)
select @name = name from sys.key_constraints where type = N'PK' and parent_object_id = object_id(N'sec.grants')

execute('alter table sec.Grants drop constraint ' + @name)

alter table sec.Grants
drop column idAccessType

alter table sec.Grants
add constraint PK_sec_Grants primary key (idSecObject, idRole)

drop table sec.AccessTypes




/****** Object:  Index [UQ_SecObject_ObjectName]    Script Date: 13.01.2018 16:17:59 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SecObject_ObjectName] ON [sec].[SecObjects]
(
	[ObjectName] ASC,
	[idApplication] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
