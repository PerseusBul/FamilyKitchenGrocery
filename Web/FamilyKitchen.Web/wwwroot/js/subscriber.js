var btn = document.getElementById('button').addEventListener('click', (e) => {
    e.preventDefault();
    var token = $("#subscribeForm input[name=__RequestVerificationToken]").val();
    var subscriber = $("#subscribedEmail").val();
    var json = { Subscriber: subscriber };

    $.ajax({
        url: "/subscribers",
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            $("#subscribedEmail").val(data.message);
        }
    });
})
