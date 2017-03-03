$(function () {  // $(document).ready()
    console.log("Inicializou jquery ok");
    $('#VIN_6').val("");
    $('#ObservacoesTxtArea').val("");
    $('#ListaMarca').val(0);
    $('#ListaModelo').val(0);
});


function TerrestreOuMaritimo() {
    
    var selecionado = $('#ListaTransportador').val();
    console.log("Dropdownlist, transportador selecionado " + selecionado);
    var arr = selecionado.split('_');
    var tipo = arr[1];
    console.log("Tipo é: " + tipo);

    if (tipo == 'M') {
        $("#lblFrotaViagem").html('Viagem');
        $("#Navio").show();
        $("#lblNavio").show();
    }
    else if (tipo == 'T'){
         $("#Navio").hide();
         $("#lblNavio").hide();
         $("#lblFrotaViagem").html('Frota');
    }

   
}

function ValidarFormularioIndex() {

    var clienteSelecionado = $("#ListaCliente").val();
    var localSelecionado = $("#ListaLocalInspecao").val();
    var checkPointSelecionado = $("#ListaLocalCheckPoint").val();
    var transportadorSelecionado = $("#ListaTransportador").val();

    //Valida Cliente
    if (clienteSelecionado == 0 || clienteSelecionado == null) {
        console.log("clienteSelecionado está nulo");
        alert("Selecionar Cliente");
        $("#ListaCliente").css("color", "red");
        //$("#ListaClienteLabelID").css("color", "red");
        return false;
    } else {
        $("#ListaCliente").css("color", "black");
    }

    //Valida Local
    if (localSelecionado == 0 || localSelecionado == null) {
        console.log("localSelecionado está nulo");
        alert("Selecionar Local");
        $("#ListaLocalInspecao").css("color", "red");
        return false;
    } else {
        $("#ListaLocalInspecao").css("color", "black");
    }


    //Valida CheckPoint
    if (checkPointSelecionado == 0 || checkPointSelecionado == null) {
        console.log("checkPointSelecionado está nulo");
        alert("Selecionar checkPoint");
        $("#ListaLocalCheckPoint").css("color", "red");
        return false;
    } else {
        $("#ListaLocalInspecao").css("color", "black");
    }

    //Valida Transportador
    if (transportadorSelecionado == 0 || transportadorSelecionado == null) {
        console.log("transportadorSelecionado está nulo");
        alert("Selecionar Transportador");
        $("#ListaTransportador").css("color", "red");
        return false;
    } else {
        $("#ListaLocalInspecao").css("color", "black");
    }
}

function ValidarFormularioInserirVeiculo() {

    var marcaSelecionado = $("#ListaMarca").val();
    var modeloSelecionado = $("#ListaModelo").val();
    
    if (marcaSelecionado == 0) {
        console.log("marcaSelecionado está nulo");
        alert("Necessário selecionar Marca");
        $("#ListaMarca").css("color", "red");
        //$("#ListaClienteLabelID").css("color", "red");
        return false;
    } else {
        $("#ListaMarca").css("color", "black");
    }
    if (modeloSelecionado == 0) {
        console.log("ListaModelo está nulo");
        alert("Necessário selecionar Modelo");
        $("#ListaModelo").css("color", "red");
        return false;
    } else {
        $("#ListaModelo").css("color", "black");
    }
}