$(function () {  // Shorthand for $( document ).ready()

    console.log("Inicializou jquery ok");
    console.log("testeOK");
    // $("#divConteudoInstitucional").show();
    // $("#divMenu").hide();
    //$('#mostrarAvarias').hide();
});

var click;

function MostrarAvarias() {
    
    //var a = "MostrarAvarias";
    //console.log(a);
    //console.log($('#botaoAvarias'))
    //alert($('#botaoAvarias').innerHTML);

    //if ($('#botaoAvarias').innerHTML == 'Mostrar Avarias') {
    //    $('#mostrarAvarias').show();
    //}
    
    //else {
    //    $('#mostrarAvarias').show();
    //}
    
   
}

//area

function areaNumericoInputFunc(){
    var selecionado = ($('#areaNumericoInput').val());
    $("#avAreaLista").prop('selectedIndex', selecionado);
}


function areaDropDownInputFunc() {
    //console.log("areaDropDownInputFunc inicializado, onchange");
    var selecionado = $('#avAreaLista').val();
    $('#areaNumericoInput').val(selecionado);
}


//condicao
function condicaoNumericoInputFunc(){
    var selecionado = ($('#condicaoNumericoInput').val());
    $("#avCondicaoLista").prop('selectedIndex', selecionado);
    
}

function condicaoDropDownInputFunc(){
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

function ValidarFormularioInserirAvaria() {

    var areaSelecionado = $("#avAreaLista").val();
    var condicaoSelecionado = $("#avCondicaoLista").val();
    var danoSelecionado = $("#avDanoRepositorioLista").val();
    var gravidadeSelecionado = $("#avGravidadeLista").val();
    var quadranteSelecionado = $("#avQuadranteLista").val();
    var severidadeSelecionado = $("#avSeveridadeLista").val();

    //Area
    if (areaSelecionado == null) {
        alert("Necessário selecionar Área");
        $("#avAreaLista").css("color", "red");
        return false;
    } else {
        $("#avAreaLista").css("color", "black");
    }

    //Condição
    if (condicaoSelecionado == null) {
        alert("Necessário selecionar Condicao");
        $("#avCondicaoLista").css("color", "red");
        return false;
    } else {
        $("#avCondicaoLista").css("color", "black");
    }

    //Dano
    if (danoSelecionado == null) {
        alert("Necessário selecionar Dano");
        $("#avDanoRepositorioLista").css("color", "red");
        return false;
    } else {
        $("#avDanoRepositorioLista").css("color", "black");
    }

    //Gravidade
    if (gravidadeSelecionado == null) {
        alert("Necessário selecionar Gravidade");
        $("#avGravidadeLista").css("color", "red");
        return false;
    } else {
        $("#avGravidadeLista").css("color", "black");
    }

    //Quadrante
    if (quadranteSelecionado == null) {
        alert("Necessário selecionar Quadrante");
        $("#avQuadranteLista").css("color", "red");
        return false;
    } else {
        $("#avQuadranteLista").css("color", "black");
    }

    //Severidade
    if (severidadeSelecionado == null) {
        alert("Necessário selecionar Severidade");
        $("#avSeveridadeLista").css("color", "red");
        return false;
    } else {
        $("#avSeveridadeLista").css("color", "black");
    }

}

function VisualizarAvarias_BtnClick() {
    $("#visualizarAvariasForm").submit();
}