USE [VDT2] 
GO

TRUNCATE TABLE ListaVeiculosVin
GO

TRUNCATE TABLE ListaVeiculos
GO

ALTER TABLE ListaVeiculos
  Add  LocalCheckPoint_ID   Int         NOT NULL,
       Lote_ID              Int        
GO