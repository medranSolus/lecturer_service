CREATE TABLE [CourseType] (
    [Type] tinyint NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_CourseType] PRIMARY KEY ([Type])
);
GO


CREATE TABLE [Day] (
    [Type] tinyint NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Day] PRIMARY KEY ([Type])
);
GO


CREATE TABLE [Lang] (
    [Type] tinyint NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Lang] PRIMARY KEY ([Type])
);
GO


CREATE TABLE [Role] (
    [Type] tinyint NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([Type])
);
GO


CREATE TABLE [Semester] (
    [Type] tinyint NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Semester] PRIMARY KEY ([Type])
);
GO


CREATE TABLE [WeekType] (
    [Type] tinyint NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_WeekType] PRIMARY KEY ([Type])
);
GO


CREATE TABLE [Lecturers] (
    [ID] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Surname] nvarchar(max) NOT NULL,
    [Mail] nvarchar(max) NOT NULL,
    [RoleTypeID] tinyint NOT NULL,
    [Phone] nvarchar(max) NULL,
    [Title] nvarchar(max) NULL,
    CONSTRAINT [PK_Lecturers] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Lecturers_Role_RoleTypeID] FOREIGN KEY ([RoleTypeID]) REFERENCES [Role] ([Type]) ON DELETE CASCADE
);
GO


CREATE TABLE [Courses] (
    [ID] nvarchar(450) NOT NULL,
    [Accepted] bit NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [TypeID] tinyint NOT NULL,
    [LanguageTypeID] tinyint NOT NULL,
    [Ects] tinyint NOT NULL,
    [HoursUniversity] tinyint NOT NULL,
    [HoursStudent] tinyint NOT NULL,
    [SemesterTypeID] tinyint NOT NULL,
    [Year] bigint NOT NULL,
    [StartMonth] tinyint NOT NULL,
    [StartDay] tinyint NOT NULL,
    [EndMonth] tinyint NOT NULL,
    [EndDay] tinyint NOT NULL,
    [LecturerID] nvarchar(450) NULL,
    [CourseGroup] nvarchar(max) NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Courses_CourseType_TypeID] FOREIGN KEY ([TypeID]) REFERENCES [CourseType] ([Type]) ON DELETE CASCADE,
    CONSTRAINT [FK_Courses_Lang_LanguageTypeID] FOREIGN KEY ([LanguageTypeID]) REFERENCES [Lang] ([Type]) ON DELETE CASCADE,
    CONSTRAINT [FK_Courses_Lecturers_LecturerID] FOREIGN KEY ([LecturerID]) REFERENCES [Lecturers] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Courses_Semester_SemesterTypeID] FOREIGN KEY ([SemesterTypeID]) REFERENCES [Semester] ([Type]) ON DELETE CASCADE
);
GO


CREATE TABLE [Passwords] (
    [ID] nvarchar(450) NOT NULL,
    [Pass] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Passwords] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Passwords_Lecturers_ID] FOREIGN KEY ([ID]) REFERENCES [Lecturers] ([ID]) ON DELETE CASCADE
);
GO


CREATE TABLE [CoursesToCheck] (
    [ID] bigint NOT NULL IDENTITY,
    [CourseID] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_CoursesToCheck] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_CoursesToCheck_Courses_CourseID] FOREIGN KEY ([CourseID]) REFERENCES [Courses] ([ID]) ON DELETE CASCADE
);
GO


CREATE TABLE [Groups] (
    [ID] nvarchar(450) NOT NULL,
    [CourseID] nvarchar(450) NOT NULL,
    [StudentsCount] int NOT NULL,
    [Room] nvarchar(max) NOT NULL,
    [Building] nvarchar(max) NOT NULL,
    [WeekTypeID] tinyint NOT NULL,
    [DayID] tinyint NOT NULL,
    [StartHour] tinyint NOT NULL,
    [StartMinute] tinyint NOT NULL,
    [EndHour] tinyint NOT NULL,
    [EndMinute] tinyint NOT NULL,
    [LecturerID] nvarchar(450) NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Groups_Courses_CourseID] FOREIGN KEY ([CourseID]) REFERENCES [Courses] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Groups_Day_DayID] FOREIGN KEY ([DayID]) REFERENCES [Day] ([Type]) ON DELETE CASCADE,
    CONSTRAINT [FK_Groups_Lecturers_LecturerID] FOREIGN KEY ([LecturerID]) REFERENCES [Lecturers] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Groups_WeekType_WeekTypeID] FOREIGN KEY ([WeekTypeID]) REFERENCES [WeekType] ([Type]) ON DELETE CASCADE
);
GO


CREATE TABLE [GroupNotification] (
    [ID] bigint NOT NULL IDENTITY,
    [GroupID] nvarchar(450) NOT NULL,
    [LecturerID] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_GroupNotification] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_GroupNotification_Groups_GroupID] FOREIGN KEY ([GroupID]) REFERENCES [Groups] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_GroupNotification_Lecturers_LecturerID] FOREIGN KEY ([LecturerID]) REFERENCES [Lecturers] ([ID]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_Courses_LanguageTypeID] ON [Courses] ([LanguageTypeID]);
GO


CREATE INDEX [IX_Courses_LecturerID] ON [Courses] ([LecturerID]);
GO


CREATE INDEX [IX_Courses_SemesterTypeID] ON [Courses] ([SemesterTypeID]);
GO


CREATE INDEX [IX_Courses_TypeID] ON [Courses] ([TypeID]);
GO


CREATE INDEX [IX_CoursesToCheck_CourseID] ON [CoursesToCheck] ([CourseID]);
GO


CREATE INDEX [IX_GroupNotification_GroupID] ON [GroupNotification] ([GroupID]);
GO


CREATE INDEX [IX_GroupNotification_LecturerID] ON [GroupNotification] ([LecturerID]);
GO


CREATE INDEX [IX_Groups_CourseID] ON [Groups] ([CourseID]);
GO


CREATE INDEX [IX_Groups_DayID] ON [Groups] ([DayID]);
GO


CREATE INDEX [IX_Groups_LecturerID] ON [Groups] ([LecturerID]);
GO


CREATE INDEX [IX_Groups_WeekTypeID] ON [Groups] ([WeekTypeID]);
GO


CREATE INDEX [IX_Lecturers_RoleTypeID] ON [Lecturers] ([RoleTypeID]);
GO


