var carSearchForSmallScreen = {
    init: function () {
        carSearchForSmallScreen.registerEvents();
    },
    registerEvents: function () {
        //        var modelId = $('#selectModel option:selected').val();
        var modelId = null;
        var styleId = null;
        var totalSeatId = null;
        var carStatus = true;

        $("#selectModel1").on("change", function () {
            modelId = this.value;
        });
        $("#selectStyle1").on("change", function () {
            styleId = this.value;
        });
        $("#selectTotalSeat1").on("change", function () {
            totalSeatId = this.value;
        });
        $("#selectCarStatus1").on("change", function () {
            carStatus = this.value;
        });

        $('#btnSearch1').off('click').on('click', function () {
            $.ajax({
                url: '/Car/CarSearch',
                type: 'POST',
                dataType: 'json',
                data: {
                    modelId: modelId,
                    styleId: styleId,
                    totalSeatId: totalSeatId,
                    carStatus: carStatus
                },
                success: function (res) {
                    //response(res.data);
                    window.location.href = "/tim-kiem.html?modelId=" + modelId + "&styleId=" + styleId + "&totalSeatId=" + totalSeatId + "&carStatus=" + carStatus;
                },
            });
        });
    }
}
carSearchForSmallScreen.init();