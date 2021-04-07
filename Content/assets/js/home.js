function stringCopy(string) {
    var copyInput = document.getElementById("copy-url-input");

    copyInput.value = string;
    copyInput.select();
    copyInput.setSelectionRange(0, 99999);

    document.execCommand("copy");

    copyInput.value = "";
}

$("#delete-all-button").click(function () {
    var text = document.createElement("p");
    text.setAttribute("class", "text-center");
    text.innerHTML = "Bu işlem sonucunda <b>tüm URL kayıtları</b> silinecektir. Bu işlem geri alınamaz. Emin misiniz?";

    swal({
        title: "Emin Misiniz?",
        content: text,
        icon: "warning",
        dangerMode: true,
        buttons: {
            cancel: {
                text: "Hayır",
                value: false,
                visible: true,
                closeModal: true
            },
            confirm: {
                text: "Evet",
                value: true
            }
        }
    }).then((value) => {
        if (value) {

            $.ajax({
                type: "POST",
                url: "/Url/DeleteAll",
                success: function (response) {
                    var data = JSON.parse(response);

                    swal(data).then(() => {
                        if (data.icon == "success") {
                            location.href = "/";
                        }
                    });
                },
                error: function (data) {
                    showRequestError(data.status);
                }
            });
        }
    });
});

$(".copy-button").click(function () {
    var url = "https://" + UrlPrefix + this.dataset.urlCode;

    stringCopy(url);

    var text = document.createElement("p");
    text.setAttribute("class", "text-center text-break");
    text.innerHTML = "<a target=\"_blank\" href=\"" + url + "\">" + url + "</a> panonuza kopyalandı.";

    swal({
        title: "URL Kopyalandı",
        content: text
    });
});

$(".edit-button").click(function () {
    var button = this;

    var form = document.createElement("form");
    form.setAttribute("id", "url-edit-form");

    form.innerHTML = "<input type=\"hidden\" name=\"code\" value=\"" + button.dataset.urlCode + "\" \>";
    form.innerHTML += "<div class=\"input-group\"><div class=\"input-group-prepend\"><span class=\"input-group-text\" id=\"url-edit-code-text\">" + UrlPrefix + "</span></div><input type=\"text\" class=\"form-control\" name=\"newCode\" placeholder=\"URL Kısa Adı\" required=\"required\" autofocus=\"autofocus\" autocomplete=\"off\" maxlength=\"100\" value=\"" + button.dataset.urlCode + "\" aria-describedby=\"url-edit-code-text\"></div>";

    swal({
        title: "URL Düzenleme",
        content: form,
        buttons: {
            cancel: {
                text: "İptal",
                value: false,
                visible: true,
                closeModal: true
            },
            confirm: {
                text: "Kaydet",
                value: true
            }
        }
    }).then((value) => {
        if (value) {

            $.ajax({
                type: "POST",
                url: "/Url/Edit",
                data: new FormData(document.getElementById("url-edit-form")),
                processData: false,
                contentType: false,
                success: function (response) {
                    var data = JSON.parse(response);

                    swal(data).then(() => {
                        if (data.icon == "success") {
                            location.href = "/";
                        }
                    });
                },
                error: function (data) {
                    showRequestError(data.status);
                }
            });
        }
    });
});

$(".see-button").click(function () {
    var text = document.createElement("p");
    text.setAttribute("class", "text-center text-break");
    text.innerHTML = "<a target=\"_blank\" href=\"" + this.dataset.url + "\">" + this.dataset.url + "</a>";

    swal({
        title: "URL Görüntüleme",
        content: text
    });
});

$(".delete-button").click(function () {
    var button = this;

    var text = document.createElement("p");
    text.setAttribute("class", "text-center");
    text.innerHTML = "Bu işlem sonucunda <b>" + button.dataset.urlCode + "</b> kodlu URL silinecektir. Bu işlem geri alınamaz. Emin misiniz?";

    swal({
        title: "Emin Misiniz?",
        content: text,
        icon: "warning",
        dangerMode: true,
        buttons: {
            cancel: {
                text: "Hayır",
                value: false,
                visible: true,
                closeModal: true
            },
            confirm: {
                text: "Evet",
                value: true
            }
        }
    }).then((value) => {
        if (value) {

            $.ajax({
                type: "POST",
                url: "/Url/Delete",
                data: {
                    code: button.dataset.urlCode
                },
                success: function (response) {
                    var data = JSON.parse(response);

                    swal(data).then(() => {
                        if (data.icon == "success") {
                            location.href = "/";
                        }
                    });
                },
                error: function (data) {
                    showRequestError(data.status);
                }
            });
        }
    });
});