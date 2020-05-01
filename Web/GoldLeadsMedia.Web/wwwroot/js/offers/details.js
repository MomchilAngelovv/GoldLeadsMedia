$('.url').on('click', function () {
    let landingPageId = $(this).data('landing-page-id');
    let trackingUrl = $('#tracking-url').text();

    if (trackingUrl.indexOf('&l=') == -1) {
        let newUrl = trackingUrl + `&l=${landingPageId}`;
        $('#tracking-url').text(newUrl);
    }
});

$('.clickble-table-row').on('click', function () {
    if ($(this).hasClass("clicked-table-row")) {
        $(this).removeClass("clicked-table-row")
    } else {
        $(this).addClass("clicked-table-row")
    }
});

$(".copyLink").tooltip({
    show: null,
    position: {
        my: "left top",
        at: "left bottom"
    },
    open: function (event, ui) {
        ui.tooltip.animate({ top: ui.tooltip.position().top + 10 }, "fast");
    }
}).click(function () {
    var copyText = document.getElementById("tracking-url").innerHTML;
    var textArea = document.createElement("textarea");

    textArea.id = 'ta';
    textArea.style.position = 'fixed';
    textArea.style.top = 0;
    textArea.style.left = 0;
    textArea.style.width = '2em';
    textArea.style.height = '2em';
    textArea.style.padding = 0;
    textArea.style.border = 'none';
    textArea.style.outline = 'none';
    textArea.style.boxShadow = 'none';
    textArea.style.background = 'transparent';
    textArea.value = copyText;
    document.body.appendChild(textArea);

    var range = document.createRange();
    range.selectNode(textArea);
    textArea.select();

    document.execCommand("copy");
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

$('#save-tracking').on('click', function (e) {
    e.preventDefault();
    e.stopPropagation();
    var offer_Id = $('#hidden-id').val();
    var postbackUrl = $('#inputPostBackUrl').val();;
    $.ajax({
        type: 'POST',
        url: '/offer/SaveUserOfferTrackingSettings',
        data: { offer_Id: offer_Id, postbackUrl: postbackUrl },
        success: function (data) {
            debugger
            $('.result-msg').css('display','block')
            $('.result-msg').find('span').empty();
            $('.result-msg').find('span').html(data.Message);
            if (data.Code == 1) {
                $('.result-msg').addClass("success-msg");
            } else {
                $('.result-msg').addClass("error-msg");
            }
            $('.result-msg').delay(5000).fadeOut('slow');
        },
        error: function (err) {
            console.log(err);
        }
    });
});