using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;
using System.Collections;
public class OneCardManager : MonoBehaviour
{

    [Header("card initializations")]
    public TMP_Text cardName;
    public TMP_Text cardDescription;
    public Image cardSprite;
    public Image cardBG;
    public Card thisCard;
    private void Start()
    {

    }
    //设置卡牌
    public void CardSetup(Card card)
    {
        thisCard = card;
        cardName.text = card.showName;
        cardDescription.text = card.cardDescription;
        cardBG.sprite = card.cardBG;
        Debug.Log(thisCard.cardName);
    }
}
