print("SVLua_InfiniteSV")
BasePanel:subClass("SVLua_InfiniteSV")
local u=require("xlua.util")

--数据相关
SVLua_InfiniteSV._itemDatas={}
SVLua_InfiniteSV._curDataLeftIndex=0
SVLua_InfiniteSV._curDataRightIndex=0
--组件相关
SVLua_InfiniteSV._scrollRect=nil
SVLua_InfiniteSV._scrollRect_Trans=nil
SVLua_InfiniteSV._itemList={}
SVLua_InfiniteSV._eventTrigger=nil
--样式相关
SVLua_InfiniteSV._isCentering=false
SVLua_InfiniteSV._itemProfab=nil
SVLua_InfiniteSV._itemShowNum=3
SVLua_InfiniteSV._itemWidth=100
SVLua_InfiniteSV._spacing=50
SVLua_InfiniteSV._itemScale=Vector3(1.5,1.5,1.5)
SVLua_InfiniteSV._itemScaleReduction=Vector3(0.2,0.2,0.2)
--运动相关
SVLua_InfiniteSV._curMidIndex=0
SVLua_InfiniteSV._midPos=Vector3(0,0,0)
SVLua_InfiniteSV._contentMidPos=Vector3(0,0,0)
SVLua_InfiniteSV._centerSpeed=10

function SVLua_InfiniteSV:SetItemScale()
    -- print(self._itemList[1].obj.transform.localScale.." ")
    print(self._itemList[1].obj.transform.localScale)
    for i=1,#self._itemList do
        if i==self._curMidIndex then
            self._itemList[i].obj.transform.localScale=Vector3(
                Mathf.Lerp(self._itemList[i].obj.transform.localScale.x,self._itemScale.x,self._centerSpeed*Time.deltaTime),
                Mathf.Lerp(self._itemList[i].obj.transform.localScale.y,self._itemScale.y,self._centerSpeed*Time.deltaTime),
                Mathf.Lerp(self._itemList[i].obj.transform.localScale.z,self._itemScale.z,self._centerSpeed*Time.deltaTime)
            )
        elseif i<self._curMidIndex then
            self._itemList[i].obj.transform.localScale=Vector3(
                Mathf.Lerp(self._itemList[i].obj.transform.localScale.x,self._itemScale.x-self._itemScaleReduction.x*math.abs(self._curMidIndex-i+1),self._centerSpeed*Time.deltaTime),
                Mathf.Lerp(self._itemList[i].obj.transform.localScale.y,self._itemScale.x-self._itemScaleReduction.x*math.abs(self._curMidIndex-i+1),self._centerSpeed*Time.deltaTime),
                Mathf.Lerp(self._itemList[i].obj.transform.localScale.z,self._itemScale.x-self._itemScaleReduction.x*math.abs(self._curMidIndex-i+1),self._centerSpeed*Time.deltaTime)
            )
        elseif i>self._curMidIndex then
            local reduceNum=i-self._curMidIndex
            self._itemList[i].obj.transform.localScale=Vector3(
                Mathf.Lerp(self._itemList[i].obj.transform.localScale.x,self._itemScale.x-self._itemScaleReduction.x*math.abs(reduceNum),self._centerSpeed*Time.deltaTime),
                Mathf.Lerp(self._itemList[i].obj.transform.localScale.y,self._itemScale.x-self._itemScaleReduction.x*math.abs(reduceNum),self._centerSpeed*Time.deltaTime),
                Mathf.Lerp(self._itemList[i].obj.transform.localScale.z,self._itemScale.x-self._itemScaleReduction.x*math.abs(reduceNum),self._centerSpeed*Time.deltaTime)
            )
        end
    end
end

function SVLua_InfiniteSV:OnUpdate()
    -- print(self._isCentering)
    -- if self._isCentering==true then
    --     local v=self._scrollRect.content.localPosition
    --     v.x=Mathf.Lerp(self._scrollRect.content.localPosition.x,self._contentMidPos.x,self._centerSpeed*Time.deltaTime)
    --     self._scrollRect.content.localPosition=v
    --     self.SetItemScale()
    --     if math.abs(self._itemList[self._curMidIndex].obj.transform.localScale.x-self._itemScale.x)<0.001 then
    --         self._isCentering=false
    --     end
    -- end
    print(self._curDataLeftIndex)
end





--初始化元素数据
function SVLua_InfiniteSV:InitData()
    local itemDataIndex=1
    self._curDataLeftIndex=1
    self._curDataRightIndex=1
    for i=1,#self._itemList do
        self._curDataRightIndex=itemDataIndex
        self._itemList[i]:UpdateData(ItemData[itemDataIndex])
        itemDataIndex=itemDataIndex+1
        if itemDataIndex>#self._itemList then
            itemDataIndex=1
        end

    end 
end

--设置元素层级
function SVLua_InfiniteSV:SetItemLayer()
    local layerIndex=1
    for i=1,#self._itemList do
        if i<self._curMidIndex then
            self._itemList[i].obj.transform:SetSiblingIndex(layerIndex);
            layerIndex=layerIndex+1
        else
            self._itemList[i].obj.transform:SetSiblingIndex(layerIndex);
            layerIndex=layerIndex-1
        end
    end
end

--初始化样式
function SVLua_InfiniteSV:Init()
    self.base.Init(self)
    print(self._isCentering)
    if self._isCentering==false then
        print(1)
    end
    --添加事件组件
    EventTriggerTools.AddDragLiseter(self.panelObj,function(event)

    end,
    function(event)
        self:OnDrag(event)
    end,
    function(event)
        self:OnEndDrag(event)
    end)
    

    self._scrollRect=self.panelObj:GetComponent(typeof(ScrollRect))
    self._scrollRect_Trans=self.panelObj:GetComponent(typeof(RectTransform))
    if self._itemShowNum<3 then
        self._itemShowNum=3
    end
    if self._itemShowNum%2==0 then
        self._itemShowNum=self._itemShowNum+1
    end
    local itemNum=self._itemShowNum+2
    local helfNum=self._itemShowNum/2
    self._curMidIndex=helfNum+1

    self._midPos=Vector3(
        self._scrollRect_Trans.sizeDelta.x/2,
        -self._scrollRect.content.sizeDelta.y/2,
        0
    )
    local leftPos=Vector3(
        self._midPos.x-self._spacing*helfNum-self._itemWidth*helfNum,
        -self._scrollRect.content.sizeDelta.y/2,
        0
    )
   
    for i=1,itemNum do
        local InfiniteSV_item=SVLua_InfiniteSV_Item:new()
        InfiniteSV_item:Init(self._scrollRect.content,self)
        --InfiniteSV_item:UpdateData(ItemData[i])
        InfiniteSV_item.obj.transform.localScale=Vector3(1,1,1)
        InfiniteSV_item.obj.transform.localPosition=leftPos+Vector3(
            self._itemWidth*(i-1)+self._spacing*(i-1),0,0
            )
        table.insert(self._itemList,InfiniteSV_item)
    end
    for i=1,#self._itemList do
        if i==self._curMidIndex then
            self._itemList[i].obj.transform.localScale=self._itemScale
        elseif i<self._curMidIndex then
            local reduceNum=self._curMidIndex-i+1
            self._itemList[i].obj.transform.localScale=Vector3(
                self._itemScale.x-self._itemScaleReduction.x*math.abs(reduceNum),
                self._itemScale.y-self._itemScaleReduction.y*math.abs(reduceNum),
                self._itemScale.z-self._itemScaleReduction.z*math.abs(reduceNum)
            )
        elseif i>self._curMidIndex then
            local reduceNum=i-self._curMidIndex
            self._itemList[i].obj.transform.localScale=Vector3(
                self._itemScale.x-self._itemScaleReduction.x*math.abs(reduceNum),
                self._itemScale.y-self._itemScaleReduction.y*math.abs(reduceNum),
                self._itemScale.z-self._itemScaleReduction.z*math.abs(reduceNum)
            )
        end

        self:InitData()
        self:SetItemLayer()
    end
    --self._itemList[self._curMidIndex].localScale=self._itemScale

end
function SVLua_InfiniteSV:MoveDataIndex(isRight)
    if isRight==true then
        self._curDataLeftIndex=self._curDataLeftIndex-1
        self._curDataRightIndex=self._curDataRightIndex-1
        if self._curDataLeftIndex<1 then
            self._curDataLeftIndex=#self._itemDatas
        end
        if self._curDataRightIndex<1 then
            self._curDataRightIndex=#self._itemDatas
        end
    else
        self._curDataLeftIndex=self._curDataLeftIndex+1
        self._curDataRightIndex=self._curDataRightIndex+1
        if self._curDataLeftIndex>#self._itemList then
            self._curDataLeftIndex=1
        end
        if self._curDataRightIndex>#self._itemList then
            self._curDataRightIndex=0
        end
    end
end

function SVLua_InfiniteSV:SetLoopItemPos(isRight)
    self:MoveDataIndex(isRight)
    if isRight==true then
        local moveObj=self._itemList[#self._itemList]
        moveObj.obj.transform.localPosition=self._itemList[1].obj.transform.localPosition-Vector3(self._itemWidth+self._spacing,0,0)
        table.remove(moveObj)
        table.insert(self._itemList,1,moveObj)
    else
        local moveObj=self._itemList[1]
        moveObj.obj.transform.localPosition=self._itemList[#self._itemList].obj.transform.localPosition+Vector3(self._itemWidth+self._spacing,0,0)
        table.remove(self._itemList,1)
        table.insert(self._itemList,1,moveObj)
    end
end

function SVLua_InfiniteSV:FindClosestPos()
    local moveDis=Vector3.Distance(self._scrollRect.content.localPosition,self._contentMidPos)
    local moveNum=math.floor(moveDis/(self._itemWidth+self._spacing)+0.5)
    if (self._scrollRect.content.localPosition.x-self._contentMidPos.x)<0 then
        self._contentMidPos=self._contentMidPos-Vector3(moveNum*(self._itemWidth+self._spacing),0,0)
        for i=1,moveNum do
            self:SetLoopItemPos(false)
        end
    elseif (self._scrollRect.content.localPosition.x-self._contentMidPos.x)==0 then
        
    else
        self._contentMidPos=self._contentMidPos+Vector3(moveNum*(self._itemWidth+self._spacing),0,0)
        for i=1,moveNum do
            self:SetLoopItemPos(true)
        end
    end
end




function SVLua_InfiniteSV:OnDrag(event)

    self._isCentering=false
    self:FindClosestPos()
    self:SetItemScale()
end

function SVLua_InfiniteSV:OnEndDrag(event)

    self._isCentering=true
    self:FindClosestPos()
end


-- XLuaObjMgr:AddOrRemoveAction(E_LifeFun_Type.UPDATE,SVLua_InfiniteSV.OnUpdate)










