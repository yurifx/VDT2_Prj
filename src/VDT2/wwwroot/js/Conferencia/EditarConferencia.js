$(function () {  // Shorthand for $( document ).ready()

    var alteraInspecao = $("#hiddenAlteraInspecao").val();
    if (alteraInspecao == 'False ') {
        $("fieldset").prop("disabled", "disabled");
        $("#btnGravar-EditarAvariasConferencia").hide();
    }

    //Mostra a informação em cima do html que tive a tip
    $('[data-toggle="tooltip"]').tooltip();

    //Recebe todos valores do post 

    //Area
    var sMarca = $('#marcaHiddenDiv').val();
    var sModelo = $('#modeloHiddenDiv').val();
    $('#ListaMarca').val(sMarca);
    $('#ListaModelo').val(sModelo);

    //Area
    $("#avAreaLista").prop('selectedIndex', $('#areaHiddenDiv').val());
    $('#areaNumericoInput').val($('#areaHiddenDiv').val());

    //Condicao
    $("#avCondicaoLista").prop('selectedIndex', $('#condicaoHiddenDiv').val());
    $('#condicaoNumericoInput').val($('#condicaoHiddenDiv').val());

    //Dano
    $("#avDanoRepositorioLista").prop('selectedIndex', $('#danoHiddenDiv').val());
    $('#danoNumericoInput').val($('#danoHiddenDiv').val());

    //Gravidade
    $("#avGravidadeLista").prop('selectedIndex', $('#gravidadeHiddenDiv').val());
    $('#gravidadeNumericoInput').val($('#gravidadeHiddenDiv').val());


    //Quadrante
    $("#avQuadranteLista").prop('selectedIndex', $('#quadranteHiddenDiv').val());
    $('#quadranteNumericoInput').val($('#quadranteHiddenDiv').val());

    //Severidade
    $("#avSeveridadeLista").prop('selectedIndex', $('#severidadeHiddenDiv').val());
    $('#severidadeNumericoInput').val($('#severidadeHiddenDiv').val());

    //DanoOrigem
    danoOrigem = $("#pDanoOrigemID").text();
    if (danoOrigem == 'true') {
        $("#InspAvaria_DanoOrigem").prop("checked", true);
    }
    else if (danoOrigem == 'false') {
        $("#InspAvaria_DanoOrigem").prop("checked", false);

    }

    $("#lblCustoTelaEditarAvarias").hover(function () {
        $("#lblCustoTelaEditarAvarias").click();
    })



    $('[data-toggle="popover"]').popover(); 

});

//Caso o usuario o número, mudar também a combobox.
function areaNumericoInputFunc() {
    var selecionado = ($('#areaNumericoInput').val());
    $("#avAreaLista").prop('selectedIndex', selecionado);
}

//Caso tenha selecionado o combobox, mudar também o numero
function areaDropDownInputFunc() {
    var selecionado = $('#avAreaLista').val();
    $('#areaNumericoInput').val(selecionado);
    ZeraValorCusto();
}


//condicao
function condicaoNumericoInputFunc() {
    var selecionado = ($('#condicaoNumericoInput').val());
    $("#avCondicaoLista").prop('selectedIndex', selecionado);
}

function condicaoDropDownInputFunc() {
    var selecionado = $('#avCondicaoLista').val();
    $('#condicaoNumericoInput').val(selecionado);
}


//dano
function danoNumericoInputFunc() {
    var selecionado = ($('#danoNumericoInput').val());
    $("#avDanoRepositorioLista").prop('selectedIndex', selecionado);

}

function danoDropDownInputFunc() {
    var selecionado = $('#avDanoRepositorioLista').val();
    $('#danoNumericoInput').val(selecionado);
}



//gravidade
function gravidadeNumericoInputFunc() {
    var selecionado = ($('#gravidadeNumericoInput').val());
    $("#avGravidadeLista").prop('selectedIndex', selecionado);

}

function gravidadeDropDownInputFunc() {
    var selecionado = $('#avGravidadeLista').val();
    $('#gravidadeNumericoInput').val(selecionado);
    ZeraValorCusto();
}

//quadrante
function quadranteNumericoInputFunc() {
    var selecionado = ($('#quadranteNumericoInput').val());
    $("#avQuadranteLista").prop('selectedIndex', selecionado);

}

function quadranteDropDownInputFunc() {
    var selecionado = $('#avQuadranteLista').val();
    $('#quadranteNumericoInput').val(selecionado);
}


//severidade
function severidadeNumericoInputFunc() {
    var selecionado = ($('#severidadeNumericoInput').val());
    $("#avSeveridadeLista").prop('selectedIndex', selecionado);

}

function severidadeDropDownInputFunc() {
    var selecionado = $('#avSeveridadeLista').val();
    $('#severidadeNumericoInput').val(selecionado);
}


//Sempre que o usuário alterar uma Área ou Gravidade, zeramos o valor do custo;
//Caso o usuário deixe zerado, o valor será calculado assim que enviarmos a informação no banco de dados
//Ou seja, essa informação é calculada na procedure.
function ZeraValorCusto() {
    $("#InspAvaria_Custo").val(0);

}




function DeletarAvaria(avaria_id) {
    console.log("Avaria será deletada: " + avaria_id);
    //var apagar = confirm('Deseja realmente excluir este registro?');
    //if (apagar) {

    //} else {
    //event.preventDefault();
    $("#formDeletarAvaria").submit();
    $("#myModal").hide();
}



