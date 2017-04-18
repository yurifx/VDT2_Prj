USE [VDT2]
GO

If  Exists (Select 1
            From   sysobjects  Where  Name = 'Lote' and type = 'U')
    Drop Table dbo.Lote
GO


CREATE TABLE Lote 
(
    Lote_ID int identity not null,
    Lote    Varchar(500)
)
