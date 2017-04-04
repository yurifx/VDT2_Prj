$(function () { 
    var alteraInspecao = $("#hiddenAlteraInspecao").val();
    if (alteraInspecao == 'False ') {
        $(".btnEditarEditarListarConferenciaAvarias").each(function () {
            $(this).text("Visualizar");
        });
    }
});


function Btn_Click_EditarAvaria(valor) {
    $("#avariaIdFormSubmit").val(valor);
    $("#editarAvariaForm").submit();
}


function Btn_Click_VisualizarFotos(valor) {
    $("#inspAvaria_ID_FormVisualizarFotos_input").val(valor);
    $("#VisualizarFotosForm").submit();
}

function EnviarFormularioDesabilitarBotao(e) {
    $(e).prop("disabled", "true");
    $("#formPrincipal").submit();
}