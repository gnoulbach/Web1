var _layout = {
    showtoast: function (mess) {
        $("#toast-message").html(mess);
        $("#toastf").toast({ delay: 5000 });
        $("#toastf").toast("show");
                }
}