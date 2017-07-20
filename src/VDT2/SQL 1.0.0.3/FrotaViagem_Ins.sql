USE [VDT2]
GO

If  Exists (Select Name
            From   sysobjects
            Where  Name = 'FrotaViagem_Ins' and type = 'P')
    Drop Procedure dbo.FrotaViagem_Ins
GO


Create Procedure dbo.FrotaViagem_Ins

----------------------------------------------------------------------------------------------------
-- Insere uma nova frota/viagem
----------------------------------------------------------------------------------------------------
(
@p_Transportador_ID   Int,
@p_Nome               Varchar(100),
@p_FrotaViagem_ID     Int OUTPUT
)
AS

SET NOCOUNT ON

Select @p_FrotaViagem_ID = FrotaViagem_ID From FrotaViagem 
            Where 
                Transportador_ID   =  @p_Transportador_ID
            And Nome               = @p_Nome

if @p_FrotaViagem_ID IS NULL
    Begin
        Insert Into FrotaViagem ( Transportador_ID, Nome )
        Values ( @p_Transportador_ID,
                 @p_Nome )

        Set @p_FrotaViagem_ID = SCOPE_IDENTITY()
    End