$('.row-details').on('click', function () {
    let id = $(this).data('id');
    let url = `/Offers/Details?id=${id}`;
    window.location = url;
});
