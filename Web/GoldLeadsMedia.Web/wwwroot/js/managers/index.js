$('.aff-details').on('click', function () {
    var id = $(this).attr('data-user');
    var url = `/manager/affiliateDetails?a_id=${id}`;
    window.location.href = url;
});