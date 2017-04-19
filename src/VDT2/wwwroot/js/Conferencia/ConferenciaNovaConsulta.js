$(function () {
    console.log("JS ConferênicaNovaConsulta.JS Inicializado com sucesso."); 

    $('li a').each(function (i, e) {
        $(e).css("cursor", "pointer");
        $(e).css("font-size", "42px");
        console.log($(e));
    })
       
    

})

function EnviarForm(cliente_id) {
    $("#inputCliente").val(cliente_id);
    $("#formPrincipal").submit();
}


