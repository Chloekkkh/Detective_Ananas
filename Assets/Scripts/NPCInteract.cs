using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{

    //new
    public Sentence[] sentence;
    public GameObject tips;




    // public string npcName;
    // [TextArea(5, 10)]
    // public string[] sentences;

    // public Sprite image;
    public GameObject icon;

    [SerializeField] private bool isEnter = false;

    [SerializeField] private bool hasTalked = false;
    
    void Start()
    {

    }
    void Update()
    {

        if (hasTalked)
        {
            icon.SetActive(false);
        }
        //当玩家停留在NPC旁边时，按E键显示对话框 禁止玩家移动
        if (isEnter && !DialogueManager.instance.dialoguePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {

                //DialogueManager.instance.ShowDialogue(npcName, sentences, image);
                DialogueManager.instance.ShowDialogue(sentence, hasTalked);
                hasTalked = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isEnter = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isEnter = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isEnter = true;
            //image = GetComponent<SpriteRenderer>().sprite;
            //tips.SetActive(true);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isEnter = false;
            //tips.SetActive(false);
        }
    }
}
