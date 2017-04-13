$(function () {


    var table = $('#tabela1').DataTable({
        "scrollX": true,
        "language": {
            "lengthMenu": "Mostrar _MENU_ registros por página",
            "zeroRecords": "Sem registros encontrados",
            "info": "Exibindo página _PAGE_ de _PAGES_",
            "infoEmpty": "Sem registros encontrados",
            "infoFiltered": "(filtrados de _MAX_ todos os registros)",
            "search": "Procurar",
            "paginate": {
                "first": "Primeira",
                "last": "Última",
                "next": "Próxima",
                "previous": "Anterior"
            }
        }
    });

    table
        .buttons()
        .container()
        .appendTo('#botoes');

    $('#BotaoExcel').click(function () {
        $("#tabela1").table2excel({
            name: "ArquivoConsulta",
            filename: "ArquivoConsulta",
            fileext: ".xls",
        });
    });
})
