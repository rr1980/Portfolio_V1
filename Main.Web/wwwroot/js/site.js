//var lh = "http://localhost:58157";
////var lh = "http://rrsound.de";


//function getData(path, data, cb) {
//    //var token = $('input[name="__RequestVerificationToken"]').val();
//    //$.extend(data, { '__RequestVerificationToken': token });
//    console.debug(lh + path);
//    $.ajax({
//        type: "GET",
//        crossDomain: true,
//        //contentType: "text/html",
//        url: lh+path,
//        //data: data,
//        cache: false,

//        success: function (response) {
//            if (cb) {
//                cb(response);
//            }
//        },
//        error: function (error, textStatus, errorThrown) {
//            console.debug(error);
//            console.debug(errorThrown);
//            console.debug(this);
//            $('#textContent').html();
//            $('#textContent').html(JSON.stringify(error));
//            $('#errIframe').contents().find('body').html(error.responseText)
//            $('.modal').modal('show');
//        }
//    });
//}

