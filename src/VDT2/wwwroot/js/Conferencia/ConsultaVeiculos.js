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
})

function ExportarExcel() {

    var dados = $("#inputFiltrosUtilizados").val();

    $.post(
        "./ExportarExcel",
        { "dados": dados},
        function (e) {
            alert(e);
        }
    )
}

function Btn_Click_VisualizarFotos(avaria_id) {
    $("#fotosConsulta").load('../Conferencia/VisualizarFotosConsulta', { 'inspAvaria_ID': avaria_id });
    $("#modalFotos").modal('show');
}