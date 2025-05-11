using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//在畫面顯示剩餘地雷數

public class TextCtrl : MonoBehaviour
{
    Text tex ;
    int textt=0;
    // Start is called before the first frame update
    void Start()
    {
        tex = GetComponent<Text>();
        tex.text =textt.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttomCtrl.isStart)
        {
            tex.text = buttomCtrl.MineText.ToString();
        }
    }
}
