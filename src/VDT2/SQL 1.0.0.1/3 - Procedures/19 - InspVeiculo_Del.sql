USE [VDT2]

if exists (Select Name
            From   sysobjects
            Where  Name = 'InspVeiculo_Del' and type = 'P')
            Drop Procedure dbo.InspVeiculo_Del
GO


Create Procedure InspVeiculo_Del
----------------------------------------------------------------------------------------------------
-- Remove um veículo do banco de dados
----------------------------------------------------------------------------------------------------
(
@p_InspVeiculo_ID Int
)
AS

SET NOCOUNT ON

if exists(select 1 from InspAvaria where InspVeiculo_ID = @p_InspVeiculo_ID)
THROW 50000, 'Não é possivel deletar Veículo com Avaria',1

Delete From InspVeiculo Where InspVeiculo_ID = @p_InspVeiculo_ID




