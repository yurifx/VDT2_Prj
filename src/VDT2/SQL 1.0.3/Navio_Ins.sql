USE [VDT2]
GO

If  Exists (Select Name
            From   sysobjects
            Where  Name = 'Navio_Ins' and type = 'P')
    Drop Procedure dbo.Navio_Ins
GO


Create Procedure dbo.Navio_Ins
----------------------------------------------------------------------------------------------------
-- Insere um novo navio
-- 13/06 - Verifica se já existe navio com este nome
----------------------------------------------------------------------------------------------------
(
@p_Nome     Varchar(100),
@p_Navio_ID Int OUTPUT
)
AS

SET NOCOUNT ON

Select  @p_Navio_ID = Navio_Id from Navio 
 Where  Nome like '%'@p_Nome'%'

IF @p_Navio_ID IS NULL
    Begin
        Insert Into Navio ( Nome )
        Values ( @p_Nome )
        Set @p_Navio_ID = SCOPE_IDENTITY()
    End