using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Analytics;
using Unity.VisualScripting;
public class CardInteract : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    // public Canvas canvas;
    [Header("card drag")]
    private new RectTransform transform;

    [Header("card preview")]
    private int cardIndex;
    private Vector3 startPositon;
    public GameObject cardPreview;

    [Header("card fusion")]

    GameObject cardSlotA;
    GameObject cardSlotB;
    // static bool canDropA = true;
    // static bool canDropB = true;
    public bool isDrop = false;
    private CanvasGroup canvasGroup;


    //inite parent
    private Transform parentToReturn = null;

    //reset fusion panel
    //closs fusion panel
    
    private void Awake()
    {
        transform = GetComponent<RectTransform>();
    }
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        cardSlotA = CardFusionManager.instance.cardSlotA;
        cardSlotB = CardFusionManager.instance.cardSlotB;

        parentToReturn = transform.parent;

    }
    void Update()
    {
    }
     //拖拽中
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        transform.Find("Discription").gameObject.SetActive(false);
    }
    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPositon = transform.anchoredPosition;
        transform.Find("Discription").gameObject.SetActive(false);

        //将拖拽物体的blockRaycast设为false，防止拖拽时卡牌被遮挡
        if(CardFusionManager.instance.canFusion)
        {
            canvasGroup.blocksRaycasts = false;
        }

    }
    //结束拖拽
    public void OnEndDrag(PointerEventData eventData)
    {
        //move to slot
        if(CardFusionManager.instance.canFusion)//合成
        {
            if(Vector3.Distance(transform.position, cardSlotA.transform.position) < 100f && CardFusionManager.instance.hasDropA)
            {
                transform.position = cardSlotA.transform.position;
                //transform.SetParent(cardSlotA.transform);
                transform.DOScale(1.4f, 0.5f);

                CardFusionManager.instance.cardObjectA = gameObject;
                CardFusionManager.instance.hasDropA = false;
            }
            else if(Vector3.Distance(transform.position, cardSlotB.transform.position) < 100f && CardFusionManager.instance.hasDropB)
            {
                transform.position = cardSlotB.transform.position;
                //transform.SetParent(cardSlotB.transform);
                transform.DOScale(1.4f, 0.5f);

                CardFusionManager.instance.cardObjectB = gameObject;
                CardFusionManager.instance.hasDropB = false;
            }
            else
            {
                // transform.SetParent(parentToReturn);
                // transform.SetSiblingIndex(cardIndex);
                transform.DOAnchorPos(startPositon, 0.5f);
                canvasGroup.blocksRaycasts = true;
            }
        }
        else
        {
            //transform.SetParent(parentToReturn);
            transform.DOAnchorPos(startPositon, 0.5f);
            canvasGroup.blocksRaycasts = true;
        }
        transform.Find("Discription").gameObject.SetActive(false);
        //CardFusionManager.instance.Fusion();
    }

    //合成卡牌不匹配 卡牌回到原位


    //鼠标悬停预览
    //出现详细描述
    public void OnPointerEnter(PointerEventData eventData)
    {
        cardIndex = transform.GetSiblingIndex();
        transform.DOScale(1.25f, 0.2f);
        transform.SetAsLastSibling();

        //显示卡牌详细信息
        transform.Find("Discription").gameObject.SetActive(true);

        AudioManager.Play("Card1");


    }
    //鼠标离开
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.2f);
        transform.SetSiblingIndex(cardIndex);

        //隐藏卡牌详细信息
        transform.Find("Discription").gameObject.SetActive(false);
    }


    //reset fusion panel
    public void ResetFusion()
    {
        Debug.Log("reset");
        transform.DOScale(1f, 0.5f);
        transform.DOAnchorPos(startPositon, 0.5f);

        // transform.SetParent(parentToReturn);
        // transform.SetSiblingIndex(cardIndex);

        canvasGroup.blocksRaycasts = true;
        CardFusionManager.instance.hasDropA = true;
        CardFusionManager.instance.hasDropB = true;
    }
}
