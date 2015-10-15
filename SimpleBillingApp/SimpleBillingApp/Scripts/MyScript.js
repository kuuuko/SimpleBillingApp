
function refreshTransactionField(subView) {
	$("#postField").html(subView);
}

var selectField = document.getElementById('selectArticle');
var quantityField = document.getElementById('quantity');

var subtotalField = document.getElementById("subtotalField");
var priceField = document.getElementById('priceField');

$("#quantity, #selectArticle").change(function () {
	var selectedPrice = selectField.options[selectField.selectedIndex].getAttribute('price');
	var quantity = quantityField.value;
	priceField.innerHTML = selectedPrice;
	subtotalField.innerHTML = ((quantity > 0) ? (selectedPrice * quantity) : 0).toFixed(2)
})