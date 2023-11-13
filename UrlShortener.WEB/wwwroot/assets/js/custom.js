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