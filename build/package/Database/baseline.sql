if exists(select * from master.dbo.syslogins where [name] = 'acme') 
	exec sp_droplogin 'acme'
GO
exec sp_addlogin 'acme', 'acme'
GO
exec sp_adduser 'acme', 'acme', 'public'
GO

CREATE TABLE changelog (
  change_number INTEGER NOT NULL,
  delta_set VARCHAR(10) NOT NULL,
  start_dt DATETIME NOT NULL,
  complete_dt DATETIME NULL,
  applied_by VARCHAR(100) NOT NULL,
  description VARCHAR(500) NOT NULL
)
GO

ALTER TABLE changelog ADD CONSTRAINT Pkchangelog PRIMARY KEY (change_number, delta_set)
GO
