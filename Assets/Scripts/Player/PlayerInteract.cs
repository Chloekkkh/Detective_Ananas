using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract instance;
    //和npc的二次对话
    [SerializeField] private bool isEnter = false;
    [SerializeField] private Sentence[] _sentence;
    public Sentence[] _graceSentences;
    public Sentence[] _kenSentences;
    public bool canTalkToGrace = false;
    [SerializeField] private bool canTalkToKen = false;

    //密室collider
    //public Collider2D screatRoomCollider;
    public GameObject mask;
    
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (isEnter && !DialogueManager.instance.dialoguePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            if (DialogueManager.instance.newCardAnimationFinished)
            {
                DialogueManager.instance.ShowDialogue(_sentence);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //禁用npcInteract
        if (other.CompareTag("ScreatRoom"))
        {
            mask.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ScreatRoom"))
        {
            mask.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //禁用npcInteract
        if (other.collider.CompareTag("Grace") && canTalkToGrace)
        {
            isEnter = true;
            if(other.gameObject.GetComponent<NPCInteract>()!=null)
            {
                other.gameObject.GetComponent<NPCInteract>().enabled = false;
            }
            _sentence = _graceSentences;
            canTalkToKen = true;
        }
        if (other.collider.CompareTag("Ken") && canTalkToKen)
        {
            isEnter = true;

            if(other.gameObject.GetComponent<NPCInteract>()!=null)
            {
                other.gameObject.GetComponent<NPCInteract>().enabled = false;
            }
            _sentence = _kenSentences;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ken"))
        {
            isEnter = false;
            //tips.SetActive(false);
        }
        if (other.collider.CompareTag("Grace"))
        {
            isEnter = false;
        }
    }
}
