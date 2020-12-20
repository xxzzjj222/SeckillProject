
// 1、订单支付页面
$(function () {
    // 1、支付
    $("#payment").click(function () {
        var OrderId = $("#OrderId").val();
        var OrderSn = $("#OrderSn").val();
        var OrderTotalPrice = $("#OrderTotalPrice").val();
        var ProductId = $("#ProductId").val();
        var UserId = $("#UserId").val();
        var ProductName = $("#ProductName").val();
         var paymentUrl = "https://localhost:5006/api/Payment/";
       // var paymentUrl = "http://116.62.212.16:5006/api/Payment/";
        $.ajax({
            method: "POST",
            url: paymentUrl,
            dataType: "json",
            data: {
                "OrderId": OrderId,
                "OrderSn": OrderSn,
                "OrderTotalPrice": OrderTotalPrice,
                "UserId": UserId,
            },
            success: function (result) {
                if (result.ErrorNo == "0") {
                    $("#qrcode").html("");
                   // 1、创建微信支付二维码
                    var qrcode = new QRCode("qrcode", {
                        text: "http://www.runoob.com" + new Date(),
                        width: 128,
                        height: 128,
                        colorDark: "#000000",
                        colorLight: "#ffffff",
                        correctLevel: QRCode.CorrectLevel.H
                    });
                } else {
                    alert(result.ErrorInfo);
                }
            }
        })
    })
})

// 支付详情
function showInfor() {
    $("#order-infor").slideToggle(100);
}