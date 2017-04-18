USE [VDT2]
GO

If  Exists (Select Name
            From   sysobjects
            Where  Name = 'InspAvaria_Cons' and type = 'P')
    Drop Procedure dbo.InspAvaria_Cons
GO


Create Procedure dbo.InspAvaria_Cons
----------------------------------------------------------------------------------------------------
-- Consulta os dados de uma avaria
----------------------------------------------------------------------------------------------------
(
    @p_Cliente_ID             Int,
    @p_Chassi                 Varchar(100),
    @p_LocalInspecao          Varchar(100),
    @p_LocalCheckPoint        Varchar(100),
    @p_Transportador          Varchar(100),
	@p_Lote                   Varchar(50),
    @p_Marca                  Varchar(100),
    @p_Modelo                 Varchar(100),
    @p_Area                   Varchar(100),
    @p_Condicao               Varchar(100),
    @p_Dano                   Varchar(100),
    @p_Quadrante              Varchar(100),
    @p_Gravidade              Varchar(100),
    @p_Severidade             Varchar(100),
    @p_TipoDefeito            Varchar(100),  --Transporte/Fábrica/Todos
    @p_DanoOrigem             Varchar(100),  -- Sim/Não/Todos
    @p_TransportadorTipo      Varchar(100),  -- Marítimo/Terrestre/Todos
    @p_FrotaViagem            Varchar(100),  -- Nome da frotaviagem
    @p_Navio                  Varchar(100),
    @p_DataInicio             Date,
    @p_DataFinal              Date
)

AS

SET NOCOUNT ON

Select 

       iv.InspVeiculo_ID*100 + ISNULL(ia.InspAvaria_ID, 0) as RowID, --Primary key

       i.Data, 
       i.Inspecao_ID,
	   i.Cliente_ID,

	   li.LocalInspecao_ID         as  LocalCodigo,
	   li.Nome                     as  LocalNome,
                                       
	   lc.LocalCheckPoint_ID       as  CheckPointCodigo,
	   lc.Nome_Pt                  as  CheckPointNome,
                                       
       t.Tipo                      as  TransportadorTipo, --TransportadorTipo
       t.Nome                      as  TransportadorNome, --TransportadorNome
                               
       fv.FrotaViagem_ID,      
       fv.Nome                     as  FrotaViagemNome,
                               
       n.Navio_ID,             
       n.Nome                      as  NavioNome,
                               
	   iv.InspVeiculo_ID           as  InspVeiculo_ID, 
       iv.VIN_6                    as  Chassi,
                                       
	   ia.InspAvaria_ID,               
	   ia.FabricaTransporte,
	   ia.DanoOrigem,
       ia.Custo,                  
                                       
	   ma.Marca_ID                 as  MarcaCodigo,
	   ma.Nome                     as  MarcaNome, 
                                       
	   mo.Modelo_ID                as  ModeloCodigo,
	   mo.Nome                     as  ModeloNome,
                                       
       a.AvArea_ID                 as  AreaCodigo,
	   a.Nome_Pt                   as  Area_Pt,
                                       
	   c.AvCondicao_ID             as  CondicaoCodigo,
	   c.Nome_Pt                   as  Condicao_Pt,
                                       
	   d.AvDano_ID                 as  DanoCodigo,
	   d.Nome_Pt                   as  Dano_Pt,
                                       
	   g.AvGravidade_ID            as  GravidadeCodigo,
	   g.Nome_Pt                   as  Gravidade_Pt,
                                  
	   q.AvQuadrante_ID            as  QuadranteCodigo,
	   q.Nome_Pt                   as  Quadrante_Pt,
                                       
       s.AvSeveridade_ID           as  SeveridadeCodigo,
	   s.Nome_Pt                   as  Severidade_Pt
                                      
From InspVeiculo iv             

Inner Join Inspecao           i     on   iv.Inspecao_ID          =      i.Inspecao_ID
Inner Join LocalInspecao     li     on   li.LocalInspecao_ID     =      i.LocalInspecao_ID
Inner Join LocalCheckPoint   lc     on   lc.LocalCheckPoint_ID   =      i.LocalCheckPoint_ID
Inner Join Transportador      t     on    t.Transportador_ID     =      i.Transportador_ID
Inner Join FrotaViagem       fv     on   fv.FrotaViagem_ID       =      i.FrotaViagem_ID
Inner Join Marca              ma    on   iv.Marca_ID             =     ma.Marca_ID
Inner Join Modelo             mo    on   iv.Modelo_ID            =     mo.Modelo_ID

Left Join Navio               n     on    n.Navio_ID             =      i.Navio_ID 
Left Join InspAvaria         ia     on   iv.InspVeiculo_ID       =     ia.InspVeiculo_ID

Left Join avArea              a    on    a.AvArea_ID            =     ia.AvArea_ID
Left Join AvCondicao          c    on    c.AvCondicao_ID        =     ia.AvCondicao_ID
Left Join AvDano              d    on    d.AvDano_ID            =     ia.AvDano_ID
Left Join AvGravidade         g    on    g.AvGravidade_ID       =     ia.AvGravidade_ID
Left Join AvQuadrante         q    on    q.AvQuadrante_ID       =     ia.AvQuadrante_ID
Left Join AvSeveridade        s    on    s.AvSeveridade_ID      =     ia.AvSeveridade_ID


Where 

@p_Cliente_ID = i.Cliente_ID

and (@p_Chassi is null
        or iv.VIN_6  like '%' + @p_Chassi + '%')
        
and (@p_LocalInspecao = '*'
        or CharIndex( '|'+ Cast(i.LocalInspecao_ID as Varchar) +'|', @p_LocalInspecao) > 0 )

and (@p_LocalCheckPoint = '*'
        or CharIndex( '|'+ Cast(i.LocalCheckPoint_ID as Varchar) +'|', @p_LocalCheckPoint) > 0 )

and (@p_Transportador = '*'
        or CharIndex( '|'+ Cast(i.Transportador_ID as Varchar) +'|', @p_Transportador) > 0 )

and (@p_Marca = '*'
        or CharIndex( '|'+ Cast(iv.Marca_ID as Varchar) +'|', @p_Marca) > 0 )

and (@p_Modelo = '*'
        or CharIndex( '|'+ Cast(iv.Modelo_ID as Varchar) +'|', @p_Modelo) > 0 )

and (@p_Area = '*'
        or CharIndex( '|'+ Cast(ia.AvArea_ID as Varchar) +'|', @p_Area) > 0 )

and (@p_Condicao = '*'
        or CharIndex( '|'+ Cast(ia.AvCondicao_ID as Varchar) +'|', @p_Condicao) > 0 )

and (@p_Dano = '*'
        or CharIndex( '|'+ Cast(ia.AvDano_ID as Varchar) +'|', @p_Dano) > 0 )

and (@p_Gravidade = '*'
        or CharIndex( '|'+ Cast(ia.AvGravidade_ID as Varchar) +'|', @p_Gravidade) > 0 )

and (@p_Quadrante = '*'
        or CharIndex( '|'+ Cast(ia.AvQuadrante_ID as Varchar) +'|', @p_Quadrante) > 0 )

and (@p_Severidade = '*'
        or CharIndex( '|'+ Cast(ia.AvSeveridade_ID as Varchar) +'|', @p_Severidade) > 0 )
        
and (@p_TipoDefeito = '*'
        or CharIndex( '|'+ Cast(ia.FabricaTransporte as Varchar) +'|', @p_TipoDefeito) > 0 )
        
and (@p_DanoOrigem = '*'
        or CharIndex( '|'+ Cast(ia.DanoOrigem as Varchar) +'|', @p_DanoOrigem) > 0 )

and (@p_TransportadorTipo = '*'
        or CharIndex( '|'+ Cast(t.Tipo as Varchar) +'|', @p_TransportadorTipo) > 0 )

and (@p_FrotaViagem is null
        or fv.Nome like '%' + @p_FrotaViagem + '%')

and (@p_Navio is null
        or n.Nome like '%' + @p_Navio + '%')

and i.Data between @p_DataInicio and @p_DataFinal

order by i.data desc, iv.VIN asc

/*
Declare @p_Chassi                 varchar(100),                                             
        @p_LocalInspecao          varchar(100),                                             
        @p_LocalCheckPoint        varchar(100),
        @p_Transportador          varchar(100),                                             
        @p_Marca                  varchar(100),                                             
        @p_Modelo                 varchar(100),                                             
        @p_Area                   varchar(100),                                             
        @p_Condicao               varchar(100),                                             
        @p_Dano                   varchar(100),                                             
        @p_Quadrante              varchar(100),                                             
        @p_Gravidade              varchar(100),                                             
        @p_Severidade             varchar(100),                                             
      --@p_Extensoes              varchar(100),                                             
        @p_TipoDefeito            varchar(100), --Transporte/Fábrica/Todos                  
        @p_DanoOrigem             varchar(100), -- Sim/Não/Todos                            
        @p_TipoTransportador      varchar(100), -- Marítimo/Terrestre/Todos                 
        @p_FrotaViagem            varchar(100),                                             
      --@p_Lote                   varchar(100),                                             
        @p_Navio                  varchar(100),                                             
        @p_DataInicio             Date,                                                     
        @p_DataFinal              Date                                                      
                                                                                            

set      @p_Chassi                 = '*'
set      @p_LocalInspecao          = '*'
set      @p_LocalCheckPoint        = '*'
set      @p_Transportador          = '*'
set      @p_Marca                  = '*'
set      @p_Modelo                 = '*'
set      @p_Area                   = '*'
set      @p_Condicao               = '*'
set      @p_Dano                   = '*'
set      @p_Quadrante              = '*'
set      @p_Gravidade              = '*'
set      @p_Severidade             = '*'
set      @p_TipoDefeito            = '*'  --|T|F|
set      @p_DanoOrigem             = '*'  --|0|1|
set      @p_TipoTransportador      = '*'  --|T|M|
set      @p_FrotaViagem            = null
--set    @p_Lote                   = '*'
set      @p_Navio                  = null
set      @p_DataInicio             = '2017/04/11'
set      @p_DataFinal              = '2017/04/11'


exec InspAvaria_Cons 
    @p_Chassi             ,
    @p_LocalInspecao      ,
    @p_LocalCheckPoint    ,   
    @p_Transportador      ,
    @p_Marca              ,  
    @p_Modelo             ,  
    @p_Area               ,  
    @p_Condicao           ,  
    @p_Dano               ,  
    @p_Quadrante          ,  
    @p_Gravidade          ,  
    @p_Severidade         ,  
    @p_TipoDefeito        ,  
    @p_DanoOrigem         ,  
    @p_TipoTransportador  ,
    @p_FrotaViagem        , 
    @p_Navio              , 
    @p_DataInicio         , 
    @p_DataFinal         
*/