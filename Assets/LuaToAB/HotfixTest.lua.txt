-- print("************第一个热补丁****************")

-- --直接写好代码 运行 时会报错的
-- --我们必须做4个非常重要的操作
-- --1.加特性
-- --2.加宏 第一次开发热补丁需要加
-- --3.生成代码
-- --4.hotfix注入 注入时可能报错 提示你要引入Tools

-- --热补丁的缺点：只要我们修改了热补丁类的代码，我们就需要重新执行第4步
-- --需要重新点击 注入

-- --lua当中 热补丁代码固定写法
-- --xlua.hotfix(类,"函数名",lua函数)

-- xlua.hotfix(CS.HotfixTest,"Add",function(self,a,b)
--     return a+b 
-- end)

-- --静态函数 不用传第一个参数
-- xlua.hotfix(CS.HotfixTest,"Speak",function(a)
--     print(a)
-- end)


-- xlua.hotfix(CS.HotfixTest,{
--     -- Update=function(self)
--     --     print(os.time())
--     -- end,
--     Add=function(self,a,b)
--         return a+b
--     end,
--     Speak=function(a)
--         print(a)
--     end
-- })

-- xlua.hotfix(CS.HotfixTestClass,{
--     --构造函数 热补丁 固定写法！！！
--     --他们和别的函数不同 不是替换 是先调用原逻辑 在调用Lua逻辑
--     [".ctor"]=function()
--         print("Lua热补丁构造函数")
--     end,
--     Speak=function(self,a)
--         print("我说"..a)
--     end,
--     --析构函数固定写法
--     Finalize=function()
        
--     end
-- })

-- print("************协程函数替换***************")

-- --xlua.hotfix(类,"函数名",lua函数)
-- --要在lua中配合C#协程函数 那么必使用它
-- util=require("xlua.util")
-- xlua.hotfix(CS.HotfixTest,{
--     TestCoroutine=function(self)
--         --返回一个正儿八经的 xlua处理过的lua协程函数
--         return util.cs_generator(function()
--             while true do
--                 coroutine.yield(CS.UnityEngine.WaitForSeconds(1))
--                 print("Lua打补丁后的协程函数")
--             end
--         end)
--     end
-- })

-- --如果我们为加了Hotfix特性的C#类新加了函数内容
-- --不能只注入 必须要先生成代码 再注入 不然注入会报错

-- print("***********属性和索引器替换**************")
-- xlua.hotfix(CS.HotfixTest,{
--     --如果是属性进行热补丁重定向
--     --set_属性名 是设置属性 的方法
--     --get_属性名 是得到属性 的方法
--     set_Age=function(self,v)
--         print("Lua重定向的属性"..v)
--     end,
--     get_Age=function(self)
--         return 10
--     end,

--     --索引器固定写法
--     --set_Item 通过设置索引器
--     --get_Item 通过索引器获取

--     set_Item=function(self,index,v)
--         print("Lua重定向索引器，索引："..index.."值"..v)
--     end,
--     get_Item=function(self,index)
--         print("Lua重定向索引器")
--         return 999
--     end
-- })


print("***********事件加减替换**************")

xlua.hotfix(CS.HotfixTest,{
    --add_事件名 代表着事件加操作
    --remove_事件名 减操作
    add_myEvent=function(self,del)
        
        print(del)
        print("添加事件函数")    
    --会去尝试使用lua使用C#的方法去添加
    --在事件加减的重定向lua函数中
    --千万不要把传入的委托往事件里存
    --否则会死循环
    --会把传入的函数 存在lua中
    --self:myEvent("+",del)
    end,
    remove_myEvent=function(self,del)
        print(del)
        print("移除事件函数")
    end
})
