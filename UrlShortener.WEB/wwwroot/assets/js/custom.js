const getCookie = (name) => {
  let cookieValue = null;
  if (document.cookie && document.cookie !== "") {
    const cookies = document.cookie.split(";");
    const cookieLength = cookies.length;

    for (let i = 0; i < cookieLength; i++) {
      const cookie = cookies[i].trim();
      if (cookie.substring(0, name.length + 1) === (name + "=")) {
        cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
        break;
      }
    }
  }
  return cookieValue;
}

function showRequestError(errorCode) {
	var errorText;

	switch (errorCode) {
		case 403:
			errorText = `Bu işlem için yetkiniz bulunmuyor. Hata kodu: ${errorCode}`;
			break;
		case 404:
			errorText = `Sayfa bulunamadı. Hata kodu: ${errorCode}`;
			break;
		default:
			errorText = `Beklenmeyen bir hata oluştu. Hata kodu: ${errorCode}`;
			break;
	}

	swal("Hata Oluştu!", errorText, "error");
}

function getModalContent(pageUrl, modal, customActions = null, customUrlParams = null) {
	//customActions: list<selector<string>, type<string>, action<function>>

	let urlParams = "";

	if (customUrlParams != null) {
		customUrlParams.forEach(param => {
			urlParams += `&${param.key}=${param.value}`;
		});
	}

	$.ajax({
		type: "GET",
		url: pageUrl + "?layout=0" + urlParams,
		success: function (data) {
			modal.innerHTML = data;

			const bsModal = bootstrap.Modal.getOrCreateInstance(modal);
			bsModal.show();

			modal.addEventListener("hidden.bs.modal", function (event) {
				this.remove();
			});

			if (customActions != null) {
				for (let action in customActions) {

					switch (action.type) {
						case "submit":
							$(action.selector).off().submit(action.action);
							break;
						case "click":
							$(action.selector).off().click(action.action);
							break;
					}
				}
			}
		},
		error: function (data) {
			showRequestError(data.status);
		}
	});
}

$(".ajax-button").off().click(function (event) {
	event.preventDefault();

	const modal = document.createElement("div");
	modal.id = "ajax-button-modal";
	modal.className = "modal fade";
	modal.setAttribute("role", "dialog");
	modal.setAttribute("aria-labelledby", "ajax-button-modal-label");
	modal.setAttribute("aria-hidden", "true");

	getModalContent(this.href, modal);
});

$("#notification-banner button:not(.action)").off().click(function (event) {
	event.preventDefault();
	this.closest("#notification-banner").remove();

	const url = new URL(location.href);
	const params = url.searchParams;

	params.delete("m");
	params.delete("c");
	params.delete("message");

	window.history.replaceState({}, document.title, url.href);
});