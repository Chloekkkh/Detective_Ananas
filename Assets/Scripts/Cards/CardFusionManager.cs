using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardFusionManager : MonoBehaviour
{
    public static CardFusionManager instance;
    //两个卡牌合成的卡槽
    public GameObject cardSlotA;
    public GameObject cardSlotB;
    public GameObject cardObjectA;
    public GameObject cardObjectB;
    public bool hasDropA = true;
    public bool hasDropB = true;
    public bool once = true;
    public bool canFusion = false;

    //room
    //密室
    public GameObject secretRoom;
    public GameObject clock;

    //soul
    public GameObject soul;

    //treasure
    public GameObject secretDoor;
    void Start()
    {
        instance = this;
    }
    void Update()
    {
        if(UIManager.instance.thinkPanel.activeSelf == true)
        {
            canFusion = true;
            FindObjectOfType<SearchInRoom>().enabled = false;
        }
        else
        {
            canFusion = false;
            FindObjectOfType<SearchInRoom>().enabled = true;
        }

    }
    public void Fusion()
    {

            if(cardObjectA != null && cardObjectB != null)
            {
                // string A = cardA.cardName;
                // string B = cardB.cardName;
                string A = cardObjectA.GetComponent<OneCardManager>().thisCard.cardName;
                string B = cardObjectB.GetComponent<OneCardManager>().thisCard.cardName;
                
                //西洋棋+放大镜=西洋棋线索
                if (A == "Chess" && B == "Zoom" || A == "Zoom" && B == "Chess")
                {
                    //加载新卡牌
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Clue/Clue_ChessClue");
                    cardObjectA.GetComponent<CardInteract>().ResetFusion();
                    cardObjectB.GetComponent<CardInteract>().ResetFusion();
                    CardsManager.instance.DeleCard(cardObjectA);
                    CardsManager.instance.DeleCard(cardObjectB);

                    //AudioManager.Play("Success");
                }

                //老夫人+放大镜=整容
                else if(A == "Zoom" && B == "Old Lady" || A == "Old Lady" && B == "Zoom")
                {
                    //加载新卡牌
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Clue/Clue_MakeFace");
                    cardObjectA.GetComponent<CardInteract>().ResetFusion();
                    cardObjectB.GetComponent<CardInteract>().ResetFusion();
                    CardsManager.instance.DeleCard(cardObjectA);
                    CardsManager.instance.DeleCard(cardObjectB);

                    //AudioManager.Play("Success");
                }

                //时钟＋墙壁厚度=时钟背后存在秘密
                else if(A == "Clock" && B == "Wall" || A == "Wall" && B == "Clock")
                {
                    //加载新卡牌
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Inference/Inference_Clock_Secreats");
                    cardObjectA.GetComponent<CardInteract>().ResetFusion();
                    cardObjectB.GetComponent<CardInteract>().ResetFusion();
                    CardsManager.instance.DeleCard(cardObjectA);
                    CardsManager.instance.DeleCard(cardObjectB);

                    //AudioManager.Play("Success");

                    //secretRoom.SetActive(true);
                    //clock.SetActive(false);
                }

                //时钟背后存在秘密+时间回溯=时钟背后的秘密
                //出现密室
                else if(A == "Clock_Secreats" && B == "Time" || A == "Time" && B == "Clock_Secreats")
                {
                    //加载新卡牌
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Truth/Truth_Clock");
                    cardObjectA.GetComponent<CardInteract>().ResetFusion();
                    cardObjectB.GetComponent<CardInteract>().ResetFusion();
                    CardsManager.instance.DeleCard(cardObjectA);
                    CardsManager.instance.DeleCard(cardObjectB);

                    secretRoom.SetActive(true);
                    clock.SetActive(false);

                    //AudioManager.Play("Success");
                }

                //密室中
                //干尸+放大镜=干尸信息
                else if(A == "Mummy" && B == "Zoom" || A == "Zoom" && B == "Mummy")
                {
                    //加载新卡牌
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Clue/Clue_MummyClue");
                    cardObjectA.GetComponent<CardInteract>().ResetFusion();
                    cardObjectB.GetComponent<CardInteract>().ResetFusion();
                    CardsManager.instance.DeleCard(cardObjectA);
                    CardsManager.instance.DeleCard(cardObjectB);

                    //AudioManager.Play("Success");
                }

                //干尸信息+时间回溯=老夫人的灵魂
                else if(A == "MummyClue" && B == "Time" || A == "Time" && B == "MummyClue")
                {
                    //加载新卡牌
                    CardsManager.instance.GetNewCard("CardsDatabase/Role/Role_Old Lady Soul");
                    cardObjectA.GetComponent<CardInteract>().ResetFusion();
                    cardObjectB.GetComponent<CardInteract>().ResetFusion();
                    CardsManager.instance.DeleCard(cardObjectA);
                    CardsManager.instance.DeleCard(cardObjectB);

                    //老夫人灵魂出现
                    soul.SetActive(true);

                    //AudioManager.Play("Success");
                }

                //真正的老夫人已经死了+火灾事件=现在的老夫人是犯人
                else if(A == "Old Lady is Dead" && B == "FireEvent" || A == "FireEvent" && B == "Old Lady is Dead")
                {
                    //加载新卡牌
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Inference/Inference_Muder is Old Lady");
                    cardObjectA.GetComponent<CardInteract>().ResetFusion();
                    cardObjectB.GetComponent<CardInteract>().ResetFusion();
                    CardsManager.instance.DeleCard(cardObjectA);
                    CardsManager.instance.DeleCard(cardObjectB);

                    //AudioManager.Play("Success");
                }

                //整容+钞票线索=老夫人整容知道新潮知识
                else if(A == "MakeFace" && B == "CashClue" || A == "CashClue" && B == "MakeFace")
                {
                    //加载新卡牌
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Inference/Inference_Old Lady Mackface");
                    cardObjectA.GetComponent<CardInteract>().ResetFusion();
                    cardObjectB.GetComponent<CardInteract>().ResetFusion();
                    CardsManager.instance.DeleCard(cardObjectA);
                    CardsManager.instance.DeleCard(cardObjectB);

                    //AudioManager.Play("Success");
                }

                //现在的老夫人是犯人+老夫人整过容知道新潮知识=犯人是女管家
                else if(A == "Muder is Old Lady" && B == "Old Lady Makeface" || A == "Old Lady Makeface" && B == "Muder is Old Lady")
                {
                    //加载新卡牌
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Inference/Inference_Muder is HouseKeeper");
                    cardObjectA.GetComponent<CardInteract>().ResetFusion();
                    cardObjectB.GetComponent<CardInteract>().ResetFusion();
                    CardsManager.instance.DeleCard(cardObjectA);
                    CardsManager.instance.DeleCard(cardObjectB);

                    //质问老夫人
                    PlayerInteract.instance.canTalkToGrace = true;

                    //AudioManager.Play("Success");
                }

                //犯人是女管家+独生子=棋谱信息
                // else if(A == "Muder is HouseKeeper" && B == "Only Son" || A == "Only Son" && B == "Muder is HouseKeeper")
                // {
                //     //加载新卡牌
                //     CardsManager.instance.GetNewCard("CardsDatabase/Prop/Clue/Clue_ChessMapInfo");
                //     cardObjectA.GetComponent<CardInteract>().ResetFusion();
                //     cardObjectB.GetComponent<CardInteract>().ResetFusion();
                //     CardsManager.instance.DeleCard(cardObjectA);
                //     CardsManager.instance.DeleCard(cardObjectB);

                //     AudioManager.Play("Success");
                // }

                //棋谱信息+老爷=肖像背后的秘密
                else if(A == "ChessMapInfo" && B == "Laoye" || A == "Laoye" && B == "ChessMapInfo")
                {
                    //加载新卡牌
                    CardsManager.instance.GetNewCard("CardsDatabase/Prop/Truth/Truth_The secret behind the Painting");
                    cardObjectA.GetComponent<CardInteract>().ResetFusion();
                    cardObjectB.GetComponent<CardInteract>().ResetFusion();
                    CardsManager.instance.DeleCard(cardObjectA);
                    CardsManager.instance.DeleCard(cardObjectB);

                    secretDoor.SetActive(true);

                    //最后对话

                    //AudioManager.Play("Success");
                }
                else
                {
                    cardObjectA.GetComponent<CardInteract>().ResetFusion();
                    cardObjectB.GetComponent<CardInteract>().ResetFusion();

                    //AudioManager.Play("Fail");
                }
            }
    }
    public void CloseFusion()
    {
        if(cardObjectA != null)
        {
            cardObjectA.GetComponent<CardInteract>().ResetFusion();
        }
        if(cardObjectB != null)
        {
            cardObjectB.GetComponent<CardInteract>().ResetFusion();
        }

    }
}
