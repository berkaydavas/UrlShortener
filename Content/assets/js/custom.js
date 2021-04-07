function showRequestError(errorCode) {
	var errorText = "";

	switch (errorCode) {
		case 403:
			errorText = "Bu işlem için yetkiniz bulunmuyor. Hata kodu: " + errorCode;
			break;
		case 404:
			errorText = "Sayfa bulunamadı. Hata kodu: " + errorCode;
			break;
		default:
			errorText = "Beklenmeyen bir hata oluştu. Hata kodu: " + errorCode;
			break;
	}

	swal("Hata Oluştu!", errorText, "error");
}

function getModalContent(pageUrl, modal, customActions = null, customUrlParams = null) {
	//customActions: list<selector<string>, type<string>, action<function>>

	var urlParams = "";

	if (customUrlParams != null) {
		customUrlParams.forEach(param => {
			urlParams += "&" + param.key + "=" + param.value;
		});
	}

	$.ajax({
		type: "GET",
		url: pageUrl + "?layout=0" + urlParams,
		success: function (data) {
			modal.innerHTML = data;

			$(modal).modal("dispose").modal();
			$(modal).on('hidden.bs.modal', function (event) {
				this.remove();
			});

			if (customActions != null) {
				for (var action of customActions) {

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

	var url = this.getAttribute("href");

	var modal = document.createElement("div");
	modal.setAttribute("id", "ajax-button-modal");
	modal.setAttribute("class", "modal fade");
	modal.setAttribute("role", "dialog");
	modal.setAttribute("aria-labelledby", "ajax-button-modal-label");
	modal.setAttribute("aria-hidden", "true");

	getModalContent(url, modal);
});

$("#notification-banner button").off().click(function (event) {
	event.preventDefault();
	this.closest("#notification-banner").remove();
	window.history.replaceState({}, document.title, "/");
});