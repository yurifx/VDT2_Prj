$(function () { //ONLOAD
    EsconderBotoesRemover();
    console.log("Inicializou jquery ok - Editar Avarias");

    EsconderBotoesSpan();
    EsconderBotoesVisualizar();
    EsconderBotoesRemover();
    $("#imgpreview").hide(0);

    //Recebe todos valores do post 
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


    //Adicionar fotos
    $("#spanFoto1").click(function () {
        $("#inputFileImgAvaria1").trigger("click");
    })

    $("#spanFoto2").click(function () {
        $("#inputFileImgAvaria2").trigger("click");
    })

    $("#spanFoto3").click(function () {
        $("#inputFileImgAvaria3").trigger("click");
    })

    $("#spanFoto4").click(function () {
        $("#inputFileImgAvaria4").trigger("click");
    })

    $("#spanFoto5").click(function () {
        $("#inputFileImgAvaria5").trigger("click");
    })

    $("#spanFoto6").click(function () {
        $("#inputFileImgAvaria6").trigger("click");
    })

    $("#spanFoto7").click(function () {
        $("#inputFileImgAvaria7").trigger("click");
    })

    $("#spanFoto8").click(function () {
        $("#inputFileImgAvaria8").trigger("click");
    })

    $("#spanFoto9").click(function () {
        $("#inputFileImgAvaria9").trigger("click");
    })

    $("#spanFoto10").click(function () {
        $("#inputFileImgAvaria10").trigger("click");
    })


    $("#visualizar1").click(function () {
        var input = $("#" + "inputFileImgAvaria1");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto1").show(0);
    })

    $("#visualizar2").click(function () {
        var input = $("#" + "inputFileImgAvaria2");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto2").show(0);
    })

    $("#visualizar3").click(function () {
        var input = $("#" + "inputFileImgAvaria3");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto3").show(0);
    })

    $("#visualizar4").click(function () {
        var input = $("#" + "inputFileImgAvaria4");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto4").show(0);
    })

    $("#visualizar5").click(function () {
        var input = $("#" + "inputFileImgAvaria5");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto5").show(0);
    })

    $("#visualizar6").click(function () {
        var input = $("#" + "inputFileImgAvaria6");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto6").show(0);
    })

    $("#visualizar7").click(function () {
        var input = $("#" + "inputFileImgAvaria7");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto7").show(0);
    })

    $("#visualizar8").click(function () {
        var input = $("#" + "inputFileImgAvaria8");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto8").show(0);
    })

    $("#visualizar9").click(function () {
        var input = $("#" + "inputFileImgAvaria9");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto9").show(0);
    })

    $("#visualizar10").click(function () {
        var input = $("#" + "inputFileImgAvaria10");
        readURL(input[0]);
        $("#imgpreview").show(0);
        EsconderBotoesRemover();
        $("#btnRemoverFoto10").show(0);
    })

    $("#imgpreview").css("width", '300px');


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
});

function RemoverFoto(i) {
    $("#btnRemoverFoto" + i).hide(0);
    $("#" + "inputFileImgAvaria" + i).val('');
    $('#imgpreview').hide(0);
    $('#visualizar' + i).hide(0);
    $("#spanFoto" + i).css('background-color' ,'white');
}

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

function MostrarFotosSelecionadas(i) {
    var arquivosSelecionados = $("#" + "inputFileImgAvaria" + i).prop('files');
    var qtdSelecionada = arquivosSelecionados.length;

    $('#spanFoto' + i).removeClass('glyphicon glyphicon-plus');
    $('#spanFoto' + i).addClass('glyphicon glyphicon-camera');

    var count = i + 1;
    $("#spanFoto" + count).show(0);
    $('#divspanFoto' + count).removeAttr('hidden');
    $("#visualizar" + i).show(0);
    $("#spanFoto" + i).css("background-color", '#ffdb41');


    EsconderBotoesRemover();
    $("#imgpreview").hide(0);
    var input = $("#" + "inputFileImgAvaria" + i);
    readURL(input[0]);

}


function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imgpreview').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}


function areaNumericoInputFunc() {
    var selecionado = ($('#areaNumericoInput').val());
    $("#avAreaLista").prop('selectedIndex', selecionado);
}


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

function ValidarFormularioInserirAvaria() {

    FotosValidadas = ValidarFotos();

    if (!FotosValidadas) {
        alert('Por favor selecione uma foto');
        $("#btnGravarAvarias").prop("disabled", false);
        return false;
    };


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

function ValidarFotos() {

    //Neste caso validamos via cor. 
    //Caso o layout mude, necessário mudar esta lógica também;
    var qtdFotosSelecionadas = 0;

    if ($("#spanFoto1").css('background-color') == 'rgb(255, 219, 65)') {
        qtdFotosSelecionadas += 1;
    }

    if ($("#spanFoto2").css('background-color') == 'rgb(255, 219, 65)') {
        qtdFotosSelecionadas += 1;
    }

    if ($("#spanFoto3").css('background-color') == 'rgb(255, 219, 65)') {
        qtdFotosSelecionadas += 1;
    }

    if ($("#spanFoto4").css('background-color') == 'rgb(255, 219, 65)') {
        qtdFotosSelecionadas += 1;
    }


    if ($("#spanFoto5").css('background-color') == 'rgb(255, 219, 65)') {
        qtdFotosSelecionadas += 1;
    }


    if ($("#spanFoto5").css('background-color') == 'rgb(255, 219, 65)') {
        qtdFotosSelecionadas += 1;
    }

    if ($("#spanFoto6").css('background-color') == 'rgb(255, 219, 65)') {
        qtdFotosSelecionadas += 1;
    }


    if ($("#spanFoto7").css('background-color') == 'rgb(255, 219, 65)') {
        qtdFotosSelecionadas += 1;
    }

    if ($("#spanFoto8").css('background-color') == 'rgb(255, 219, 65)') {
        qtdFotosSelecionadas += 1;
    }


    if ($("#spanFoto9").css('background-color') == 'rgb(255, 219, 65)') {
        qtdFotosSelecionadas += 1;
    }

    if ($("#spanFoto10").css('background-color') == 'rgb(255, 219, 65)') {
        qtdFotosSelecionadas += 1;
    }

    if (qtdFotosSelecionadas > 0) {
        return true;
    } else {
        return false;
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


function removerImagemAvaria(valor) {
    var arr = valor.split('_');
    var imagem = arr[0];
    var inspAvaria_ID = arr[1];

    //1.png  - separa em 1 e png  -> adiciona o \\ para conseguir funcionar via ID => exemplo: id =  1\\.png 
    var imagemArr = arr[0].split('.');
    var imagemDiv = imagemArr[0] + '\\.' + imagemArr[1];
    $("#" + imagemDiv).hide(0);

    //ENVIANDO POST AJAX PARA O CONTROLLER:
    request = $.ajax({
        type: "POST",
        url: "DeletarFoto",
        data: {
            'imagem': imagem,
            'inspAvaria_ID': inspAvaria_ID
        },
    });
}

function EnviarFormularioDesabilitarBotao(e) {
    //e - botão
    $(e).prop("disabled", "true");
    $("#formPrincipal").submit();
}

function ValidarFormularioInspecaoEditarAvarias() {

    FotosValidadas = ValidarFotos();

    var Fabrica = $("#inputFabrica").is(":checked");
    var Transporte = $("#inputTransporte").is(":checked");

    if (Fabrica == true || Transporte == true){
        return true;
    }
    else {
        alert("Por favor selecionar tipo de avaria")
        $("#inputFabrica").css("color", "red");
        $("#inputTransporte").css("color", "red")
        $("#btnGravar").prop("disabled", false);
        return false;
    }

}

