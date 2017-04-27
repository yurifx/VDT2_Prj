-------------------------------------------------------------------------
-- 27/04 Script de altera��o da tabela InspAvaria
-- Adi��o dos campos de custo
-------------------------------------------------------------------------

USE [VDT2] 
GO

Truncate Table InspAvaria
GO

/* Verifica se existe a coluna custo, caso exista, dropa */

if exists(Select COLUMN_NAME 
            From INFORMATION_SCHEMA.COLUMNS 
            Where table_name = 'InspAvaria' and COLUMN_NAME = 'Custo')

BEGIN
    ALTER TABLE InspAvaria
    DROP COLUMN Custo
END


/* Verifica se n�o existe as colunas de valores da avaria, caso n�o exista, cria */
IF not exists(Select COLUMN_NAME 
            From INFORMATION_SCHEMA.COLUMNS 
            Where table_name = 'InspAvaria' and 
            (   COLUMN_NAME = 'HorasReparo' 
             or COLUMN_NAME = 'CustoReparo'
             or COLUMN_NAME =  'SubstituicaoPeca'
             or COLUMN_NAME =  'ValorPeca'
             or COLUMN_NAME =  'CustoTotal'))
BEGIN
ALTER TABLE InspAvaria
  ADD  
       HorasReparo int,
       CustoReparo decimal(7,2) default 0,
       SubstituicaoPeca bit default 0, /* 0 = n�o, 1 = sim */
       ValorPeca decimal (7,2) default 0,
       CustoTotal decimal (7,2) default 0
END