EXEC sys.sp_configure N'remote access', N'1'
GO
RECONFIGURE WITH OVERRIDE
GO

-- Setup enums in code
INSERT INTO CourseType (Type, Name)
VALUES
    (0, 'Other'),
    (1, 'Lecture'),
    (2, 'Lab'),
    (3, 'Exercise'),
    (4, 'Project'),
    (5, 'Seminar');

INSERT INTO Lang (Type, Name)
VALUES
    (0, 'Polish'),
    (1, 'English'),
    (2, 'Deutch');

INSERT INTO Role (Type, Name)
VALUES
    (0, 'Admin'),
    (1, 'Normal');

INSERT INTO Semester (Type, Name)
VALUES
    (0, 'Winter'),
    (1, 'Summer');

INSERT INTO WeekType (Type, Name)
VALUES
    (0, 'All'),
    (1, 'Even'),
    (2, 'Odd');
GO

-- Example data
INSERT INTO Lecturers (ID, Name, Surname, Mail, RoleTypeID, Phone, Title)
VALUES ('jb2137', 'Kazik', 'Nergal', 'jb@op.pl', 0, '696 420 366', 'pOTĘŻNY Decymator');
GO

INSERT INTO Passwords (ID, Pass)
VALUES ('jb2137', 'dc5944bd2a2fb8c6f569090305b7cf2ec600c40da0a0e5c5e2cb3b14c2f0ee68'); -- TAJNE
GO

INSERT INTO Courses
    (ID, Name, TypeID, LanguageTypeID, Ects, HoursUniversity, HoursStudent,
    SemesterTypeID, Year, StartMonth, StartDay, EndMonth, EndDay, LecturerID, CourseGroup)
VALUES
    ('INEU15003P', 'Zastosowanie informatyki w medycynie', 4, 0, 3, 15, 70,
    1, 2021, 3, 4, 6, 10, 'jb2137', 'INEU15003Wsp');
GO

INSERT INTO Groups
    (ID, CourseID, StudentsCount, Room, Building, WeekTypeID,
    StartHour, StartMinute, EndHour, EndMinute, LecturerID)
VALUES
    ('E08-15e', 'INEU15003P', 25, 'L2.8', 'C-16', 1,
    18, 55, 20, 35, NULL);
GO