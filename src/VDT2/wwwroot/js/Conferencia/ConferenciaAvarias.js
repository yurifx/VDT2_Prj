﻿$(function () { 
    var alteraInspecao = $("#hiddenAlteraInspecao").val();
    if (alteraInspecao == 'False ') {
        $(".btnEditarEditarListarConferenciaAvarias").each(function () {
            $(this).text("Visualizar");
        });
    }
});


function Btn_Click_EditarAvaria(valor) {

    var veiculo_avaria = valor.split(';');
    var veiculo = veiculo_avaria[0];
    var avaria = veiculo_avaria[1];

    $("#inputVeiculoIdFormSubmit").val(veiculo);
    $("#inputAvariaIdFormSubmit").val(avaria);
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