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

grant select on Student to $DBUSER
