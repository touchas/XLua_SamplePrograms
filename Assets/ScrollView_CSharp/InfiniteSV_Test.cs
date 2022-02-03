using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScrollView_CSharp
{
    public class InfiniteSV_Test : MonoBehaviour
    {
        public Text text;
        public InfiniteSV _infiniteSV;
        // Start is called before the first frame update
        void Start()
        {
            _infiniteSV._OnCenterChange+=_OnCenterChange_CustomCall;
        }

        void _OnCenterChange_CustomCall(InfiniteSV_ItemData _ItemData)
        {
            text.text=_ItemData.ItemInfo;
        }

    }
}