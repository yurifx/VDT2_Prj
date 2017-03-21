$(function () {  // Shorthand for $( document ).ready()

});


function Btn_Click_EditarAvaria(valor) {
    inspAvaria_ID = valor;
    $("#avariaIdFormSubmit").val(inspAvaria_ID);
    console.log("Avaria a ser editada.:", inspAvaria_ID)
    $("#editarAvariaForm").submit();
}


function Btn_Click_VisualizarFotos(valor) {
    inspAvaria_ID = valor;
    $("#VisualizarFotosFormSubmit").val(inspAvaria_ID);
    console.log("Avaria a ser editada.:", inspAvaria_ID)
    $("#VisualizarFotosForm").submit();
}