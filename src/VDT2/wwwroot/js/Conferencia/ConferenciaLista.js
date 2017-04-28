$(function () {  // $(document).ready()
    console.log("Inicializou jquery ok - Conferência PackingList");

    $("#btnUpload").click(function () {

        if (!$("#radioPacking").is(':checked')) {
            $("#lblConfirmacaoLote").text("");
            $("#lblConfirmacaoLote").append("Confirmação de lote: " + $("#inputLote").val());
            $("#lblConfirmacaoLote").css('font-weight', 'bold');
        }
        else {
            $("#lblConfirmacaoLote").text("Deseja confirmar?");
            $("#lblConfirmacaoLote").css('font-weight', 'bold');
        }
        
    })


    $("#radioPacking").on('click', function () {
        $("#divLote").hide();
        $("#inputLote").val("");
    });

    $("#radioLoading").on('click', function () {
        $("#divLote").show();
    });

    $("#radioDischarging").on('click', function () {
        $("#divLote").show();
    });
    
})


//Recebe dados do localcheckpoint, dependendo do local de inspeção. via AJAX
function PreencheListaCheckPoint() {
    var localInspecao_ID = $("#ListaLocalInspecao").val();
    var LocalCheckPoint = $("#ListaLocalCheckPoint");
    var path = window.location.pathname;
    var i = path.indexOf('EditarInspecao');
    var url = "";

    url = path + '../../../Inspecao/RecebeDadosLocalCheckPoint';

    $.getJSON(
        //'/Inspecao/RecebeDadosLocalCheckPoint',
        url,
        { 'localInspecao_ID': localInspecao_ID },
        function (response) {
            LocalCheckPoint.empty();
            $.each(response, function (index, item) {
                $(document.createElement('option'))
                    .attr('value', item.codigo)
                    .text(item.nome_Pt)
                    .appendTo(LocalCheckPoint);
            });
        });
}



function Enviar() {

    var qtdArquivos = document.getElementById('inputFilesEnviarLista').files.length;
    var Cliente = $("#ListaCliente").val();
    var LocalInspecao = $("#ListaLocalInspecao").val();
    var LocalCheckPoint = $("#ListaLocalCheckPoint").val();
    var Lote = $("#inputLote").val();

    if (Cliente == 0 || Cliente == null) {
        alert("Por favor informe um Cliente");
        return false;
    }

    if (LocalInspecao == 0 || LocalInspecao == null) {
        alert("Por favor informe um Local de Inspeção");
        return false;
    }

    if (LocalCheckPoint == 0 || LocalCheckPoint == null) {
        alert("Por favor informe um Local de CheckPoint");
        return false;
    }
    
    if (!$("#radioPacking").is(":checked")){
    if (Lote == "") {
        alert("Por favor informe um Lote");
        return false;
        }
    }

    if (qtdArquivos == 0) {
        alert("Por favor informe um Arquivo");
        return false;
    }

    $("#frmEnviarLista").submit();

}