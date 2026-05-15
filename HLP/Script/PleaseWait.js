function PleaseWait() {
    // make the mouse cursor into the "wait" hourglass
    //document.body.style.cursor = "wait";

    // show the please wait div
    if (document.getElementById) {
        var message = document.getElementById("PleaseWait");
        if (message) {
            message.style.display = "block";
            var _content = document.getElementById("form1");
            _content.style.backgroundColor = "#696969";
            _content.style.opacity = 0.5;
            _content.style.pointerEvents = "none";
        }
    }

    // allow the form to post back
    return true;
}