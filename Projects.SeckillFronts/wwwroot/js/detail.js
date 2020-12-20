// 秒杀商品详情页面
var picIndex = 0;
var timer;
$(function () {

    setInterval(() => {
        countDown2();
    }, 1000);

    showStart();
    $(".swiper-button-pre,.swiper-button-next").hover(function () {
        clearInterval(timer);
    }, function () {
        showStart();
    });
    $(".swiper-button-pre").click(function () {
        picIndex--;
        if (picIndex == -1) {
            picIndex = 2;
        }
        showAll();
    });
    $(".swiper-button-next").click(function () {
        picIndex++;
        if (picIndex == 3) {
            picIndex = 0;
        }
        showAll();
    });
    $(".num-list li").hover(function () {
        clearInterval(timer);
        picIndex = $(this).index();
        showAll();
    }, function () {
        showStart();
    });

    // 1、加载秒杀商品详情
     var seckillDetailUrl = "https://localhost:5006/api/SeckillDetail/";
    //var seckillDetailUrl = "http://116.62.212.16:5006/api/SeckillDetail/";
    var seckillId = $("#seckillId").val();
    $.ajax({
        method: "GET",
        url: seckillDetailUrl + seckillId,
        dataType: "json",
        success: function (result) {
            if (result.ErrorNo == "0") {
                // 1、获取结果
                var resultDic = result.ResultDic;

                // 2、填充数据到页面上
                console.log(resultDic);
                $(".Producth2").html(resultDic.ProductTitle);
                $(".pro-title").html(resultDic.ProductTitle);
                $(".pro-summary").html(resultDic.ProductDescription);
                $(".pro-mspice").find("span").html("￥" + resultDic.SeckillPrice);
                $(".pro-scprice").find("span").html("￥" + resultDic.ProductPrice);
                $(".pro-stock").find("span").html(resultDic.SeckillStock);

                // 3、秒杀倒计时
                countDown2();

                // 4、添加url数据到pro-order中
                $(".pro-order a").data("url", "/Order/Index?ProductId=" + resultDic.ProductId + "&ProductCount=1&ProductPrice=" + resultDic.SeckillPrice + "&ProductUrl=" + resultDic.ProductUrl + "&ProductTitle=" + resultDic.ProductTitle +"");
            } else {
                alert(result.ErrorInfo);
            }
        }
    })


    // 2、立即抢购
    $(".pro-order a").click(function () {
        if (isHasLogin()) {
            location.href = $(this).data("url");
        }
    })
})

function addZero(i) {
    return i < 10 ? "0" + i : i + "";
}
function countDown2() {
    var nowtime = new Date();
    var endtime = new Date($("#endtime").val());
    var lefttime = parseInt((endtime.getTime() - nowtime.getTime()) / 1000);
    var d = parseInt(lefttime / (24 * 60 * 60))
    var h = parseInt(lefttime / (60 * 60) % 24);
    var m = parseInt(lefttime / 60 % 60);
    var s = parseInt(lefttime % 60);
    h = addZero(h);
    m = addZero(m);
    s = addZero(s);
    var sTime = "倒计时：" + d + "天" + h + "时" + m + "分" + s + "秒";
    $(".pro-time").html(sTime).css("line-height", "20px");
    $(".pro-order a").text("立即抢购").removeClass("btnDis");
    if (lefttime <= 0) {
        $(".pro-time").html("活动已结束");
        $(".pro-order a").text("活动已结束").addClass("btnDis").attr("href", "javascript:void(0);");
        return;
    }
    if (parseInt($(".pro-stock span").text()) == 0) {
        $(".pro-order a").text("已售罄").addClass("btnDis").attr("href", "javascript:void(0);");
        return;
    }
}

function showStart() {
    timer = setInterval(function () {
        picIndex++;
        if (picIndex == 3) {
            picIndex = 0;
        }
        showAll();
    }, 2000);
}
function showAll() {
    $(".img-list li").eq(picIndex).stop(true, true).fadeIn().siblings().stop(true, true).fadeOut();
    $(".num-list li").eq(picIndex).addClass("bg").siblings().removeClass("bg");
}