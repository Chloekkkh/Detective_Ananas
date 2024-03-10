using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public static EndGame instance;

    public Sentence[] sentence;

    public void Awake()
    {
        instance = this;
    }

    public void EndGameDialogue()
    {
        DialogueManager.instance.ShowDialogue(sentence);
    }
    
}
