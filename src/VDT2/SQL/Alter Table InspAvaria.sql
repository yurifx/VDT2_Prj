USE [VDT2] 
GO

Truncate Table InspAvaria
GO

ALTER TABLE InspAvaria
DROP COLUMN Custo
GO

ALTER TABLE InspAvaria
  Add  
       HorasReparo int,
       CustoReparo decimal(7,2) default 0,
       SubstituicaoPeca bit default 0, -- 0 = não, 1 = sim
       ValorPeca decimal (7,2) default 0,
       CustoTotal decimal (7,2) default 0
GO