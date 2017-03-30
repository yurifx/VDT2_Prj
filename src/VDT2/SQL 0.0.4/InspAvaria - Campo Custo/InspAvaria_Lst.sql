USE [VDT2]
GO

If  Exists (Select Name
            From   sysobjects
            Where  Name = 'InspAvaria_Lst' and type = 'P')
    Drop Procedure dbo.InspAvaria_Lst
GO

Create Procedure dbo.InspAvaria_Lst
----------------------------------------------------------------------------------------------------
-- Lista as avarias de um veículo (InspVeiculo_ID)
----------------------------------------------------------------------------------------------------
(
@p_InspVeiculo_ID Int
)
AS

SET NOCOUNT ON

Select InspAvaria_ID,
       InspVeiculo_ID,
       AvArea_ID,
       AvDano_ID,
       AvSeveridade_ID,
       AvQuadrante_ID,
       AvGravidade_ID,
       AvCondicao_ID,
       FabricaTransporte,
       DanoOrigem,
       Custo

From  InspAvaria
Where InspVeiculo_ID = @p_InspVeiculo_ID

GO

/*
EXEC InspAvaria_Lst @p_InspVeiculo_ID = 1
*/

-- FIM
