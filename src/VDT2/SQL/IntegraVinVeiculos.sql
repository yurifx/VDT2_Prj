USE [VDT2]
GO

If  Exists (Select Name
            From   sysobjects
            Where  Name = 'IntegraVinVeiculos' and type = 'P')
    Drop Procedure dbo.IntegraVinVeiculos
GO

Create Procedure dbo.IntegraVinVeiculos
----------------------------------------------------------------------------------------------------
-- Realiza update do VIN na tabela InspVeiculo recebendo os dados da Tabela ListaVeiculos 
-- 17/04/2017 Alterações para mostrar pendências após update
----------------------------------------------------------------------------------------------------
(
@p_Cliente_ID  Int,
@p_LocalInspecao_ID Int,
@p_LocalCheckPoint_ID Int,
@p_DataInspecao DateTime
)
AS

SET NOCOUNT ON

Update InspVeiculo  
   Set VIN = lvv.VIN

From InspVeiculo v

Inner Join Inspecao           i on i.Inspecao_ID       = v.Inspecao_ID
Inner Join ListaVeiculos     lv on i.Cliente_ID        = lv.Cliente_ID and lv.LocalInspecao_ID = i.LocalInspecao_ID
Inner Join ListaVeiculosVin lvv on lv.ListaVeiculos_ID = lvv.ListaVeiculos_ID

Where i.Cliente_ID = @p_Cliente_ID
 and   i.LocalInspecao_ID = @p_LocalInspecao_ID
 and   v.VIN_6 = lvv.VIN_6
 and   v.VIN is null



Select 'L' as Tipo, lvv.VIN_6
From ListaVeiculosVin lvv

inner join ListaVeiculos lv on lv.ListaVeiculos_ID = lvv.ListaVeiculos_ID
left  join ( select iv.InspVeiculo_ID, iv.vin_6 from InspVeiculo iv 
                   inner join Inspecao i      on iv.Inspecao_ID = i.Inspecao_ID 
                   where i.LocalInspecao_ID   = @p_LocalInspecao_ID
                    and  i.LocalCheckPoint_ID = @p_LocalCheckPoint_ID
                    and  i.Data = @p_DataInspecao) as Veiculos

      on lvv.VIN_6 = Veiculos.VIN_6
      where lv.LocalInspecao_ID = @p_LocalInspecao_ID
       and  lv.LocalCheckPoint_ID = @p_LocalCheckPoint_ID
       and  Veiculos.InspVeiculo_ID is null

union 

select 'V' as Tipo, iv.VIN_6 from InspVeiculo iv

inner join Inspecao i on i.Inspecao_ID = iv.Inspecao_ID
    where iv.VIN is null
		  and i.LocalInspecao_ID = @p_LocalInspecao_ID
		  and i.LocalCheckPoint_ID = @p_LocalCheckPoint_ID
	      and i.Data = @p_DataInspecao


/*
exec IntegraVinVeiculos 1, 2,5, '2017-04-17'*/