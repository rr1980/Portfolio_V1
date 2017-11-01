

function getCookie(c_name) {

    var c = Cookies.get("SettingsCookie");
    console.debug(c)
    console.debug(JSON.parse(c))
    return JSON.parse(c).Ports;
}

