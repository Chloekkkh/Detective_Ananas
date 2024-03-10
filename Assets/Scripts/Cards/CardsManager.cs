using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CardsManager : MonoBehaviour
{
	public static CardsManager instance;

	[Header("Card Panels")]
	public GameObject cardCanvas;
   	public GameObject assistCardPanel;
   	public GameObject propCardPanel;
   	public GameObject roleCardPanel;

	public GameObject parent;

	[Header("Card display")]
	public float assistCardPanelWidth = 100f;
	public float propCardPanelWidth = 200f;
	public float roleCardPanelWidth = 150f;

   	[Header("current cards")]
   	private List<GameObject> roleCards = new List<GameObject>();
  	private List<GameObject> propCards = new List<GameObject>();
   	private List<GameObject> assistCards = new List<GameObject>();

	[Header("new cards")]
	public GameObject newCardPanel;
	//public TMP_Text newCardName;
	public GameObject show;
	private bool showCardFinished = false;
	void Awake()
	{
		instance = this;
	}
   	void Start()
	{
		LoadCard("CardsDatabase/Assist/Assist_Zoom");
		LoadCard("CardsDatabase/Assist/Assist_Time");
		DisplayAssistCars();

	}

	void Update()
	{
	}


	public void LoadCard(string cardPath)
	{
		Card card = Resources.Load<Card>(cardPath);
		if (card == null)
			return;
		//如果没生成过
		if(!card.isCreated)
		{
			GameObject cardObject = Instantiate(Resources.Load<GameObject>("Prefabs/CardPrefab"));
			cardObject.GetComponent<OneCardManager>().CardSetup(card);

			Debug.Log(card.cardName);

			switch (card.cardType)
			{
				case CardType.Human:
					cardObject.transform.SetParent(roleCardPanel.transform);
					cardObject.transform.SetParent(parent.transform);
					cardObject.GetComponent<RectTransform>().localScale = roleCardPanel.GetComponent<RectTransform>().localScale;
					roleCards.Add(cardObject);
					DisplayRoleCards();
					break;
				case CardType.Prop:
					cardObject.transform.SetParent(propCardPanel.transform);
					cardObject.transform.SetParent(parent.transform);
					cardObject.GetComponent<RectTransform>().localScale = propCardPanel.GetComponent<RectTransform>().localScale;
					propCards.Add(cardObject);
					DisplayPropCards();
					break;
				case CardType.Assist:
					//cardObject.transform.SetParent(assistCardPanel.transform);
					cardObject.transform.SetParent(parent.transform);
					// cardObject.GetComponent<RectTransform>().anchoredPosition = assistCardPanel.GetComponent<RectTransform>().anchoredPosition;
					cardObject.GetComponent<RectTransform>().localScale = assistCardPanel.GetComponent<RectTransform>().localScale;
					assistCards.Add(cardObject);
					DisplayAssistCars();
					return;
			}
			card.isCreated = true;
			newCardPanel.SetActive(true);
			FindObjectOfType<SearchInRoom>().canSearch = false;
			showCardFinished = false;
			StartCoroutine(ViewNewCardAni(cardPath));

			AudioManager.Play("Success");

			if(!showCardFinished)
			{
				//禁止鼠标操作和键盘操作

			}
		}
		//如果生成过
		else
		{
			//newCardPanel.SetActive(true);
			//ViewNewCard(cardPath);
			//StartCoroutine(ViewNewCardAni(cardPath));
			DispalyCurrentCards();
		}
		
	}

	public void DispalyCurrentCards()
	{
		//DisplayAssistCars();
		DisplayPropCards();
		DisplayRoleCards();
	}

	public void DisplayAssistCars()
	{
		float offset = assistCardPanelWidth / assistCards.Count;

		Vector2 startPos = new Vector2(-assistCards.Count / 2.0f * offset + offset * 0.5f, 0) + assistCardPanel.GetComponent<RectTransform>().anchoredPosition;
		for (int i = 0; i < assistCards.Count; i++)
		{
			assistCards[i].GetComponent<RectTransform>().anchoredPosition = startPos + new Vector2(offset * i, 0);
			assistCards[i].GetComponent<RectTransform>().DOScale(1.0f, 0.5f);
		}
	}
	//可以写一个动画
	public void DisplayPropCards()
	{
		float offset = propCardPanelWidth / propCards.Count;

		Vector2 startPos = new Vector2(-propCards.Count / 2.0f * offset + offset * 0.5f, 0) + propCardPanel.GetComponent<RectTransform>().anchoredPosition;
		for (int i = 0; i < propCards.Count; i++)
		{
			propCards[i].GetComponent<RectTransform>().anchoredPosition = startPos + new Vector2(offset * i, 0);
			propCards[i].GetComponent<RectTransform>().DOScale(1.0f, 0.5f);
		}
	}
	public void DisplayRoleCards()
	{
		float offset = roleCardPanelWidth / roleCards.Count;

		Vector2 startPos = new Vector2(-roleCards.Count / 2.0f * offset + offset * 0.5f, 0) + roleCardPanel.GetComponent<RectTransform>().anchoredPosition;
		for (int i = 0; i < roleCards.Count; i++)
		{
			roleCards[i].GetComponent<RectTransform>().anchoredPosition = startPos + new Vector2(offset * i, 0);
			roleCards[i].GetComponent<RectTransform>().DOScale(1.0f, 0.5f);
		}
	}

	public void GetNewCard(string cardPath)
	{
		LoadCard(cardPath);
	}

	// public void ViewNewCard(string cardPath)
	// {
	// 	//show new card
	// 	Card card = Resources.Load<Card>(cardPath);
	// 	GameObject cardObject = Instantiate(Resources.Load<GameObject>("Prefabs/CardPrefab"));
	// 	cardObject.transform.SetParent(newCardPanel.transform);
	// 	cardObject.GetComponent<RectTransform>().anchoredPosition = newCardPanel.GetComponent<RectTransform>().anchoredPosition;
	// 	cardObject.GetComponent<RectTransform>().transform.DOScale(2f, 0.5f);
	// 	cardObject.GetComponent<OneCardManager>().CardSetup(card);
	// 	cardObject.GetComponent<CardInteract>().enabled = false;
	// 	newCardName.text = card.cardName;
	// 	//关闭是销毁
	// }

	IEnumerator ViewNewCardAni(string cardPath)
	{
		Card card = Resources.Load<Card>(cardPath);
		GameObject cardObject = Instantiate(Resources.Load<GameObject>("Prefabs/CardPrefab"));
		cardObject.transform.SetParent(newCardPanel.transform);
		cardObject.GetComponent<RectTransform>().anchoredPosition = newCardPanel.GetComponent<RectTransform>().anchoredPosition- new Vector2(0, 10);
		cardObject.GetComponent<RectTransform>().transform.DOScale(1.6f, 0.5f);
		//背后的光也有个动画
		show.GetComponent<RectTransform>().transform.DOScale(1f, 0.5f).From(0f);
		cardObject.GetComponent<OneCardManager>().CardSetup(card);
		cardObject.GetComponent<CardInteract>().enabled = false;
		showCardFinished = false;
		FindObjectOfType<SearchInRoom>().enabled = false;
		Debug.Log(FindObjectOfType<SearchInRoom>().enabled);
		//newCardName.text = card.cardName;
		yield return new WaitForSeconds(2f);

		//协程结束之后
		//关闭面板
		//恢复游戏
		//销毁预览卡牌
		
		showCardFinished = true;
		newCardPanel.SetActive(false);
		FindObjectOfType<SearchInRoom>().canSearch = true;
		DialogueManager.instance.newCardAnimationFinished = true;
		Destroy(cardObject);
	}

	public void DeleCard(GameObject cardObject)
	{
		if(cardObject.GetComponent<OneCardManager>().thisCard.cardType == CardType.Prop)
		{
			propCards.Remove(cardObject);
			Destroy(cardObject);
			DisplayPropCards();
		}
		else return;
	}

}
