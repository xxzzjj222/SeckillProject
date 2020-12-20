--[[
	1、函数定义
]]

--1.单品限流
local function seckillLimit()
local seckillLimitKey=ARGV[2];
--1.获取单品已请求数量
local limitCount=tonumber(redis.call('get',seckillLimitKey) or "0");
local requestCountLimits=tonumber(ARGV[4]);--限制的请求数量
local seckillLimitKeyExpire=tonumber(ARGV[5]);--2秒过期
if limitCount+1>requestCountLimits then --超出限流大小
return 0,seckillLimitKeyExpire.."内只能请求"..requestCountLimits.."次";--失败
else --请求数+1,并设置过期时间
redis.call('INCRBY',seckillLimitKey,"1");
redis.call('expire',seckillLimitKey,seckillLimitKeyExpire);
return 1;--成功
end
end

--2.记录订单号，目的：创建订单方法幂等性，调用网络超市可以重复调用，存在订单号直接返回抢购成功，不至于超读
local function recordOrderSn()
local requestIdKey=ARGV[6];--订单号key
local orderSn=ARGV[7];--订单号
local hasOrderSn=tostring(redis.cal('get',requestIdKey)or"");
if string.len(hasOrderSn)==0 then
redis.call('set',requestIdKey,orderSn);
return 1;--设置成功
else
return 0,"不能重复下单";--失败
end
end


--3.用户购买限制
local function userBuyLimit()
local userBuyLimitKey=ARGV[1]; --购买限制key
local productKey=KEYS[1];--商品key
local productCount=tonumber(ARGV[3]);--商品数量

--1.用户已经购买数量
local userHasBuyCount=tonumber(redis.call('hget',userBuyLimitKey,"UserBuyLimit") or "0");
--2.获取限制的数量
local seckillLimit=tonumber(redis.call('hget',productKey,"SeckillLimit") or "0");
if userHasBuyCount+1>seckillLimit then --超出购买数量
return 0,"该商品只能购买"..seckillLimit.."件";--失败
else
redis.call('HINCRBY',userBuyLimitKey,"UserBuyLimit",productCount)
return 1;--成功
end
end

--4.扣减库存
local function substractSeckillStock()
local productKey=KEYS[1];--商品key
local productCount=tonumber(ARGV[3]);--商品数量
--1.1扣减库存
local lastNum=redis.call('HINCRBY',productKey,"SeckillStock",-productCount)
--1.2判断库存是否完成
if lastNum<0 then 
return 0,"秒杀已结束";--失败
else
return 1;--成功
end
end

--[[
	函数调用
]]--

--1.单品限流
local status,msg=seckillLimit();
if status==0 then
return msg
end

--2.记录订单号
local status,msg=recordOrderSn();
if status==0 then
return msg
end

--3.用户购买限制
status,msg=userBuyLimit();
if status==0 then
return msg;
end

--4.扣减秒杀库存
status,msg=substractSeckillStock();
if status==0 then
return msg;
end
--返回成功标识
return 1;

