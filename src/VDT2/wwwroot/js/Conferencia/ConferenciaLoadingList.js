﻿$(function () {  // $(document).ready()
})

    //Recebe dados do localcheckpoint, dependendo do local de inspeção. via AJAX
    function PreencheListaCheckPoint() {
        var localInspecao_ID = $("#ListaLocalInspecao").val();
        var LocalCheckPoint = $("#ListaLocalCheckPoint");
        var path = window.location.pathname;
        var i = path.indexOf('EditarInspecao');
        var url = "";

        if (i > 0) {
            url = path + '../Inspecao/RecebeDadosLocalCheckPoint';
        } else {
            url = path + '../Inspecao/RecebeDadosLocalCheckPoint'
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
