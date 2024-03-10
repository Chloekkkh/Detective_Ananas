using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using DG.Tweening;

public class EndText : MonoBehaviour
{
    public TMP_Text dialogueText;
    public TMP_Text end;
    public string sentences;
    public float typeTime = 0.1f;

    private bool isScrolling = false;

    void Start()
    {
        sentences = dialogueText.text;
        StartCoroutine(ScrollText(sentences));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (end.gameObject.activeInHierarchy && end.alpha >= 0.99)
            {
                Debug.Log("end");
                ScenceManager.instance.StartCoroutine(ScenceManager.instance.Fadeout());
            }
        }
    }
    private IEnumerator ScrollText(string sentences)
    {
        isScrolling = true;
        dialogueText.text = "";
        foreach (char letter in sentences.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeTime);
        }
        isScrolling = false;
        yield return new WaitForSeconds(0.5f);

        //end渐变浮现
        end.gameObject.SetActive(true);
        end.DOFade(1, 1f).From(0);
    }
}