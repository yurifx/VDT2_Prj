 --------------------------------------------------
 -- Script: AtualizaInspecoesRIG.sql 
 -- Solicitado por: Alessandro Machado
 -- Data: 19/07/2017
 -- Grupo Asserth: Yuri Vasconcelos
 --------------------------------------------------
 
BEGIN TRY
    BEGIN TRANSACTION 
       
        Update Inspecao
        Set PublicacaoData = null, publicado = 0, publicadopor = null
        Where
                    LocalInspecao_ID   = 1 
                and LocalCheckPoint_ID = 2
                and Data in ('2017-07-03', '2017-07-10', '2017-07-11') 
                and PublicacaoData    Is Not Null
    COMMIT
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
        DECLARE @ErrorState INT = ERROR_STATE()

    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH