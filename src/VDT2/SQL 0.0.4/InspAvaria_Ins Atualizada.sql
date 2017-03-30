USE [VDT2]
GO

If  Exists (Select Name
            From   sysobjects
            Where  Name = 'InspAvaria_InsTT' and type = 'P')
    Drop Procedure dbo.InspAvaria_InsTT
GO

Create Procedure dbo.InspAvaria_InsTT
----------------------------------------------------------------------------------------------------
-- Insere um novo registro de avaria de um veículo
----------------------------------------------------------------------------------------------------
(
@p_InspVeiculo_ID     Int,
@p_AvArea_ID          Int,
@p_AvDano_ID          Int,
@p_AvSeveridade_ID    Int,
@p_AvQuadrante_ID     Int,
@p_AvGravidade_ID     Int,
@p_AvCondicao_ID      Int,
@p_FabricaTransporte  Char(1), -- F:Defeito de Fábrica  T:Avaria de Transporte
@p_DanoOrigem         Bit,
@p_CustoReparo        Decimal (7,2),
@p_InspAvaria_ID      Int OUTPUT
)
AS

SET NOCOUNT ON

Declare @Inspecao_ID Int
Select  @Inspecao_ID = Inspecao_ID From InspVeiculo Where InspVeiculo_ID = @p_InspVeiculo_ID

Declare @Cliente_ID Int
Select  @Cliente_ID = Cliente_ID From Inspecao Where Inspecao_ID = @Inspecao_ID


--Inicio dos testes atualizações:
Declare @Custo Int
If @p_CustoReparo IS NULL
BEGIN
  Set @Custo = (select custo from CustoReparo cr where cr.AvArea_ID = @p_AvArea_ID and cr.AvGravidade_ID = @p_AvGravidade_ID)
END
Else 
  Set @Custo = @p_CustoReparo

-- Verifica se a 'área' corresponde ao 'cliente'
If Not Exists ( Select 1
                From  AvArea
                Where AvArea_ID   = @p_AvArea_ID
                 and  Cliente_ID  = @Cliente_ID ) Begin

  THROW 50000, 'Área incompatível com Cliente',1
End

-- Verifica se o 'dano' corresponde ao 'cliente'
If Not Exists ( Select 1
                From  AvDano
                Where AvDano_ID   = @p_AvDano_ID
                 and  Cliente_ID  = @Cliente_ID ) Begin

  THROW 50000, 'Dano incompatível com Cliente',1
End

-- Verifica se a 'severidade' corresponde ao 'cliente'
If Not Exists ( Select 1
                From  AvSeveridade
                Where AvSeveridade_ID = @p_AvSeveridade_ID
                 and  Cliente_ID      = @Cliente_ID ) Begin

  THROW 50000, 'Severidade incompatível com Cliente',1
End

-- Verifica se o 'quadrante' corresponde ao 'cliente'
If Not Exists ( Select 1
                From  AvQuadrante
                Where AvQuadrante_ID = @p_AvQuadrante_ID
                 and  Cliente_ID     = @Cliente_ID ) Begin

  THROW 50000, 'Quadrante incompatível com Cliente',1
End

-- Verifica se a 'gravidade' corresponde ao 'cliente'
If Not Exists ( Select 1
                From  AvGravidade
                Where AvGravidade_ID = @p_AvGravidade_ID
                 and  Cliente_ID     = @Cliente_ID ) Begin

  THROW 50000, 'Gravidade incompatível com Cliente',1
End




-- Insere a nova avaria
Insert Into InspAvaria ( InspVeiculo_ID,
                         AvArea_ID,
                         AvDano_ID,
                         AvSeveridade_ID,
                         AvQuadrante_ID,
                         AvGravidade_ID,
                         AvCondicao_ID,
                         FabricaTransporte,
                         DanoOrigem,
                         Custo)

Values ( @p_InspVeiculo_ID,
         @p_AvArea_ID,
         @p_AvDano_ID,
         @p_AvSeveridade_ID,
         @p_AvQuadrante_ID,
         @p_AvGravidade_ID,
         @p_AvCondicao_ID,
         @p_FabricaTransporte,
         @p_DanoOrigem,
         @Custo )

Set @p_InspAvaria_ID = SCOPE_IDENTITY()

GO

/*
DECLARE 
@p_InspVeiculo_ID     Int,
@p_AvArea_ID          Int,
@p_AvDano_ID          Int,
@p_AvSeveridade_ID    Int,
@p_AvQuadrante_ID     Int,
@p_AvGravidade_ID     Int,
@p_AvCondicao_ID      Int,
@p_FabricaTransporte  Char(1), -- F:Defeito de Fábrica  T:Avaria de Transporte
@p_DanoOrigem         Bit,
@p_CustoReparo        Decimal (7,2)

SET @p_InspVeiculo_ID = 1
SET @p_AvArea_ID = 5
SET @p_AvDano_ID = 1
SET @p_AvSeveridade_ID = 1
SET @p_AvQuadrante_ID = 1
SET @p_AvGravidade_ID = 1
SET @p_AvCondicao_ID = 1
SET @p_FabricaTransporte = 1
SET @p_DanoOrigem = 1
SET @p_CustoReparo = 100.00

Exec InspAvaria_InsTT
 @p_InspVeiculo_ID,
 @p_AvArea_ID,
 @p_AvDano_ID,
 @p_AvSeveridade_ID,
 @p_AvQuadrante_ID, 
 @p_AvGravidade_ID, 
 @p_AvCondicao_ID,
 @p_FabricaTransporte,
 @p_DanoOrigem,
 @p_CustoReparo,
 null

 */