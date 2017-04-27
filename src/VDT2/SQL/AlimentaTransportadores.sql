Declare @comando varchar(300),
        @foreignInspecao varchar(50)

Select @foreignInspecao = CONSTRAINT_NAME From INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME LIKE '%FK__Inspecao__Transp%'

SET @COMANDO = 'ALTER TABLE Inspecao DROP CONSTRAINT '+ @foreignInspecao
EXEC(@COMANDO)

GO

Declare @comando varchar(300),
        @foreignFrotaViagem varchar(50)

Select @foreignFrotaViagem = CONSTRAINT_NAME From INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME LIKE '%FK__FrotaViag__Tran%'

SET @COMANDO = 'ALTER TABLE FrotaViagem DROP CONSTRAINT '+ @foreignFrotaViagem
EXEC(@COMANDO)

GO

TRUNCATE TABLE Transportador
GO

---Transportadores Terrestres
BEGIN TRANSACTION
Insert into Transportador values ('TRANSZERO', 'T', 1)
Insert into Transportador values ('TEGMA', 'T', 1)
Insert into Transportador values ('BRAZUL', 'T', 1)
Insert into Transportador values ('TRANSAUTO', 'T', 1)
Insert into Transportador values ('JULIO SIMÕES', 'T', 1)
COMMIT TRANSACTION

--Transportadores Marítimos
BEGIN TRANSACTION
Insert into Transportador values ('K-LINE', 'M', 1)
Insert into Transportador values ('CSAV', 'M', 1)
Insert into Transportador values ('MARUBA', 'M', 1)
Insert into Transportador values ('GRIMALDI', 'M', 1)
Insert into Transportador values ('NYK', 'M', 1)
COMMIT TRANSACTION


--Retorna com as foreigns

Alter Table Inspecao
ADD Foreign Key (Transportador_ID) references Transportador(Transportador_ID)

GO 

Alter Table FrotaViagem
ADD Foreign Key (Transportador_ID) references Transportador(Transportador_ID)