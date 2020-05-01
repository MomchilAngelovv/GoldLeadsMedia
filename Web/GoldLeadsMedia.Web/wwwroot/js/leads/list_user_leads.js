$('#submitLeadIds').on('click', function () {
    var ids = $('input[name="lead_id"]:checked').toArray();
    var sendData = [];
    ids.forEach(function (el) {
        sendData.push(el.defaultValue);
    });
    $.ajax({
        type: 'POST',
        url: '/lead/SubmitIds',
        data: { ids: sendData },
        success: function (data) {
            var url = '/lead/index';
            window.location.href = url;
        },
        error: function (err) {

        }
    });
});