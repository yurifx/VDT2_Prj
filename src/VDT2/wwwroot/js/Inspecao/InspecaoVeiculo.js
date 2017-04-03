$(function () {  // $(document).ready()
    console.log("Inicializou InspecaoVeiculo.JS");

    //O html com 'tip', é acionado quando mouseouver;
    $('[data-toggle="tooltip"]').tooltip();

    //Recebe valores de Marca e modelo
    var sMarca = $('#MarcaLabelHidden').val();
    var sModelo = $('#ModeloLabelHidden').val();
    $('#ListaMarca').val(sMarca);
    $('#ListaModelo').val(sModelo);


    //Caso esteja em edição, muda o nome do botão para: Alterar
    var edicao = $('#edicaoId').val();
    if (edicao == 1) {
        $("#botoesClasseVeiculoGravar").text("Alterar");
    } else {
        $("#botoesClasseVeiculoGravar").text("Gravar");
    }
});

//Verifica se o Chassi informado pelo usuário corresponde com os requisitos básicos
function ValidarChassi() {
    var qtd = $("#chassiInput").val()
    if (qtd.length == 6) {
        $("#chassiSuccess").removeClass("glyphicon-remove");
        $("#chassiSuccess").addClass("glyphicon-ok");
        $("#divChassi").addClass("has-success has-feedback");
    }
    else {
        $("#chassiSuccess").addClass("glyphicon-remove");
        $("#chassiSuccess").removeClass("glyphicon-ok");
        $("#divChassi").removeClass("has-success has-feedback");
    }
}

//Realiza validação front-end de todo o formulário de veículo
function ValidarFormularioInserirVeiculo() {

    var marcaSelecionado = $("#ListaMarca").val();
    var modeloSelecionado = $("#ListaModelo").val();

    if (marcaSelecionado == 0) {
        console.log("marcaSelecionado está nulo");
        alert("Necessário selecionar Marca");
        $("#ListaMarca").css("color", "red");
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

function EditarVeiculo_BtnCLick() {
    var VeiculoId = $('#id_inspVeiculo').val();
    console.log(VeiculoId);
    if (VeiculoId == 0) {
        alert("Não é possível editar veículo anterior");
        $("#botoesClasseVeiculoGravar").text("Gravar");
    } else {
        $("#botoesClasseVeiculoGravar").text("Alterar");
        $("#editarVeiculoForm").submit();
    }
}

function EditarInspecao_BtnCLick() {
    $("#editarInspecaoForm").submit();
}

//Realiza validação do Formulário de marca; Caso esteja correto, realiza a inserção da nova marca no bdd via ajax post
function ValidarFormularioNovaMarca() {

    var marcaid = 0;
    var cliente_ID = $("#novaMarcaCliente").val();
    var novaMarcaNome = $('#novaMarcaNome').val();

    if (novaMarcaNome == "") {
        alert("Por favor insira o nome da Marca");
        return false;
    }

    else {
        //envia o post
        request = $.ajax({
            type: "POST",
            url: "InserirNovaMarca",
            data: {
                'cliente_ID': cliente_ID,
                'novaMarcaNome': novaMarcaNome
            },
        });

        //recebe o ID e adiciona uma option
        request.done(function (response, textStatus, jqXHR) {
            if (response == -1) {
                alert("Erro ao inserir marca");
            }
            else {
                $('#ListaMarca').append($('<option>',
                    {
                        value: response,
                        text: novaMarcaNome
                    }))
                //informa o usuário que a  marca foi adidioncada: Esta linha se faz necessário pois senão terei que adicionar callback.
                alert("Nova marca adicionada com sucesso");
            };

            
            //seta como selected o valor da resposta (id) 
            $('#ListaMarca').val(response);
            $("#ListaMarca").css("color", "blue");

            //fecha o modal
            $("#fecharModalNovaMarca").trigger('click');

            $('#novaMarcaNome').val("")
            return true;
        });
    }
}

//Realiza validação do Formulário de Modelo; Caso esteja correto, realiza a inserção da nova modelo no bdd via ajax post
function ValidarFormularioNovoModelo() {

    var cliente_ID = $("#novoModeloClienteID").val();
    var novoModeloNome = $('#novoModeloNome').val();
    var modeloid = 0;

    if (novoModeloNome == "") {
        alert("Por favor insira o nome da Marca");
        return false;
    }

    else {
        //envia o postt
        request = $.ajax({
            type: "POST",
            url: "InserirNovoModelo",
            data: {
                'cliente_ID': cliente_ID,
                'novoModeloNome': novoModeloNome
            },
        });

        //recebe o ID e adiciona uma option
        request.done(function (response, textStatus, jqXHR) {
            if (response == -1) {
                alert("Erro ao inserir Modelo");
            } else {

                $('#ListaModelo').append($('<option>', {
                    value: response,
                    text: novoModeloNome
                }));
                //informa o usuário - esta linha se faz necessário pois senão terei que adicionar callback.
                alert("Novo Modelo adicionado com sucesso");
            }

            //seta como selected o valor da resposta (id) 
            $('#ListaModelo').val(response);
            $("#ListaModelo").css("color", "blue");

            //fecha o modal
            $("#fecharModalNovoModelo").trigger('click');

            $('#novoModeloNome').val("")

            return true;
        });
    }
}
