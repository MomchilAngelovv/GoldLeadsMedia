$('.clear-filters-btn').click(function () {
    window.location = '/Offers/All';
})

$('.row-details').on('click', function () {
    let id = $(this).data('id');
    let url = `/Offers/Details/${id}`;

    window.location = url;
});
