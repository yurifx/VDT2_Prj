USE [VDT2]
GO

If  Exists (Select Name
            From   sysobjects
            Where  Name = 'ListaVeiculos_Ins' and type = 'P')
    Drop Procedure dbo.ListaVeiculos_Ins
GO

Create Procedure dbo.ListaVeiculos_Ins
----------------------------------------------------------------------------------------------------
-- Insere os dados gerais de uma lista de veículos
-- 22/03 - Inclusão dos campos LocalInspecao_ID e Tipo
-- 17/04 - Inclusão do campo LocalCheckPoint_ID
-- 18/04 - Inclusão do campo Lote e alterações
----------------------------------------------------------------------------------------------------
(
@p_Cliente_ID          Int,
@p_Usuario_ID          Int,          -- Identificação do usuário que está incluindoi os dados do arquivo na base de dados
@p_NomeArquivo         Varchar(50),  -- Nome do arquivo sem as pastas
@p_LocalInspecao_ID    Int,
@p_LocalCheckPoint_ID  Int,
@p_Tipo                Char(1),
@p_Lote                Varchar(50),
@p_ListaVeiculos_ID    Int OUTPUT,
@p_Lote_ID             Int OUTPUT
)
AS

SET NOCOUNT ON

--Verifica se existe um lote com o nome informado, caso não exista insere novo;
if @p_Lote is not null Begin

Select @p_Lote_ID = Lote_ID from Lote where Lote = @p_Lote

    if @p_Lote_ID is null Begin
        Insert into Lote (Lote) values (@p_Lote)
        Set @p_Lote_ID = SCOPE_IDENTITY()
    End

End

Insert Into ListaVeiculos ( Cliente_ID, Usuario_ID, NomeArquivo, LocalInspecao_ID, Tipo, Lote_ID, LocalCheckPoint_ID)

Values ( @p_Cliente_ID,
         @p_Usuario_ID,
         @p_NomeArquivo,
		 @p_LocalInspecao_ID,
         @p_Tipo,
         @p_Lote_ID,
		 @p_LocalCheckPoint_ID )

Set @p_ListaVeiculos_ID = SCOPE_IDENTITY()

