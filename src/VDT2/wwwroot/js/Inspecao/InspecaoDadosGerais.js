$(function () {  // $(document).ready()
    console.log("Inicializou jquery ok");
    var LocalCheckPoint = $("#ListaLocalCheckPoint");
    var ListaCLiente = $("#ListaCliente");
    var ListaLocalInspecao = $("#ListaLocalInspecao");
    var ListaTransportador = $("#ListaTransportador");
    var FrotaViagem = $("#FrotaViagem")
    var Navio = $("#Navio");


    $("#botaoLimparID").on('click', function () {
        LocalCheckPoint.val();
        LocalCheckPoint.val();
        ListaCLiente.val();
        ListaLocalInspecao.val();
        ListaTransportador.val();
        FrotaViagem.val();
        Navio.val();
    })

    //Verifica se a inspeção está em edição. Caso esteja muda o nome de 'Gravar' para 'Alterar' e pega todos os dados da edição
    var edicao = $('#edicaoInputini').val();
    if (edicao == 1) {
        $("#botaoEnviarId").text("Alterar");

        //recebe os campos
        var ClienteSelecionado = $("#clienteIdini").val();
        var LocalInspecao = $("#localInspecaoIdini").val();
        var LocalCheckPoint = $("#localCheckPointIdini").val();
        var Transportador = $("#transportadorIdini")
        var FrotaViagemNome = $("#frotaViagemNomeini").val();
        var NavioNome = $("#nomeNavioini").val();
        var tipoTransportador = $("#tipoTransportadorini").val();
        var idTipo = $("#idTipoini").val();
        var tipo = $('#tipotransportadorini').val();

        console.log("Tipo é: " + tipo);

        if (tipo === 'M') {
            $("#lblFrotaViagem").html('Viagem');
            $("#Navio").show();
            $("#lblNavio").show();
            $("#Navio").prop('required', true);
        }
        else if (tipo == 'T') {
            $("#Navio").hide();
            $("#lblNavio").hide();
            $("#Navio").removeAttr('required');
            $("#lblFrotaViagem").html('Frota');
        }

        //seta os valores
        $("#ListaCliente").val(ClienteSelecionado);
        $("#ListaLocalInspecao").val(LocalInspecao);
        $("#ListaLocalCheckPoint").val(LocalCheckPoint);
        $("#ListaTransportador").val(idTipo);
        $("#FrotaViagem").val(FrotaViagemNome);
        $("#Navio").val(NavioNome);

    } else {
        $("#botaoEnviarId").text("Gravar");
    }

    $("#ListaTransportador").on("click", function () {
        TerrestreOuMaritimo();
    });


});


//Verifica se o transportador
function TerrestreOuMaritimo() {
    var selecionado = $('#ListaTransportador').val();
    var arr = selecionado.split('_');
    var tipo = arr[1];

    if (tipo === 'M') {
        $("#lblFrotaViagem").html('Viagem');
        $("#Navio").show();
        $("#lblNavio").show();
        $("#Navio").prop('required', true);
    }
    else if (tipo == 'T') {
        $("#Navio").hide();
        $("#lblNavio").hide();
        $("#Navio").val("");
        $("#Navio").removeAttr('required');
        $("#lblFrotaViagem").html('Frota');
    }
}

//Realiza a validação front-end do formulário
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
        return false;
    }
    else {
        $("#ListaCliente").css("color", "black");
    }

    //Valida Local
    if (localSelecionado == 0 || localSelecionado == null) {
        console.log("localSelecionado está nulo");
        alert("Selecionar Local");
        $("#ListaLocalInspecao").css("color", "red");
        return false;
    }
    else {
        $("#ListaLocalInspecao").css("color", "black");
    }


    //Valida CheckPoint
    if (checkPointSelecionado == 0 || checkPointSelecionado == null) {
        console.log("checkPointSelecionado está nulo");
        alert("Selecionar checkPoint");
        $("#ListaLocalCheckPoint").css("color", "red");
        return false;
    } else {
        $("#ListaLocalCheckPoint").css("color", "black");
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

//Recebe dados do localcheckpoint, dependendo do local de inspeção. via AJAX
function PreencheListaCheckPoint() {
    var localInspecao_ID = $("#ListaLocalInspecao").val();
    var LocalCheckPoint = $("#ListaLocalCheckPoint");
    var path = window.location.pathname;
    var i = path.indexOf('EditarInspecao');
    var url = "";

    //No momento não estou fazendo nenhuma lógica, mas para tests no servidor, mudar o url path do ELSE
    if (i > 0) {
        url = path + '/../RecebeDadosLocalCheckPoint';
    } else {
        url = path + '/../RecebeDadosLocalCheckPoint'
    }

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


//Recebe dados do Transportador, dependendo do local de inspeção. via AJAX
function PreencheListaTransportador() {

    var localCheckPoint_ID = $("#ListaLocalCheckPoint").val();
    var listaTransportador = $("#ListaTransportador");
    var path = window.location.pathname;
    var i = path.indexOf('EditarInspecao');

    var url = "";

    //No momento não estou fazendo nenhuma lógica, mas para tests no servidor, mudar o url path do ELSE
    if (i > 0) {
        url = path + '/../RecebeDadosTransportador';
    } else {
        url = path + '/../RecebeDadosTransportador'
    }

    $.getJSON(
        url,
        { 'localCheckPoint_ID': localCheckPoint_ID },
        function (response) {
            listaTransportador.empty();
            $.each(response, function (index, item) {
                $(document.createElement('option'))
                    .attr('value', item.idTipo)
                    .text(item.nome)
                    .appendTo(listaTransportador);
            });
            TerrestreOuMaritimo();
        });

}