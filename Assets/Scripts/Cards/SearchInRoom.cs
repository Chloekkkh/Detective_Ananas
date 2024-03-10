using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchInRoom : MonoBehaviour
{
    RaycastHit2D hit;
    public bool canSearch = true;
    void Update()
    {
        if(canSearch)
            Search();
    
    }
    public void Search()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 将鼠标点击的屏幕坐标转换为世界坐标
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 射线检测
            hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            //删掉collider
            // 如果点击到了物体
            if (hit.collider != null)
            {
                // 如果点击到了物体 collider取消 icon取消
                if (hit.collider.CompareTag("Clock"))//clock
                {
                   CardsManager.instance.GetNewCard("CardsDatabase/Prop/Prop/Prop_Clock");
                }
                if (hit.collider.CompareTag("Chess"))//chess
                {
                    hit.collider.enabled = false;
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Prop/Prop_Chess");
                    //guide
                    Guide.instance.ChessGuide();
                    hit.collider.enabled = false;
                }
                if (hit.collider.CompareTag("Painting1"))//laoye
                {
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Prop/Prop_Laoye");
                }
                if (hit.collider.CompareTag("Painting2"))//xiaojie
                {
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Prop/Prop_Xiaojie");
                }
                if (hit.collider.CompareTag("Painting3"))//xiaojie fist husband
                {
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Prop/Prop_XiaojieHusband");
                }
                if (hit.collider.CompareTag("Wall"))//xiaojie second husband
                {
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Clue/Clue_WallThickness");
                }
                if (hit.collider.CompareTag("Mummy"))//Mummy
                {
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Prop/Prop_Mummy");
                }
                if (hit.collider.CompareTag("Treasure"))//treausre
                {
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Truth/Truth_Treasure");
                    Debug.Log("Treasure");

                    //end game
                    EndGame.instance.EndGameDialogue();

                }
            }
        }
    }
}
