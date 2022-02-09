require("Object")
print("InitClass")
function IsNull( obj )
	-- body
	if obj==nil or obj:Equals(nil) then
		return true;
	end
	return false;
end
Json= require("dkjson")

GameObject=CS.UnityEngine.GameObject
Resources=CS.UnityEngine.Resources
Transform=CS.UnityEngine.Transform
RectTransform=CS.UnityEngine.RectTransform
TextAsset=CS.UnityEngine.TextAsset

SpriteAtlas=CS.UnityEngine.U2D.SpriteAtlas

Mathf=CS.UnityEngine.Mathf
Time=CS.UnityEngine.Time

Vector3=CS.UnityEngine.Vector3
Vector2=CS.UnityEngine.Vector2
Sprite=CS.UnityEngine.Sprite

UI=CS.UnityEngine.UI
Image=UI.Image
Text=UI.Text
Button=UI.Button
Toggle=UI.Toggle
ScrollRect=UI.ScrollRect
UIBehaviour=CS.UnityEngine.EventSystems.UIBehaviour
Canvas=GameObject.Find("Canvas").transform

EventTrigger=CS.UnityEngine.EventSystems.EventTrigger
EventTriggerTools=CS.EventTriggerTools


ABMgr=CS.ABMgr.GetInstance()
XLuaObjMgr=CS.XLuaObjMgr.GetInstance()
E_LifeFun_Type=CS.E_LifeFun_Type