
function Save() {

    let NumeroEstudiantes = $("#NumeroEstudiantes").val();

    for (var i = 0; i < NumeroEstudiantes; i++) {

        let nota1 = $("#ListaCursoEstudiante_" + i + "__Nota1").val();
        nota1 = nota1.replace(",", ".");

        if (Number(nota1) < 0 || Number(nota1) > 5) {
            toastr.error('Las Nota 1 solo van de 0.0 a 5.0', 'ERROR');
            return false;
        }

        let nota2 = $("#ListaCursoEstudiante_" + i + "__Nota2").val();
        nota2 = nota2.replace(",", ".");
        if (Number(nota2) < 0 || Number(nota2) > 5) {
            toastr.error('Las Nota 2 solo van de 0.0 a 5.0', 'ERROR');
            return false;
        }

        let nota3 = $("#ListaCursoEstudiante_" + i + "__Nota3").val();
        nota3 = nota3.replace(",", ".");
        if (Number(nota3) < 0 || Number(nota3) > 5) {
            toastr.error('Las Nota 3 solo van de 0.0 a 5.0', 'ERROR');
            return false;
        }

        let nota4 = $("#ListaCursoEstudiante_" + i + "__Nota3").val();
        nota4 = nota3.replace(",", ".");
        if (Number(nota4) < 0 || Number(nota4) > 5) {
            toastr.error('Las Nota 4 solo van de 0.0 a 5.0', 'ERROR');
            return true;
        }



    }





}