-------------------------------------------------------------------------
-- 27/04 Script de altera��o da tabela InspVeiculo
-- Adi��o do campo Lote_ID
-------------------------------------------------------------------------
USE VDT2

if not exists(Select COLUMN_NAME 
                From INFORMATION_SCHEMA.COLUMNS 
                Where table_name = 'InspVeiculo' 
                  and COLUMN_NAME = 'Lote_ID')

BEGIN
    ALTER TABLE InspVeiculo
    Add Lote_ID Int References Lote(Lote_ID)
END


--select * from InspVeiculo

