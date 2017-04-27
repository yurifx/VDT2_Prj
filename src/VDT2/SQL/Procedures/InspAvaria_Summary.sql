USE [VDT2]
GO

If  Exists (Select Name
            From   sysobjects
            Where  Name = 'InspAvaria_Summary' and type = 'P')
    Drop Procedure dbo.InspAvaria_Summary
GO

Create Procedure InspAvaria_Summary 

-------------------------------------------------------------------
-- 20/04 Criação da procedure
-------------------------------------------------------------------

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

DECLARE @IDs table (InspVeiculo_ID int, VIN varchar(17))

Insert into @IDs (InspVeiculo_ID, VIN) 
select distinct iv.InspVeiculo_ID, IsNull(VIN, VIN_6)From InspVeiculo iv
Inner Join  Inspecao           i      on   iv.Inspecao_ID          =      i.Inspecao_ID
Inner Join  Transportador      t      on    t.Transportador_ID     =      i.Transportador_ID
Inner Join  FrotaViagem       fv      on   fv.FrotaViagem_ID       =      i.FrotaViagem_ID
Left  Join  Lote               l      on   iv.Lote_ID              =      l.Lote_ID
Left  Join  Navio              n      on    n.Navio_ID             =      i.Navio_ID 
Left  Join  InspAvaria        ia      on   iv.InspVeiculo_ID       =     ia.InspVeiculo_ID


Where 

i.Publicado = 1

and  @p_Cliente_ID = i.Cliente_ID

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

and (@p_Lote is null
        or l.Lote  like '%' + @p_Lote + '%')

and i.Data between @p_DataInicio and @p_DataFinal



Select 1 as ID, 'TodosVeiculos' as Tipo, count(distinct VIN) Total 
From @IDs

Union

Select 2 ID, 'VeiculosComAvarias' as Tipo, count(distinct iv.InspVeiculo_ID) Total from inspVeiculo iv
Inner Join @IDs tmp on iv.InspVeiculo_ID = tmp.InspVeiculo_ID
Inner Join InspAvaria ia on iv.InspVeiculo_ID = ia.InspVeiculo_ID

Union

Select 3 as ID, 'VeiculosSemAvaria' as Tipo, count(distinct iv.InspVeiculo_ID) Total from inspVeiculo iv
Inner Join @IDs tmp on iv.InspVeiculo_ID = tmp.InspVeiculo_ID
Where Not Exists (select 1 from inspAvaria ia where ia.InspVeiculo_ID = iv.InspVeiculo_ID)

Union 

Select 4 as ID, 'QuantidadeAvarias' as Tipo, count(inspAvaria_id) Total from InspAvaria ia
Inner Join @IDs tmp on ia.InspVeiculo_ID = tmp.InspVeiculo_ID


Union 

Select 5 as ID, 'QuantidadeAvariasTransporte' as Tipo, count(inspAvaria_id) Total from InspAvaria ia
Inner Join @IDs tmp on ia.InspVeiculo_ID = tmp.InspVeiculo_ID
Where ia.FabricaTransporte = 'T'


Union

Select 6 as ID, 'QuantidadeAvariasFabrica' as Tipo, count(inspAvaria_id) Total from InspAvaria ia
Inner Join @IDs tmp on ia.InspVeiculo_ID = tmp.InspVeiculo_ID
Where ia.FabricaTransporte = 'F'


/*
USE VDT2
Declare @p_Cliente_ID             Int,
        @p_Chassi                 varchar(100),                                             
        @p_LocalInspecao          varchar(100),                                             
        @p_LocalCheckPoint        varchar(100),
        @p_Transportador          varchar(100),
        @p_Lote                   Varchar(50),                                             
        @p_Marca                  varchar(100),                                             
        @p_Modelo                 varchar(100),                                             
        @p_Area                   varchar(100),                                             
        @p_Condicao               varchar(100),                                             
        @p_Dano                   varchar(100),                                             
        @p_Quadrante              varchar(100),                                             
        @p_Gravidade              varchar(100),                                             
        @p_Severidade             varchar(100),                                             
        @p_TipoDefeito            varchar(100),   --Transporte/Fábrica/Todos                  
        @p_DanoOrigem             varchar(100),   -- Sim/Não/Todos                            
        @p_TransportadorTipo      varchar(100),   -- Marítimo/Terrestre/Todos                 
        @p_FrotaViagem            varchar(100),                                                                                    
        @p_Navio                  varchar(100),                                             
        @p_DataInicio             Date,                                                     
        @p_DataFinal              Date                                                      
                                                                                            
set      @p_Cliente_ID             = 1
set      @p_Chassi                 = null
set      @p_LocalInspecao          = '*'
set      @p_LocalCheckPoint        = '*'
set      @p_Transportador          = '*'
set      @p_Lote                   = null
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
set      @p_TransportadorTipo      = '*'  --|T|M|
set      @p_FrotaViagem            = null
set      @p_Navio                  = null
set      @p_DataInicio             = '2017-04-27'
set      @p_DataFinal              = '2017-04-27'


exec InspAvaria_Summary
    @p_Cliente_ID         ,
    @p_Chassi             ,
    @p_LocalInspecao      ,
    @p_LocalCheckPoint    ,   
    @p_Transportador      ,
    @p_Lote               ,
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
    @p_TransportadorTipo  ,
    @p_FrotaViagem        , 
    @p_Navio              , 
    @p_DataInicio         , 
    @p_DataFinal         
    
*/