CREATE TABLE [Lecturers] (
    [ID] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Surname] nvarchar(max) NOT NULL,
    [Mail] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NULL,
    [Title] nvarchar(max) NULL,
    CONSTRAINT [PK_Lecturers] PRIMARY KEY ([ID])
);
GO


CREATE TABLE [Passwords] (
    [ID] nvarchar(450) NOT NULL,
    [Hash] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Passwords] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Passwords_Lecturers_ID] FOREIGN KEY ([ID]) REFERENCES [Lecturers] ([ID]) ON DELETE CASCADE
);
GO


