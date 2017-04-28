USE [VDT2] 
GO

ALTER TABLE Inspecao
  
  Add  Publicado      Int default 0,
       PublicacaoData DateTime,
       PublicadoPor   Int Foreign Key References Usuario(Usuario_Id)
GO

/*
select * from inspecao
*/