EXEC sys.sp_configure N'remote access', N'1'
GO
RECONFIGURE WITH OVERRIDE
GO

INSERT INTO Lecturers (ID, Name, Surname, Title, Mail, Phone)
VALUES ("JB", "Kazik", "Mordewicz", "doc", "jb@op.pl", "696 420 366");