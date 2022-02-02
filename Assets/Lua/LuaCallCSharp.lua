print("调用LuaCallCSharp")
GameObject=CS.UnityEngine.GameObject
Debug=CS.UnityEngine.Debug
Vector3=CS.UnityEngine.Vector3

-- local obj1=CS.UnityEngine.GameObject()
-- local obj2=CS.UnityEngine.GameObject("测试游戏对象")

-- --类中的静态对象 可以直接使用.来调用
-- obj2.transform.position=Vector3(100,100,100)
-- local obj3= GameObject.Find("测试游戏对象")
-- Debug.Log(obj3.transform.position)

-- --调用Unity自带方法
-- local obj4= GameObject.Find("测试游戏对象")
-- obj4.transform:Translate(Vector3.right)
-- Debug.Log(obj4.transform.position)
-- --调用C#自定义类，有命名空间需要打上命名空间
-- local t1=CS.Test()
-- t1:Speak("testSpeak")
-- local t2=CS.TestNameSpace.Test2()
-- t2:Speak("test2Speak")

-- --枚举调用
-- --调用Unity当中的枚举
-- --枚举的调用规则和 类的调用规则是一样的
-- --CS.命名空间.枚举名.枚举成员
-- --也支持取别名
-- PrimitiveType=CS.UnityEngine.PrimitiveType
-- GameObject=CS.UnityEngine.GameObject
-- local obj=GameObject.CreatePrimitive(PrimitiveType.Cube)
-- E_MyEnum=CS.E_MyEnum
-- local e = E_MyEnum.Idle
-- print(e)

-- --枚举转换相关
-- --数值转枚举
-- local a=E_MyEnum.__CastFrom(1)
-- print(a)
-- --字符串转枚举
-- local b=E_MyEnum.__CastFrom("Atk")
-- print(b)

-- local arrayTestObj=CS.ArrayTest()
-- --访问C#中的数组长度
-- print("array数组长度："..arrayTestObj.array.Length)
-- --访问元素
-- print("访问元素："..arrayTestObj.array[0])

-- for i=0,arrayTestObj.array.Length-1 do
--     print("array数组第"..i.."个元素的值："..arrayTestObj.array[i])
-- end

-- --在lua中创建一个C#狐族
-- local lua_CSharpList=CS.System.Array.CreateInstance(typeof(CS.System.Int32),10)
-- print("lua创建C#数组："..lua_CSharpList.Length)
-- --这里的索引遵循C#
-- print("lua创建C#数组，第1个元素："..lua_CSharpList[0])
-- print("lua创建C#数组，第2个元素："..lua_CSharpList[1])



-- --使用静态方法
-- --CS.命名空间.类名.静态方法名()
-- CS.ExtensionTestClass.Eat()

-- --成员方法 实例化出来用
-- local obj=CS.ExtensionTestClass()
-- --成员方法 一定用冒号
-- obj:Speak("ABC:39023sidjflkq0e")

-- --使用拓展方法 和使用成员方法一致
-- --要调用 C#某个类的拓展方法 那一定要在拓展方法的静态类前面加上LuaCallCSharp特性
-- obj:Move()

-- RefOutFunTestClass=CS.RefOutFunTestClass
-- local obj=RefOutFunTestClass()

-- print("********************Lua调用C# ref********************")
-- --ref参数 会以多返回值的形式返回给lua
-- --如果函数存在返回值 那么第一个值 就是该返回值
-- --之后的返回值 就是ref的结果 从左到右一一对应
-- --ref参数 需要传入一个默认值（0,0） 占位置（a接收的是函数默认的返回值）
-- --a相当于函数返回值
-- --b 第一个ref
-- --c 第二个ref
-- local a,b,c = obj:RefFun(1,0,0,1)
-- print(a)
-- print(b)
-- print(c)

-- print("********************Lua调用C# out********************")
-- --out参数 会以多返回值的形式返回给lua
-- --如果函数存在返回值 那么第一个值就是该返回值
-- --之后的返回值 就是out的结果 从左到右一一队形
-- --out参数 不需要传占位置的值
-- local a,b,c=obj:OutFun(20,30)
-- print(a)
-- print(b)
-- print(c)

-- print("********************Lua调用C# ref out********************")
-- --混合使用时 综合上面的规则
-- --ref需站位 out不用传
-- --第一个是函数的返回值 之后 从左到右依次对应ref或者out
-- local a,b,c=obj:RefOutFun(20,1)
-- print(a)--300
-- print(b)--200
-- print(c)--400

-- local obj =CS.OverloadedFuncTest()

-- --虽然Lua自己不支持写重载函数
-- --但是Lua支持调用C#中的重载函数
-- print(obj:Calc())
-- print(obj:Calc(15,1))

-- --lua虽然支持调用C#重载函数
-- --但是因为Lua中的数值类型 只有Number
-- --对C#中多精度的重载函数支持不好
-- --在使用时 可能出现意想不到的问题
-- print(obj:Calc(10))
-- --这里float的输出结果为0
-- print(obj:Calc(10.2))

-- --解决重载函数含糊的问题
-- --XLua提供了解决方案 反射机制
-- --这种方法只做了解 尽量别用
-- --Type是反射的关键类
-- --得到指定函数的相关信息
-- local m1= typeof(CS.OverloadedFuncTest):GetMethod("Calc",{typeof(CS.System.Int32)})
-- local m2= typeof(CS.OverloadedFuncTest):GetMethod("Calc",{typeof(CS.System.Single)})

-- --通过xLua提供的一个方法 把它转成lua函数来使用
-- --一般我们转依次 然后重复使用
-- local f1=xlua.tofunction(m1)
-- local f2=xlua.tofunction(m2)

-- --成员方法 第一个参数传对象
-- --静态方法 不用传对象
-- print(f1(obj,10))
-- print(f2(obj,10.2))

-- print("********************Lua调用C# 委托事件********************")
-- local obj=CS.CallDelTesTClass()

-- --委托是用来装函数的
-- --使用C#中的委托 就是用来装Lua函数的
-- local fun=function()
-- 	print("Lua函数Fun")
-- end

-- --Lua中没有复合运算符 不能+=
-- --如果第一次往委托中加函数 因为是nil
-- --所以第一次 要先等=
-- obj.del=fun
-- --obj.del=obj.del+fun
-- obj.del=obj.del+fun
-- --不建议这么写 不好减去 最好哈市先声明函数再加
-- obj.del=obj.del+function()
-- 	print("临时声明的函数")
-- end

-- obj.del()
-- print("********************开始减函数********************")
-- obj.del=obj.del-fun
-- obj.del=obj.del-fun
-- --委托执行
-- obj.del()
-- print("********************清空函数********************")
-- --清空所有存储的函数
-- obj.del=nil
-- --清空过后得先等
-- obj.del=fun
-- --调用
-- obj.del()

-- print("********************Lua调用C# 事件相关********************")
-- local fun2=function()
-- 	print("事件加的函数")
-- end
-- print("*************事件加函数**************")
-- --事件加减函数 和委托非常不一样
-- --lua中使用C#事件 加函数
-- --有点类似使用成员方法 冒号事件名
-- obj:eventAction("+",fun2)
-- --最好最好不要匿名这样写,假如要去掉该函数时，就无法知道要去掉那个函数了
-- obj:eventAction("+",function()
-- 	print("事件加的匿名函数")
-- end)

-- obj:DoEvent()
-- print("*************事件减函数**************")
-- obj:eventAction("-",fun2)
-- obj:DoEvent()

-- print("*************事件清除**************")
-- --清事件，不能直接设空
-- --obj.eventAction=nil
-- --obj.DoEvent()
-- obj:ClearEvent()
-- obj:DoEvent()



-- local obj=CS.Array2DTraversalTestClass()

-- --获取长度
-- print("行"..obj.array:GetLength(0))
-- print("列"..obj.array:GetLength(1))

-- --获取元素
-- --不能通过[0,0]或者[0][0]访问元素，会报错
-- print(obj.array:GetValue(0,0))
-- print(obj.array:GetValue(1,0))

-- print("**************************************")
-- for i=0,obj.array:GetLength(0)-1 do
-- 	for j=0,obj.array:GetLength(1)-1 do
-- 		print(obj.array:GetValue(i,j))
-- 	end
-- end


-- local obj=GameObject("测试加脚本")
-- --得到身上的刚体组件 如果没有 就加 有就不管
-- Rigidbody=CS.UnityEngine.Rigidbody
-- local rig =obj:GetComponent(typeof(Rigidbody))
-- print(rig)
-- --判断空
-- --nil和null 没法进行==比较
-- --第一种方法
-- --但是若rig本来就是空的话，会报错
-- --if rig:Equals(nil) then
-- --第二种方法 写一个判空的全局方法
-- --判空全局函数

-- --if IsNull(rig) then
-- if IsNull(rig) then
-- 	print("123")
-- 	rig=obj:AddComponent(typeof(Rigidbody))
-- end
-- print(rig)

-- UI=CS.UnityEngine.UI
-- local slider=GameObject.Find("Slider")
-- print(slider)
-- local sliderScript = slider:GetComponent(typeof(UI.Slider))
-- print(sliderScript)
-- sliderScript.onValueChanged:AddListener(function (f)
-- 	print(f)
-- end)


-- --xlua提供的一个工具表
-- --一定是要通过require调用之后才能用
-- print("*****************Lua调用C# 协程相关知识点*********************")
-- --xlua提供的一个工具表
-- --一定是要通过require调用之后才能用
-- local u=require("xlua.util")
-- --C#中协程启动都是通过继承了Mono的l类 通过里面的启动函数StartCoroutine

-- GameObject=CS.UnityEngine.GameObject
-- WaitForSeconds=CS.UnityEngine.WaitForSeconds
-- --在场景中新建一个空物体 然后挂一个脚本上去 脚本继承Mono使用它来开启协程
-- local obj=GameObject("Coroutine")
-- local mono=obj:AddComponent(typeof(CS.LuaCallCSharp))
-- fun=function()
-- 	local a=1
-- 	while true do
-- 		--lua中 不能直接使用 C#中的 yield return
-- 		--就使用lua中的协程返回
-- 		coroutine.yield(WaitForSeconds(1))
-- 		print(a)
-- 		a=a+1
-- 		if a>10 then
-- 			mono:StopCoroutine(b)
-- 		end
-- 	end
-- end
-- --我们不能直接将 lua函数传入到开启协程中！！！！
-- --如果要把lua函数当做协程函数传入
-- --必须 先调用 xlua.util中的cs_generator(lua函数)
-- b= mono:StartCoroutine(u.cs_generator(fun))


local obj=CS.T_TestClass()

local child=CS.T_TestClass.TestChild()
local father=CS.T_TestClass.TestFather()

--支持有约束有参数的泛型函数
obj:TestFun1(child,father)
obj:TestFun1(father,child)

--lua中不支持 没有约束的泛型函数
--obj:TestFun2(child)

--lua中不支持 有约束 但是没有参数的泛型函数
--obj:TestFun3()

--lua中不支持 非class的约束
--obj:TestFun4(child)


--PlayerSettings中设置打包方式
--Mono打包 这种方式支持使用
--IL2cpp打包 如果泛型参数是引用类型才可以使用
--IL2cpp打包 如果泛型参数是值类型，除非C#那边已经调用过了 同类型的泛型参数 lua中才能够被使用

--补充知识 让上面 不支持使用的泛型函数 变得能用
--有一定的使用限制，性能较低，不推荐使用
--得到通用函数
--设置泛型类型再使用
--xlua.get_generic_method(类,"函数名")
local testFun2= xlua.get_generic_method(CS.T_TestClass,"TestFun2")
local testFun2_R= testFun2(CS.System.Int32)
--调用
--成员方法 第一个参数 传调用函数的对象
--静态方法 不用传
testFun2_R(obj,1)
