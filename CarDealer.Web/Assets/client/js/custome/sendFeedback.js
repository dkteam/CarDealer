var sendFeedback = {
    init: function () {
        sendFeedback.registerEvents();
    },
    registerEvents: function () {

        $('#sendfeedback').off('click').on('click', function () {

            $.ajax({
                url: '/Feedback/CreateFeeback',
                type: 'POST',
                dataType: 'json',
                data: {
                    name: $('#inputName').val(),
                    email: $('#inputEmail').val(),
                    mobile: $('#inputMobile').val(),
                    message: $('#inputMessage').val()
                },
                success: function (res) {
                    if (res.data == true) {
                        alert("Gửi thành công! Chúng tôi sẽ sớm liên hệ lại, xin cảm ơn quý khách.")
                    } else {
                        alert("Quý khách vui lòng điền đầy đủ thông tin trước khi nhấn nút gửi. Xin cảm ơn!")
                    }
                },
            });
        });
    }
}
sendFeedback.init();