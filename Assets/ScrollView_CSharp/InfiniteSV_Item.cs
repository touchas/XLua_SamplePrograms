using System.Security.Cryptography.X509Certificates;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ScrollView_CSharp
{
    public class InfiniteSV_ItemData
    {
        public string ItemImageName = "";
        public string ItemInfo = "";

        public InfiniteSV_ItemData(string itemImageName, string itemInfo)
        {
            ItemImageName = itemImageName;
            ItemInfo = itemInfo;
        }
    }
    public class InfiniteSV_Item : MonoBehaviour, IPointerDownHandler
    {
        public int _itemIndex = 0;
        public RectTransform _itemRect;
        public Image _itemImage;
        public Text _itemText;
        public InfiniteSV_ItemData _itemData;
        public InfiniteSV _infiniteSV;
        public void InitItem(InfiniteSV _targetInfiniteSV, string itemImageName, string itmeInfo = "", int itemIndex = 0)
        {
            _infiniteSV = _targetInfiniteSV;
            _itemRect = GetComponent<RectTransform>();
            _itemImage = GetComponent<Image>();
            _itemText = transform.Find("Text").GetComponent<Text>();
            _itemData = new InfiniteSV_ItemData(itemImageName, itmeInfo);
            _itemIndex = itemIndex;
            InitData();
        }
        public void InitData()
        {
            _itemImage.sprite = Resources.Load<Sprite>("UI/" + _itemData.ItemImageName);
            _itemText.text = _itemData.ItemInfo;
        }
        public void UpadatData(InfiniteSV_ItemData _infiniteSV_ItemData, int itemIndex = 0)
        {
            _itemData = _infiniteSV_ItemData;
            _itemIndex = itemIndex;
            InitData();

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _infiniteSV.SetCenterItemByNum(transform.localPosition);
        }
    }
}