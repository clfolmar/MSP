
$(document).ready(function () {

    var specificationCount = 0;

    $("#addSpec").click(function () {

        specificationCount = specificationCount + 1;

        $("#messageSpecifications").append("<input type='text' id='[" + specificationCount + "].Specification' name='[" + specificationCount + "].Specification' class='form-control' value='' />");

    });

    $("#removeSpec").click(function () {
        if (specificationCount != 0) {

            //$("#[" + specificationCount + "].Specification").remove();
            document.getElementById("[" + specificationCount + "].Specification").remove();

            specificationCount = specificationCount - 1;

        }

    });

    $("#removeAll").click(function () {
        if (specificationCount != 0) {

            for (var i = specificationCount; i > 0; i--){

                document.getElementById("[" + specificationCount + "].Specification").remove();

                specificationCount = specificationCount - 1;

            }
        }

    });

});