Object:subClass("BasePanel")
BasePanel.panelName=nil

BasePanel.panelObj=nil

BasePanel.controls={}

BasePanel.isInitEvent=false

function BasePanel:subClass(name)
    self.base.subClass(self,name)
    self.panelName=name
end

function BasePanel:Init()
    if self.panelObj==nil then
        self.panelObj=ABMgr:LoadRes("ui",self.panelName,typeof(GameObject))
        self.panelObj.transform:SetParent(Canvas,false)

        local allControls=self.panelObj:GetComponentsInChildren(typeof(UIBehaviour))

        for i=0,allControls.Length-1 do
            local controlName=allControls[i].name
            if string.find(controlName,"btn")~=nil or
                string.find(controlName,"tog")~=nil or
                string.find(controlName,"img")~=nil or
                string.find(controlName,"sv")~=nil then
                    
                local typeName=allControls[i]:GetType().Name

                if self.controls[controlName]~=nil then
                    self.controls[controlName][typeName]=allControls[i]
                else
                    self.controls[controlName]={[typeName]=allControls[i]}
                end
            end
        end
    end
end

function BasePanel:GetControl(name,typeName)
    if self.controls[name]~=nil then
        local sameNameControls=self.controls[name]
        if sameNameControls[typeName]~=nil then
            return sameNameControls[typeName]
        end
    end
    return nil
end

function BasePanel:ShowMe()
    self:Init(self)
    self.panelObj:SetActive(true)
end

function BasePanel:HideMe()
    self.panelObj:SetActive(false)
end