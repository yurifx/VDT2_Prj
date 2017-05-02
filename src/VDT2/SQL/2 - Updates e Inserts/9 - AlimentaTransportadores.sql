--------------------------------------------------------------------
-- 27/04 - Script de alimentação de transportadores
--------------------------------------------------------------------
USE VDT2

/* Deleta as Foreign Key's para poder fazer o truncate */ 

Declare @Comando varchar(300),
        @ForeignInspecao varchar(50)

Select @ForeignInspecao = CONSTRAINT_NAME From INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME LIKE '%FK__Inspecao__Transp%'

SET @Comando = 'ALTER TABLE Inspecao DROP CONSTRAINT '+ @ForeignInspecao
EXEC(@Comando)

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
Insert into Transportador values ('TRANSZERO', 'T', 1)
Insert into Transportador values ('TEGMA', 'T', 1)
Insert into Transportador values ('BRAZUL', 'T', 1)
Insert into Transportador values ('TRANSAUTO', 'T', 1)
Insert into Transportador values ('JULIO SIMÕES', 'T', 1)


--Transportadores Marítimos
Insert into Transportador values ('K-LINE', 'M', 1)
Insert into Transportador values ('CSAV', 'M', 1)
Insert into Transportador values ('MARUBA', 'M', 1)
Insert into Transportador values ('GRIMALDI', 'M', 1)
Insert into Transportador values ('NYK', 'M', 1)


--Realiza a criação das foreigns key's que foram deletadas
Alter Table Inspecao
ADD Foreign Key (Transportador_ID) references Transportador(Transportador_ID)
GO 

Alter Table FrotaViagem
ADD Foreign Key (Transportador_ID) references Transportador(Transportador_ID)


/*
select * from Transportador 
select * from INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
WHERE 
(CONSTRAINT_NAME LIKE '%FK__Inspecao__Transp%'
or CONSTRAINT_NAME LIKE '%FK__FrotaViag__Tran%')
--count deve ser 2
*/

