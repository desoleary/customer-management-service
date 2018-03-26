-- Script generated at 2010-02-07T19:50:55





--------------- Fragment begins: #1: 001_addStudentTable.sql ---------------
INSERT INTO changelog (change_number, delta_set, start_dt, applied_by, description) VALUES (1, 'Main', getdate(), user_name(), '001_addStudentTable.sql')
GO


-- Change script: #1: 001_addStudentTable.sql
CREATE TABLE Student (
[ID] INTEGER IDENTITY(1,1) NOT NULL PRIMARY KEY,
[FirstName] VARCHAR(20),
[LastName] VARCHAR(20),
[DateOfBirth] DATETIME,
[PhoneNumber] VARCHAR(20),
[AddressLine1] VARCHAR(50),
[AddressLine2] VARCHAR(50),
[Grade] VARCHAR(20)
)

grant select on Student to acme

UPDATE changelog SET complete_dt = getdate() WHERE change_number = 1 AND delta_set = 'Main'
GO

--------------- Fragment ends: #1: 001_addStudentTable.sql ---------------

--------------- Fragment begins: #2: 002_addStudentData.sql ---------------
INSERT INTO changelog (change_number, delta_set, start_dt, applied_by, description) VALUES (2, 'Main', getdate(), user_name(), '002_addStudentData.sql')
GO


-- Change script: #2: 002_addStudentData.sql
INSERT INTO Student
           ([FirstName]
           ,[LastName]
           ,[DateOfBirth]
           ,[PhoneNumber]
           ,[AddressLine1]
           ,[AddressLine2]
           ,[Grade])
     VALUES
           ('Joe',
           'Blog',
           CAST('20070529 00:00:00' AS datetime),
           '403-123-1234',
           'address line one',
           'address line two',
           'Kindergarden')

UPDATE changelog SET complete_dt = getdate() WHERE change_number = 2 AND delta_set = 'Main'
GO

--------------- Fragment ends: #2: 002_addStudentData.sql ---------------
