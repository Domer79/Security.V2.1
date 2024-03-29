/****** Object:  Schema [sec]    Script Date: 22.11.2018 17:22:37 ******/
CREATE SCHEMA [sec]
GO
/****** Object:  UserDefinedFunction [sec].[GetIdentificationMode]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [sec].[GetIdentificationMode]()
returns nvarchar(max)
as
begin
	return sec.GetSettings('identification_mode')
end
GO
/****** Object:  UserDefinedFunction [sec].[GetSettings]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [sec].[GetSettings](@name nvarchar(100))
returns nvarchar(max)
as
begin
	declare @value nvarchar(max)

	select @value = value from sec.Settings where name = @name

	return @value
end
GO
/****** Object:  UserDefinedFunction [sec].[IsAllowById]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [sec].[IsAllowById](@idSecObject int, @idMember int)
returns bit
as
begin
	declare @result bit = 0

	--Проверяем доступ по списку всех групп, в которых состоит пользователь, не забывая при этом вклювить самого пользователя в этот список
    if exists(select 1 from sec.Users where idMember = @idMember and status = 0)
        return @result

	select
		top 1
		@result = isAllow
	from
	(
		select
			1 isAllow
		from
			sec.Grants gr inner join (select idSecObject from sec.SecObjects) so 
			on gr.idSecObject = so.idSecObject inner join sec.MemberRoles mr
			on gr.idRole = mr.idRole 
		where 
			so.idSecObject = @idSecObject
			and mr.idMember in (select idGroup from sec.UserGroups where idUser = @idMember union all select idMember from sec.Users where idMember = @idMember)
		union
		select 
			0
	)s

	return @result
end
GO
/****** Object:  UserDefinedFunction [sec].[IsAllowByName]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [sec].[IsAllowByName](@secObject nvarchar(200), @loginOrEmail nvarchar(200), @appName nvarchar(200))
returns bit
as
begin
declare @idApplication int = (select idApplication from sec.Applications where appName = @appName)
declare @idSecObject int = (select idSecObject from sec.SecObjects where ObjectName = @secObject and idApplication = @idApplication)
declare @idMember int = (select idMember from sec.UsersView where login = @loginOrEmail or email = @loginOrEmail)

return sec.IsAllowById(@idSecObject, @idMember)
end
GO
/****** Object:  UserDefinedFunction [sec].[IsAllowByToken]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE function [sec].[IsAllowByToken](@secObject nvarchar(200), @token nvarchar(200), @appName nvarchar(200))
returns bit
as
begin
declare @idApplication int = (select idApplication from sec.Applications where appName = @appName)
declare @idSecObject int = (select idSecObject from sec.SecObjects where ObjectName = @secObject and idApplication = @idApplication)
declare @idMember int = -1

select @idMember = idUser from sec.Tokens where tokenId = @token and expire > GETUTCDATE()

if @idMember = -1
	return 0

if @idMember = 1
	return 1

return sec.IsAllowById(@idSecObject, @idMember)
end
GO
/****** Object:  Table [sec].[Groups]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[Groups](
	[idMember] [int] NOT NULL,
	[description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[idMember] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [sec].[Members]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[Members](
	[idMember] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NOT NULL,
	[id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idMember] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [sec].[GroupsView]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [sec].[GroupsView]
as
select
	m.id,
    m.idMember,
	m.name,
    g.description
from 
    sec.Groups g
        inner join sec.Members m on g.idMember = m.idMember

GO
/****** Object:  Table [sec].[MemberRoles]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[MemberRoles](
	[idMember] [int] NOT NULL,
	[idRole] [int] NOT NULL,
 CONSTRAINT [PK_MemberRole] PRIMARY KEY CLUSTERED 
(
	[idMember] ASC,
	[idRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sec].[Roles]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[Roles](
	[idRole] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NOT NULL,
	[description] [nvarchar](max) NULL,
	[idApplication] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [sec].[Applications]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[Applications](
	[idApplication] [int] IDENTITY(1,1) NOT NULL,
	[appName] [nvarchar](200) NOT NULL,
	[description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[idApplication] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [sec].[RoleOfMembers]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [sec].[RoleOfMembers]
as
select
	r.idRole,
	r.name roleName,
	r.description roleDescription,
	r.idApplication,
	r.appName,
	m.idMember,
	m.name memberName
from
	(
	select  
		r.idRole,
		r.name,
		r.description,
		a.idApplication,
		a.appName
	from sec.Roles r inner join sec.Applications a on r.idApplication = a.idApplication) r 
	inner join sec.MemberRoles mr 
on 
	r.idRole = mr.idRole inner join sec.Members m
on
	mr.idMember = m.idMember

GO
/****** Object:  Table [sec].[Users]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[Users](
	[idMember] [int] NOT NULL,
	[password] [varbinary](max) NULL,
	[firstName] [nvarchar](200) NOT NULL,
	[lastName] [nvarchar](200) NOT NULL,
	[middleName] [nvarchar](200) NULL,
	[email] [nvarchar](450) NOT NULL,
	[status] [bit] NOT NULL,
	[dateCreated] [datetime2](7) NOT NULL,
	[lastActivityDate] [datetime2](7) NULL,
	[passwordSalt] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idMember] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [sec].[UsersView]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [sec].[UsersView]
as
select
    m.idMember,
    m.name as login,    
	m.id,
	u.firstName,
    u.lastName,
    u.middleName,
    u.email,
    u.status,
	u.passwordSalt,
    u.dateCreated,
    u.lastActivityDate
from 
    sec.Users u 
        inner join sec.Members m on u.idMember = m.idMember

GO
/****** Object:  Table [sec].[Grants]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[Grants](
	[idSecObject] [int] NOT NULL,
	[idRole] [int] NOT NULL,
 CONSTRAINT [PK_sec_Grants] PRIMARY KEY CLUSTERED 
(
	[idSecObject] ASC,
	[idRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sec].[Logs]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[Logs](
	[idLog] [int] IDENTITY(1,1) NOT NULL,
	[message] [nvarchar](max) NOT NULL,
	[dateCreated] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idLog] ASC
)WITH (PAD_INDEX = ON, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 98) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [sec].[SecObjects]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[SecObjects](
	[idSecObject] [int] IDENTITY(1,1) NOT NULL,
	[ObjectName] [nvarchar](200) NOT NULL,
	[Type] [nvarchar](100) NULL,
	[Discriminator] [nvarchar](200) NULL,
	[idApplication] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idSecObject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sec].[Settings]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[Settings](
	[idSettings] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[value] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[changedDate] [datetime2](7) NULL,
	[inDblifetime] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[idSettings] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [sec].[Tokens]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[Tokens](
	[tokenId] [nvarchar](100) NOT NULL,
	[created] [datetime2](7) NOT NULL,
	[expire] [datetime2](7) NOT NULL,
	[idUser] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[tokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sec].[UserGroups]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[UserGroups](
	[idUser] [int] NOT NULL,
	[idGroup] [int] NOT NULL,
 CONSTRAINT [PK_UserGroups] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC,
	[idGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Application_Name]    Script Date: 22.11.2018 17:22:37 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Application_Name] ON [sec].[Applications]
(
	[appName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UQ_Group_idMember]    Script Date: 22.11.2018 17:22:37 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Group_idMember] ON [sec].[Groups]
(
	[idMember] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Member_Name]    Script Date: 22.11.2018 17:22:37 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Member_Name] ON [sec].[Members]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Role_Name]    Script Date: 22.11.2018 17:22:37 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Role_Name] ON [sec].[Roles]
(
	[name] ASC,
	[idApplication] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_SecObject_ObjectName]    Script Date: 22.11.2018 17:22:37 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SecObject_ObjectName] ON [sec].[SecObjects]
(
	[ObjectName] ASC,
	[idApplication] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Settings_Name]    Script Date: 22.11.2018 17:22:37 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Settings_Name] ON [sec].[Settings]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_secTokens_for_Expire]    Script Date: 22.11.2018 17:22:37 ******/
CREATE NONCLUSTERED INDEX [IX_secTokens_for_Expire] ON [sec].[Tokens]
(
	[expire] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UQ_User_idMember]    Script Date: 22.11.2018 17:22:37 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_User_idMember] ON [sec].[Users]
(
	[idMember] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_UserProfile_PasswordSalt]    Script Date: 22.11.2018 17:22:37 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_UserProfile_PasswordSalt] ON [sec].[Users]
(
	[passwordSalt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Users_Email]    Script Date: 22.11.2018 17:22:37 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Users_Email] ON [sec].[Users]
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [sec].[Logs] ADD  DEFAULT (getdate()) FOR [dateCreated]
GO
ALTER TABLE [sec].[Members] ADD  CONSTRAINT [DF_Members_Id]  DEFAULT (newsequentialid()) FOR [id]
GO
ALTER TABLE [sec].[Roles] ADD  CONSTRAINT [DF_IdApplication_Roles]  DEFAULT ((1)) FOR [idApplication]
GO
ALTER TABLE [sec].[SecObjects] ADD  CONSTRAINT [DF_IdApplication_SecObjects]  DEFAULT ((1)) FOR [idApplication]
GO
ALTER TABLE [sec].[Users] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [sec].[Users] ADD  DEFAULT (getdate()) FOR [dateCreated]
GO
ALTER TABLE [sec].[Users] ADD  CONSTRAINT [DF_Users_PasswordSalt]  DEFAULT (lower(replace(CONVERT([nvarchar](100),newid()),N'-',N''))) FOR [passwordSalt]
GO
ALTER TABLE [sec].[Grants]  WITH CHECK ADD  CONSTRAINT [FK_Grants_Roles] FOREIGN KEY([idRole])
REFERENCES [sec].[Roles] ([idRole])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[Grants] CHECK CONSTRAINT [FK_Grants_Roles]
GO
ALTER TABLE [sec].[Grants]  WITH CHECK ADD  CONSTRAINT [FK_Grants_SecObjects] FOREIGN KEY([idSecObject])
REFERENCES [sec].[SecObjects] ([idSecObject])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[Grants] CHECK CONSTRAINT [FK_Grants_SecObjects]
GO
ALTER TABLE [sec].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Members] FOREIGN KEY([idMember])
REFERENCES [sec].[Members] ([idMember])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[Groups] CHECK CONSTRAINT [FK_Groups_Members]
GO
ALTER TABLE [sec].[MemberRoles]  WITH CHECK ADD  CONSTRAINT [FK_MemberRoles_Members] FOREIGN KEY([idMember])
REFERENCES [sec].[Members] ([idMember])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[MemberRoles] CHECK CONSTRAINT [FK_MemberRoles_Members]
GO
ALTER TABLE [sec].[MemberRoles]  WITH CHECK ADD  CONSTRAINT [FK_MemberRoles_Roles] FOREIGN KEY([idRole])
REFERENCES [sec].[Roles] ([idRole])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[MemberRoles] CHECK CONSTRAINT [FK_MemberRoles_Roles]
GO
ALTER TABLE [sec].[Roles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_Applications] FOREIGN KEY([idApplication])
REFERENCES [sec].[Applications] ([idApplication])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[Roles] CHECK CONSTRAINT [FK_Roles_Applications]
GO
ALTER TABLE [sec].[SecObjects]  WITH CHECK ADD  CONSTRAINT [FK_SecObjects_Applications] FOREIGN KEY([idApplication])
REFERENCES [sec].[Applications] ([idApplication])
GO
ALTER TABLE [sec].[SecObjects] CHECK CONSTRAINT [FK_SecObjects_Applications]
GO
ALTER TABLE [sec].[Tokens]  WITH CHECK ADD  CONSTRAINT [FK_Tokens_Users] FOREIGN KEY([idUser])
REFERENCES [sec].[Users] ([idMember])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[Tokens] CHECK CONSTRAINT [FK_Tokens_Users]
GO
ALTER TABLE [sec].[UserGroups]  WITH CHECK ADD  CONSTRAINT [FK_UserGroups_Groups] FOREIGN KEY([idGroup])
REFERENCES [sec].[Groups] ([idMember])
GO
ALTER TABLE [sec].[UserGroups] CHECK CONSTRAINT [FK_UserGroups_Groups]
GO
ALTER TABLE [sec].[UserGroups]  WITH CHECK ADD  CONSTRAINT [FK_UserGroups_Users] FOREIGN KEY([idUser])
REFERENCES [sec].[Users] ([idMember])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[UserGroups] CHECK CONSTRAINT [FK_UserGroups_Users]
GO
ALTER TABLE [sec].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Members] FOREIGN KEY([idMember])
REFERENCES [sec].[Members] ([idMember])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[Users] CHECK CONSTRAINT [FK_Users_Members]
GO
/****** Object:  StoredProcedure [sec].[AddApp]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [sec].[AddApp]
	@appName nvarchar(200),
	@description nvarchar(max)
as
insert into sec.Applications values(@appName, @description)
select SCOPE_IDENTITY() as idApplication
GO
/****** Object:  StoredProcedure [sec].[AddEmptyGroup]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [sec].[AddEmptyGroup]
@id uniqueidentifier,
@prefix nvarchar(50)
as
set nocount on

declare @ident int
select @ident = IDENT_CURRENT('sec.Members')

declare @name nvarchar(200) = concat(@prefix, @ident)
exec sec.AddGroup @id = @id, @name = @name
GO
/****** Object:  StoredProcedure [sec].[AddEmptyUser]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [sec].[AddEmptyUser]
@id uniqueidentifier,
@prefix nvarchar(50)
as
set nocount on

declare @ident int
select @ident = IDENT_CURRENT('sec.Members')

declare @login nvarchar(200) = concat(@prefix, @ident)
declare @firstName nvarchar(200) = concat(@prefix, @ident)
declare @lastName nvarchar(200) = concat(@prefix, @ident)
declare @email nvarchar(450) = concat(@prefix, @ident, '@email.net')

exec sec.AddUser @id = @id, @login = @login, @firstName = @firstName, @lastName = @lastName, @middleName = null, @email = @email, @status = 0
GO
/****** Object:  StoredProcedure [sec].[AddGrant]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [dbo].[Grant_Insert] @IdSecObject=6,@IdRole=1,@IdAccessType=11,@ObjectName=NULL,@RoleName=NULL,@AccessName=NULL
CREATE procedure [sec].[AddGrant]
	@IdSecObject int,
	@IdRole int
as
set nocount on
insert into sec.Grants(idSecObject, idRole) values(@IdSecObject, @IdRole)
GO
/****** Object:  StoredProcedure [sec].[AddGroup]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [sec].[AddGroup]
	@id uniqueidentifier,
	@name nvarchar(200),
	@description nvarchar(max) = null
as
set nocount on

declare @idMember int

insert into Members(name) values(@name)
select @idMember = SCOPE_IDENTITY()
insert into Groups(idMember, description) values(@idMember, @description)
select @idMember as idMember
GO
/****** Object:  StoredProcedure [sec].[AddUser]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [sec].[AddUser]
	@id uniqueidentifier,
	@login nvarchar(200),
    @firstName nvarchar(200),
    @lastName nvarchar(200),
    @middleName nvarchar(200),
    @email nvarchar(450),
    @status bit,
	@passwordSalt nvarchar(100) = null,
    @dateCreated datetime2 = null
as
set nocount on

declare @idMember int

insert into Members(name) values(@login)
select @idMember = SCOPE_IDENTITY()

if @dateCreated is null
    set @dateCreated = GETDATE()

if @passwordSalt is null
	set @passwordSalt = NEWID()

insert into Users(idMember, firstName, lastName, middleName, email, status, passwordSalt, dateCreated) 
    values(@idMember, @firstName, @lastName, @middleName, @email, @status, @passwordSalt, @dateCreated)

select @idMember as idMember
GO
/****** Object:  StoredProcedure [sec].[DeleteApp]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [sec].[DeleteApp]
	@idApplication int
as
delete from sec.Roles where idApplication = @idApplication
delete from sec.SecObjects where idApplication = @idApplication
delete from sec.Applications where idApplication = @idApplication
GO
/****** Object:  StoredProcedure [sec].[DeleteAppByName]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [sec].[DeleteAppByName]
	@appName nvarchar(200) 
as
declare @idApplication int
select @idApplication = idApplication from sec.Applications where appName = @appName
exec sec.DeleteApp @idApplication
GO
/****** Object:  StoredProcedure [sec].[DeleteGrant]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [sec].[DeleteGrant]
	@IdSecObject int,
	@IdRole int
as
set nocount on
delete from sec.Grants where idSecObject = @IdSecObject and idRole = @IdRole
GO
/****** Object:  StoredProcedure [sec].[DeleteGroup]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [sec].[DeleteGroup]
	@idMember int
as
set nocount on
delete from sec.UserGroups where idGroup = @idMember
delete from sec.Members where idMember = @idMember
GO
/****** Object:  StoredProcedure [sec].[DeleteUser]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [sec].[DeleteUser]
	@idMember int
as
set nocount on
delete from sec.Members where idMember = @idMember
GO
/****** Object:  StoredProcedure [sec].[GetSecurityObjects]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [sec].[GetSecurityObjects]
	@login nvarchar(max) null,
	@appName nvarchar(max),
	@allowAll bit
as
declare @idApplication int = (select idApplication from sec.Applications where appName = @appName)
declare @idMember int = (select idMember from sec.Members where name = @login)
if @allowAll = 1
begin
	select 
		s.ObjectName
	from 
		sec.Grants g inner join sec.SecObjects s on g.idSecObject = s.idSecObject
	where 
		g.idRole in (select idRole from sec.Roles where idApplication = @idApplication)
end
else
begin
	select 
		s.ObjectName
	from 
		sec.Grants g inner join sec.SecObjects s on g.idSecObject = s.idSecObject
	where 
		g.idRole in (select idRole from sec.RoleOfMembers where idMember in (select idGroup from sec.UserGroups where idUser = @idMember union all select idMember from sec.Users where idMember = @idMember) and idApplication = @idApplication)
end
GO
/****** Object:  StoredProcedure [sec].[GrantToPublic]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [sec].[GrantToPublic]
    @toPrint BIT = 1
AS
SET NOCOUNT ON
BEGIN

DECLARE @str VARCHAR(4000)
DECLARE @stmt NVARCHAR(MAX)
DECLARE @crlf VARCHAR(50)

SET @stmt = ''
SET @crlf = /*CHAR(13) + 'GO' +*/ CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10)

DECLARE grantcursor CURSOR
FOR 
SELECT 'GRANT EXEC ON [' + schm.name + '].[' + p.name + '] TO PUBLIC;' FROM sys.procedures p inner join sys.schemas schm on p.schema_id = schm.schema_id WHERE LOWER(p.name) <> 'granttopublic'
UNION ALL
SELECT 'GRANT SELECT, UPDATE, INSERT, DELETE ON [' + schm.name + '].[' + p.name + '] TO PUBLIC;' FROM sys.tables p inner join sys.schemas schm on p.schema_id = schm.schema_id
UNION ALL
SELECT 'GRANT SELECT ON [' + schm.name + '].[' + p.name + '] TO PUBLIC;' FROM sys.views p inner join sys.schemas schm on p.schema_id = schm.schema_id
UNION ALL
select 'GRANT EXEC ON [' + schm.name + '].[' + p.name + '] TO PUBLIC;' from sys.objects p inner join sys.schemas schm on p.schema_id = schm.schema_id where type_desc = 'SQL_SCALAR_FUNCTION'

    OPEN grantcursor
    FETCH NEXT FROM grantcursor INTO @str
    WHILE @@FETCH_STATUS = 0
    BEGIN
		IF @toPrint = 1
		BEGIN
			IF LEN(@stmt + @str) > 4000
			BEGIN
				PRINT @stmt
				SET @stmt = ''
			END
		END

        SET @stmt += @str
        IF @toPrint = 1
			SET @stmt += @crlf
        FETCH NEXT FROM grantcursor INTO @str
    END

    CLOSE grantcursor
    DEALLOCATE grantcursor

IF @toPrint = 0 
    EXEC sp_executesql @stmt
ELSE
    PRINT @stmt

END

/****** Object:  UserDefinedFunction [sec].[GetIdentificationMode]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [sec].[SetIdentificationMode]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [sec].[SetIdentificationMode]
	@mode nvarchar(100)
as
set nocount on
if not exists(select 1 from sec.Settings where name = 'identification_mode')
	insert into sec.Settings(name, value) values('identification_mode', @mode)
else
	update sec.Settings set value = @mode where name = 'identification_mode'
GO
/****** Object:  StoredProcedure [sec].[SetPassword]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [sec].[SetPassword]
    @login nvarchar(200),
    @password varbinary(max)
as
update sec.Users set password = @password where idMember = (select idMember from sec.Members where name = @login)
GO
/****** Object:  StoredProcedure [sec].[UpdateApp]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [sec].[UpdateApp]
	@idApplication int,
	@description nvarchar(max)
as
Update sec.Applications set description = @description where idApplication = @idApplication
GO
/****** Object:  StoredProcedure [sec].[UpdateAppByName]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [sec].[UpdateAppByName]
	@appName nvarchar(200),
	@description nvarchar(max)
as
Update sec.Applications set description = @description where appName = @appName
GO
/****** Object:  StoredProcedure [sec].[UpdateGrant]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [sec].[UpdateGrant]
	@IdSecObject int,
	@IdRole int
as
set nocount on
raiserror('not_modified', 16, 1)
GO
/****** Object:  StoredProcedure [sec].[UpdateGroup]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [sec].[UpdateGroup]
	@id uniqueidentifier,
	@idMember int,
	@name nvarchar(200),
	@description nvarchar(max)
as
set nocount on
update sec.Members set name = @name where idMember = @idMember
update sec.Groups set description = @description where idMember = @idMember
GO
/****** Object:  StoredProcedure [sec].[UpdateUser]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [sec].[UpdateUser]
	@idMember int,
	@login nvarchar(200),
    @firstName nvarchar(200),
    @lastName nvarchar(200),
    @middleName nvarchar(200),
    @email nvarchar(450),
    @status bit,
	@passwordSalt nvarchar(100),
    @dateCreated datetime2,
    @lastActivityDate datetime2
as
set nocount on
update sec.Members set name = @login where idMember = @idMember
update 
    sec.Users 
set 
    firstName = @firstName, 
    lastName = @lastName, 
    middleName = @middleName, 
    email = @email, 
    status = @status, 
	passwordSalt = @passwordSalt,
    dateCreated = @dateCreated, 
    lastActivityDate = @lastActivityDate 
where 
    idMember = @idMember
GO
/****** Object:  Trigger [sec].[OnInsertGrant]    Script Date: 22.11.2018 17:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [sec].[OnInsertGrant] on [sec].[Grants]
after INSERT, UPDATE
as
--Триггер проверяет, при добавлении или обновлении (хотя такого никогда не должно быть) записей, 
--что нет ни одной из таковых, в которой бы объект безопасности, роль и тип доступа не принадлежали 
--одному и тому же приложению
if exists(
			select 
				1
			from
			(
				select 
					gr.idSecObject, gr.idRole, so.idApplication secObjectIdApp, r.idApplication roleIdApp
				from 
					inserted gr left join sec.SecObjects so 
				on 
					gr.idSecObject = so.idSecObject left join sec.Roles r 
				on 
					gr.idRole = r.idRole 
			) as extendGrants
			where
				secObjectIdApp <> roleIdApp
		)
raiserror(N'appconflict', 16, 1)
GO
ALTER TABLE [sec].[Grants] ENABLE TRIGGER [OnInsertGrant]
GO
/****** Object:  Trigger [sec].[OnDeleteGroup]    Script Date: 22.11.2018 17:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [sec].[OnDeleteGroup] on [sec].[Groups]
after delete
as
set nocount on

--Ограничение на удаление записи в sec.Groups, если не удалена запись в sec.Member
if exists(select 1 from sec.Members where idMember in (select idMember from deleted))
	begin
		raiserror('fk_member_error', 16, 10)
		rollback
		return
	end

delete from sec.UserGroups where idGroup in (select idMember from deleted)
GO
ALTER TABLE [sec].[Groups] ENABLE TRIGGER [OnDeleteGroup]
GO
/****** Object:  Trigger [sec].[SetChangedDate]    Script Date: 22.11.2018 17:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create trigger [sec].[SetChangedDate] on [sec].[Settings]
after insert, update
as
begin 
print 1
end
GO
ALTER TABLE [sec].[Settings] ENABLE TRIGGER [SetChangedDate]
GO
/****** Object:  Trigger [sec].[OnDeleteUser]    Script Date: 22.11.2018 17:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [sec].[OnDeleteUser] on [sec].[Users]
after delete
as
set nocount on

--Ограничение на удаление записи в sec.Users, если не удалена запись в sec.Member
if exists(select 1 from sec.Members where idMember in (select idMember from deleted))
	begin
		raiserror('fk_member_error', 16, 10)
		rollback
		return
	end
GO
ALTER TABLE [sec].[Users] ENABLE TRIGGER [OnDeleteUser]
GO
/****** Object:  Trigger [sec].[OnDeleteGroupsView]    Script Date: 22.11.2018 17:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [sec].[OnDeleteGroupsView] on [sec].[GroupsView]
instead of delete
as
delete from sec.UserGroups where idGroup in (select idMember from deleted)
delete from sec.Members where idMember in (select idMember from deleted)
GO
/****** Object:  Trigger [sec].[OnInsertGroupsView]    Script Date: 22.11.2018 17:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [sec].[OnInsertGroupsView] on [sec].[GroupsView]
instead of insert
as

insert into sec.Members(name) select name from inserted
insert into sec.Groups(idMember, description) select m.idMember, ins.description from Members m inner join inserted ins on m.name = ins.name
GO
/****** Object:  Trigger [sec].[OnUpdateGroupsView]    Script Date: 22.11.2018 17:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [sec].[OnUpdateGroupsView] on [sec].[GroupsView]
instead of update
as

update sec.Members set name = inserted.name from inserted where inserted.idMember = sec.Members.idMember 
update sec.Groups set description = inserted.description from inserted where inserted.idMember = sec.Groups.idMember
GO
/****** Object:  Trigger [sec].[OnDeleteUsersView]    Script Date: 22.11.2018 17:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [sec].[OnDeleteUsersView] on [sec].[UsersView]
instead of delete
as

delete from sec.Members where idMember in (select idMember from deleted)
GO
/****** Object:  Trigger [sec].[OnInsertUsersView]    Script Date: 22.11.2018 17:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [sec].[OnInsertUsersView] on [sec].[UsersView]
instead of insert
as

insert into sec.Members(name) select login from inserted
insert into sec.Users(idMember, firstName, lastName, middleName, email, status, dateCreated, lastActivityDate) 
    select 
        m.idMember, 
        ins.firstName, 
        ins.lastName, 
        ins.middleName, 
        ins.email, 
        ins.status, 
        ins.dateCreated, 
        ins.lastActivityDate 
    from 
        Members m inner join inserted ins 
    on 
        m.name = ins.login

select SCOPE_IDENTITY()
GO
/****** Object:  Trigger [sec].[OnUpdateUsersView]    Script Date: 22.11.2018 17:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [sec].[OnUpdateUsersView] on [sec].[UsersView]
instead of update
as

update sec.Members set name = inserted.login from inserted where inserted.idMember = sec.Members.idMember 
update sec.Users set 
    firstName =  ins.firstName, 
    lastName =  ins.lastName, 
    middleName =  ins.middleName, 
    email =  ins.email, 
    status =  ins.status, 
    dateCreated =  ins.dateCreated, 
    lastActivityDate = ins.lastActivityDate
from 
    inserted ins 
where ins.idMember = sec.Users.idMember

GO


declare @idMember int = 1
declare @idApplication int

set identity_insert sec.Members on
insert sec.Members (idMember, id, name) values(@idMember, NEWID(), 'admin')
set identity_insert sec.Members off
INSERT [sec].[Users] ([idMember], [password], [firstName], [lastName], [middleName], [email], [status], [dateCreated], [lastActivityDate], [passwordSalt]) VALUES (@idMember, 0x8EEAB7885B1E87A164C181A84BCD6E7CAE8A6FBC, N'admin', N'admin', NULL, N'admin@mail.ru', 1, GETUTCDATE(), NULL, N'c3995452688b434890d857280582fa4d')
insert sec.Applications(appName, description) values('SecurityManager', 'Система управления доступом')
select @idApplication = SCOPE_IDENTITY()

/*Policies*/

insert sec.SecObjects(idApplication, ObjectName) values(@idApplication, 'showUsers')
insert sec.SecObjects(idApplication, ObjectName) values(@idApplication, 'showRoles')
insert sec.SecObjects(idApplication, ObjectName) values(@idApplication, 'showGroups')
insert sec.SecObjects(idApplication, ObjectName) values(@idApplication, 'addNewUser')
insert sec.SecObjects(idApplication, ObjectName) values(@idApplication, 'removeUser')
insert sec.SecObjects(idApplication, ObjectName) values(@idApplication, 'editUserProfile')
insert sec.SecObjects(idApplication, ObjectName) values(@idApplication, 'editUserGroups')
insert sec.SecObjects(idApplication, ObjectName) values(@idApplication, 'editUserRoles')

/*Policies*/