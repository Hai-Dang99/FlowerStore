$(function () {
    fillData();
    addEvent();
})

function refresh() {
    fillData();
    addEvent();
}

function addEvent() {
    // Gán sự kiện nút Xóa
    $('.delProduct').off('click').on('click', function (e) {
        var productCode = $(e.currentTarget).attr('valProductCode'),
            cart = [],
            storageVal = sessionStorage['StorageFlowerStorage'];

        if (storageVal) {
            cart = JSON.parse(storageVal);
        }

        cart = cart.filter(e => e.ProductCode != productCode);
        sessionStorage.setItem('StorageFlowerStorage', JSON.stringify(cart));

        refresh();
    })

    // Gán sự kiện Đặt hàng
    $('#buyNow').off('click').on('click', e => {
        $.ajax({
            url: '/GioHangs/Order',
            method: 'POST',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: {
                    data: sessionStorage['StorageFlowerStorage']
                },
        }).done(function (res) {

        })
    });
}

function fillData() {
    var cart = [],
        storageVal = sessionStorage['StorageFlowerStorage'],
        bodyHtml = '';

    if (storageVal) {
        cart = JSON.parse(storageVal);
    }

    cart.filter(function (i) {
        bodyHtml += `<tr>
                        <td>${i.ProductCode}</td>
                        <td>${i.Name}</td>
                        <td>${i.Price}</td>
                        <td>${i.Img}</td>
                        <td style="width:30px"><input type="number" min=1 value=${i.Number}></input></td>
                        <td><a class="delProduct" valProductCode="${i.ProductCode}">Xóa</a></td>
                    </tr>`;
    })

    $('.cartDetailCtn table tbody').html(bodyHtml ? bodyHtml : 'Chưa có sản phẩm nào trong giỏ hàng !');


}