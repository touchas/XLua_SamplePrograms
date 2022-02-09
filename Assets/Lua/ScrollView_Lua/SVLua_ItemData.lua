Object:subClass("SVLua_ItemData")

local txt=ABMgr:LoadRes("json","ImgData",typeof(TextAsset))
--print(txt.text)
--获取它的文本信息 进行json解析
local itemList=Json.decode(txt.text)

--加载出来是一个像数组结构的数据
--不方便我们通过 id去获取里面的内容 所以 我们用一张新表转存一次
--而且这张 新的道具表 在任何地方 都能够被使用
--一张用来存储道具信息的表
--键值对形式 键是道具ID 值时道具表的一行信息
ItemData={}
for _,value in pairs(itemList) do
    ItemData[value.ImgIndex]=value
end
-- for key,value in pairs(ItemData) do
--     print(key,value)
-- end
-- print(ItemData[1].ImgIndex..ItemData[1].ImgName)
