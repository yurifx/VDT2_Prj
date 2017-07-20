$(function () {  // $(document).ready()
    console.log("Inicializou jquery ok - ConferenciaTelaInicial.js");

    var ListaLocalCheckPoint = $("#ListaLocalCheckPoint");
    var ListaLocalInspecao = $("#ListaLocalInspecao");

    //Recebe o valor atual do ListaLocalInspecao
    var LocalInspecao = $("#ListaLocalInspecao").prop('value');
    
    //Caso o local inspeção seja != 'Selecione o Local', carrega os dados do checkpoint daquele local.
    //Fizemos isto pois quando o usuário clicava em voltar, a combo do localcheckpoint estava completamente prenchida, sendo que só precisava estar os dados 
    // do localcheckpoint daquele referido local.
    if (typeof (LocalInspecao != 'undefined')) {

        if (LocalInspecao != 'Selecione Local') {
            PreencheListaCheckPoint();
        }
    }
});



function ValidarFormularioIndex() {
    var localSelecionado = $("#ListaLocalInspecao").val();
    var checkPointSelecionado = $("#ListaLocalCheckPoint").val();
    var dataSelecionada = $("#data").val();

    //Valida Local
    if (localSelecionado == 0 || localSelecionado == null) {
        alert("Selecionar Local");
        $("#ListaLocalInspecao").css("color", "red");
        return false;
    }
    else {
        $("#ListaLocalInspecao").css("color", "black");
    }

    //Valida CheckPoint
    if (checkPointSelecionado == 0 || checkPointSelecionado == null) {
        alert("Selecionar checkPoint");
        $("#ListaLocalCheckPoint").css("color", "red");
        return false;
    } else {
        $("#ListaLocalCheckPoint").css("color", "black");
    }

    //Valida Data
    if (dataSelecionada == 0 || dataSelecionada == null) {
        alert("Selecionar Data");
        $("#data").css("color", "red");
        return false;
    } else {
        $("#data").css("color", "black");
    }
}

//Função acionada toda vez que o usuário altera o local de inspeção.
function PreencheListaCheckPoint() {
    var localInspecao_ID = $("#ListaLocalInspecao").val();
    var LocalCheckPoint = $("#ListaLocalCheckPoint");
    var path = window.location.pathname;
    var i = path.indexOf('EditarInspecao');
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