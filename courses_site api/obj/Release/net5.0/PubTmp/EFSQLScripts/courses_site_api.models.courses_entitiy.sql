IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220813154241_intiat')
BEGIN
    CREATE TABLE [course_Detailes] (
        [id] int NOT NULL IDENTITY,
        [name] nvarchar(max) NOT NULL,
        [imgpath] nvarchar(max) NOT NULL,
        [price] int NOT NULL,
        [discount] int NOT NULL,
        [description] nvarchar(max) NOT NULL,
        [numberofvideos] int NOT NULL,
        [numberofhours] int NOT NULL,
        [date] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_course_Detailes] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220813154241_intiat')
BEGIN
    CREATE TABLE [purchasedcourses] (
        [id] int NOT NULL IDENTITY,
        [email] nvarchar(max) NOT NULL,
        [code] nvarchar(max) NOT NULL,
        [courseid] int NOT NULL,
        CONSTRAINT [PK_purchasedcourses] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220813154241_intiat')
BEGIN
    CREATE TABLE [registrations] (
        [id] int NOT NULL IDENTITY,
        [username] nvarchar(20) NOT NULL,
        [password] nvarchar(max) NOT NULL,
        [phonenumber] nvarchar(11) NULL,
        [gender] nvarchar(max) NOT NULL,
        [address] nvarchar(40) NULL,
        [email] nvarchar(max) NOT NULL,
        [avatarpath] nvarchar(max) NULL,
        CONSTRAINT [PK_registrations] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220813154241_intiat')
BEGIN
    CREATE TABLE [courses_Categories] (
        [id] int NOT NULL IDENTITY,
        [name] nvarchar(max) NOT NULL,
        [Course_Detailesid] int NOT NULL,
        CONSTRAINT [PK_courses_Categories] PRIMARY KEY ([id]),
        CONSTRAINT [ForeignKey-Courses_category-Course_Detailes] FOREIGN KEY ([Course_Detailesid]) REFERENCES [course_Detailes] ([id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220813154241_intiat')
BEGIN
    CREATE TABLE [comments] (
        [id] int NOT NULL IDENTITY,
        [comment] nvarchar(150) NOT NULL,
        [date] nvarchar(max) NOT NULL,
        [registrationid] int NOT NULL,
        CONSTRAINT [PK_comments] PRIMARY KEY ([id]),
        CONSTRAINT [ForeignKey-comments-registration] FOREIGN KEY ([registrationid]) REFERENCES [registrations] ([id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220813154241_intiat')
BEGIN
    CREATE TABLE [courses_Videos] (
        [id] int NOT NULL IDENTITY,
        [name] nvarchar(max) NOT NULL,
        [description] nvarchar(max) NOT NULL,
        [videopath] nvarchar(max) NOT NULL,
        [Courses_Categoryid] int NOT NULL,
        CONSTRAINT [PK_courses_Videos] PRIMARY KEY ([id]),
        CONSTRAINT [ForeignKey-Courses_videos-Courses_category] FOREIGN KEY ([Courses_Categoryid]) REFERENCES [courses_Categories] ([id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220813154241_intiat')
BEGIN
    CREATE INDEX [IX_comments_registrationid] ON [comments] ([registrationid]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220813154241_intiat')
BEGIN
    CREATE INDEX [IX_courses_Categories_Course_Detailesid] ON [courses_Categories] ([Course_Detailesid]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220813154241_intiat')
BEGIN
    CREATE INDEX [IX_courses_Videos_Courses_Categoryid] ON [courses_Videos] ([Courses_Categoryid]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220813154241_intiat')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220813154241_intiat', N'5.0.17');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220816223512_chatmigration')
BEGIN
    CREATE TABLE [chats] (
        [id] int NOT NULL IDENTITY,
        [Sender] nvarchar(max) NULL,
        [Receiver] nvarchar(max) NULL,
        [message] nvarchar(max) NULL,
        [date] datetime2 NOT NULL,
        [Registrationid] int NOT NULL,
        CONSTRAINT [PK_chats] PRIMARY KEY ([id]),
        CONSTRAINT [FK_chats_registrations_Registrationid] FOREIGN KEY ([Registrationid]) REFERENCES [registrations] ([id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220816223512_chatmigration')
BEGIN
    CREATE INDEX [IX_chats_Registrationid] ON [chats] ([Registrationid]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220816223512_chatmigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220816223512_chatmigration', N'5.0.17');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220819192355_updateRegisterRemoveAddress_phNumber')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[registrations]') AND [c].[name] = N'address');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [registrations] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [registrations] DROP COLUMN [address];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220819192355_updateRegisterRemoveAddress_phNumber')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[registrations]') AND [c].[name] = N'phonenumber');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [registrations] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [registrations] DROP COLUMN [phonenumber];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220819192355_updateRegisterRemoveAddress_phNumber')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220819192355_updateRegisterRemoveAddress_phNumber', N'5.0.17');
END;
GO

COMMIT;
GO

