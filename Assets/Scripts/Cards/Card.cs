using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public enum CardType
{
    Human,
    Prop,
    Assist
}
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;

    public string showName;
    public CardType cardType;
    
    [TextArea(3, 10)]
    public string cardDescription;

    public Sprite cardSprite;
    public Sprite cardBG;

    public bool isCreated = false;
    public void Awake()
    {
        isCreated = false;
    }
}
    
