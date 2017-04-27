-------------------------------------------------------------------------
-- 27/04 Script de altera��o da tabela InspVeiculo
-- Adi��o do campo Lote
-------------------------------------------------------------------------
USE VDT2

if not exists(Select COLUMN_NAME 
                From INFORMATION_SCHEMA.COLUMNS 
                Where table_name = 'InspVeiculo' 
                  and COLUMN_NAME = 'Lote')

BEGIN
    ALTER TABLE InspVeiculo
    Add Lote Int
END
