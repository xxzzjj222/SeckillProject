// seckill 页面js
$(function () {
    // 1、秒杀倒计时
    setInterval(() => {
        countDown();
    }, 1000);

    // 4、购买J_currentCon
    $(".J_currentCon").on("click",".J_buy", function () {
        //判断是否登陆
        //如果没有登陆
        /*layer.msg("还未登陆不能秒杀", {
            time: 1000,
            icon: 5
        })*/
        // 4.1、是否已经登录
        if (isHasLogin()) {
            // 4.2 跳转到详情页
            location.href = $(this).data("url");
        }
    });

    // 5、加载页面数据,加载 秒杀时间
     var seckillTimeUrl = "https://localhost:5006/api/Seckill";
    //var seckillTimeUrl = "http://116.62.212.16:5006/api/Seckill"
    $.ajax({
        method: "GET",
        url: seckillTimeUrl,
        dataType: "json",
        success: function (result) {
            if (result.ErrorNo == "0") {
                // 1、在页面上增加显示时间
                var resultList = result.ResultList;
                
                // 2、先将页面数据缓存在数组中
                var bannerList = [];
                $.each(resultList, function (i, val) {
                    if (i == 0) {
                        bannerList.push("<li class='active J_currentBanner'>");
                    } else {
                        bannerList.push("<li>");
                    }
                    bannerList.push("<a href='javascript: void (0);'>");
                    if (i == 0) {
                        var endtime = val.SeckillDate + "," + val.SeckillEndtime;
                        bannerList.push("<em id='endtime' data-endtime='" + endtime + "' data-timeId='" + val.Id +"'>" + val.SeckillStarttime + "</em>");
                        bannerList.push("<span id='showtime'></span>");
                    } else {
                        bannerList.push("<em>" + val.SeckillStarttime + "</em>");
                        bannerList.push("<span>即将开始</span>");
                    }
                    bannerList.push("</a>");
                    bannerList.push("</li>");
                });
              /*  bannerList.push('<li id="userbox"> <a href="javascript:void(0);"><em onclick="loginBox()">登陆</em><span onclick="registryBox()"> 注册</span></a> </li>');*/
                // 3、显示到页面上
                $("#seckill-ul").html(bannerList.join(""));

                // 4、显示倒计时
                countDown();

                // 5、加载秒杀商品
                loadSeckills();
            } else {
                alert(result.ErrorInfo);
            }
        }
    })

})



// 加载秒杀商品
 var seckillUrl = "https://localhost:5006/api/Seckill/";
//var seckillUrl = "http://116.62.212.16:5006/api/Seckill/"
function loadSeckills() {
    var TimeId = $("#endtime").attr("data-timeId");
    $.ajax({
        method: "GET",
        url: seckillUrl + TimeId,
        dataType: "json",
        success: function (result) {
            if (result.ErrorNo == "0") {
                // 1、在页面上增加显示时间
                var resultList = result.ResultList;
                console.log(resultList);
                // 2、先将页面数据缓存在数组中
                var seckillList = [];
                $.each(resultList, function (i, val) {
                    seckillList.push("<li>");
                    seckillList.push("<a href='#' class='seckill-box'></a>");
                    seckillList.push("<div class='item-box'>");
                    seckillList.push("<span class='img-con'>");
                    seckillList.push("<img class='done' src = '" + val.ProductUrl +"' >");
                    seckillList.push("</span> <span class='pro-con'>");
                    seckillList.push("<span class='name'>" + val.ProductTitle +"</span>");
                    seckillList.push("<span class='desc'>" + val.ProductDescription +"</span>");
                    seckillList.push("<span class='pro-num'><span>库存:</span><em class='s-num-9199'>" + val.SeckillStock +"</em></span>");
                    seckillList.push("<span class='price'>");
                    seckillList.push("" + val.SeckillPrice +"元");
                    seckillList.push("<del>" + val.ProductPrice +"元</del>");
                    seckillList.push("</span>");
                    seckillList.push("</div>");
                    seckillList.push("<a href='javascript:;' data-url='Detail/Index?seckillId=" + val.Id + "&endtime=" + $("#endtime").attr("data-endtime") + "' class='J_buy  J_bug_9199' data-id='2194100014'> 立即抢购</a >");
                   
                    seckillList.push("</li>");
                });
                // 3、显示到页面上
                $(".J_currentCon").html(seckillList.join(""));
            } else {
                alert(result.ErrorInfo);
            }
        }
    })
}

/*定时器*/
function countDown() {
    var nowtime = new Date();
    var time = $("#endtime").attr("data-endtime");
    var endtime = new Date(time); // "2020/8/15,15:00:00"
    var lefttime = parseInt((endtime.getTime() - nowtime.getTime()) / 1000);
    var d = parseInt(lefttime / (24 * 60 * 60))
    var h = parseInt(lefttime / (60 * 60) % 24);
    var m = parseInt(lefttime / 60 % 60);
    var s = parseInt(lefttime % 60);
    h = addZero(h);
    m = addZero(m);
    s = addZero(s);
    var sTime = "活动倒计时<br>" + d + "天" + h + "时" + m + "分" + s + "秒";
    $("#showtime").html(sTime).css("line-height", "20px");
    $(".J_buy").text("立即抢购").removeClass("btnDis");
    if (lefttime <= 0) {
        $("#showtime").html("活动已结束");
        $(".J_buy").text("活动已结束").addClass("btnDis").attr("href", "javascript:void(0);");
        return;
    }
    if (parseInt($(".s-num-9199").text()) == 0) {
        $(".J_bug_9199").text("已售罄").addClass("btnDis").attr("href", "javascript:void(0);");
        return;
    }
}

function addZero(i) {
    return i < 10 ? "0" + i : i + "";
}

