using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//遊戲勝利判斷以及導入地雷圖片

public class DetectCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CallSpr1 = N1;
        CallSpr2 = N2;
        CallSpr3 = N3;
        CallSpr4 = N4;
        CallSpr5 = N5;
        CallSpr6 = N6;
        CallSpr7 = N7;
        CallSpr8 = N8;
        RightMine = RMine;
        Cancel = Canc;
        Mine = minee;
    }

    static public int MineCtrlPosX=-10, MineCtrlPosY=-10;
    static public bool detec= false;
    static public bool isGameOver = false;
    static public bool StartToMineNum = false;
    static public List<int> saveNum = new List<int>();
    public Sprite N1, N2, N3, N4, N5, N6, N7, N8, RMine,Canc,minee;
    static public Sprite CallSpr1, CallSpr2, CallSpr3, CallSpr4, CallSpr5, CallSpr6, CallSpr7, CallSpr8, RightMine,Cancel,Mine;
    static public int Clear = 0;

    // Update is called once per frame
    void Update()
    {
        if ((buttomCtrl.forAx* buttomCtrl.forAy)-buttomCtrl.MineCo ==Clear && buttomCtrl.isStart)
        {
            GameOverCtrl.GameClear = true;
            //Debug.Log("遊戲勝利");
        }
    }
    static public int CallNum = 0;
}
