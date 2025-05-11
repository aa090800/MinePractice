using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//標記所有地雷位置

public class DebugCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool ShowAll = false;
    float a=1;
    public void ShowAllMines()
    {
        a++;
        if (a % 2 == 0) { ShowAll = true; }
        else { ShowAll = false; }
    }
}
