
// 订单确认页面
$(function () {
    // 1、下单
    $(".btn-order").click(function () {
        // 判断是否登录
        if (!isHasLogin()) {
            return;
        }

        var ProductId = $("#ProductId").val();
        var ProductUrl = $("#ProductUrl").val();
        var ProductTitle = $("#ProductTitle").val();
        var ProductPrice = $("#ProductPrice").val();
        var ProductCount = $("#ProductCount").val();
         var orderUrl = "https://localhost:5006/api/Order/";
        // var orderUrl = "http://116.62.212.16:5006/api/Order/";
        $.ajax({
            method: "POST",
            url: orderUrl,
            dataType: "json",
            data: {
                "ProductId": ProductId,
                "ProductUrl": ProductUrl,
                "ProductName": ProductTitle,
                "OrderTotalPrice": ProductPrice,
                "ProductCount": ProductCount,
                "RequestId": getRequestId()
            },
            success: function (result) {
                if (result.ErrorNo == "0") {
                    // 1、跳转到支付页面
                    var resultDic = result.ResultDic;
                    location.href = "/Payment/Index?OrderId=" + resultDic.OrderId + "&OrderSn=" + resultDic.OrderSn + "&OrderTotalPrice=" + resultDic.OrderTotalPrice + "&UserId=" + resultDic.UserId + "&ProductId=" + resultDic.ProductId + "&ProductName=" + resultDic.ProductName +"";
                } else {
                    alert(result.ErrorInfo);
                }
            }
        })
    })
})

//创建请求唯一id 方法：时间戳 + UserId
function createRequestId(UserId) {
   // return Number(Math.random().toString().substr(3, length) + Date.now() + UserId).toString(37);
    return (Date.now() + UserId).toString();
}

// 保存请求id
function saveRequestId(userId, requestId) {
    // 1、存储requestId
    sessionStorage.setItem(userId, requestId);
}

// 获取请求id
function getRequestId() {
    // 1、获取userId
    var user = getCache("user");

    // 2、从sessionStorage中获取requestId
    var requestId = sessionStorage.getItem(user.UserId);

    // 3、判断requestId是否存在
    if (!requestId) {
        requestId =  createRequestId(user.UserId);
    }
    // 4、存储requestId
    saveRequestId(user.UserId, requestId);
    return requestId;
}

