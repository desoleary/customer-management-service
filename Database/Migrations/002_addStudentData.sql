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