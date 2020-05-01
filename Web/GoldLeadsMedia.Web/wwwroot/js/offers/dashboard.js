$('.group-filter').on('click', function () {
    let id = $(this).data('group-id');
    $.get(`/offer/getoffersbygroup?group_Id=${id}`, function (data) {
        $('#offers-group-list').html(data)
    });
})