USE [VDT2]
GO

If  Exists (Select Name
            From   sysobjects
            Where  Name = 'InspAvaria_Conf' and type = 'P')
    Drop Procedure dbo.InspAvaria_Conf
GO

Create Procedure dbo.InspAvaria_Conf
----------------------------------------------------------------------------------------------------
-- Consulta os dados de uma avaria_conferencia
-- 23/03/2017 - DanoOrigem
-- 23/03/2017 - Adicionar filtro Cliente_ID
-- 30/03/2017 - Adicionar campo: Custo
----------------------------------------------------------------------------------------------------
(
@p_Cliente_ID Int,
@p_LocalInspecao_ID Int,
@p_LocalCheckPoint_ID Int,
@p_Data DateTime
)
AS

SET NOCOUNT ON
Select i.Data, 
       i.Inspecao_ID,
	   
	   li.LocalInspecao_ID       as  LocalCodigo,
	   li.Nome                   as  LocalNome,
                                     
	   lc.LocalCheckPoint_ID     as  CheckPointCodigo,
	   lc.Nome_Pt                as  CheckPointNome,
                                     
	   iv.InspVeiculo_ID         as  InspVeiculo_ID, 
       iv.VIN_6                  as  VIN_6,
                                     
	   ia.InspAvaria_ID,             
	   ia.FabricaTransporte,         
	   ia.DanoOrigem,
       ia.Custo,                
                                     
	   ma.Marca_ID               as  MarcaCodigo,
	   ma.Nome                   as  MarcaNome, 
                                     
	   mo.Modelo_ID              as  ModeloCodigo,
	   mo.Nome                   as  ModeloNome,
                                     
       a.AvArea_ID               as  AreaCodigo,
	   a.Nome_Pt                 as  Area_Pt,
                                     
	   c.AvCondicao_ID           as  CondicaoCodigo,
	   c.Nome_Pt                 as  Condicao_Pt,
                                     
	   d.AvDano_ID               as  DanoCodigo,
	   d.Nome_Pt                 as  Dano_Pt,
                                     
	   g.AvGravidade_ID          as  GravidadeCodigo,
	   g.Nome_Pt                 as  Gravidade_Pt,
                                
	   q.AvQuadrante_ID          as  QuadranteCodigo,
	   q.Nome_Pt                 as  Quadrante_Pt,
                                     
     s.AvSeveridade_ID         as  SeveridadeCodigo,
	   s.Nome_Pt                 as  Severidade_Pt
                                      
From InspVeiculo iv             

Inner Join Inspecao         i    on  iv.Inspecao_ID         =    i.Inspecao_ID
Inner Join InspAvaria       ia   on  iv.InspVeiculo_ID      =   ia.InspVeiculo_ID
Inner Join Marca            ma   on  iv.Marca_ID            =   ma.Marca_ID
Inner Join Modelo           mo   on  iv.Modelo_ID           =   mo.Modelo_ID
Inner Join avArea           a    on   a.AvArea_ID           =   ia.AvArea_ID
Inner Join AvCondicao       c    on   c.AvCondicao_ID       =   ia.AvCondicao_ID
Inner Join AvDano           d    on   d.AvDano_ID           =   ia.AvDano_ID
Inner Join AvGravidade      g    on   g.AvGravidade_ID      =   ia.AvGravidade_ID
Inner Join AvQuadrante      q    on   q.AvQuadrante_ID      =   ia.AvQuadrante_ID
Inner Join AvSeveridade     s    on   s.AvSeveridade_ID     =   ia.AvSeveridade_ID
Inner Join LocalInspecao    li   on  li.LocalInspecao_ID    =    i.LocalInspecao_ID
Inner Join LocalCheckPoint  lc   on  lc.LocalCheckPoint_ID  =    i.LocalCheckPoint_ID

Where  i.Data               =    @p_Data
 and   i.LocalInspecao_ID   =    @p_LocalInspecao_ID
 and   i.LocalCheckPoint_ID =    @p_LocalCheckPoint_ID
 and   i.Cliente_ID         =    @p_Cliente_ID

/*
EXEC InspAvaria_Conf 1, 1, '03/30/2017'
*/

-- FIM