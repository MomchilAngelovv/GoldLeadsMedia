$('.clickable-table-row').on('click', function () {
    let inputCheckBox = $(this).children().find('input');

    if (inputCheckBox.prop("checked") === true) {
        inputCheckBox.prop("checked", false);
    }
    else {
        inputCheckBox.prop('checked', true)
    }   

    console.log($(this))
    if ($(this).hasClass("clicked-table-row")) {
        $(this).removeClass("clicked-table-row")
    } else {
        $(this).addClass("clicked-table-row")
    }
})

$('#send-leads-btn').click(function () {
    let leadIds = $('input[name="lead-id"]:checked').toArray().map(el => el.value);
    let partnerId = $('input[name="partner-id"]:checked')[0].value;
    let partnerOfferId = $('#partner-offer-id').val();   

    $.post({
        url: `https://localhost:44322/api/partners/${partnerId}/SendLeads`,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({ partnerId, leadIds, partnerOfferId }),
        success: function (data) {
            console.log("done ok");
        },
        error: function (err) {
            console.log("error");
        }
    });
});
