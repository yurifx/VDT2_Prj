$(function () {  // Shorthand for $( document ).ready()

});


function Btn_Click_EditarAvaria(valor) {
    $("#avariaIdFormSubmit").val(valor);
    $("#editarAvariaForm").submit();
}


function Btn_Click_VisualizarFotos(valor) {
    $("#inspAvaria_ID_FormVisualizarFotos_input").val(valor);
    $("#VisualizarFotosForm").submit();
}