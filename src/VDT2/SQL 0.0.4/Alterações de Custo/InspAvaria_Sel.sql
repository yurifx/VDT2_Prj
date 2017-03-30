USE [VDT2]
GO

If  Exists (Select Name
            From   sysobjects
            Where  Name = 'InspAvaria_Sel' and type = 'P')
    Drop Procedure dbo.InspAvaria_Sel
GO

Create Procedure dbo.InspAvaria_Sel
----------------------------------------------------------------------------------------------------
-- Consulta os dados de uma avaria
-- 08/03/2017 - Atualização: Adição dos campos com nomes: a.Nome_pt, es, en e seus respectivos joins
-- 08/03/2017 - Atualização: Adição do campos de Inspeção - ID e VIN_6
-- 09/03/2017 - Adição dos campos de codigo
-- 23/03/2017 - Adição do campo DanoOrigem
-- 30/03/2017 - Adição do campo Custo
----------------------------------------------------------------------------------------------------
(
@p_InspAvaria_ID Int
)
AS

SET NOCOUNT ON

Select av.InspAvaria_ID,
       av.InspVeiculo_ID,
       av.AvArea_ID,
       av.AvDano_ID,
       av.AvSeveridade_ID,
       av.AvQuadrante_ID,
       av.AvGravidade_ID,
       av.AvCondicao_ID,
       av.FabricaTransporte,
       av.DanoOrigem,
       av.Custo,

       a.Codigo    as AreaCodigo,
       a.Nome_Pt   as Area_Pt,
       a.Nome_En   as Area_En,
       a.Nome_Es   as Area_Es,

       c.Codigo    as CondicaoCodigo,
       c.Nome_Pt   as Condicao_Pt,
       c.Nome_En   as Condicao_En,
       c.Nome_Es   as Condicao_Es,

       d.Codigo    as DanoCodigo,
       d.Nome_Pt   as Dano_Pt,
       d.Nome_En   as Dano_En,
       d.Nome_Es   as Dano_Es,

       g.Codigo    as GravidadeCodigo,
       g.Nome_Pt   as Gravidade_Pt,
       g.Nome_En   as Gravidade_En,
       g.Nome_Es   as Gravidade_Es,

       q.Codigo    as QuadranteCodigo,
       q.Nome_Pt   as Quadrante_Pt,
       q.Nome_En   as Quadrante_En,
       q.Nome_Es   as Quadrante_Es,

       s.Codigo    as SeveridadeCodigo,
       s.Nome_Pt   as Severidade_Pt,
       s.Nome_En   as Severidade_En,
       s.Nome_Es   as Severidade_Es,

       iv.Inspecao_ID as Inspecao_ID,
       iv.VIN_6 as VIN_6
					
From  InspAvaria av

Inner Join AvArea        a   on av.AvArea_ID       = a.AvArea_ID
Inner Join AvCondicao    c   on av.AvCondicao_ID   = c.AvCondicao_ID
Inner Join AvDano        d   on av.AvDano_ID       = d.AvDano_ID
Inner Join AvGravidade   g   on av.AvGravidade_ID  = g.AvGravidade_ID
Inner Join AvQuadrante   q   on av.AvQuadrante_ID  = q.AvQuadrante_ID
Inner Join AvSeveridade  s   on av.AvSeveridade_ID = s.AvSeveridade_ID
Inner Join InspVeiculo   iv  on iv.InspVeiculo_ID  = Av.InspVeiculo_ID

Where InspAvaria_ID = @p_InspAvaria_ID

GO

/*
EXEC InspAvaria_Sel @p_InspAvaria_ID = 1
*/

-- FIM