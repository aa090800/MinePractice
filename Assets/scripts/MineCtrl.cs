using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//格子觸發邏輯

public class MineCtrl : MonoBehaviour
{
    int posx, posy;
    private void Awake()//為每個按鈕上編號
    {
        posx = buttomCtrl.MinePosX;
        posy = buttomCtrl.MinePosY;
    }

    static public int Blockx, Blocky;
    void Start()
    {
        Blockx = posx;
        Blocky = posy;
        testx = Blockx;
        testy = Blocky;
        but = GetComponent<Button>();
        img = GetComponent<Image>();
        if (!GameOverCtrl.GameClear) { but.enabled = true; }
    }
    
    public int testx, testy,MineNum;
    //static public bool disabl = false;//偵測是否被按
    static public bool isRightClick = false;//偵測是否按右鍵
    bool StartBool = false;
    bool ClearCheck=false;
    
    // Update is called once per frame
    void Update()
    {
        ///debug用 顯示所有地雷!!!
        if (DebugCtrl.ShowAll&&isMine) { img.color = Color.yellow; }
        if (!DebugCtrl.ShowAll) { img.color = Color.white; }
        /// 
        
        ///格子判斷自己是不是在偵測格的位置 
        ///偵測啟動且沒被按下→judge判斷→是否在上一個被按的格子周遭→是不是地雷(是地雷沒事)/不是地雷→按下格的四周8格被判定
        ///
        if (DetectCtrl.isGameOver)//遊戲結束時取修所有格子功能
        {            
            GameOver();
            GameOverCtrl.GameLose = true;
            but.enabled = false;
            if (img.sprite == DetectCtrl.RightMine&&!isMine) { img.color = Color.red; }//遊戲結束右鍵標籤填錯
        }
        if (GameOverCtrl.GameClear)//遊戲勝利時沒填的地雷顯示
        {
            but.enabled = false;
            if (isMine) { img.sprite = DetectCtrl.RightMine; }
        }


        if (!but.enabled&&!ClearCheck&&img.sprite!= DetectCtrl.RightMine)//地雷計數以偵測遊戲是否結束
        {
            DetectCtrl.Clear++;
            ClearCheck = true;
        }


        if (buttomCtrl.isStart&&!StartBool)//開始時偵測 
        {
            if (!isMine)//檢查自己是不是地雷
            {
                if (buttomCtrl.list1.Contains(Va = (int)buttomCtrl.mine.GetValue(posx - 1, posy - 1))) { isMine = true; }                
            }
            if (!isMine)
            {
                if (StartCheckNum == 0) { StartCheck(); }//檢查自己是不是在地雷四周格子 是的話增加數字                
                MineNum = StartCheckNum;
            }
            StartBool = true;
        }

        if (DetectCtrl.detec)
        {
            if (DetectCtrl.saveNum.Contains((int)buttomCtrl.mine.GetValue(posx - 1, posy - 1)))
            {
                if (StartCheckNum == 0) { DetGrid(); }
                ShowImgNum();
                but.enabled = false;
            }
        }
    }

    void GameOver()
    {
        if (isMine && img.sprite != DetectCtrl.RightMine) { img.color = Color.red; }
        if (!isMine) { img.color = Color.gray; }
    }


    int Va,Xv;
    public bool isMine = false;
    private Button but;
    private Image img;
    public void Disdable()//按按鈕執行
    {
        ///遊戲開始 鎖定地雷位置        
        if (!buttomCtrl.isStart)//確認遊戲開始不可改變地雷位置
        {
            //起始格不是地雷
            buttomCtrl.Startpx = posx;
            buttomCtrl.Startpy = posy;

            buttomCtrl.MineNum();//建立格子編號+分配地雷位置

            //建立所有格子 周圍是地雷的計數

            StartCheck();//檢查自己是不是在地雷四周格子 是的話增加數字
            ShowImgNum();
            MineNum = StartCheckNum;

            buttomCtrl.isStart = true;//遊戲開始
        }

        if (isMine)//踩到地雷遊戲結束
        {
            img.sprite = DetectCtrl.Mine;
            DetectCtrl.isGameOver = true;
        }
        
        
        ///生成8個數字代表四周8格
        if (!isMine&&StartCheckNum==0)//不是地雷的話 
        {
            Run();
            img.sprite = DetectCtrl.Cancel;
            DetectCtrl.detec = true;
        }
        if(StartCheckNum != 0)
        {

            ShowImgNum();
            ///取消格子功能
            but.enabled = false;
        }

        ///取消格子功能
        but.enabled = false;
    }


    
    void RightClickIsMine()
    {
        //img.fillCenter = false;
        but.enabled = false;
        img.sprite = DetectCtrl.RightMine;
    }



    void ShowImgNum()
    {
        if (StartCheckNum == 1) { img.sprite = DetectCtrl.CallSpr1; }
        if (StartCheckNum == 2) { img.sprite = DetectCtrl.CallSpr2; }
        if (StartCheckNum == 3) { img.sprite = DetectCtrl.CallSpr3; }
        if (StartCheckNum == 4) { img.sprite = DetectCtrl.CallSpr4; }
        if (StartCheckNum == 5) { img.sprite = DetectCtrl.CallSpr5; }
        if (StartCheckNum == 6) { img.sprite = DetectCtrl.CallSpr6; }
        if (StartCheckNum == 7) { img.sprite = DetectCtrl.CallSpr7; }
        if (StartCheckNum == 8) { img.sprite = DetectCtrl.CallSpr8; }
    }


    int StartCheckNum = 0;
    void StartCheck()
    {
        if (posx == 1 && posy != 1 && posy != buttomCtrl.forAy)//左邊排X=1 上 右上 右 下 右下
        {
            Case = 2;
            EightSqu();//讀取四周格
            StChif();//四周格是地雷的話 地雷計數++
            Case = 3;
            EightSqu();
            StChif();
            Case = 5;
            EightSqu();
            StChif();
            Case = 7;
            EightSqu();
            StChif();
            Case = 8;
            EightSqu();
            StChif();
            Case = 0;
        }
        if (posx == buttomCtrl.forAx && posy != buttomCtrl.forAy && posy != 1) //右編排X=邊界 左上 上 左 左下 下
        {
            Case = 1;
            EightSqu();
            StChif();
            Case = 2;
            EightSqu();
            StChif();
            Case = 4;
            EightSqu();
            StChif();
            Case = 6;
            EightSqu();
            StChif();
            Case = 7;
            EightSqu();
            StChif();
            Case = 0;
        }
        if (posy == 1 && posx != 1 && posx != buttomCtrl.forAx)  //上面列 Y=1 左 右 左下 下 右下
        {
            Case = 4;
            EightSqu();
            StChif();
            Case = 5;
            EightSqu();
            StChif();
            Case = 6;
            EightSqu();
            StChif();
            Case = 7;
            EightSqu();
            StChif();
            Case = 8;
            EightSqu();
            StChif();
            Case = 0;
        }
        if (posy == buttomCtrl.forAy && posx != 1 && posx != buttomCtrl.forAx) //下面列 Y=邊界 左上 上 右上 左 右
        {
            Case = 1;
            EightSqu();
            StChif();
            Case = 2;
            EightSqu();
            StChif();
            Case = 3;
            EightSqu();
            StChif();
            Case = 4;
            EightSqu();
            StChif();
            Case = 5;
            EightSqu();
            StChif();
            Case = 0;
        }
        if (posx == 1 && posy == 1) //左上格的話 右 下 右下
        {
            Case = 5;
            EightSqu();
            StChif();
            Case = 7;
            EightSqu();
            StChif();
            Case = 8;
            EightSqu();
            StChif();
            Case = 0;
        }
        if (posx == buttomCtrl.forAx && posy == 1) //右上格的話 左 左下 下 
        {
            Case = 4;
            EightSqu();
            StChif();
            Case = 6;
            EightSqu();
            StChif();
            Case = 7;
            EightSqu();
            StChif();
            Case = 0;
        }
        if (posx == 1 && posy == buttomCtrl.forAy) //左下格的話 上 右上 右 
        {
            Case = 2;
            EightSqu();
            StChif();
            Case = 3;
            EightSqu();
            StChif();
            Case = 5;
            EightSqu();
            StChif();
            Case = 0;
        }
        if (posx == buttomCtrl.forAx && posy == buttomCtrl.forAy) //右下格的話 左上 上 左 
        {
            Case = 1;
            EightSqu();
            StChif();
            Case = 2;
            EightSqu();
            StChif();
            Case = 4;
            EightSqu();
            StChif();
            Case = 0;
        }
        if (posx != 1 && posy != 1 && posx != buttomCtrl.forAx && posy != buttomCtrl.forAy)//剩下的四周全跑一次
        {
            Case = 1;
            EightSqu();
            StChif();
            Case = 2;
            EightSqu();
            StChif();
            Case = 3;
            EightSqu();
            StChif();
            Case = 4;
            EightSqu();
            StChif();
            Case = 5;
            EightSqu();
            StChif();
            Case = 6;
            EightSqu();
            StChif();
            Case = 7;
            EightSqu();
            StChif();
            Case = 8;
            EightSqu();
            StChif();
            Case = 0;
        }
    }

    int Case;
    void EightSqu()
    {
        switch (Case)
        {
            case 1:
                Xv = (int)buttomCtrl.mine.GetValue(posx - 2, posy - 2);//左上
                break;
            case 2:
                Xv = (int)buttomCtrl.mine.GetValue(posx - 1, posy - 2);//上
                break;
            case 3:
                Xv = (int)buttomCtrl.mine.GetValue(posx, posy - 2);//右上
                break;
            case 4:
                Xv = (int)buttomCtrl.mine.GetValue(posx - 2, posy - 1);//左
                break;
            case 5:
                Xv = (int)buttomCtrl.mine.GetValue(posx, posy - 1);//右
                break;
            case 6:
                Xv = (int)buttomCtrl.mine.GetValue(posx - 2, posy);//左下
                break;
            case 7:
                Xv = (int)buttomCtrl.mine.GetValue(posx - 1, posy);//下
                break;
            case 8:
                Xv = (int)buttomCtrl.mine.GetValue(posx, posy);//右下
                break;
        }
    }
    void Xvif()
    {
        if (!DetectCtrl.saveNum.Contains(Xv)) { DetectCtrl.saveNum.Add(Xv); }
    }
    void StChif()//四周地雷計數++
    {
        if (buttomCtrl.list1.Contains(Xv)) { StartCheckNum++; }
    }


    void Run()//需寫限制格子外部判定
    {
        //自己是posx posy(1,1)的話 格子編號(posx-1 posy-1)0,0 是第1格
        //往右x增加 往下y增加
        
        if (posx == 1&&posy!=1&&posy!=buttomCtrl.forAy)//左邊排X=1 上 右上 右 下 右下
        {
            Case = 2;
            EightSqu();
            Xvif();
            Case = 3;
            EightSqu();
            Xvif();
            Case = 5;
            EightSqu();
            Xvif();
            Case = 7;
            EightSqu();
            Xvif();
            Case = 8;
            EightSqu();
            Xvif();
            Case = 0;
        }
        if (posx == buttomCtrl.forAx && posy != buttomCtrl.forAy && posy!=1) //右編排X=邊界 左上 上 左 左下 下
        {
            Case = 1;
            EightSqu();
            Xvif();
            Case = 2;
            EightSqu();
            Xvif();
            Case = 4;
            EightSqu();
            Xvif();
            Case = 6;
            EightSqu();
            Xvif();
            Case = 7;
            EightSqu();
            Xvif();
            Case = 0;
        }
        if (posy == 1 && posx !=1 && posx != buttomCtrl.forAx)  //上面列 Y=1 左 右 左下 下 右下
        {
            Case = 4;
            EightSqu();
            Xvif();
            Case = 5;
            EightSqu();
            Xvif();
            Case = 6;
            EightSqu();
            Xvif();
            Case = 7;
            EightSqu();
            Xvif();
            Case = 8;
            EightSqu();
            Xvif();
            Case = 0;
        }
        if (posy == buttomCtrl.forAy && posx != 1 && posx != buttomCtrl.forAx) //下面列 Y=邊界 左上 上 右上 左 右
        {
            Case = 1;
            EightSqu();
            Xvif();
            Case = 2;
            EightSqu();
            Xvif();
            Case = 3;
            EightSqu();
            Xvif();
            Case = 4;
            EightSqu();
            Xvif();
            Case = 5;
            EightSqu();
            Xvif();
            Case = 0;
        }
        if(posx ==1&& posy == 1) //左上格的話 右 下 右下
        {
            Case = 5;
            EightSqu();
            Xvif();
            Case = 7;
            EightSqu();
            Xvif();
            Case = 8;
            EightSqu();
            Xvif();
            Case = 0;
        }
        if (posx == buttomCtrl.forAx && posy == 1) //右上格的話 左 左下 下 
        {
            Case = 4;
            EightSqu();
            Xvif();
            Case = 6;
            EightSqu();
            Xvif();
            Case = 7;
            EightSqu();
            Xvif();
            Case = 0;
        }
        if (posx == 1 && posy == buttomCtrl.forAy) //左下格的話 上 右上 右 
        {
            Case = 2;
            EightSqu();
            Xvif();
            Case = 3;
            EightSqu();
            Xvif();
            Case = 5;
            EightSqu();
            Xvif();
            Case = 0;
        }
        if (posx == buttomCtrl.forAx && posy == buttomCtrl.forAy) //右下格的話 左上 上 左 
        {
            Case = 1;
            EightSqu();
            Xvif();
            Case = 2;
            EightSqu();
            Xvif();
            Case = 4;
            EightSqu();
            Xvif();
            Case = 0;
        }
        if (posx != 1 && posy != 1 && posx != buttomCtrl.forAx && posy != buttomCtrl.forAy)//剩下的四周全跑一次
        {
            Case = 1;
            EightSqu();
            Xvif();
            Case = 2;
            EightSqu();
            Xvif();
            Case = 3;
            EightSqu();
            Xvif();
            Case = 4;
            EightSqu();
            Xvif();
            Case = 5;
            EightSqu();
            Xvif();
            Case = 6;
            EightSqu();
            Xvif();
            Case = 7;
            EightSqu();
            Xvif();
            Case = 8;
            EightSqu();
            Xvif();
            Case = 0;
        }

    }
    
    public void DetGrid()
    {
        ///鑑測自己是不是地雷
        if (isMine)
        {
            DetectCtrl.saveNum.Remove(Va);
        }
        if (!isMine)//不是地雷的話 傳送位置值到GameManager偵測
        {
            ///取消格子功能
            but.enabled = false;
            img.sprite = DetectCtrl.Cancel;
            Run();
        }
    }
}
