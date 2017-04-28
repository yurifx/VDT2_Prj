-------------------------------------------------------------------------
-- 27/04 Script de alteração da tabela LocalCheckPoint
-- Adição do campo LocalCheckPoint
-------------------------------------------------------------------------

if not exists(Select COLUMN_NAME 
                From INFORMATION_SCHEMA.COLUMNS 
                Where table_name = 'LocalCheckPoint' 
                  and COLUMN_NAME = 'Tipo')

Alter table LocalCheckPoint
Add Tipo char

