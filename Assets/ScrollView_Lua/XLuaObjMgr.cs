using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum E_LifeFun_Type
{
    START,
    UPDATE,
    LATEUPDATE,
    FIXEDUPDATE,
    ENABLE,
    DISABLE,
    DESTROY,
}
public delegate void LuaCallCSharpDel(GameObject obj);
public class XLuaObjMgr : SingletonAutoMono<XLuaObjMgr>
{
    public UnityAction start;
    public UnityAction update;
    public UnityAction fixedUpdate;
    public UnityAction lateUpdate;
    public UnityAction onEnable;
    public UnityAction onDisable;
    public UnityAction destory;


   
    private void Start()
    {
        start?.Invoke();
    }
    private void Update()
    {
        update?.Invoke();
    }

    private void FixedUpdate()
    {
        fixedUpdate?.Invoke();
    }

    private void LateUpdate()
    {
        lateUpdate?.Invoke();
        
    }

    private void OnEnable()
    {
        onEnable?.Invoke();
    }

    private void OnDisable()
    {
        onDisable?.Invoke();
    }

    private void OnDestroy()
    {
        destory?.Invoke();
        start = null;
        update = null;
        fixedUpdate = null;
        lateUpdate = null;
        onEnable = null;
        onDisable = null;
        destory = null;
    }

    public void AddOrRemoveAction(E_LifeFun_Type lifeType,UnityAction fun,bool isAdd=true)
    {
        switch (lifeType)
        {
            case E_LifeFun_Type.START:
                if (isAdd)
                    start += fun;
                else
                    start -= fun;
                break;
            case E_LifeFun_Type.UPDATE:
                if (isAdd)
                    update += fun;
                else
                    update -= fun;
                break;
            case E_LifeFun_Type.LATEUPDATE:
                if (isAdd)
                    lateUpdate += fun;
                else
                    lateUpdate -= fun;
                break;
            case E_LifeFun_Type.FIXEDUPDATE:
                if (isAdd)
                    fixedUpdate += fun;
                else
                    fixedUpdate -= fun;
                break;
            case E_LifeFun_Type.ENABLE:
                if (isAdd)
                    onEnable += fun;
                else
                    onEnable -= fun;
                break;
            case E_LifeFun_Type.DISABLE:
                if (isAdd)
                    onDisable += fun;
                else
                    onDisable -= fun;
                break;
            case E_LifeFun_Type.DESTROY:
                if (isAdd)
                    destory += fun;
                else
                    destory -= fun;
                break;
        }
    }

}
