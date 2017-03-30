USE [VDT2]
GO

If  Exists (Select Name
            From   sysobjects
            Where  Name = 'InspAvaria_LstVin' and type = 'P')
    Drop Procedure dbo.InspAvaria_LstVin
GO

Create Procedure dbo.InspAvaria_LstVin
----------------------------------------------------------------------------------------------------
-- Lista as avarias de um veículo (VIN)
-- 23/03 - Adicionando o campo DanoOrigem
-- 30/03 - Adicionando o campo Custo
----------------------------------------------------------------------------------------------------
(
@p_Cliente_ID      Int,
@p_VIN_6           Varchar(6),    -- Últimos seis caracteres do chassi
@p_VIN             Char(17)       -- Chassi completo
)
AS

SET NOCOUNT ON

Select  i.Inspecao_ID,
        i.Cliente_ID,
        i.LocalInspecao_ID,
        i.LocalCheckPoint_ID,

        v.InspVeiculo_ID,

        i.Data,
        
        v.VIN,
        v.VIN_6,

        l.Nome            as LocalInspecaoNome,

        p.Codigo          as LocalCheckPointCodigo,
        p.Nome_Pt         as LocalCheckPointNome_Pt,
        p.Nome_En         as LocalCheckPointNome_En,
        p.Nome_Es         as LocalCheckPointNome_Es,
        p.Operacao        as LocalCheckPointOperacao,

       av.InspAvaria_ID,
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

       d.Codigo    as DanoCodigo,
       d.Nome_Pt   as Dano_Pt,
       d.Nome_En   as Dano_En,
       d.Nome_Es   as Dano_Es,

       s.Codigo    as SevCodigo,
       s.Nome_Pt   as Severidade_Pt,
       s.Nome_En   as Severidade_En,
       s.Nome_Es   as Severidade_Es,

       q.Codigo    as QuadranteCodigo,
       q.Nome_Pt   as Quadrante_Pt,
       q.Nome_En   as Quadrante_En,
       q.Nome_Es   as Quadrante_Es,

       c.Codigo    as CondicaoCodigo,
       c.Nome_Pt   as Condicao_Pt,
       c.Nome_En   as Condicao_En,
       c.Nome_Es   as Condicao_Es,

	   g.Codigo    as GravidadeCodigo,
       g.Nome_Pt   as Gravidade_Pt,
       g.Nome_En   as Gravidade_En,
       g.Nome_Es   as Gravidade_Es

From       Inspecao        i
Inner Join LocalInspecao   l on i.LocalInspecao_ID    =  l.LocalInspecao_ID
Inner Join LocalCheckPoint p on i.LocalCheckPoint_ID  =  p.LocalCheckPoint_ID
Inner Join InspVeiculo     v on i.Inspecao_ID         =  v.Inspecao_ID
Inner Join InspAvaria     av on v.InspVeiculo_ID      = av.InspVeiculo_ID
Inner Join AvArea          a on av.AvArea_ID          =  a.AvArea_ID
Inner Join AvDano          d on av.AvDano_ID          =  d.AvDano_ID
Inner Join AvSeveridade    s on av.AvSeveridade_ID    =  s.AvSeveridade_ID
Inner Join AvQuadrante     q on av.AvQuadrante_ID     =  q.AvQuadrante_ID
Inner Join AvCondicao      c on av.AvCondicao_ID      =  c.AvCondicao_ID
Inner Join AvGravidade	   g on av.AvGravidade_ID	  =  g.AvGravidade_ID

Where i.Cliente_ID = @p_Cliente_ID
 and  (   ( @p_VIN    Is Not Null and v.VIN = @p_VIN )
       OR ( @p_VIN_6  Is Not Null and v.VIN_6 = @p_VIN_6 ) )

GO

--FIM
