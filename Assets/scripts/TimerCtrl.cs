using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//計時器顯示

public class TimerCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        timertext=GetComponent<Text>();
    }
    Text timertext;
    float time,timeTen ,tenT,tenTten;    
    // Update is called once per frame
    void Update()
    {
        if (buttomCtrl.isStart&& !GameOverCtrl.GameClear|| DetectCtrl.isGameOver)
        {
            time += Time.deltaTime;
            if (time >= 10)
            {
                timeTen++;
                time = 0;
            }
            if (timeTen >= 6)
            {
                timeTen = 0;
                tenT++;
                if (tenT >= 10)
                {
                    tenT = 0;
                    tenTten++;
                }
            }
            
            timertext.text =Mathf.Floor(tenTten).ToString()+ Mathf.Floor(tenT).ToString()+ "："+ Mathf.Floor(timeTen).ToString()+ Mathf.Floor(time).ToString();            
        }
        
    }
    public void TimerReset()
    {
        time = 0;
        timertext.text = Mathf.Floor(tenTten).ToString() + Mathf.Floor(tenT).ToString() + "：" + Mathf.Floor(timeTen).ToString() + Mathf.Floor(time).ToString();
    }
}
