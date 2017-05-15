--select * from LocalCheckPoint
USE VDT2
UPDATE LocalCheckPoint  SET TIPO = 'M' 
WHERE
(
       Nome_Pt Like      'Descida Navio - RIG'
    or Nome_Pt Like      'Subida Navio - RIG'
    or Nome_Pt Like      'Subida Navio Deicmar - SSZ'
    or Nome_Pt Like      'Subida Navio Rodrimar - SSZ'
    or Nome_Pt Like      'Subida Navio TEV - GJA'
    or Nome_Pt Like      'Subida Navio - SSO'
    or Nome_Pt Like      'Descida Navio - SSO'
    or Nome_Pt Like      'Descida Navio - SUA'
)

GO

UPDATE LocalCheckPoint  SET TIPO = 'T' 
WHERE
(
       Nome_Pt Like      'Saida Porto - RIG'
    or Nome_Pt Like      'Entrada Porto - RIG'
    or Nome_Pt Like      'Entrada Deicmar - SSZ'
    or Nome_Pt Like      'Entrada Rodrimar - SSZ'
    or Nome_Pt Like      'Entrada TEV - GJA'
    or Nome_Pt Like      'Entrada Porto - SSO'
    or Nome_Pt Like      'Saída Porto - SSO'
    or Nome_Pt Like      'Entrada Patio PPV - SUA'
    or Nome_Pt Like      'Saida Patio PPV - SUA'
)