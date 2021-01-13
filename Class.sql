--CREATE DATABASE SchoolDB
--GO
USE [SchoolDB]
GO
--დაწერეთ sql რომელიც ააგებს შესაბამის table-ებს.
CREATE TABLE Pupil (
	PupilID INT PRIMARY KEY IDENTITY(1,1),
	FirstName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	Gender VARCHAR(1) NULL,
	Degree INT NOT NULL
);
CREATE TABLE Teacher(
	TeacherID INT PRIMARY KEY IDENTITY(1,1),
	FirstName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	Gender VARCHAR(1) NOT NULL,
	Subj NVARCHAR(20) NOT NULL,

);

CREATE TABLE Class(
	ClassID INT NOT NULL,
	Degree INT NOT NULL,
	SubjectName NVARCHAR(20) NOT NULL,
	TeacherID INT NOT NULL,
	PupilID INT NOT NULL,
	FOREIGN KEY(PupilID) REFERENCES Pupil(PupilID),
	FOREIGN KEY(TeacherID) REFERENCES Teacher(TeacherID),
);

INSERT INTO [Pupil]  (FirstName, LastName, Gender, Degree) VALUES (N'თორნიკე', N'ქურდაძე', 'M', 12);
INSERT INTO [Pupil]  (FirstName, LastName, Gender, Degree) VALUES (N'გიორგი', N'მაისურაძე', 'M', 11);
INSERT INTO [Pupil]  (FirstName, LastName, Gender, Degree) VALUES (N'დათა', N'თუთაშხია', 'M', 11);
INSERT INTO [Pupil]  (FirstName, LastName, Gender, Degree) VALUES (N'ანი', N'ხელაძე', 'F', 11);
INSERT INTO [Pupil]  (FirstName, LastName, Gender, Degree) VALUES (N'მარი', N'კაპანაძე', 'F', 12);
INSERT INTO [Pupil]  (FirstName, LastName, Gender, Degree) VALUES (N'ვაჟა', N'ჭავჭავაძე', 'M', 12);


INSERT INTO [Teacher]  (FirstName, LastName, Gender, Subj) VALUES (N'ლაურა', N'ლობჟანიძე', 'F', N'ქართული');
INSERT INTO [Teacher]  (FirstName, LastName, Gender, Subj) VALUES (N'კახა', N'ვაშაძე', 'M', N'მათემატიკა');
INSERT INTO [Teacher]  (FirstName, LastName, Gender, Subj) VALUES (N'მაია', N'ვაშაყმაძე', 'F', N'ინგლისური');
INSERT INTO [Teacher]  (FirstName, LastName, Gender, Subj) VALUES (N'ნანა', N'ქავთარაძე', 'F', N'ისტორია');


INSERT INTO [Class] (ClassID, Degree,SubjectName,TeacherID,PupilID) VALUES (1 , 12 , N'მათემატიკა' , 2 , 1 );
INSERT INTO [Class] (ClassID, Degree,SubjectName,TeacherID,PupilID) VALUES (1 , 12 , N'მათემატიკა' , 2 , 1 );
INSERT INTO [Class] (ClassID, Degree,SubjectName,TeacherID,PupilID) VALUES (2 , 11 , N'ინგლისური' , 3 , 2 );
INSERT INTO [Class] (ClassID, Degree,SubjectName,TeacherID,PupilID) VALUES (3 , 11 , N'ისტორია' , 4 , 2 );
INSERT INTO [Class] (ClassID, Degree,SubjectName,TeacherID,PupilID) VALUES (4 , 11 , N'ქართული' , 1 , 2 );
INSERT INTO [Class] (ClassID, Degree,SubjectName,TeacherID,PupilID) VALUES (5 , 11 , N'მათემატიკა' , 1 , 2 );

SELECT *
FROM Class;
--DROP TABLE Teacher ;
--DROP TABLE Class;
--DROP TABLE Pupil;
--დაწერეთ sql რომელიც დააბრუნებს ყველა მასწავლებლებს, რომელიც ასწავლის მოსწავლეს, რომელის სახელია: „გიორგი“ 
SELECT T.FirstName, T.LastName 
FROM Teacher T 
JOIN Class C 
ON T.TeacherID = C.TeacherID 
JOIN Pupil P 
ON P.PupilID = C.PupilID 
WHERE P.FirstName = N'გიორგი'
