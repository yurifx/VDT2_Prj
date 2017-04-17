USE [VDT2] 
GO

ALTER TABLE Inspecao
  Add  Publicado Int default 0,
       PublicacaoData DateTime,
       PublicadoPor Int foreign key references Usuario(Usuario_Id)
GO

