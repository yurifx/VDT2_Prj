$(function () {  // Shorthand for $( document ).ready()
    console.log("Jquery Inicializado OK");

    EsconderBotoesVisualizar();
    $("#imgpreview").hide(0);

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

//Esconde a foto para posteriormente remover da pasta
function RemoverFoto(i) {
    $("#btnRemoverFoto" + i).hide(0);
    $("#" + "inputFileImgAvaria" + i).val('');
    $('#imgpreview').hide(0);
    $('#visualizar' + i).hide(0);
    $("#spanFoto" + i).removeClass('glyphicon glyphicon-camera');
    $("#spanFoto" + i).addClass('glyphicon glyphicon-plus');
    $("#spanFoto" + i).css('background-color', 'white');
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


//Visualizar a foto adicionada pelo usuário
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imgpreview').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

//Remover fotos
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
        url: "../Inspecao/DeletarFoto",
        data: {
            'imagem': imagem,
            'inspAvaria_ID': inspAvaria_ID
        },
    });
}

function DesabilitarBotao(e) {
    $(e).prop("disabled", "true");
}