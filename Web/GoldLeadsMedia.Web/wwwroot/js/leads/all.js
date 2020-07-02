//$('.clickable-table-row').on('click', function () {
//    let inputCheckBox = $(this).children().find('input');

//    if (inputCheckBox.prop("checked") === true) {
//        inputCheckBox.prop("checked", false);
//    }
//    else {
//        inputCheckBox.prop('checked', true)
//    }   

//    console.log($(this))
//    if ($(this).hasClass("clicked-table-row")) {
//        $(this).removeClass("clicked-table-row")
//    } else {
//        $(this).addClass("clicked-table-row")
//    }
//})

$('#send-leads-btn').click(function () {
    let leadIds = $('input[name="lead-id"]:checked').toArray().map(el => el.value);
    let brokerId = $('input[name="broker-id"]:checked')[0].value;

    $.post({
        url: `https://stagingcoreapi.goldleadsmedia.com/Brokers/${brokerId}/SendLeads`,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({ brokerId, leadIds }),
        success: function (data) {
            console.log(data);
            if (data.errors > 0) {
                $('#send-leads-fail').text(`Failed leads to send: ${data.errors}! Page will refresh in 5 seconds!`).show(200).delay(5000).hide(200);
                setTimeout(RefreshPage, 5000);
            }
            else {
                $('#send-leads-success').show(200).delay(5000).hide(200);
                setTimeout(RefreshPage, 5000);
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
});

function RefreshPage() {
    window.location.href = `/Leads/All`
}
