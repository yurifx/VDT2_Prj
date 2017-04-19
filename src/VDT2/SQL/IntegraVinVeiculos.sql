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
-- 19/04/2017 Alterações para registrar o lote
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
   Set VIN     = lvv.VIN, 
       Lote_ID = lv.Lote_ID
   
From InspVeiculo v

Inner Join Inspecao           i  on i.Inspecao_ID       =   v.Inspecao_ID
Inner Join ListaVeiculos     lv  on i.Cliente_ID        =  lv.Cliente_ID and lv.LocalInspecao_ID = i.LocalInspecao_ID
Inner Join ListaVeiculosVin lvv  on lv.ListaVeiculos_ID = lvv.ListaVeiculos_ID

Where i.Cliente_ID = @p_Cliente_ID
 and   i.LocalInspecao_ID = @p_LocalInspecao_ID
 and   v.VIN_6 = lvv.VIN_6
 and   v.VIN is null

 
--Recebe a lista de Veículos encontrados na tabela ListaVeículosVin, porém não há registro de Inspeção
Select 'L' as Tipo, lvv.VIN_6
From ListaVeiculosVin lvv

Inner Join ListaVeiculos lv on lv.ListaVeiculos_ID = lvv.ListaVeiculos_ID
Left  Join ( Select iv.InspVeiculo_ID, iv.vin_6 from InspVeiculo iv 
                   inner join Inspecao i        on iv.Inspecao_ID = i.Inspecao_ID 
                   Where i.LocalInspecao_ID   = @p_LocalInspecao_ID
                    and  i.LocalCheckPoint_ID = @p_LocalCheckPoint_ID) as Veiculos

                on lvv.VIN_6 = Veiculos.VIN_6

        Where lv.LocalInspecao_ID = @p_LocalInspecao_ID
         and  lv.LocalCheckPoint_ID = @p_LocalCheckPoint_ID
         and  Veiculos.InspVeiculo_ID is null
Union 

--Recebe a lista de veículos encontrados que não estão em nenhuma Lista, Ou seja, Não tem VIN;
Select 'V' as Tipo, iv.VIN_6 from InspVeiculo iv

inner join Inspecao i on i.Inspecao_ID = iv.Inspecao_ID
    where iv.VIN is null
		  and i.LocalInspecao_ID = @p_LocalInspecao_ID
		  and i.LocalCheckPoint_ID = @p_LocalCheckPoint_ID
	      and i.Data = @p_DataInspecao


/*
exec IntegraVinVeiculos 1, 2,5, '2017-04-17'*/