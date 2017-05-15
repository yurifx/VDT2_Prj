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
-- 13/04/2017 - Adicionar o campo: VIN
-- 17/04/2017 - LeftJoins na Avaria
-- 17/04/2017 - Adicionar RowId
-- 20/04/2017 - Adicionar HorasReparo - tbl CustoReparo
-- 24/04/2017 - Adicionar Campos de custos de peça e valor total
-- 15/05/2017 - Adicionar Campos do transportador e lote
----------------------------------------------------------------------------------------------------
(
@p_Cliente_ID Int,
@p_LocalInspecao_ID Int,
@p_LocalCheckPoint_ID Int,
@p_Data DateTime
)
AS

SET NOCOUNT ON
Select 
       iv.InspVeiculo_ID*100 + ISNULL(ia.InspAvaria_ID, 0) as RowID, --Primary key
       i.Data, 
       i.Inspecao_ID,
	   
	   li.LocalInspecao_ID       as  LocalCodigo,
	   li.Nome                   as  LocalNome,
                                     
	   lc.LocalCheckPoint_ID     as  CheckPointCodigo,
	   lc.Nome_Pt                as  CheckPointNome,
       lc.Operacao               as  Operacao,
       
       t.Transportador_ID        as TransportadorCodigo,
       t.Nome                    as TransportadorNome,
       t.Tipo                    as TransportadorTipo,

       fv.FrotaViagem_ID,
       fv.Nome                   as FrotaViagemNome,
                                    
       n.Navio_ID,
       n.Nome                    as NavioNome,                                     
	   iv.InspVeiculo_ID,
       iv.VIN_6,
	   iv.VIN,

       l.Lote                    as LoteNome,
       l.Lote_ID                 as LoteCodigo,
                                     
	   ia.InspAvaria_ID,             
	   ia.FabricaTransporte,         
	   ia.DanoOrigem,
       ia.HorasReparo,
       ia.CustoReparo,
       ia.SubstituicaoPeca,
       ia.ValorPeca,
       ia.CustoTotal,

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

/* Junções referentes - Inspeção */
Inner Join Inspecao         i    on  iv.Inspecao_ID         =    i.Inspecao_ID
Inner Join LocalInspecao    li   on  li.LocalInspecao_ID    =    i.LocalInspecao_ID
Inner Join LocalCheckPoint  lc   on  lc.LocalCheckPoint_ID  =    i.LocalCheckPoint_ID
Inner Join Transportador    t    on  i.Transportador_ID     =    t.Transportador_ID
Inner Join FrotaViagem      fv   on  fv.FrotaViagem_ID      =    i.FrotaViagem_ID
Left Join  Navio            n    on   n.Navio_ID            =    i.Navio_ID

Left Join  Lote             l    on   iv.Lote_ID            =    l.Lote_ID

/* Junções referentes - Veículo */
Inner Join Marca           ma    on  iv.Marca_ID            =   ma.Marca_ID
Inner Join Modelo          mo    on  iv.Modelo_ID           =   mo.Modelo_ID

/* Junções referentes - Avaria */

Left  Join InspAvaria      ia    on  iv.InspVeiculo_ID      =   ia.InspVeiculo_ID
Left  Join avArea           a    on   a.AvArea_ID           =   ia.AvArea_ID
Left  Join AvCondicao       c    on   c.AvCondicao_ID       =   ia.AvCondicao_ID
Left  Join AvDano           d    on   d.AvDano_ID           =   ia.AvDano_ID
Left  Join AvGravidade      g    on   g.AvGravidade_ID      =   ia.AvGravidade_ID
Left  Join AvQuadrante      q    on   q.AvQuadrante_ID      =   ia.AvQuadrante_ID
Left  Join AvSeveridade     s    on   s.AvSeveridade_ID     =   ia.AvSeveridade_ID
Left join  Custoreparo     cr    on   cr.AvArea_ID          =   ia.AvArea_ID  
                                 and  ia.AvGravidade_ID     =   cr.AvGravidade_ID


Where  i.Data               =    @p_Data
 and   i.LocalInspecao_ID   =    @p_LocalInspecao_ID
 and   i.LocalCheckPoint_ID =    @p_LocalCheckPoint_ID
 and   i.Cliente_ID         =    @p_Cliente_ID

/*
EXEC InspAvaria_Conf 1, 2, 5, '2017-05-12'
*/

-- FIM