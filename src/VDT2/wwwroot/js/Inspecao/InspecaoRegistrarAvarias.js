$(function () {
    console.log("Inicializou - InspecaoRegistrarAvarias.js");

    $("#imgpreview").hide(0);
    $("#visualizar1").hide(0);

    $("#spanFoto1").click(function () {
        $("#inputFileImgAvaria1").trigger("click")
    })

    $("#spanFoto2").click(function () {
        $("#inputFileImgAvaria2").trigger("click")
    })

    $("#spanFoto3").click(function () {
        $("#inputFileImgAvaria3").trigger("click")
    })

    $("#spanFoto4").click(function () {
        $("#inputFileImgAvaria4").trigger("click")
    })

    $("#spanFoto5").click(function () {
        $("#inputFileImgAvaria5").trigger("click")
    })

    $("#spanFoto6").click(function () {
        $("#inputFileImgAvaria6").trigger("click")
    })

    $("#spanFoto7").click(function () {
        $("#inputFileImgAvaria7").trigger("click")
    })

    $("#spanFoto8").click(function () {
        $("#inputFileImgAvaria8").trigger("click")
    })

    $("#spanFoto9").click(function () {
        $("#inputFileImgAvaria9").trigger("click")
    })

    $("#spanFoto10").click(function () {
        $("#inputFileImgAvaria10").trigger("click")
    })

    $("#visualizar1").click(function () {
        var input = $("#" + "inputFileImgAvaria1");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto1").show();
    })

    $("#visualizar2").click(function () {
        var input = $("#" + "inputFileImgAvaria2");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto2").show();
    })

    $("#visualizar3").click(function () {
        var input = $("#" + "inputFileImgAvaria3");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto3").show();
    })

    $("#visualizar4").click(function () {
        var input = $("#" + "inputFileImgAvaria4");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto4").show();
    })

    $("#visualizar5").click(function () {
        var input = $("#" + "inputFileImgAvaria5");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto5").show();
    })

    $("#visualizar6").click(function () {
        var input = $("#" + "inputFileImgAvaria6");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto6").show();
    })

    $("#visualizar7").click(function () {
        var input = $("#" + "inputFileImgAvaria7");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto7").show();
    })

    $("#visualizar8").click(function () {
        var input = $("#" + "inputFileImgAvaria8");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto8").show();
    })

    $("#visualizar9").click(function () {
        var input = $("#" + "inputFileImgAvaria9");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto9").show();
    })

    $("#visualizar10").click(function () {
        var input = $("#" + "inputFileImgAvaria10");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto10").show();
    })


    $("#btnRemoverFoto1").on("click", function () {
        RemoverFoto(1);
    })

    $("#btnRemoverFoto2").on("click", function () {
        RemoverFoto(2);
    })

    $("#btnRemoverFoto3").on("click", function () {
        RemoverFoto(3);
    })

    $("#btnRemoverFoto4").on("click", function () {
        RemoverFoto(4);
    })

    $("#btnRemoverFoto5").on("click", function () {
        RemoverFoto(5);
    })

    $("#btnRemoverFoto6").on("click", function () {
        RemoverFoto(6);
    })

    $("#btnRemoverFoto7").on("click", function () {
        RemoverFoto(7);
    })

    $("#btnRemoverFoto8").on("click", function () {
        RemoverFoto(8);
    })

    $("#btnRemoverFoto9").on("click", function () {
        RemoverFoto(9);
    })

    $("#btnRemoverFoto10").on("click", function () {
        RemoverFoto(10);
    })
})


function EsconderBotoesSpan() {
    $("#spanFoto2").hide(0);
    $("#spanFoto3").hide(0);
    $("#spanFoto4").hide(0);
    $("#spanFoto5").hide(0);
    $("#spanFoto6").hide(0);
    $("#spanFoto7").hide(0);
    $("#spanFoto8").hide(0);
    $("#spanFoto9").hide(0);
    $("#spanFoto10").hide(0);
}

function EsconderBotoesVisualizar() {
    $("#visualizar1").hide(0);
    $("#visualizar2").hide(0);
    $("#visualizar3").hide(0);
    $("#visualizar4").hide(0);
    $("#visualizar5").hide(0);
    $("#visualizar6").hide(0);
    $("#visualizar7").hide(0);
    $("#visualizar8").hide(0);
    $("#visualizar9").hide(0);
    $("#visualizar10").hide(0);
}

function EsconderBotoesRemover() {
    $("#btnRemoverFoto1").hide(0);
    $("#btnRemoverFoto2").hide(0);
    $("#btnRemoverFoto3").hide(0);
    $("#btnRemoverFoto4").hide(0);
    $("#btnRemoverFoto5").hide(0);
    $("#btnRemoverFoto6").hide(0);
    $("#btnRemoverFoto7").hide(0);
    $("#btnRemoverFoto8").hide(0);
    $("#btnRemoverFoto9").hide(0);
    $("#btnRemoverFoto10").hide(0);
}

//Esconde a foto no banco de daods
function RemoverFoto(i) {
    $("#btnRemoverFoto" + i).hide(0);
    $("#" + "inputFileImgAvaria" + i).val('');
    $('#imgpreview').hide(0);
    $('#visualizar' + i).hide(0);
    $("#spanFoto" + i).removeClass('glyphicon glyphicon-camera');
    $("#spanFoto" + i).addClass('glyphicon glyphicon-plus');
    $("#spanFoto" + i).css('background-color', 'white');
}


//Quando o usuário clicar em 'foto', ele visualizará a foto que selecionou/tirou
function MostrarFotosSelecionadas(i) {
    var arquivosSelecionados = $("#" + "inputFileImgAvaria" + i).prop('files');
    var qtdSelecionada = arquivosSelecionados.length;

    $('#spanFoto' + i).removeClass('glyphicon glyphicon-plus');
    $('#spanFoto' + i).addClass('glyphicon glyphicon-camera');


    var count = i + 1;
    $("#spanFoto" + count).show(0);
    $('#divspanFoto' + count).removeAttr('hidden');
    $("#visualizar" + i).show(0);
    $("#visualizar" + count).hide(0);
    $("#spanFoto" + i).css("background-color", '#ffdb41');


    EsconderBotoesRemover();
    $("#imgpreview").hide(0);
    var input = $("#" + "inputFileImgAvaria" + i);
    readURL(input[0]);
}


//Visualiza a foto dependendo do arquivo selecionado
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imgpreview').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

//AREA
//Caso tenha selecionado via Combobox, mudar também o valor do input numérico
function areaNumericoInputFunc() {
    var selecionado = ($('#areaNumericoInput').val());
    $("#avAreaLista").prop('selectedIndex', selecionado);
}

//Caso tenha selecionado via Input Numérico, mudar também o Combobox
function areaDropDownInputFunc() {
    var selecionado = $('#avAreaLista').val();
    $('#areaNumericoInput').val(selecionado);

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


//Realiza a validação front-end do formulário
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
        $("#btnGravarAvarias").prop("disabled", false);
        return false;
    } else {
        $("#avAreaLista").css("color", "black");
    }

    //Condição
    if (condicaoSelecionado == null) {
        alert("Necessário selecionar Condicao");
        $("#avCondicaoLista").css("color", "red");
        $("#btnGravarAvarias").prop("disabled", false);
        return false;
    } else {
        $("#avCondicaoLista").css("color", "black");
    }

    //Dano
    if (danoSelecionado == null) {
        alert("Necessário selecionar Dano");
        $("#avDanoRepositorioLista").css("color", "red");
        $("#btnGravarAvarias").prop("disabled", false);
        return false;
    } else {
        $("#avDanoRepositorioLista").css("color", "black");
    }

    //Gravidade
    if (gravidadeSelecionado == null) {
        alert("Necessário selecionar Gravidade");
        $("#avGravidadeLista").css("color", "red");
        $("#btnGravarAvarias").prop("disabled", false);
        return false;
    } else {
        $("#avGravidadeLista").css("color", "black");
    }

    //Quadrante
    if (quadranteSelecionado == null) {
        alert("Necessário selecionar Quadrante");
        $("#avQuadranteLista").css("color", "red");
        $("#btnGravarAvarias").prop("disabled", false);
        return false;
    } else {
        $("#avQuadranteLista").css("color", "black");
    }

    //Severidade
    if (severidadeSelecionado == null) {
        alert("Necessário selecionar Severidade");
        $("#avSeveridadeLista").css("color", "red");
        $("#btnGravarAvarias").prop("disabled", false);
        return false;
    } else {
        $("#avSeveridadeLista").css("color", "black");
    }
}

function VisualizarAvarias_BtnClick() {
    $("#visualizarAvariasForm").submit();
}

function EditarVeiculo_BtnCLick() {
    $("#editarVeiculoForm").submit();
}


function EditarInspecao_BtnCLick() {
    $("#editarInspecaoForm").submit();
}


function NovoVeiculo_BtnCLick() {
    $("#novoVeiculoForm").submit();
}

function Btn_Click_EditarAvaria(valor) {
    inspAvaria_ID = valor;
    $("#avariaIdFormSubmit").val(inspAvaria_ID);
    console.log("Avaria a ser editada.:", inspAvaria_ID)
    $("#editarAvariaForm").submit();
}


function EnviarFormularioDesabilitarBotao(e) {
    //e = botão acionado
    $(e).prop("disabled", true);
    $("#formPrincipal").submit();

}