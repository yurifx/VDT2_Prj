USE [VDT2]
GO

If  Exists (Select Name
            From   sysobjects
            Where  Name = 'Publicar' and type = 'P')
    Drop Procedure dbo.Publicar
GO

Create Procedure dbo.Publicar
----------------------------------------------------------------------------------------------------
-- Realiza a publicação das inspeções
-- 17/04/2017 Criação
-------------------------------------------------------------------------------
(
@p_Usuario_ID Int,
@p_Inspecoes Varchar(4000)  --String contendo a concatenação de inspeções delimitadas por ;  Ex: 1;2;3...100;
)
AS

SET NOCOUNT ON

DECLARE @Tamanho     Int,
        @Posicao     Int,
        @Inspecao    Int,
        @InspecaoAux VarChar(4000)

Set @InspecaoAux =  @p_Inspecoes
Set @Tamanho     =  Len(@p_Inspecoes)
Set @Posicao     =  CharIndex(';', @p_Inspecoes)

While @Posicao > 0 Begin

    Set @Inspecao = LEFT(@InspecaoAux, @Posicao-1)

    --Aqui realiza a atualizacao
    Update Inspecao set Publicado = 1, PublicadoPor = @p_Usuario_ID, PublicacaoData = SYSDATETIME()
    Where Inspecao_Id = @Inspecao


    --Pega a próxima Inspeção 
    Set @InspecaoAux =  SUBSTRING(@InspecaoAux, @Posicao+1, Len(@InspecaoAux))

    --Verifica se existe mais algum ponto e virgula, caso não exista sai do loop
    Set @Posicao =  CharIndex(';', @InspecaoAux)

End


/*
exec publicar 1, '11;3;5'
*/

