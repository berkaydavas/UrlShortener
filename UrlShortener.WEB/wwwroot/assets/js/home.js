function stringCopy(string) {
  const copyInput = document.getElementById("copy-url-input");

  copyInput.value = string;
  copyInput.select();
  copyInput.setSelectionRange(0, 99999);

  document.execCommand("copy");

  copyInput.value = "";
}

const getVerificationToken = () => document.querySelector("[name=__RequestVerificationToken]").value;

$("#delete-all-button").click(async function () {
  const text = document.createElement("p");
  text.className = "text-center";
  text.innerHTML = "Bu işlem sonucunda <strong>tüm URL kayıtları</strong> silinecektir. Bu işlem geri alınamaz. Emin misiniz?";

  const value = await swal({
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
  });

  if (value) {
    const form = document.createElement("form");
    form.action = `/Url/Delete`;
    form.method = "POST";
    form.className = "d-none";

    form.innerHTML = `<input type="hidden" name="__RequestVerificationToken" value="${getVerificationToken()}">`;

    document.body.appendChild(form);

    form.submit();
  }
});

$(".copy-button").click(function () {
  const url = `https://${UrlPrefix}${this.dataset.urlCode}`;

  stringCopy(url);

  const text = document.createElement("p");
  text.setAttribute("class", "text-center text-break");
  text.innerHTML = `<a target="_blank" href="${url}">${url}</a> panonuza kopyalandı.`;

  swal({
    title: "URL Kopyalandı",
    content: text
  });
});

$(".edit-button").click(async function () {
  const button = this;

  const form = document.createElement("form");
  form.action = `/Url/Edit/${button.dataset.urlCode}`;
  form.method = "POST";

  form.innerHTML = `<input type="hidden" name="__RequestVerificationToken" value="${getVerificationToken()}">`;
  form.innerHTML += `<div class="input-group">
  <span class="input-group-text">${UrlPrefix}</span>
  <input type="text" class="form-control" name="NewCode" placeholder="URL Kısa Adı" required autofocus autocomplete="off" maxlength="100" value="${button.dataset.urlCode}">
  </div>`;

  const value = await swal({
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
  });

  if (value) {
    form.submit();
  }
});

$(".see-button").click(function () {
  const text = document.createElement("p");
  text.setAttribute("class", "text-center text-break");
  text.innerHTML = `<a target="_blank" href="${this.dataset.url}">${this.dataset.url}</a>`;

  swal({
    title: "URL Görüntüleme",
    content: text
  });
});

$(".delete-button").click(async function () {
  const button = this;

  const text = document.createElement("p");
  text.className = "text-center";
  text.innerHTML = `Bu işlem sonucunda <strong>${button.dataset.urlCode}</strong> kodlu URL silinecektir. Bu işlem geri alınamaz. Emin misiniz?`;

  const value = await swal({
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
  });

  if (value) {
    const form = document.createElement("form");
    form.action = `/Url/Delete/${button.dataset.urlCode}`;
    form.method = "POST";
    form.className = "d-none";

    form.innerHTML = `<input type="hidden" name="__RequestVerificationToken" value="${getVerificationToken()}">`;

    document.body.appendChild(form);

    form.submit();
  }
});