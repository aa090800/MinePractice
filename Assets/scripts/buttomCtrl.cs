using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//地雷生成數據 計算地雷生成邏輯

public class buttomCtrl : MonoBehaviour
{
    public GameObject Mine;
    
    Vector3 StartP = new Vector3(100, 900, 0);
    public Text DefXt, DefYt,InpXt,InpYt;
    int X, Y;
    public static int forAx,forAy;
    public int XLine=20, YLine=30;

    public static int MineX, MineY, MineDif;
    // Start is called before the first frame update
    void Start()
    {
        X = int.Parse(DefXt.text);//*輸入自訂格子 暫時不需要
        Y = int.Parse(DefYt.text);
        forAx = X;
        forAy = Y;

    }

    static public int dif = 3;//地雷%數
    float MineC;
    static public int MineText;
    static public int MineCo;
    private void MineCount()
    {
        MineC =Mathf.Floor(forAx * forAy * dif * 0.01f);
        MineText= (int)MineC;
        MineCo =(int)MineC;

    }

    static public bool isStart = false;

    static public int MinePosX,MinePosY;//按鈕編號

    public void Clear()//重製遊戲的數據
    {
        dif = MineDif;
        forAx = MineX;
        forAy = MineY;

        for (int i = 0; i < this.transform.childCount; i++)
        {            
            Destroy(this.transform.GetChild(i).gameObject);
        }
        isStart = false;
        list1 = new List<int>();
        DetectCtrl.detec = false;
        DetectCtrl.saveNum = new List<int>();
        DetectCtrl.isGameOver = false;
        DetectCtrl.Clear = 0;
        GameOverCtrl.GameClear = false;
        GameOverCtrl.GameLose = false;
        
    }


    public void ButtonSpawn()
    {
        MineCount();
        float SPx, SPy;
        SPx = StartP.x;
        SPy = StartP.y;
        for(int i = 1; i <= forAy; i++)///Y行
        {
            for(int j = 1; j <= forAx; j++)///X行
            {
                MinePosX = j;
                MinePosY = i;
                Instantiate(Mine, new Vector3(SPx,SPy,0), Quaternion.identity, this.transform);
                SPx += 27;
            }
            SPx = 100;
            SPy -= 27;
        }
    }

    static public int[,] mine;
    static int NotMine;



    static public int Startpx, Startpy;
    static public void MineNum()///建立格子編號
    {
        int Num = 1;//地雷編號數
        mine = new int[forAx, forAy];//forax= 20 foray=30

        for (int i = 0; i < forAx; i++)//x行20
        {
            for (int j = 0; j < forAy; j++)//Y行30
            {
                
                mine[i, j] = Num;
                Num++;
            }
        }
        
        NotMine = (int)mine.GetValue(Startpx - 1, Startpy - 1);//起始第一格不是地雷
        MinePos();//分配地雷位置
    }

    
    static public List<int> list1 = new List<int>();//地雷編號
        
    static public void MinePos()//分配地雷位置
    {
        for(int i = 0; list1.Count < MineCo; i++)
        {
            int numb = Random.Range(1, forAx * forAy);
            if (!list1.Contains(numb)&&numb!=NotMine)
            {
                list1.Add(numb);
            }
            i++;
        }
    }
}

