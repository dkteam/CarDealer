var sorting = {
    init: function () {
        sorting.registerEvents();
    },
    registerEvents: function () {
        var page = $('#forSorting').val();
        $("#selectSorting").on("change", function () {
            var sortingValue = this.value;
            if (sortingValue == 1) {
                window.location.href = "?page=" + page + "&sort=bestseller";
                $("#selectSorting").val(sortingValue);
            } else if (sortingValue == 2) {
                window.location.href = "?page=" + page + "&sort=price";

            } else if (sortingValue == 3) {
                window.location.href = "?page=" + page + "&sort=pricedescending";
                $("#selectSorting").val(sortingValue);
            } else if (sortingValue == 4) {
                window.location.href = "?page=" + page + "&sort=new";
                $("#selectSorting").val(sortingValue);
            }
        })
    }
}
sorting.init();