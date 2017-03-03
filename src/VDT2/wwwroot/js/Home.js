$(function () {  // Shorthand for $( document ).ready()

    // $("#divConteudoInstitucional").show();
    // $("#divMenu").hide();
});

var processandoLogin = false;

function ExibePainelLogin() {
    $("#divLoginErro").hide();
    $("#divLoginProcessando").hide();
    $("#Identificacao").val("");
    $("#Senha").val("");
    $('#modalLogin').modal('show');
}

function PreLogin() {
    $("#divLoginErro").hide();

    var uid = $("#Identificacao").val().trim();
    var pwd = $("#Senha").val().trim();

    if (uid.length == 0 || pwd.length == 0) {

        $("#divLoginErro").html("Você deve informar sua identificação e sua senha");
        $("#divLoginErro").show();

        return false;
    }

    $("#divLoginProcessando").show();

    $("#btnEfetuarLogin").hide();
    $("#lnkEsqueciMinhaSenha").hide();

    processandoLogin = true;

    ExibeProgresso(10);
}

function ExibeProgresso(valor) {

    if (!processandoLogin) return;

    if (valor > 100) {
        valor = 0;
    }

    $("#loginGauge").attr("aria-valuenow", valor);
    $("#loginGauge").width(valor + "%");

    window.setTimeout(function () {
        ExibeProgresso(valor + 10);
    }, 500);
}

function VerificaLogin(data) {

    processandoLogin = false;

    $("#divLoginProcessando").hide();

    if (data.search('OK:') !== "-1") {

        //var n = data.search("auditor_ID:");
        //var i = data.search("login");

        //localStorage["auditorID"] = data.substr(n + 11, i - n - 12);
        //localStorage["login"] = data.substr(i + 6);

        location.reload();
    }
    else if (data == "RedefinirSenha") {
        window.location = "../RedefinirSenha";
    }
    else {
        $("#divLoginErro").html(data);
        $("#divLoginErro").show();

        $("#btnEfetuarLogin").show();
        $("#lnkEsqueciMinhaSenha").show();
    }
}

function ErroLogin(date) {

    processandoLogin = false;

    $("#divLoginProcessando").hide();

    $("#divLoginErro").html("Não foi possível efetuar o login. Por favor tente novamente, se o problema persistir, entre em contato com o Bureau Veritas.");
    $("#divLoginErro").show();

    $("#btnEfetuarLogin").show();
    $("#lnkEsqueciMinhaSenha").show();
}

