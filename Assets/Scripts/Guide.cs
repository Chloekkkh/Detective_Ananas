using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Guide : MonoBehaviour
{
    public static Guide instance;
    public GameObject thinkPanel;
    public GameObject reasonPanel;


    //text
    public string tips;

    public Sentence[] sentence;

    public void Awake()
    {
        instance = this;
    }

    public void ChessGuide()
    {
        StartCoroutine(ShowTip());
    }

    IEnumerator ShowTip()
    {
        yield return new WaitForSeconds(2.5f);
        DialogueManager.instance.ShowDialogue(sentence);
        while (DialogueManager.instance.dialoguePanel.activeInHierarchy)
        {
            yield return null;
        }
        thinkPanel.SetActive(true);
        reasonPanel.transform.DOScale(1, 0.5f).From(0);
    }
    
}
