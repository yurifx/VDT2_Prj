use VDT2
GO

if Exists(select 1 
            From sys.objects 
            Where name like '%CustoReparo%')

BEGIN
  Truncate Table CustoReparo
  Drop 	   Table CustoReparo
END

--------------------------------------------------------------------------------
-- Custos Reparo
-- Criação: 30/03
-- Alteração - Adição do campo Quantidade Horas
--------------------------------------------------------------------------------
Create Table CustoReparo (
  CustoReparo_ID    Int Not Null Identity Primary Key,
  AvArea_ID         Int Foreign Key References AvArea(AvArea_ID),
  AvGravidade_ID    Int Foreign Key References AvGravidade(AvGravidade_ID),
  SubstituicaoPeca  Char, 
  HorasReparo       Decimal (5,2),
  Custo             Decimal (7,2)
)


-- Select * from CustoReparo