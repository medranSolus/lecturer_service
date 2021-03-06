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

INSERT INTO Day (Type, Name)
VALUES
    (0, 'Monday'),
    (1, 'Tuesday'),
    (2, 'Wednesday'),
    (3, 'Thursday'),
    (4, 'Friday');

INSERT INTO Department (Type, Name)
VALUES
    (0, 'W1'),
    (1, 'W2'),
    (2, 'W3'),
    (3, 'W4'),
    (4, 'W5'),
    (5, 'W6'),
    (6, 'W7'),
    (7, 'W8'),
    (8, 'W9'),
    (9, 'W10'),
    (10, 'W11'),
    (11, 'W12'),
    (12, 'W13'),
    (13, 'Filia w Jeleniej Górze'),
    (14, 'Filia w Wałbrzychu'),
    (15, 'Filia w Legnicy');

INSERT INTO Lang (Type, Name)
VALUES
    (0, 'Polish'),
    (1, 'English'),
    (2, 'Deutch');

INSERT INTO Role (Type, Name)
VALUES
    (0, 'Admin'),
    (1, 'Normal');

INSERT INTO WeekType (Type, Name)
VALUES
    (0, 'All'),
    (1, 'Even'),
    (2, 'Odd');
GO

-- Example data
INSERT INTO Semesters (ID, IsWinter, StartYear, StartMonth, StartDay, EndYear, EndMonth, EndDay)
VALUES
    ('Zimowy 2020-2021', 1, 2020, 9, 2, 2021, 2, 2),
    ('Letni 2020-2021', 0, 2021, 3, 1, 2021, 6, 22),
    ('Zimowy 2021-2022', 1, 2021, 9, 1, 2022, 2, 4);
GO

INSERT INTO Lecturers (ID, Name, Surname, Mail, RoleTypeID, Phone, Title)
VALUES
    ('jb2137', 'Kazik', 'Nergal', 'jb@op.pl', 0, '696 420 366', 'pOTĘŻNY Decymator'),
    ('dziekanat', 'Anna', 'Nowak', 'an@op.pl', 0, '575 191 801', 'Pani z dziekanatu'),
    ('sumiks', 'Bezi', 'Typ', 'bt@op.pl', 1, '915 150 151', 'Jakiś typ');
GO
INSERT INTO Passwords (ID, Pass)
VALUES
    ('jb2137', 'DC5944BD2A2FB8C6F569090305B7CF2EC600C40DA0A0E5C5E2CB3B14C2F0EE68'), -- TAJNE
    ('dziekanat', 'DC5944BD2A2FB8C6F569090305B7CF2EC600C40DA0A0E5C5E2CB3B14C2F0EE68'), -- TAJNE
    ('sumiks', 'DC5944BD2A2FB8C6F569090305B7CF2EC600C40DA0A0E5C5E2CB3B14C2F0EE68'); -- TAJNE
GO

INSERT INTO Courses
    (ID, Accepted, Name, DepartmentID, TypeID, LanguageTypeID, Ects,
    HoursUniversity, HoursStudent, SemesterID, Year, LecturerID, CourseGroup)
VALUES
    ('INEU15003P', 1, 'Zastosowanie informatyki w medycynie', 3, 4, 0, 3,
    15, 70, 'Letni 2020-2021', 2021, 'jb2137', 'INEU15003Wsp'),
    ('INEU17002P', 0, 'Zastosowanie informatyki w gospodarce', 3, 4, 0, 2,
    15, 70, 'Letni 2020-2021', 2021, 'dziekanat', 'INEU17002Wp');
GO
INSERT INTO Groups
    (ID, CourseID, StudentsCount, Room, Building, WeekTypeID, StartMonth, StartDay,
    EndMonth, EndDay, DayID, StartHour, StartMinute, EndHour, EndMinute, LecturerID)
VALUES
    ('E08-15e', 'INEU15003P', 25, 'L2.8', 'C-16', 1, 3, 4,
    6, 10, 2, 18, 55, 20, 35, 'jb2137'),
    ('E08-15f', 'INEU15003P', 25, 'L2.6', 'C-16', 1, 3, 4,
    6, 10, 2, 18, 55, 20, 35, 'sumiks'),
    ('E08-15g', 'INEU15003P', 25, 'L2.4', 'C-16', 1, 3, 4,
    6, 10, 1, 17, 05, 18, 45, 'jb2137'),
    ('E08-20a', 'INEU17002P', 18, 'L2.8', 'C-16', 2, 3, 1,
    6, 7, 4, 18, 55, 20, 35, NULL),
    ('E08-20b', 'INEU17002P', 18, 'L2.6', 'C-16', 2, 3, 1,
    6, 7, 3, 18, 55, 20, 35, 'sumiks'),
    ('E08-20c', 'INEU17002P', 18, 'L2.4', 'C-16', 2, 3, 1,
    6, 7, 0, 17, 05, 18, 45, NULL);
GO