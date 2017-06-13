If  Exists (Select Name
            From   sysobjects
            Where  Name = 'Inspecao_Ins' and type = 'P')
    Drop Procedure dbo.Inspecao_Ins
GO

Create Procedure dbo.Inspecao_Ins


----------------------------------------------------------------------------------------------------
-- Insere um novo registro de inspeção (não é uma nova inspeção)
-- Uma ou mais linhas compõem uma inspeção pois:
-- a) Vários inspetores podem registrar dados ao mesmo tempo durante a mesma operação de inspeção)
-- b) Uma inspeção pode envolver vários transportadores
-- 12/06 - Inspeção, verifica se já existe uma inspeção com aquela data, local e checkpoint informados
-- 13/06 - Adicionar também Navio/Transportador/FrotaViagem
----------------------------------------------------------------------------------------------------
(
@p_Cliente_ID         Int,
@p_LocalInspecao_ID   Int,
@p_LocalCheckPoint_ID Int,
@p_Transportador_ID   Int,
@p_FrotaViagem_ID     Int,
@p_Navio_ID           Int,
@p_Usuario_ID         Int,  -- Identificação do inspetor
@p_Data               Date, -- Data da inspeção
@p_Inspecao_ID        Int OUTPUT
)
AS

SET NOCOUNT ON

-- Verifica se o 'check-point' corresponde ao 'local'
If Not Exists ( Select 1
                From  LocalCheckPoint
                Where LocalCheckPoint_ID = @p_LocalCheckPoint_ID
                 and  LocalInspecao_ID   = @p_LocalInspecao_ID ) Begin

  THROW 50000, 'LocalCheckPoint incompatível com LocalInspecao',1
End

-- Verifica se 'trota/viagem' corresponde ao 'transportador'
If Not Exists ( Select 1
                From  FrotaViagem
                Where FrotaViagem_ID   = @p_FrotaViagem_ID
                 and  Transportador_ID = @p_Transportador_ID ) Begin

  THROW 50000, 'FrotaViagem incompatível com Transportador',1
End

--Verifica se já existe inspeção nesta data com localinspecao e localcheckpoint informados
Select @p_Inspecao_ID = Inspecao_ID From Inspecao 
            Where 
                LocalInspecao_ID   =  @p_LocalInspecao_ID
            And LocalCheckPoint_ID =  @p_LocalCheckPoint_ID
            And Cliente_ID         =  @p_Cliente_ID
            And Transportador_ID   =  @p_Transportador_ID
            And FrotaViagem_ID     =  @p_FrotaViagem_ID
            And Navio_ID           =  @p_Navio_ID
            And Data               =  @p_Data
            

if @p_Inspecao_ID IS NULL
    begin
        -- Insere a nova linha
        Insert Into Inspecao ( Cliente_ID,
                               LocalInspecao_ID,
                               LocalCheckPoint_ID,
                               Transportador_ID,
                               FrotaViagem_ID,
                               Navio_ID,
                               Usuario_ID,
                               Data )
        Values ( @p_Cliente_ID,
                 @p_LocalInspecao_ID,
                 @p_LocalCheckPoint_ID,
                 @p_Transportador_ID,
                 @p_FrotaViagem_ID,
                 @p_Navio_ID,
                 @p_Usuario_ID,
                 @p_Data )

        Set @p_Inspecao_ID = SCOPE_IDENTITY()
    end