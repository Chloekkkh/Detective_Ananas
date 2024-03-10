using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StartDialogue : MonoBehaviour
{
    public static StartDialogue instance;   
    public Sentence[] sentence;

    public GameObject skillCardPanel;
    public GameObject card;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        DialogueManager.instance.ShowDialogue(sentence);
    }

    void Update()
    {
    }

    public IEnumerator ViewSkillCardAni()
	{
        //展示skillcard两秒
        skillCardPanel.SetActive(true);
        card.transform.DOScale(1, 0.5f).From(0);
        AudioManager.Play("Success");
		yield return new WaitForSeconds(2f);

        skillCardPanel.SetActive(false);
        DialogueManager.instance.newCardAnimationFinished = true;
	}

}
