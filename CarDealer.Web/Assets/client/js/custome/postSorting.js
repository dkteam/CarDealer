var sorting = {
    init: function () {
        sorting.registerEvents();
    },
    registerEvents: function () {
        var page = $('#forSorting').val();
        $("#selectSorting").on("change", function () {
            var sortingValue = this.value;
            if (sortingValue == 1) {
                window.location.href = "?page=" + page + "&sort=new";
                $("#selectSorting").val(sortingValue);
            } else if (sortingValue == 2) {
                window.location.href = "?page=" + page + "&sort=viewcount";

            }
        })
    }
}
sorting.init();