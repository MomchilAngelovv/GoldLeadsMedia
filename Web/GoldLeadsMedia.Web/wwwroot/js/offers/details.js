var baseUrl = $('#tracking-url').text();

$('.landing-page-info').on('click', function () {
    let landingPageId = $(this).data('landing-page-id');
    let newUrl = baseUrl + `&landingPageId=${landingPageId}`;

    $('.landing-page-info').removeClass("clicked-table-row");
    $(this).addClass("clicked-table-row");

    $('#tracking-url').text(newUrl);
});

$(".openNewTab").tooltip({
    show: null,
    position: {
        my: "left top",
        at: "left bottom"
    },
    open: function (event, ui) {
        ui.tooltip.animate({ top: ui.tooltip.position().top + 10 }, "fast");
    }
}).click(function () {
    var link = document.getElementById("tracking-url").innerHTML;
    window.open(link, '_blank')
});

$('#save-tracker-urls-btn').on('click', async function (e) {
    e.preventDefault();
    e.stopPropagation();

    let leadPostbackUrl = $('#lead-postback-url').val();
    let ftdPostbackUrl = $('#ftd-postback-url').val();

    let url = '/Affiliates/UpdateTrackerConfiguration'

    let data = {
        leadPostbackUrl,
        ftdPostbackUrl
    }

    let response = await $.post({
        url: url,
        data: data,
        dataType: 'application/json'
    });

    console.log(response.status);
});