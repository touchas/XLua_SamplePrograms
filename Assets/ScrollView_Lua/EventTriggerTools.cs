using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using XLua;
[CSharpCallLua]
public class EventTriggerTools
{
    [CSharpCallLua]
    //声明回调的lua方法
    public delegate void LuaFuncWithPointerEventDataDelegate (BaseEventData eventData);
    
    //提供为gameobject对象添加拖动的回调
    public static void AddDragLiseter(GameObject gameObject
                        ,LuaFuncWithPointerEventDataDelegate beginDragCallback
                        ,LuaFuncWithPointerEventDataDelegate dragCallback
                        ,LuaFuncWithPointerEventDataDelegate endDragCallback)
            {
                EventTrigger eventTrigger =  gameObject.GetComponent<EventTrigger>();

                if (eventTrigger == null) {
                    eventTrigger = gameObject.AddComponent<EventTrigger> ();
                }
                //添加组件
                
    
                //创建队列
                eventTrigger.triggers = new List<EventTrigger.Entry>(); 
                //初始化队列
                EventTrigger.Entry entry = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.BeginDrag,//事件类型
                    callback = new EventTrigger.TriggerEvent() //创建回调函数
                };
            
                //添加回调函数    
                entry.callback.AddListener((data)=> {
                    beginDragCallback(data);
                });
                EventTrigger.Entry entry2 = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.Drag,//事件类型
                    callback = new EventTrigger.TriggerEvent() //创建回调函数
                };
            
                //添加回调函数    
                entry2.callback.AddListener((data)=> {
                    dragCallback(data);
                });
                EventTrigger.Entry entry3 = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.EndDrag,//事件类型
                    callback = new EventTrigger.TriggerEvent() //创建回调函数
                };
            
                //添加回调函数    
                entry3.callback.AddListener((data)=> {
                    endDragCallback(data);
                });
        
                //对事件系统的队列添加事件队列
                eventTrigger.triggers.Add(entry);
                eventTrigger.triggers.Add(entry2);
                eventTrigger.triggers.Add(entry3);
    
            }

}
