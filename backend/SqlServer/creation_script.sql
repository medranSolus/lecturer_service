CREATE TABLE [Lecturers] (
    [ID] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NULL,
    [Surname] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [Mail] nvarchar(max) NULL,
    [Phone] nvarchar(max) NULL,
    CONSTRAINT [PK_Lecturers] PRIMARY KEY ([ID])
);
GO