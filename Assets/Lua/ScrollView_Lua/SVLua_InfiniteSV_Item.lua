
Object:subClass("SVLua_InfiniteSV_Item")
SVLua_InfiniteSV_Item.obj=nil
SVLua_InfiniteSV_Item.Text=nil
SVLua_InfiniteSV_Item.Img=nil
SVLua_InfiniteSV_Item.InfiniteSV=nil

function SVLua_InfiniteSV_Item:Init(father,_InfiniteSV)
    self.obj=ABMgr:LoadRes("ui","SVLua_InfiniteSV_Item",typeof(GameObject))
    self.InfiniteSV=_InfiniteSV
    self.Text=self.obj.transform:Find("Text"):GetComponent(typeof(Text))
    self.Img=self.obj.transform:GetComponent(typeof(Image))
    self.obj.transform:SetParent(father)
    self.obj.transform:GetComponent(typeof(RectTransform))
end

function SVLua_InfiniteSV_Item:UpdateData(data)
    self.Img.sprite=Resources.Load("UI/"..data.ImgName,typeof(Sprite))
    self.Text.text=data.ImgInfo
end




