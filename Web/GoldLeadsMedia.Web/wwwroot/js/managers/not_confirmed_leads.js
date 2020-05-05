$('#confirm-leads-btn').click(function () {
    let leadIds = $('input[name="leadId"]:checked').toArray().map(el => el.value);

    let requestBody = {
       leadIds
    };

    $.post({
        url: '/Managers/ConfirmLeads',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(requestBody),

        success: function (data) {
            window.location.href = '/Managers/NotConfirmedLeads';
        },

        error: function (err) {

        }
    });
});