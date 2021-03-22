CREATE TABLE [Lecturers] (
    [ID] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Surname] nvarchar(max) NOT NULL,
    [Mail] nvarchar(max) NOT NULL,
    [Role] tinyint NOT NULL,
    [Phone] nvarchar(max) NULL,
    [Title] nvarchar(max) NULL,
    CONSTRAINT [PK_Lecturers] PRIMARY KEY ([ID])
);
GO


CREATE TABLE [Courses] (
    [ID] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Type] tinyint NOT NULL,
    [Language] tinyint NOT NULL,
    [Ects] tinyint NOT NULL,
    [HoursUniversity] tinyint NOT NULL,
    [HoursStudent] tinyint NOT NULL,
    [Semester] tinyint NOT NULL,
    [Year] bigint NOT NULL,
    [WeekType] tinyint NOT NULL,
    [Start] int NOT NULL,
    [End] int NOT NULL,
    [LecturerID] nvarchar(450) NULL,
    [CourseFlow] nvarchar(max) NULL,
    [Group] nvarchar(max) NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Courses_Lecturers_LecturerID] FOREIGN KEY ([LecturerID]) REFERENCES [Lecturers] ([ID]) ON DELETE NO ACTION
);
GO


CREATE TABLE [Passwords] (
    [ID] nvarchar(450) NOT NULL,
    [Hash] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Passwords] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Passwords_Lecturers_ID] FOREIGN KEY ([ID]) REFERENCES [Lecturers] ([ID]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_Courses_LecturerID] ON [Courses] ([LecturerID]);
GO


