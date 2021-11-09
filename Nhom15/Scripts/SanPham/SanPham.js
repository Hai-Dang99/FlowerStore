$(function () {
    addEvent();
})

function addEvent() {
    // Gán sự kiện button Thê và giỏ hàng
    $('#btnAddToCart').click(function () {
        // Lấy mã SP
        var productCode = window.location.pathname;
        productCode = productCode.substr(productCode.lastIndexOf('/') + 1);

        if (productCode.endsWith('#')) {
            productCode = productCode.substring(0, productCode.length - 1);
        }

        // Lưu vào sessionStorage
        var cart = [],
            storeVal = sessionStorage['StorageFlowerStorage'];

        if (storeVal) {
            cart = JSON.parse(storeVal);
        }

        var existsItemIdx = cart.findIndex(function (i) {
            return i.ProductCode == productCode;
        })

        // Nếu Sp đã có thì tăng số lượng
        if (existsItemIdx != -1) {
            cart[existsItemIdx].Number++;
        } else {
            // Thêm mới vào giỏ hàng
            cart.push({
                ProductCode: productCode,
                Name: $('#txtProductName').html(),
                Price: parseFloat($('.price span').html()),
                Number: 1
            })
        }

        sessionStorage.setItem('StorageFlowerStorage', JSON.stringify(cart));

        // Show alert
        alert('Đã thêm vào giỏ hàng');

    })

    $('#btnAddToCartError').click(function () {
        alert('hàng đã hết, không thể mua');
    })
    $('#btnAddToCartError1').click(function () {

        
        alert('Bạn phải đăng nhập để mua hàng');
    })
        
    
}