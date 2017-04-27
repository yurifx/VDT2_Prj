USE [VDT2]
GO

If  Exists (Select 1
            From   sysobjects  Where  Name = 'Lote' and type = 'U')
    Drop Table dbo.Lote
GO


CREATE TABLE Lote 
(
    Lote_ID Int Identity Not Null Primary Key,
    Lote    Varchar(500)
)
