using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//難易度調整


public class DifCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        A = "Easy";
        B = "Normal";
        C = "Hard";
    }

    // Update is called once per frame
    void Update()
    {

    }
    Text text;
    string A, B, C;
    public void EasyDif()
    {
        buttomCtrl.MineDif = 3;
        buttomCtrl.MineX = 10;
        buttomCtrl.MineY = 15;
        Debug.Log("難度：簡單－－" + buttomCtrl.MineDif + "," + buttomCtrl.MineX + "," + buttomCtrl.MineY);
    }
    public void NormalDif()
    {
        buttomCtrl.MineDif = 8;
        buttomCtrl.MineX = 17;
        buttomCtrl.MineY = 23;
        Debug.Log("難度：普通－－" + buttomCtrl.MineDif + "," + buttomCtrl.MineX + "," + buttomCtrl.MineY);
    }
    public void HardDif()
    {
        buttomCtrl.MineDif = 15;
        buttomCtrl.MineX = 20;
        buttomCtrl.MineY = 30;
        Debug.Log("難度：困難－－" + buttomCtrl.MineDif + "," + buttomCtrl.MineX + "," + buttomCtrl.MineY);
    }
    public void Easy()
    {
        text.text = A;
    }
    public void Normal()
    {
        text.text = B;
    }
    public void Hard()
    {
        text.text = C;
    }
}
