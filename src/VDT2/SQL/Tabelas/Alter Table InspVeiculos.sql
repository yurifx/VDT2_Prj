-------------------------------------------------------------------------
-- 27/04 Script de alteração da tabela InspVeiculo
-- Adição do campo Lote
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
