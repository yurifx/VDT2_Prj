function Btn_Click_EditarAvaria(valor) {
    inspAvaria_ID = valor;
    $("#avariaIdFormSubmit").val(inspAvaria_ID);
    console.log("Avaria a ser editada.:", inspAvaria_ID)
    $("#editarAvariaForm").submit();
}
