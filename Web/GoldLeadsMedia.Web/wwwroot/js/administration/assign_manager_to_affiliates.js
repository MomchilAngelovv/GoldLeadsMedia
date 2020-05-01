$('.clickble-table-row').on('click', function () {
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

$('#assign_manager_to_affiliates').on('click', function () {
    let userIds = $('input[name="affiliate_id"]:checked').toArray().map(el => el.value).join(';');
    let managerId = $('input[name="manager_id"]:checked').val();
    if (managerId === undefined) {
        alert("Please, select a manager.");
    } else if (userIds.length === 0) {
        alert("Please, select at laest 1 lead.");
    } else {
        $.ajax({
            type: 'POST',
            url: '/account/AssignManagerToAffiliates',
            dataType: 'json',
            data: { userIds, managerId },
            success: function (data) {
                var url = '/Offer/Dashboard';
                window.location.href = url;
            },
            error: function (err) {

            }
        });
    }
});
