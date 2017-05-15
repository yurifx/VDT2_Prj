USE [VDT2] 
GO

If  Exists (Select 1
            From   sysobjects  Where  Name = 'ListaVeiculosVin' and type = 'U')
    Drop Table dbo.ListaVeiculosVin
GO


If  Exists (Select 1
            From   sysobjects  Where  Name = 'ListaVeiculos' and type = 'U')
    Drop Table dbo.ListaVeiculos
GO

Create Table ListaVeiculos (
  ListaVeiculos_ID   Int Not Null Identity Primary Key,
  Cliente_ID         Int Not Null References Cliente(Cliente_ID),
  Usuario_ID         Int Not Null References Usuario(Usuario_ID),  -- Identificação do usuário que incluiu os dados do arquivo na base de dados
  NomeArquivo        Varchar(50)   Not Null,  -- Nome do arquivo sem as pastas
  DataHoraInclusao   SmallDateTime Not Null Default GetDate(),  -- Data e hora da inclusão do arquivo na base de dados
  LocalInspecao_ID   Int Not Null References LocalInspecao(LocalInspecao_ID),
  LocalCheckPoint_ID Int Not Null References LocalCheckPoint(LocalCheckPoint_ID),
  Tipo               Char Not Null,
  Lote_ID            Int Not Null References Lote(Lote_ID)
)
GO


Create Table ListaVeiculosVin (
  ListaVeiculosVin_ID Int Not Null Identity Primary Key,
  ListaVeiculos_ID    Int Not Null References ListaVeiculos(ListaVeiculos_ID),
  VIN                 Char(17) Not Null,          -- Chassi completo
  VIN_6               AS Right(VIN, 6) PERSISTED  -- Últimos seis caracteres do chassi
)
GO