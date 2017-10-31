var lf = "http://rrsound.de";
//var lf = "http://localhost:58157";

function onClickDown() {
    postData("/Home/VolumeStepDown", {}, function (response) {
        setMuteBtn(response);
    });
}

function onClickMute() {
    postData("/Home/ToggleMute", {}, function (response) {
        setMuteBtn(response);
    });
}

function onClickUp() {
    postData("/Home/VolumeStepUp", {}, function (response) {
        setMuteBtn(response);
    });
}

function setMuteBtn(data) {
    var btn = $('.muteBtn');
    $(btn).html(data.item2);

    if (data.item1) {
        $(btn).removeClass('btn-success').addClass('btn-danger');
    }
    else {
        $(btn).removeClass('btn-danger').addClass('btn-success');
    }
}

function postData(path, data, cb) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.extend(data, { '__RequestVerificationToken': token });

    $.ajax({
        type: "POST",
        contentType: "application/x-www-form-urlencoded",
        url: lf+path,
        data: data,
        cache: false,

        success: function (response) {
            if (cb) {
                cb(response);
            }
        },
        error: function (error, textStatus, errorThrown) {
            console.debug(error);
            console.debug(errorThrown);
            console.debug(this);
            $('#textContent').html();
            $('#textContent').html(JSON.stringify(error));
            $('#errIframe').contents().find('body').html(error.responseText)
            $('.modal').modal('show');
        }
    });
}