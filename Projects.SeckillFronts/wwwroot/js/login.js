// 登录js
$(function () {
    
    // 1、判断是否登录
    isLogin();

})



// 1、登录弹框
function loginBox() {
    layer.open({
        type: 1,
        title: "登陆",
        content: $('#loginbox')
    });
}

// 2、注册弹框
function registryBox() {
    layer.open({
        type: 1,
        title: "注册",
        content: $('#regbox')
    });
}


// 3、弹框登陆
function userLogin() {
    // 1、获取用户信息
    var username = $("#username").val(); // 用户名
    var pwd = $("#pwd").val(); // 用户密码
    if (username == "" || pwd == "") {
        layer.msg("用户名或者密码不能为空", {
            time: 1000,
            icon: 5
        })
        return;
    }

    // 2、根据用户信息进行注册
     var loginUrl = "https://localhost:5006/api/User/Login";
    //var loginUrl = "http://116.62.212.16:5006/api/User/Login"
    $.post(loginUrl, { "UserName": username, "Password": pwd }, function (result) {
        if (result.ErrorNo == "0") {
            // 1、用户信息
            var ResultDic = result.ResultDic;

            // 2、用户信息存储到本地缓存中
           // localStorage.setItem("AccessToken", ResultDic.AccessToken);
           // localStorage.setItem("UserName", ResultDic.UserName);
            setCache("user", ResultDic, ResultDic.ExpiresIn);

            // 3、关闭登录框
            layer.closeAll();

            // 4、登录提示
            alert("登录成功");

            // 5、显示登录信息
            isLogin();
        } else {
            alert(result.ErrorInfo);
        }
    });

}


// 4、弹框注册
function regLogin() {
    // 1、获取用户信息
    var username = $("#r_username").val(); // 用户名
    var pwd = $("#r_pwd").val(); // 用户密码
    var qq = $("#r_qq").val();// 用户QQ
    var tel = $("#r_tel").val();// 用户手机号
    if (username == "" || pwd == "" || qq == "" || tel == "") {
        layer.msg("各项不能为空", {
            time: 1000,
            icon: 5
        })
        return;
    }

    // 2、根据用户信息进行注册
     var registryUrl = "https://localhost:5006/api/User";
    //var registryUrl = "http://116.62.212.16:5006/api/User"
    $.post(registryUrl, { "UserName": username, "Password": pwd, "UserQQ": qq, "UserPhone": tel }, function (result) {
        if (result.ErrorNo == "0") {
            alert("注册成功");
            // 1、关闭注册框
            layer.closeAll();

            // 2、弹出登录框
            loginBox();
        } else {
            alert(result.ErrorInfo);
        }
    });
}

// 5、判断是否登录
function isLogin() {
    // 1、显示登录信息
    var user = getCache("user");

    // 2、判断是否登录
    if (!$.isEmptyObject(user)) {
        $("#useLogin").show();
        $("#noneLogin").hide();
        $("#manage").html("hello " + user.UserName);
        return true;
    } else {
        $("#useLogin").hide();
        $("#noneLogin").show();
        return false;
    }
}

// 6、是否已经登录
function isHasLogin() {
    if (isLogin()) {
        return true;
    } else {
        // 1、弹出登录框
        loginBox();
        return false;
    }
}

// 7、注销
function logout() {
    // 2.1 注销信息
    /*localStorage.removeItem("AccessToken");
    localStorage.removeItem("UserName");*/
    removeCache("user");

    // 2.2 重置为登录页面
    isLogin();
}