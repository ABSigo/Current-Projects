/* Check if database already exists and delete it if it does exist*/
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'elevateDB')
BEGIN
	DROP DATABASE elevateDB
	print '' print '*** Dropping Database elevateDB ***'
END
GO

print '' print '*** Creating Database elevateDB ***'
GO 
CREATE DATABASE elevateDB
GO

print '' print '*** Using Database elevateDB ***'
GO
USE [elevateDB]
GO

print '' print '**** Create Statements ****'
GO 

/* *** Create Table Statements **** */

print '' print ' Creating Trainer Table ***'
GO
/* ***** Object: Table [dbo].[Trainer]   1 ********* */
CREATE TABLE [dbo].[Trainer](
	[TrainerID]		     [nvarchar](100)		NOT NULL,
	[PasswordHash]		 [varchar](100)			NOT NULL DEFAULT 'e7cf3ef4f17c3999a94f2c6f612e8a888e5b1026878e4e19398b23bd38ec221a',
	[TrainerFirstName]	 [nvarchar](100)		NOT NULL,
	[TrainerLastName]	 [nvarchar](100)		NOT NULL,
	[TrainerPhoneNumber] [nvarchar](15)			NOT NULL,
	[TrainerStatus]		 [bit]					NOT NULL DEFAULT 1,
	[TrainerPoleLevel]	 [nvarchar](15)			NOT NULL,
	[TrainerAcroLevel]	 [nvarchar](15)			NOT NULL,
	[TrainerSilksLevel]  [nvarchar](15)			NOT NULL,
	[TrainerLyraLevel]	 [nvarchar](15)			NOT NULL,
	
	CONSTRAINT [pk_TrainerID] PRIMARY KEY([TrainerID] ASC)
)
GO

print '' print ' Creating Skill Table ***'
GO
/* ***** Object: Table [dbo].[Skill]  7  ********* */
CREATE TABLE [dbo].[Skill](
	[SkillID]		[nvarchar](25)				NOT NULL,
								
	
	CONSTRAINT [pk_SkillID] PRIMARY KEY([SkillID] ASC)
	
)
GO

print '' print ' Creating Level Table ***'
GO
/* ***** Object: Table [dbo].[Level]  8  ********* */
CREATE TABLE [dbo].[Level](
	[LevelID]	[nvarchar](25)			NOT NULL,
	
	CONSTRAINT [pk_LevelID] PRIMARY KEY([LevelID] ASC)
)
GO


print '' print ' Creating Class Table ***'
GO
/* ***** Object: Table [dbo].[Class]   2 ********* */
CREATE TABLE [dbo].[Class](
	[ClassID]				[int] IDENTITY(100,1)			NOT NULL,
	[ClassDate]				[datetime]						NOT NULL,
	[ClassTime]				[datetime]						NOT NULL,
	[SkillID]				[nvarChar](25)					NOT NULL,
	[LevelID]			    [nvarChar](25)					NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1, 
	[Maximum]				[int]					NULL,
	
	
	
	CONSTRAINT [pk_ClassID] PRIMARY KEY([ClassID] ASC)
	
)
GO

print '' print ' Creating ClassTrainer Table ***'
GO
/* ***** Object: Table [dbo].{ClassTrainer}  3  ********* */
CREATE TABLE [dbo].[ClassTrainer](
	[TrainerID]			[nvarchar](100)						NOT NULL,
	[ClassID]			[int]						NOT NULL,
	
	CONSTRAINT [pk_TrainerIDClassID] PRIMARY KEY([TrainerID] ASC, [ClassID] ASC)
	
)
GO


/* ***** Object: Table [dbo].{Member} 4   ********* */
print '' print ' Creating Member Table ***'
GO
CREATE TABLE [dbo].[Member](
	[MemberID]			[nvarchar](100)			NOT NULL,
	[PasswordHash]		[varchar](100)			NOT NULL DEFAULT 'e7cf3ef4f17c3999a94f2c6f612e8a888e5b1026878e4e19398b23bd38ec221a',
	[FirstName]  		[nvarchar](100)			NOT NULL,
	[LastName]	   		[nvarchar](100)			NOT NULL,
	[PhoneNumber]   	[nvarchar](15)			NOT NULL,
	[Status]			[bit]					NOT NULL DEFAULT 1,
	[Birthday]			[date]					NOT NULL,
	[StartDate]		    [date]					NOT NULL,
	[MembershipTypeID]	[nvarchar](100)	        NOT NULL,
	[MemberPoleLevel]	[nvarchar](15)			NOT NULL,
	[MemberAcroLevel] 	[nvarchar](15)			NOT NULL,
	[MemberSilksLevel]	[nvarchar](15)			NOT NULL,
	[MemberLyraLevel]	[nvarchar](15)			NOT NULL,
	
	CONSTRAINT [pk_MemberID] PRIMARY KEY([MemberID] ASC)

)
GO

print '' print ' Creating ClassMembers Table ***'
GO
/* ***** Object: Table [dbo].{ClassMembers}  5  ********* */
CREATE TABLE [dbo].[ClassMembers](
	[LineID]			[int] IDENTITY (100,1)				NOT NULL,
	[MemberID]			[nvarchar](100)						NOT NULL,
	[ClassID]			[int]								NOT NULL,
	CONSTRAINT [pk_LineID] PRIMARY KEY([LineID] ASC)
)
GO


print '' print ' Creating MembershipType Table ***'
GO
/* ***** Object: Table [dbo].{MembershipType}  6  ********* */
CREATE TABLE [dbo].[MembershipType](
	[MembershipTypeID]			[varChar] (100)			NOT NULL,
	
	CONSTRAINT [pk_MembershipTypeID] PRIMARY KEY([MembershipTypeID] ASC),
)
GO
print '' print '*** Inserting Trainer Records ***'
INSERT INTO [dbo].[Trainer]
	([TrainerID], [TrainerFirstName], [TrainerLastName], [TrainerPhoneNumber], 
		[TrainerPoleLevel], [TrainerAcroLevel], [TrainerSilksLevel], [TrainerLyraLevel])
		VALUES	
			('Joyous@elevate.com', 'Joyous', 'Fisher', '3195551000','Level 5','Level 5','Level 5','Level 5'),
			('Scott@elevate.com', 'Scott', 'Monroe', '3195551001','Level 5','Level 5','Level 5','Level 5'),
			('Lissa@elevate.com', 'Lissa', 'Tann', '3195551002','Level 5','Level 5','Level 5','Level 5')
GO


print '' print '*** Inserting Skill Records ***'
GO
INSERT INTO [dbo].[Skill]
	([SkillID]) 
		VALUES	
			('Lyra'),
			('Silks'),
			('Acro'),
			('Pole')
			
GO


print '' print '*** Inserting Level Records ***'
GO
INSERT INTO [dbo].[Level]
	([LevelID]) 
		VALUES	
			('Level 1'),
			('Level 2'),
			('Level 3'),
			('Level 4'),
			('Level 5')
GO			


print '' print '*** Inserting Class Records ***'
INSERT INTO [dbo].[Class]
	([ClassDate], [ClassTime], [SkillID], [LevelID])
	VALUES	
		('11/15/2016','18:00', 'Lyra', 'Level 1'),
		('11/15/2016','19:00', 'Acro', 'Level 4' ),
		('11/15/2016','20:00', 'Open', 'Level 1' ),
		('11/16/2016','18:00', 'Silks', 'Level 2')
GO

print '' print '**** Inserting ClassTrainer Records ***'
INSERT INTO [dbo].[ClassTrainer]
	([TrainerID], [ClassID])
	VALUES	
		('Joyous@elevate.com', 100),
		('Scott@elevate.com', 101),
		('Lissa@elevate.com', 102)
GO

print '' print '**** Inserting Member Records ***'
INSERT INTO [dbo].[Member]
	([MemberID],[FirstName], [LastName], [PhoneNumber], [Birthday], [StartDate], [MembershipTypeID], [MemberPoleLevel], [MemberAcroLevel], [MemberSilksLevel], [MemberLyraLevel])
	VALUES	
		('leira337@gmail.com','Ariel','Sigo','3195551111','03/21/1991','01/01/2016','Monthly','Level 1','Level 4','Level 1','Level 2'),
		('overthemoon@gmail.com','Lindsey', 'Moon','3195551112','05/04/1990','01/01/2016','Daily','Level 1','Level 4','Level 1','Level 2'),
		('jampulski@gmail.com', 'Jaci', 'Ampulski','3195551113','08/02/1989','01/01/2016', 'Private','Level 1','Level 4','Level 1','Level 2')
GO

print '' print '**** Inserting ClassMember Records ***'
INSERT INTO [dbo].[ClassMembers]
	([MemberID], [ClassID])
	VALUES	
		('leira337@gmail', 100),
		('overthemoon@gmail', 100),
		('jampulski@gmail', 101)

GO
print '' print '*** Inserting Membership Type Records ***'
INSERT INTO [dbo].[MembershipType]
	([MembershipTypeID]) 
		VALUES	
			('First'),
			('Daily'),
			('Monthly'),
			('Groupon'),
			('Private')
GO

print '' print '**** Foreign Key Scripts *****'
GO

/* **** Foreign Key Scripts ****  */

print '' print ' Creating SkillID Class Foreign Key ***'
GO
ALTER TABLE [dbo].[Class] WITH NOCHECK
	ADD CONSTRAINT [FK_SkillID] FOREIGN KEY([SkillID])
	REFERENCES [dbo].[Skill] ([SkillID])
	ON UPDATE CASCADE
GO

print '' print 'Creating Level Class Foreign Key ***'
GO
ALTER TABLE [dbo].[Class]  WITH NOCHECK 
	ADD CONSTRAINT [FK_LevelID] FOREIGN KEY([LevelID])
	REFERENCES [dbo].[Level] ([LevelID])
	ON UPDATE CASCADE
GO
					

print '' print ' Creating ClassMembers Class Foreign Key ***'
GO
ALTER TABLE [dbo].[ClassMembers]  WITH NOCHECK 
	ADD CONSTRAINT [FK_ClassID] FOREIGN KEY([ClassID])
	REFERENCES [dbo].[Class] ([ClassID])
	ON UPDATE CASCADE
GO		

print '' print ' Creating ClassMembers Member Foreign Key ***'
GO
ALTER TABLE [dbo].[ClassMembers]  WITH NOCHECK 
	ADD CONSTRAINT [FK_MemberID] FOREIGN KEY([MemberID])
	REFERENCES [dbo].[Member] ([MemberID])
	ON UPDATE CASCADE
GO				

print '' print ' Creating Stored Procedures ****'
GO
			
/* **** Stored Procedures **** */			

print '' print ' Creating sp_update_trainer_passworHash ***'
GO
CREATE PROCEDURE [dbo].[sp_update_trainer_passwordHash]
    (
    @TrainerID      varchar(100),
    @OldPasswordHash    varchar(100),
    @NewPasswordHash    varchar(100)
    )
AS
    BEGIN 
        UPDATE Trainer
            SET PasswordHash = @NewPasswordHash
            WHERE TrainerID = @TrainerID
            AND PasswordHash = @OldPasswordHash
        RETURN @@ROWCOUNT
    END
GO

print '' print ' Creating sp_update_member_passworHash ***'
GO
CREATE PROCEDURE [dbo].[sp_update_member_passwordHash]
    (
    @MemberID      varchar(100),
    @OldPasswordHash    varchar(100),
    @NewPasswordHash    varchar(100)
    )
AS
    BEGIN 
        UPDATE Member
            SET PasswordHash = @NewPasswordHash
            WHERE MemberID = @MemberID
            AND PasswordHash = @OldPasswordHash
        RETURN @@ROWCOUNT
    END
GO

print '' print ' Creating sp_authenticate_trainer'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_trainer]
	(
	@TrainerID		varchar(100),
	@PasswordHash	varchar(100)
	)
AS
	BEGIN
		SELECT COUNT(TrainerID)
		FROM Trainer
		WHERE TrainerID = @TrainerID
		AND PasswordHash = @PasswordHash
	END
GO

print '' print ' Creating sp_authenticate_member'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_member]
	(
	@MemberID		varchar(100),
	@PasswordHash	varchar(100)
	)
AS
	BEGIN
		SELECT COUNT(MemberID)
		FROM Member 
		WHERE MemberID = @MemberID
		AND PasswordHash = @PasswordHash
	END
GO


print '' print ' Creating sp_retrieve_trainer_by_trainerID ****'
GO

CREATE PROCEDURE [dbo].[sp_retrieve_trainer_by_trainerID]
    (
        @TrainerID      varchar(100)
        
    )
AS
    BEGIN 
        SELECT TrainerID, TrainerFirstName, TrainerLastName, TrainerPhoneNumber, TrainerStatus, TrainerPoleLevel, TrainerAcroLevel, TrainerSilksLevel, TrainerLyraLevel
        FROM Trainer
        WHERE TrainerID = @TrainerID
    END
GO

print '' print ' Creating sp_insert_Trainer ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_trainer]
    (
        @TrainerID          [varchar](100),
        @TrainerFirstName   [varchar](100),
        @TrainerLastName    [varchar](100),
        @TrainerPhoneNumber [varchar](15),
        @TrainerPoleLevel   [varchar](15),
        @TrainerAcroLevel   [varchar](15),
        @TrainerSilksLevel  [varchar](15),
        @TrainerLyraLevel   [Varchar](15)
    
    )
AS
    BEGIN
        INSERT INTO [dbo].[Trainer]
            ([TrainerID], [TrainerFirstName], [TrainerLastName], [TrainerPhoneNumber], [TrainerPoleLevel], [TrainerAcroLevel], [TrainerSilksLevel], [TrainerLyraLevel])
            VALUES
                (@TrainerID, @TrainerFirstName, @TrainerLastName, @TrainerPhoneNumber, @TrainerPoleLevel, @TrainerAcroLevel, @TrainerSilksLevel, @TrainerLyraLevel)
            RETURN @@ROWCOUNT
        END
GO

print '' print ' Creating sp_update_trainer ***'
GO
CREATE PROCEDURE [dbo].[sp_update_trainer]
	(
	@TrainerID	[varchar](100),
	
	@OldTrainerID [varchar](100),
	@OldTrainerFirstName [varChar](100),
	@OldTrainerLastName [varChar](100),
	@OldTrainerPhoneNumber [varChar](15),
	@OldTrainerStatus [bit],
	@OldTrainerPoleLevel [varChar](15),
	@OldTrainerAcroLevel [varChar](15),
	@OldTrainerSilksLevel[varChar](15),
	@OldTrainerLyraLevel [varChar](15),
	
	@NewTrainerID [varchar](100),
	@NewTrainerFirstName [varChar](100),
	@NewTrainerLastName [varChar](100),
	@NewTrainerPhoneNumber [varChar](15),
	@NewTrainerStatus [bit],
	@NewTrainerPoleLevel [varChar](15),
	@NewTrainerAcroLevel [varChar](15),
	@NewTrainerSilksLevel[varChar](15),
	@NewTrainerLyraLevel [varChar](15)
	
	)
AS 
	BEGIN 
		UPDATE Trainer	
			SET TrainerID = @NewTrainerID,
				TrainerFirstName = @NewTrainerFirstName,	 
				TrainerLastName	 = @NewTrainerLastName,
				TrainerPhoneNumber = @NewTrainerPhoneNumber,
				TrainerStatus	= @NewTrainerStatus,	 
				TrainerPoleLevel = @NewTrainerPoleLevel,
				TrainerAcroLevel = @NewTrainerAcroLevel,	 
				TrainerSilksLevel  = @NewTrainerSilksLevel,
				TrainerLyraLevel = @NewTrainerLyraLevel
			WHERE TrainerID = @TrainerID
			AND	TrainerFirstName = @OldTrainerFirstName	 
			AND	TrainerLastName	 = @OldTrainerLastName
			AND	TrainerPhoneNumber = @OldTrainerPhoneNumber
			AND	TrainerStatus	= @OldTrainerStatus	 
			AND	TrainerPoleLevel = @OldTrainerPoleLevel
			AND	TrainerAcroLevel = @OldTrainerAcroLevel	 
			AND	TrainerSilksLevel  = @OldTrainerSilksLevel
			AND	TrainerLyraLevel = @OldTrainerLyraLevel
		RETURN @@ROWCOUNT
	END
GO
print '' print ' Creating sp_update_trainerID'
GO
CREATE PROCEDURE [dbo].[sp_update_trainerID]
	(
	@TrainerID		varchar(100),
	@NewTrainerID	varchar(100)
	)
AS
	BEGIN
		UPDATE Trainer
			SET TrainerID = @NewTrainerID
			WHERE TrainerID = @TrainerID
		
		RETURN @@ROWCOUNT
	END
GO



print '' print ' Creating sp_insert_member ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_member]
    (
        @MemberID            [varchar](100),
        @FirstName           [varchar](100),
        @LastName            [varchar](100),
        @PhoneNumber         [varchar](15),
		@Status				[bit],	
        @Birthday           [date],
        @StartDate          [date],
        @MembershipTypeID   [varchar](15),
        @MemberPoleLevel    [varchar](15),
        @MemberAcroLevel    [varchar](15),
        @MemberSilksLevel    [varchar](15),
        @MemberLyraLevel    [varchar](15)
    )
AS
    BEGIN
        INSERT INTO [dbo].[Member]
        ([MemberID], [FirstName], [LastName], [PhoneNumber], [Status], [Birthday], [StartDate], [MembershipTypeID], [MemberPoleLevel], [MemberAcroLevel], [MemberSilksLevel], [MemberLyraLevel])
        VALUES
            (@MemberID, @FirstName, @LastName, @PhoneNumber, @Status, @Birthday, @StartDate, @MembershipTypeID, @MemberPoleLevel, @MemberAcroLevel, @MemberSilksLevel, @MemberLyraLevel)
            RETURN @@ROWCOUNT
        END
GO
print '' print ' Creating sp_update_member ****'
GO 
CREATE PROCEDURE [dbo].[sp_update_member]
    (
        @MemberID   [varchar](100),
        
        @OldMemberID    [varchar](100),
        @OldFirstName   [varchar](100),
        @OldLastName    [varchar](100),
        @OldPhoneNumber [varchar](15),
        @OldStatus      [bit],
		@OldBirthday	[dateTime],
		@OldStartDate	[dateTime],
        @OldMembershipTypeID [varchar](15),
        @OldMemberPoleLevel  [varchar](15),    
        @OldMemberAcroLevel  [varchar](15),
        @OldMemberSilksLevel [varchar](15),
        @OldMemberLyraLevel  [varchar](15),
        
        @NewMemberID    [varchar](100),
        @NewFirstName   [varchar](100),
        @NewLastName    [varchar](100),
        @NewPhoneNumber [varchar](15),
        @NewStatus      [bit],
		@NewBirthday	[date],
        @NewStartDate	[date],
		@NewMembershipTypeID [varchar](15),
        @NewMemberPoleLevel [varchar](15),
        @NewMemberAcroLevel [varchar](15),
        @NewMemberSilksLevel [varchar](15),
        @NewMemberLyraLevel [varchar](15)
        
        )
AS
    BEGIN
        UPDATE Member
            SET MemberID = @NewMemberID,
                FirstName = @NewFirstName,
                LastName = @NewLastName,
                PhoneNumber = @NewPhoneNumber,
                Status = @NewStatus,
				Birthday = @NewBirthday,
				StartDate = @NewStartDate,
                MembershipTypeID = @NewMembershipTypeID,
                MemberPoleLevel = @NewMemberPoleLevel,
                MemberAcroLevel = @NewMemberAcroLevel,
                MemberSilksLevel = @NewMemberSilksLevel,
                MemberLyraLevel = @NewMemberLyraLevel
            WHERE MemberID = @OldMemberID
            AND FirstName = @OldFirstName
            AND LastName = @OldLastName
            AND PhoneNumber = @OldPhoneNumber
            AND Status = @OldStatus
			AND Birthday = @OldBirthday
			AND StartDate = @OldStartDate
            AND MembershipTypeID = @OldMembershipTypeID
            AND MemberPoleLevel = @OldMemberPoleLevel
            AND MemberAcroLevel = @OldMemberAcroLevel
            AND MemberSilksLevel = @OldMemberSilksLevel
            AND MemberLyraLevel = @OldMemberLyraLevel
        RETURN @@ROWCOUNT
    END
GO

print '' print ' Creating sp_retrieve_member_by_memberID ****'
GO

CREATE PROCEDURE [dbo].[sp_retrieve_member_by_memberID]
    (
        @MemberID      varchar(100)
        
    )
AS
    BEGIN 
        SELECT MemberID, FirstName, LastName, PhoneNumber, Status, Birthday, StartDate, MembershipTypeID, MemberPoleLevel, MemberAcroLevel, MemberSilksLevel, MemberLyraLevel
        FROM Member
        WHERE MemberID = @MemberID
    END

GO

print '' print ' Creating sp_retrieve_class_by_status'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_class_by_status]
    (
        @Active     [bit]
        
    )
AS
    BEGIN 
        SELECT ClassID, ClassDate, ClassTime, SkillID, LevelID, Active
		FROM Class
		WHERE Active =  @Active
    END


print '' print ' Creating sp_update_class_status ****'
GO
CREATE PROCEDURE [dbo].[sp_update_class_status]
    (
		@Active [bit],
		
		@OldClassDate [datetime],
		@OldClassTime [dateTime],
		@OldSkillID [varChar](25),
		@OldLevelID [varChar](25),
		@OldActive [bit],
		@OldMaximum [int],
		
		@NewClassID [int],
		@NewClassDate [dateTime],
		@NewClassTime [dateTime],
		@NewSkillID [varChar](25),
		@NewLevelID [varChar](25),
		@NewActive [bit],
		@NewMaximum [int]
        
    )
AS
    BEGIN 
        UPDATE Class
			SET ClassDate = @NewClassDate,
				ClassTime = @NewClassTime,
				SkillID = @NewSkillID,
				LevelID = @NewLevelID,
				Active = @NewActive
			WHERE Active = @OldActive
			AND	ClassDate = @OldClassDate
			AND ClassTime = @OldClassTime
			AND	SkillID = @OldSkillID
			AND	LevelID = @OldLevelID
		RETURN @@ROWCOUNT
    END
GO

print '' print ' Creating sp_insert_Class ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_Class]
   (
   @ClassID [int],
	@ClassDate [dateTime],
	@ClassTime [dateTime],
	@SkillID [varChar](25),
	@LevelID [varChar](25),
	@Active [bit]
    
    )
AS
    BEGIN
        INSERT INTO [dbo].[Class]
            ([ClassDate], [ClassTime], [SkillID], [LevelID], [Active] )
            VALUES
                (@ClassDate, @ClassTime, @SkillID, @LevelID, @Active)
            RETURN @@ROWCOUNT
        END
GO

