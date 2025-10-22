using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//按下右鍵後標記格子為地雷

public class RightClick : MonoBehaviour, IPointerClickHandler
{
    private Button but;
    private Image img;
    private void Start()
    {
        but = GetComponent<Button>();
        img = GetComponent<Image>();
        onRight.AddListener(new UnityAction(ButtonRightClick));
    }
    public UnityEvent onRight;
    // Start is called before the first frame update
   
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
            onRight.Invoke();

    }
    float a=1;
    public Sprite butt;
    private void ButtonRightClick()
    {
        if(but.enabled&&img.sprite!= DetectCtrl.RightMine||!but.enabled&&img.sprite == DetectCtrl.RightMine)
        {
            a++;
            if (a % 2 == 0)//按下
            {
                img.fillCenter = false;
                but.enabled = false;
                img.sprite = DetectCtrl.RightMine;
                buttomCtrl.MineText--;
            }
            else//取消
            {
                img.fillCenter = true;
                but.enabled = true;
                img.sprite = butt;
                buttomCtrl.MineText++;
            }
        }

        
    }
    
}
