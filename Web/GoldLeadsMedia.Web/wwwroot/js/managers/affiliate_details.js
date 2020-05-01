$('.date-range').daterangepicker({
    ranges: {
        'Today': [moment(), moment()],
        'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
        'Last 7 Days': [moment().subtract(6, 'days'), moment()],
        'Last 30 Days': [moment().subtract(29, 'days'), moment()],
        'This Month': [moment().startOf('month'), moment().endOf('month')],
        'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
    },
    "alwaysShowCalendars": true,
}, function (start, end, label) {
    console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
});

$('#makePayment').on('click', function () {
    var user_Id = $(this).data('user');
    var price = $('#paymentAmount').val();
    $.ajax({
        type: 'POST',
        url: '/manager/makepayment',
        data: { User_Id: user_Id, Price: price },
        success: function (data) {
            if (data.Code == "1") {
                var date = $('.date-range-payments').val();
                GetUserPayments(user_Id, date);
                $('#paymentAmount').val("");
            } else {

            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}) 


function GetUserPayments(user_Id, date) {
    $.ajax({
        type: 'POST',
        url: '/report/getuserpaymentsdata',
        data: { User_Id: user_Id, Date: date },
        success: function (data) {
            $('.offer-content-payments').empty();
            $('.offer-content-payments').html(data);
        },
        error: function (err) {
            console.log(err);
        }
    });
}


//$('.userReport').on('click', function () {
//    var user_Id = $(this).data('user');
//    var date = $('.date-range-report').val();
//    debugger;
//    //$.ajax({
//    //    type: 'POST',
//    //    url: '/report/getuserreport',
//    //    data: { User_Id: user_Id, Date: date },
//    //    success: function (data) {
//    //        $('.offer-content-report').empty();
//    //        $('.offer-content-report').html(data);
//    //    },
//    //    error: function (err) {
//    //        console.log(err);
//    //    }
//    //});
//    //CBE9EB6B52DC4AC3A62DCD4AD474138C
//    window.location.href = `http://localhost:64008/manager/affiliateDetails?a_Id=${user_Id}`;
//})

$('.userPaymentsReport').on('click', function () {
    debugger
    var user_Id = $(this).data('user');
    var date = $('.date-range-payments').val();
    debugger
    GetUserPayments(user_Id, date);
})