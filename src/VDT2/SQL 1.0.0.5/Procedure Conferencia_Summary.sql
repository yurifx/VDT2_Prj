If  Exists (Select Name
            From   sysobjects
            Where  Name = 'Conferencia_Summary' and type = 'P')
    Drop Procedure dbo.Conferencia_Summary
GO

Create Procedure dbo.Conferencia_Summary

----------------------------------------------------------------------------------------------------
-- 18-07-2017: Verifica a quantidade de veículos, avarias em um determinado local, checkpoint e data
----------------------------------------------------------------------------------------------------
(
@p_Cliente_ID int,
@p_LocalInspecao int,
@p_LocalCheckpoint int,
@p_Data datetime
)
AS

DECLARE @IDs table (InspVeiculo_ID int, VIN varchar(17))

Insert into @IDs (InspVeiculo_ID, VIN) 
select distinct iv.InspVeiculo_ID, IsNull(VIN, VIN_6) From InspVeiculo iv
Inner Join  Inspecao           i      on   iv.Inspecao_ID          =      i.Inspecao_ID
Left Join   InspAvaria         ia     on   iv.InspVeiculo_ID       =     ia.InspVeiculo_ID

Where 

i.Cliente_ID = @p_Cliente_ID
        
and i.LocalInspecao_ID = @p_LocalInspecao

and i.LocalCheckPoint_ID = @p_LocalCheckPoint

and i.Data = @p_Data


select 'TodosVeiculos' as Tipo,  Count(VIN) Total 
from @IDs

union

select 'VeiculosComAvarias' as Tipo, count(distinct iv.InspVeiculo_ID) Total from inspVeiculo iv
inner join @IDs tmp on iv.InspVeiculo_ID = tmp.InspVeiculo_ID
inner join InspAvaria ia on iv.InspVeiculo_ID = ia.InspVeiculo_ID

union

select 'VeiculosSemAvaria' as Tipo, count(distinct iv.InspVeiculo_ID) Total from inspVeiculo iv
inner join @IDs tmp on iv.InspVeiculo_ID = tmp.InspVeiculo_ID
where not exists (select 1 from inspAvaria ia where ia.InspVeiculo_ID = iv.InspVeiculo_ID)

union 

select 'QuantidadeAvarias' as Tipo, count(inspAvaria_id) Total from InspAvaria ia
inner join @IDs tmp on ia.InspVeiculo_ID = tmp.InspVeiculo_ID

union 

select 'QuantidadeAvariasTransporte' as Tipo, count(inspAvaria_id) Total from InspAvaria ia
inner join @IDs tmp on ia.InspVeiculo_ID = tmp.InspVeiculo_ID
where ia.FabricaTransporte = 'T'

union

select 'QuantidadeAvariasFabrica' as Tipo, count(inspAvaria_id) Total from InspAvaria ia
inner join @IDs tmp on ia.InspVeiculo_ID = tmp.InspVeiculo_ID
where ia.FabricaTransporte = 'F'