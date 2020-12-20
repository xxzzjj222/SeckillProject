// 缓存js
//封装过期控制代码
function setCache(key, value, exp) {
    var time = new Date();
    time.setSeconds(exp);
    var expTime = time.getTime();
    localStorage.setItem(key, JSON.stringify({ data: value, time: expTime }));
}
function getCache(key) {
    var data = localStorage.getItem(key);
    console.log(data);
    if (data == null) {
        // 返回空对象
        return {};
    }
    var dataObj = JSON.parse(data);
    console.log(new Date().getTime());
    console.log(dataObj.time);
    if (new Date().getTime() > dataObj.time) {
        console.log('信息已过期');
        localStorage.removeItem(key);
        return {};
    } else {
        var dataObjDatatoJson = dataObj.data;
        return dataObjDatatoJson;
    }
}

function removeCache(key) {
    localStorage.removeItem(key);
}

