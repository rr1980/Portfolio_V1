﻿//var lf = "http://rrsound.de:81";
//var lf = ":58157";
//var lf = "http://" + window.location.hostname + ":81";
//var lf = "http://" + window.location.hostname + ":58157";
//var lf = "http://" + window.location.hostname + ":" + portToPost + "/Sound/";
//var lf = "http://" + window.location.hostname + "/Sound/";
var lf = "Sound/Api/";

console.debug(window);

function onClickDown() {
    postData("VolumeStepDown", {}, function (response) {
        setMuteBtn(response);
    });
}

function onClickMute() {
    console.debug(window.location.hostname);

    postData("ToggleMute", {}, function (response) {
        setMuteBtn(response);
    });
}

function onClickUp() {
    postData("VolumeStepUp", {}, function (response) {
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
            console.debug(window.location);
            console.debug(this);
            if (cb) {
                cb(response);
            }
        },
        error: function (error, textStatus, errorThrown) {
            console.debug(error);
            console.debug(errorThrown);
            console.debug(this);
            //$('#textContent').html();
            //$('#textContent').html(JSON.stringify(error));
            //$('#errIframe').contents().find('body').html(error.responseText)
            //$('.modal').modal('show');
        }
    });
}