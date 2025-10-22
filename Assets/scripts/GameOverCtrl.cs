using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//顯示勝負文字

public class GameOverCtrl : MonoBehaviour
{

    static public bool GameLose = false;
    static public bool GameClear = false;
    public GameObject textFall,textClear;
    // Start is called before the first frame update
    void Start()
    {
        //textFall = GetComponent<Text>();
        //textClear = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameLose) { textFall.SetActive(true); }
        if (!GameLose) { textFall.SetActive(false); }
        if (GameClear) { textClear.SetActive(true); }
        if (!GameClear) { textClear.SetActive(false); }
    }
}
