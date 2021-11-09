$(function () {
    addEvent();
})

function addEvent() {
    // Gán sự kiện cho input số lượng

    $(".inputNumber").off('change').on('change', function () {
        let index = parseInt(this.getAttribute('data-bind')),
            value = parseFloat($(`table tbody td .price-${index}`).html().trim()),
            quality = parseInt(this.value),
            value_total = value * quality;
        if (value_total) {
            $(`table tbody td.price-display-${index}`)[0].innerHTML = value_total.toString();
        }
    })
}
