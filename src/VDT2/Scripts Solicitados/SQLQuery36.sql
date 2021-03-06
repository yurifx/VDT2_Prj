USE [VDT2_prod_1907]
GO
/****** Object:  StoredProcedure [dbo].[ListaVeiculosVin_Ins]    Script Date: 19/07/2017 15:18:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER Procedure [dbo].[ListaVeiculosVin_Ins]
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


If exists (select 1 from ListaVeiculosVin where vin = @p_VIN)
begin{}



else
begin
Insert Into ListaVeiculosVin( ListaVeiculos_ID, VIN)

Values ( @p_ListaVeiculos_ID,
         @p_VIN )
end         


Set @p_ListaVeiculosVin_ID = SCOPE_IDENTITY()

