-----------------------------------------------------------------------------------------------
--Scripts cria��o dos Custos reparo referente a planilha enviada pelo Alessandro
-- Cria��o: 30/03
-- Altera��o: 20/04 - Composi��o de 'horas reparo'
-----------------------------------------------------------------------------------------------

USE VDT2
TRUNCATE table CustoReparo
GO

Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '03' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '03' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  5.50,   489.5    From   AvArea a, AvGravidade g     where a.Codigo = '03' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '04' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '04' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  5.50,   489.5    From   AvArea a, AvGravidade g     where a.Codigo = '04' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '05' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '05' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  4.00,   356      From   AvArea a, AvGravidade g     where a.Codigo = '05' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '06' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '06' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  4.00,   356      From   AvArea a, AvGravidade g     where a.Codigo = '06' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '07' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '07' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  4.00,   356      From   AvArea a, AvGravidade g     where a.Codigo = '07' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '08' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '08' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  4.00,   356      From   AvArea a, AvGravidade g     where a.Codigo = '08' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '09' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '09' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  7.00,   623      From   AvArea a, AvGravidade g     where a.Codigo = '09' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '10' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '10' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  7.00,   623      From   AvArea a, AvGravidade g     where a.Codigo = '10' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '11' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '11' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  7.00,   623      From   AvArea a, AvGravidade g     where a.Codigo = '11' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '12' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '12' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  7.00,   623      From   AvArea a, AvGravidade g     where a.Codigo = '12' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '13' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '13' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  7.00,   623      From   AvArea a, AvGravidade g     where a.Codigo = '13' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '14' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '14' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  5.50,   489.5    From   AvArea a, AvGravidade g     where a.Codigo = '14' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  4.00,   356      From   AvArea a, AvGravidade g     where a.Codigo = '15' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  4.00,   356      From   AvArea a, AvGravidade g     where a.Codigo = '15' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  7.00,   623      From   AvArea a, AvGravidade g     where a.Codigo = '15' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '16' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '16' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  5.50,   489.5    From   AvArea a, AvGravidade g     where a.Codigo = '16' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  4.00,   356      From   AvArea a, AvGravidade g     where a.Codigo = '17' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  4.00,   356      From   AvArea a, AvGravidade g     where a.Codigo = '17' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  7.00,   623      From   AvArea a, AvGravidade g     where a.Codigo = '17' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '18' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '18' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '18' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '19' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '19' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '19' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '20' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '20' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '20' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '21' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '21' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '21' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '22' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '22' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.50,   133.5    From   AvArea a, AvGravidade g     where a.Codigo = '22' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '24' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '24' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '24' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '25' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '25' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '25' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '26' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '26' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '26' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '27' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '27' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  7.00,   623      From   AvArea a, AvGravidade g     where a.Codigo = '27' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '30' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '30' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '30' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '31' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '31' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '31' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '33' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '33' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  4.00,   356      From   AvArea a, AvGravidade g     where a.Codigo = '33' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '34' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '34' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  4.00,   356      From   AvArea a, AvGravidade g     where a.Codigo = '34' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '35' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '35' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  4.00,   356      From   AvArea a, AvGravidade g     where a.Codigo = '35' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '36' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '36' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  4.00,   356      From   AvArea a, AvGravidade g     where a.Codigo = '36' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  4.00,   356      From   AvArea a, AvGravidade g     where a.Codigo = '37' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  4.00,   356      From   AvArea a, AvGravidade g     where a.Codigo = '37' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  9.00,   801      From   AvArea a, AvGravidade g     where a.Codigo = '37' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '38' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '38' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.50,   133.5    From   AvArea a, AvGravidade g     where a.Codigo = '38' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '39' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '39' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.50,   133.5    From   AvArea a, AvGravidade g     where a.Codigo = '39' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '41' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '41' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '41' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '42' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '42' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '42' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '43' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '43' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '43' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '44' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '44' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '44' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '52' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '52' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  5.50,   489.5    From   AvArea a, AvGravidade g     where a.Codigo = '52' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '60' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '60' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '60' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '61' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '61' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '61' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '62' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '62' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '62' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '63' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '63' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '63' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '64' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '64' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '64' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '65' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '65' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '65' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.50,   133.5    From   AvArea a, AvGravidade g     where a.Codigo = '69' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.50,   133.5    From   AvArea a, AvGravidade g     where a.Codigo = '69' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '69' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.50,   133.5    From   AvArea a, AvGravidade g     where a.Codigo = '70' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.50,   133.5    From   AvArea a, AvGravidade g     where a.Codigo = '70' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '70' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.50,   133.5    From   AvArea a, AvGravidade g     where a.Codigo = '71' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.50,   133.5    From   AvArea a, AvGravidade g     where a.Codigo = '71' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '71' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.50,   133.5    From   AvArea a, AvGravidade g     where a.Codigo = '72' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.50,   133.5    From   AvArea a, AvGravidade g     where a.Codigo = '72' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '72' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.50,   133.5    From   AvArea a, AvGravidade g     where a.Codigo = '73' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.50,   133.5    From   AvArea a, AvGravidade g     where a.Codigo = '73' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '73' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.50,   133.5    From   AvArea a, AvGravidade g     where a.Codigo = '74' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.50,   133.5    From   AvArea a, AvGravidade g     where a.Codigo = '74' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '74' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  0.50,   44.5     From   AvArea a, AvGravidade g     where a.Codigo = '81' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  0.50,   44.5     From   AvArea a, AvGravidade g     where a.Codigo = '81' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '81' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '86' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '86' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '86' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '87' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '87' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '87' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '88' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '88' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '88' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '89' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '89' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  3.00,   267      From   AvArea a, AvGravidade g     where a.Codigo = '89' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '100' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '100' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '100' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '101' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '101' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '101' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '102' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '102' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '102' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '103' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '103' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '103' and g.Codigo = 'Z'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '112' and g.Codigo = 'S'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  1.00,   89       From   AvArea a, AvGravidade g     where a.Codigo = '112' and g.Codigo = 'Y'
Insert Into CustoReparo (AvArea_ID, avGravidade_id, HorasReparo, custo) Select a.AvArea_Id, g.AvGravidade_ID,  2.00,   178      From   AvArea a, AvGravidade g     where a.Codigo = '112' and g.Codigo = 'Z'
                                                                                                        
/*
use vdt2

Select   cr.CustoReparo_ID,
         cr.AvArea_ID,
         a.Area_Pt, 
         a.Local_Pt, 
         a.Lado_Pt, 
         cr.AvGravidade_ID, 
         g.Nome_Pt,
         g.Codigo,
         cr.custo

From CustoReparo       cr
Inner Join avArea      a      on  a.AvArea_ID      = cr.AvArea_ID
Inner Join AvGravidade g      on  g.AvGravidade_ID = cr.AvGravidade_ID
*/