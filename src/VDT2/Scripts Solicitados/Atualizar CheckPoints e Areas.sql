-----------------------------------------
-- Script "Atualiza CheckPoints.SQL
-- Data criação: 02/08
-- Solicitação: Alessandro Machado
-- Responsável: Grupo Asserth
-----------------------------------------
USE VDT2
GO

If Not Exists(Select 1 From LocalCheckPoint where LocalInspecao_ID = 2 and codigo = '18' and  Nome_Pt = 'Descida Navio Rodrimar - SSZ')
Begin 
  Insert into LocalCheckPoint (LocalInspecao_ID, Codigo, Nome_Pt, Nome_En, Nome_Es, Operacao, Ativo, Tipo) values (2, '18', 'Descida Navio Rodrimar - SSZ', 'Vessel Discharge Rodrimar - SSZ', null, 'I', 1, 'M' )
End

If Not Exists(Select 1 From LocalCheckPoint where LocalInspecao_ID = 2 and codigo = '19' and  Nome_Pt = 'Saída Rodrimar - SSZ')
Begin
  Insert into LocalCheckPoint (LocalInspecao_ID, Codigo, Nome_Pt, Nome_En, Nome_Es, Operacao, Ativo, Tipo) values (2, '19', 'Saída Rodrimar - SSZ', 'Leaving Port Rodrimar - SSZ', null, 'I', 1, 'T' )
End


If Not Exists(Select 1 From AvArea where Cliente_ID = 1 And Codigo = '119' And  Area_Pt = 'Interior Porta Malas')
Begin
  Insert into AvArea (Cliente_ID, Codigo, Area_Pt, Ativo) values (1, '119', 'Interior Porta Malas', 1)
End


If Not Exists(Select 1 from AvArea where Cliente_ID = 1 and Codigo = '120'  and  Area_Pt = 'Tampa do Gancho Reboque')
Begin
  Insert into AvArea (Cliente_ID, Codigo, Area_Pt, Ativo) values (1, '120', 'Tampa do Gancho Reboque', 1)
End
