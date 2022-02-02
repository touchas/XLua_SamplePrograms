using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ScrollView_CSharp
{
    public class InfiniteSV : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        #region 数据相关
        /// <summary>
        /// 用于存储元素的数据
        /// </summary>
        public List<InfiniteSV_ItemData> _ItemDatas = new List<InfiniteSV_ItemData>();
        /// <summary>
        /// 当前显示的左方数据索引
        /// </summary>
        public int _curDataLeftIndex = 0;
        /// <summary>
        /// 当前显示的右方数据索引
        /// </summary>
        public int _curDataRightIndex = 0;
        #endregion

        #region 组件相关

        #endregion
        ScrollRect _scrollRect;

        RectTransform _scrollRect_Trans;
        /// <summary>
        /// 列表元素
        /// </summary>
        List<GameObject> itemList = new List<GameObject>();

        List<InfiniteSV_Item> InfiniteSV_Items = new List<InfiniteSV_Item>();

        [Header("是否在居中")]
        [SerializeField]
        /// <summary>
        /// 是否正在居中
        /// </summary>
        bool isCentering = false;

        /// <summary>
        /// 元素预制体
        /// </summary>
        public GameObject ItemPrefab;
        [Header("元素展示数量")]
        /// <summary>
        /// 元素展示数量
        /// </summary>
        public int ItemShowNum = 3;

        [Header("元素宽度")]
        /// <summary>
        /// 元素宽度
        /// </summary>
        public float itemWidth = 100;
        [Header("元素间距")]
        /// <summary>
        /// 间距
        /// </summary>
        public float spacing = 50;
        [Header("元素居中缩放")]
        public Vector3 _itemScale = new Vector3(1.2f, 1.2f, 1.2f);
        [Header("元素缩放衰减")]
        /// <summary>
        /// 元素缩放衰减
        /// </summary>
        /// <returns></returns>
        public Vector3 _itemScaleReduction = new Vector3(0.2f, 0.2f, 0.2f);
        /// <summary>
        /// 中心索引
        /// </summary>
        public int _curMidIndex;
        /// <summary>
        /// 中间位置
        /// </summary>
        Vector3 midPos = Vector3.zero;
        /// <summary>
        /// 容器居中位置
        /// </summary>
        Vector3 _contentMidPos = Vector3.zero;
        [Header("居中速度")]
        /// <summary>
        /// 居中速度
        /// </summary>
        public float centerSpeed = 1;

        void Start()
        {
            _ItemDatas.Add(new InfiniteSV_ItemData("0", "0"));
            // _ItemDatas.Add(new InfiniteSV_ItemData("1","1"));
            // _ItemDatas.Add(new InfiniteSV_ItemData("2","2"));
            // _ItemDatas.Add(new InfiniteSV_ItemData("3","3"));
            // _ItemDatas.Add(new InfiniteSV_ItemData("4","4"));
            // _ItemDatas.Add(new InfiniteSV_ItemData("5","5"));
            // _ItemDatas.Add(new InfiniteSV_ItemData("6","6"));
            // _ItemDatas.Add(new InfiniteSV_ItemData("7","7"));
            // _ItemDatas.Add(new InfiniteSV_ItemData("8","8"));
            // _ItemDatas.Add(new InfiniteSV_ItemData("9","9"));
            // _ItemDatas.Add(new InfiniteSV_ItemData("10","10"));
            // _ItemDatas.Add(new InfiniteSV_ItemData("11","11"));

            // _ItemDatas.Add(new InfiniteSV_ItemData("0",""));
            // _ItemDatas.Add(new InfiniteSV_ItemData("1",""));
            // _ItemDatas.Add(new InfiniteSV_ItemData("2",""));
            // _ItemDatas.Add(new InfiniteSV_ItemData("3",""));
            // _ItemDatas.Add(new InfiniteSV_ItemData("4",""));
            // _ItemDatas.Add(new InfiniteSV_ItemData("5",""));
            // _ItemDatas.Add(new InfiniteSV_ItemData("6",""));
            // _ItemDatas.Add(new InfiniteSV_ItemData("7",""));
            // _ItemDatas.Add(new InfiniteSV_ItemData("8",""));
            // _ItemDatas.Add(new InfiniteSV_ItemData("9",""));
            // _ItemDatas.Add(new InfiniteSV_ItemData("10",""));
            // _ItemDatas.Add(new InfiniteSV_ItemData("11",""));
            Init();
        }
        void Update()
        {
            if (isCentering)
            {
                Vector3 v = _scrollRect.content.localPosition;
                v.x = Mathf.Lerp(_scrollRect.content.localPosition.x, _contentMidPos.x, centerSpeed * Time.deltaTime);
                _scrollRect.content.localPosition = v;
                SetItemScale();
                if (Mathf.Abs(itemList[_curMidIndex].transform.localScale.x - _itemScale.x) < 0.001)
                {
                    isCentering = false;
                }
            }
        }
        public void Init()
        {
            //获取组件
            _scrollRect = GetComponent<ScrollRect>();
            _scrollRect_Trans = _scrollRect.GetComponent<RectTransform>();
            //设置展示元素个数，强制设置为奇数 展示数量最少为3
            if (ItemShowNum < 3)
            {
                ItemShowNum = 3;
            }
            if (ItemShowNum % 2 == 0)
            {
                ItemShowNum += 1;
            }

            //设置一般元素的个数 
            int itemNum = ItemShowNum + 2;
            //一半的元素个数
            int helfNum = itemNum / 2;
            _curMidIndex = helfNum;
            //设置初始化的中心位置
            midPos = new Vector3
            (_scrollRect_Trans.sizeDelta.x / 2,
            -_scrollRect.content.sizeDelta.y / 2,
            0);
            //设置最左边的位置
            Vector3 leftPos = new Vector3
            (midPos.x - spacing * helfNum - itemWidth * helfNum,
            -_scrollRect.content.sizeDelta.y / 2,
            0);
            //创建展示元素
            for (int i = 0; i < itemNum; i++)
            {
                GameObject itemObj = Instantiate(ItemPrefab);
                itemObj.transform.SetParent(_scrollRect.content);
                itemObj.transform.localScale = Vector3.one;
                itemObj.transform.localPosition = leftPos + new Vector3(itemWidth * i + spacing * i, 0, 0);
                InfiniteSV_Items.Add(itemObj.GetComponent<InfiniteSV_Item>());
                itemList.Add(itemObj);
            }
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == _curMidIndex)
                {
                    itemList[i].transform.localScale = _itemScale;
                }
                else if (i < _curMidIndex)
                {
                    itemList[i].transform.localScale = new Vector3(
                    _itemScale.x - _itemScaleReduction.x * Mathf.Abs(_curMidIndex - i),
                    _itemScale.y - _itemScaleReduction.y * Mathf.Abs(_curMidIndex - i),
                   _itemScale.z - _itemScaleReduction.z * Mathf.Abs(_curMidIndex - i)
                    );
                }
                else if (i > _curMidIndex)
                {
                    int reduceNum = i - _curMidIndex;
                    itemList[i].transform.localScale = new Vector3(
                    _itemScale.x - _itemScaleReduction.x * Mathf.Abs(reduceNum),
                    _itemScale.y - _itemScaleReduction.y * Mathf.Abs(reduceNum),
                    _itemScale.z - _itemScaleReduction.z * Mathf.Abs(reduceNum)
                    );
                }
            }
            itemList[_curMidIndex].transform.localScale = _itemScale;

            InitData();
            SetItemLayer();

            SetCenterItemByNum(itemList[0].transform.localPosition);
        }
        public void InitData()
        {
            int itemDataIndex = 0;
            _curDataLeftIndex = 0;
            _curDataRightIndex = 0;
            for (int i = 0; i < itemList.Count; i++)
            {
                _curDataRightIndex = itemDataIndex;
                itemList[i].GetComponent<InfiniteSV_Item>().InitItem(this, _ItemDatas[itemDataIndex].ItemImageName, _ItemDatas[itemDataIndex].ItemInfo, itemDataIndex);
                itemDataIndex++;
                if (itemDataIndex >= _ItemDatas.Count)
                {
                    itemDataIndex = 0;
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            isCentering = false;
            //查找容器停止距离
            FindClosestPos();
            //设置元素缩放
            SetItemScale();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isCentering = true;
            //查找容器停止距离
            FindClosestPos();
        }
        /// <summary>
        /// 设置元素缩放
        /// </summary>
        void SetItemScale()
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == _curMidIndex)
                {
                    itemList[i].transform.localScale = new Vector3(
                    Mathf.Lerp(itemList[i].transform.localScale.x, _itemScale.x, centerSpeed * Time.deltaTime),
                    Mathf.Lerp(itemList[i].transform.localScale.y, _itemScale.y, centerSpeed * Time.deltaTime),
                    Mathf.Lerp(itemList[i].transform.localScale.z, _itemScale.z, centerSpeed * Time.deltaTime)
                    );
                }
                else if (i < _curMidIndex)
                {
                    itemList[i].transform.localScale = new Vector3(
                    Mathf.Lerp(itemList[i].transform.localScale.x, _itemScale.x - _itemScaleReduction.x * Mathf.Abs(_curMidIndex - i), centerSpeed * Time.deltaTime),
                    Mathf.Lerp(itemList[i].transform.localScale.y, _itemScale.y - _itemScaleReduction.y * Mathf.Abs(_curMidIndex - i), centerSpeed * Time.deltaTime),
                    Mathf.Lerp(itemList[i].transform.localScale.z, _itemScale.z - _itemScaleReduction.z * Mathf.Abs(_curMidIndex - i), centerSpeed * Time.deltaTime)
                    );
                }
                else if (i > _curMidIndex)
                {
                    int reduceNum = i - _curMidIndex;
                    itemList[i].transform.localScale = new Vector3(
                    Mathf.Lerp(itemList[i].transform.localScale.x, _itemScale.x - _itemScaleReduction.x * Mathf.Abs(reduceNum), centerSpeed * Time.deltaTime),
                    Mathf.Lerp(itemList[i].transform.localScale.y, _itemScale.y - _itemScaleReduction.y * Mathf.Abs(reduceNum), centerSpeed * Time.deltaTime),
                    Mathf.Lerp(itemList[i].transform.localScale.z, _itemScale.z - _itemScaleReduction.z * Mathf.Abs(reduceNum), centerSpeed * Time.deltaTime)
                    );
                }
            }
        }
        /// <summary>
        /// 设置循环元素位置.true为列表最后一位移至第一
        /// </summary>
        void SetLoopItemPos(bool isRight)
        {
            MoveDataIndex(isRight);
            //Debug.Log(_curDataRightIndex+" "+_curDataLeftIndex);
            //设置元素列表顺序
            if (isRight)
            {
                GameObject moveObj = itemList[itemList.Count - 1];
                moveObj.transform.localPosition = itemList[0].transform.localPosition - new Vector3(itemWidth + spacing, 0, 0);
                itemList.Remove(moveObj);
                itemList.Insert(0, moveObj);
                //把最右边的元素移动到最左边，将最左边的数据赋给移动过后的列表元素
                moveObj.GetComponent<InfiniteSV_Item>().UpadatData(_ItemDatas[_curDataLeftIndex], _curDataLeftIndex);
            }
            else
            {

                GameObject moveObj = itemList[0];
                moveObj.transform.localPosition = itemList[itemList.Count - 1].transform.localPosition + new Vector3(itemWidth + spacing, 0, 0);
                itemList.Remove(moveObj);
                itemList.Add(moveObj);
                //把最右边的元素移动到最左边，将最右边的数据赋给移动过后的列表元素
                moveObj.GetComponent<InfiniteSV_Item>().UpadatData(_ItemDatas[_curDataRightIndex], _curDataRightIndex);
            }
            SetItemLayer();

        }
        /// <summary>
        /// 设置元素层级
        /// </summary>
        void SetItemLayer()
        {
            int layerIndex = 1;
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i < _curMidIndex)
                {
                    itemList[i].transform.SetSiblingIndex(layerIndex);
                    layerIndex++;
                }
                else
                {
                    itemList[i].transform.SetSiblingIndex(layerIndex);
                    layerIndex--;
                }
            }
        }
        /// <summary>
        /// 查找容器停止位置
        /// </summary>
        void FindClosestPos()
        {

            float moveDis = (_scrollRect.content.localPosition - _contentMidPos).magnitude;
            //移动的数量
            int moveNum = Mathf.RoundToInt(moveDis / (itemWidth + spacing));
            //计算移动位置
            if ((_scrollRect.content.localPosition - _contentMidPos).x < 0)
            {
                //向左移动
                _contentMidPos = _contentMidPos - new Vector3(moveNum * (itemWidth + spacing), 0, 0);
                for (int i = 0; i < moveNum; i++)
                {
                    SetLoopItemPos(false);
                }
            }
            else if ((_scrollRect.content.localPosition - _contentMidPos).x == 0)
            {

            }
            else
            {
                //向右移动
                _contentMidPos = _contentMidPos + new Vector3(moveNum * (itemWidth + spacing), 0, 0);
                for (int i = 0; i < moveNum; i++)
                {
                    SetLoopItemPos(true);
                }
            }

        }
        public void SetCenterItemByNum(Vector3 itemPositeion)
        {
            isCentering = true;
            float moveDis = (itemPositeion - itemList[_curMidIndex].transform.localPosition).magnitude;

            //移动的数量
            int moveNum = Mathf.RoundToInt(moveDis / (itemWidth + spacing));
            //计算移动位置
            Debug.Log(moveDis + " " + moveNum);
            if ((itemPositeion - itemList[_curMidIndex].transform.localPosition).x > 0)
            {
                //向左移动
                _contentMidPos = _contentMidPos - new Vector3(moveNum * (itemWidth + spacing), 0, 0);
                for (int i = 0; i < moveNum; i++)
                {
                    SetLoopItemPos(false);
                }
            }
            else if ((itemPositeion - itemList[_curMidIndex].transform.localPosition).x == 0)
            {

            }
            else
            {
                //向右移动
                _contentMidPos = _contentMidPos + new Vector3(moveNum * (itemWidth + spacing), 0, 0);
                for (int i = 0; i < moveNum; i++)
                {
                    SetLoopItemPos(true);
                }
            }

        }
        void MoveDataIndex(bool isRight)
        {
            //Debug.Log(isRight);
            if (isRight)
            {
                _curDataLeftIndex--;
                _curDataRightIndex--;
                if (_curDataLeftIndex < 0)
                {
                    _curDataLeftIndex = _ItemDatas.Count - 1;
                }
                if (_curDataRightIndex < 0)
                {
                    _curDataRightIndex = _ItemDatas.Count - 1;
                }
            }
            else
            {
                _curDataLeftIndex++;
                _curDataRightIndex++;
                if (_curDataRightIndex >= _ItemDatas.Count)
                {
                    _curDataRightIndex = 0;
                }
                if (_curDataLeftIndex >= _ItemDatas.Count)
                {
                    _curDataLeftIndex = 0;
                }
            }
        }
    }
}