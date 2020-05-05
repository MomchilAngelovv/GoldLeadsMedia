$('.affiliate-details').click(function () {
    let id = $(this).data('affiliate-id');

    debugger;
    window.location = `/Affiliates/Details/${id}`;
});