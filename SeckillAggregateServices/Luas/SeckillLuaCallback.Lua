--[[
	函数定义
]]--

--1.删除记录订单号:目的：创建订单方法幂等性，调用方网络超时可以重复调用,存在订单号直接返回购买成功，不至于超卖
local function delRecordOrderSn()
local requestIdKey=ARGV[3];--订单号key
local orderSn=ARGV[4];--订单号
--删除订单号
redis.call('del',requestIdKey)
end

--2.删除用户购买限制
local function delUserBuyLimit()
local userBuyLimitKey=ARGV[1];--购买限制key
local productKey=KEYS[1];--商品key
local productCount=tonumber(ARGV[2]);--商品数量
redis.call('HINCRBY',userBuyLimitKey,"UserBuyLimit",-productCount);
end

--3.恢复库存
local function recoverSeckillStock()
local productKey=KEYS[1];--商品key
local productCount=tonumber(ARGV[2]);--商品数量
redis.call('HINCRBY',productKey,"SeckillStock",productCount);
end

--[[
	函数调用
]]--

--1.删除订单号
delRecordOrderSn();

--2.撤销用户购买限制
delUserBuyLimit();

--3.恢复库存
recoverSeckillStock();
