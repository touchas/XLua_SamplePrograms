print("LuaTest")

testInt=10
testStr="ABC"
testBool=true
testFloat=1.2
--无参数无返回值的函数
testFun1=function()
    print("无参无返回函数")
end

--有参数无返回值的函数
testFun2_1=function(a)
    print("有参无返回函数，参数："..a)
end

--有参数无返回值的函数
testFun2_2=function(a,b)
    print("有参无返回函数，参数1："..a.."参数2："..b)
end

testFun2_3=function(a)
    return a
end

--多返回值的函数
testFun3=function(a)
	return 100,200,false,"字符",a
end
--变长参数函数1
testfun4_1=function(...)
    arg={...}
    for k,v in pairs(arg) do
        print(k,v)
    end
end
--变长参数函数2
testfun4_2=function(a,...)
    print("变长参数的固定参数："..a)
    arg={...}
    for k,v in pairs(arg) do
        print(k,v)
    end
end

--C#无法直接获取Lua中的本地变量
local testLocal=20

--List
testList={11,22,33,44,55,66}
testList2={"ABCD",123,true,11,1.23}

testDic={
	["1"]=11,
	["2"]=22,
	["3"]=33,
	["4"]=44
}

testClass={
	testInt=2,
	testBool=true,
	testFloat=1.2,
	testString="123",
	testFun=function()
		print("lua中的类方法执行")
	end
}
