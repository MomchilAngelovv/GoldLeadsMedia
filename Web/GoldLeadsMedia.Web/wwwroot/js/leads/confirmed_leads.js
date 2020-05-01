$('.clickble-table-row').on('click', function () {
    let inputCheckBox = $(this).children().find('input')    ;

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

$('#submitLeadsToPartner').on('click', function () {
    let ids = $('input[name="id"]:checked').toArray().map(el => el.value);

    let partner_Id = undefined;
    let partnerCheckedInputElement = $('input[name="partner"]:checked');
    if (partnerCheckedInputElement.length === 1) {
        partner_Id = $('input[name="partner"]:checked')[0].value;
    }


    if (partner_Id === undefined) {
        alert("Please, select a partner.");
    } else if (ids.length === 0) {
        alert("Please, select at laest 1 lead.");
    } else {
        $.ajax({
            type: 'POST',
            url: '/lead/sendleadstopartners',
            dataType: 'json',
            data: { ids: ids, partner_Id: partner_Id },
            success: function (data) {
                var url = '/lead/getconfirmedleads';
                window.location.href = url;
            },
            error: function (err) {

            }
        });
    }
});
