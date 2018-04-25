// Category
var changeFeedbackStatus = {
    init: function () {
        changeFeedbackStatus.registerEvents();
    },
    registerEvents: function () {
        $('#changeStatus').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Feedback/ChangeStatus",
                data: { id: id },
                datatype: "json",
                type: 'POST',
                success: function (response) {
                    if (response.status == true) {
                        btn.text('Đã xử lý');
                        //btn.removeClass('text-danger');
                        //btn.addClass('text-primary');
                    }
                    else {
                        btn.text('Chưa xử lý');
                        //btn.removeClass('text-primary');
                        //btn.addClass('text-danger');
                    }
                }
            });
        });
    }
}
changeFeedbackStatus.init();