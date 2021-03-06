USE [VDT2]
GO

If  Exists (Select Name
            From   sysobjects
            Where  Name = 'ListaVeiculosVin_Ins' and type = 'P')
    Drop Procedure dbo.ListaVeiculosVin_Ins
GO

Create Procedure dbo.ListaVeiculosVin_Ins
----------------------------------------------------------------------------------------------------
-- Insere um veículo em uma 'lista de veículos'
----------------------------------------------------------------------------------------------------
(
@p_ListaVeiculos_ID    Int,
@p_VIN                 Char(17),
@p_ListaVeiculosVin_ID Int OUTPUT
)
AS

SET NOCOUNT ON

Insert Into ListaVeiculosVin( ListaVeiculos_ID, VIN)

Values ( @p_ListaVeiculos_ID,
         @p_VIN )

Set @p_ListaVeiculosVin_ID = SCOPE_IDENTITY()

