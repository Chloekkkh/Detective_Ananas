using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

[System.Serializable]
public struct Sentence
{
    public string name;
    [TextArea(5, 10)]
    public string sentences;
    public Sprite image;
}
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    //dialoguePanel
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image npcImage;

    //back
    public GameObject back;

    public GameObject Ananas;

    //public Sprite gardenerImage;

    //dialogue struct
    public Sentence[] sentenceStruct;

    //dialogue content

    public float typeTime = 1f;

    //current sentence
    [SerializeField]private int currentIndex;

    private bool isScrolling = false;
    public bool newCardAnimationFinished = true;
    private string cardPath = null;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }
    void Update()
    {
        //空格键一句一句显示
        //当对话框激活时才能按空格键
        if(dialoguePanel.activeInHierarchy)
        {
            if(FindObjectOfType<SearchInRoom>() != null&&FindObjectOfType<PlayerMove>() != null)
            {
                FindObjectOfType<PlayerMove>().canMove = false;
                FindObjectOfType<SearchInRoom>().canSearch = false;
            }
            if((Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButtonDown(0)) && !isScrolling && newCardAnimationFinished)
            {
                
                currentIndex++;
                if(currentIndex < sentenceStruct.Length)
                {
                    npcImage.sprite = sentenceStruct[currentIndex].image;
                    nameText.text = sentenceStruct[currentIndex].name;

                    if(sentenceStruct[currentIndex].name == "Ananas")
                    {
                        Ananas.SetActive(true);
                    }
                    else
                    {
                        Ananas.SetActive(false);
                    }
                    cardPath = FindCard();

                    StartCoroutine(ScrollText());//一个字一个字显示
                }
                else
                {
                    if(SceneManager.GetActiveScene().buildIndex == 1 && currentIndex == sentenceStruct.Length)
                    {
                        ScenceManager.instance.StartCoroutine(ScenceManager.instance.Fadeout());
                    }
                    dialoguePanel.SetActive(false);//最后一句话关闭对话框
                    if(FindObjectOfType<SearchInRoom>() != null&&FindObjectOfType<PlayerMove>() != null)
                    {
                        FindObjectOfType<PlayerMove>().canMove = true;
                        FindObjectOfType<SearchInRoom>().canSearch = true;
                    }
                }
            }
        }
        
    }

    //new 打开对话框
    public void ShowDialogue(Sentence[] sentencesStruct, bool hasTalked)
    {
        this.sentenceStruct = sentencesStruct;

        currentIndex = 0;
        nameText.text = sentencesStruct[currentIndex].name;
        npcImage.sprite = sentencesStruct[currentIndex].image;

        if(sentenceStruct[currentIndex].name == "Ananas")
        {
            Ananas.SetActive(true);
        }
        else
        {
            Ananas.SetActive(false);
        }

        dialoguePanel.SetActive(true);
        if(hasTalked) back.SetActive(true);
        else back.SetActive(false);

        cardPath = FindCard();

        StartCoroutine(ScrollText());
        
        if(FindObjectOfType<SearchInRoom>() != null&&FindObjectOfType<PlayerMove>() != null)
        {
            FindObjectOfType<PlayerMove>().canMove = false;
            FindObjectOfType<SearchInRoom>().canSearch = false;
        }
    }

    public void ShowDialogue(Sentence[] sentencesStruct)
    {
        this.sentenceStruct = sentencesStruct;

        currentIndex = 0;
        nameText.text = sentencesStruct[currentIndex].name;
        npcImage.sprite = sentencesStruct[currentIndex].image;

        if(sentenceStruct[currentIndex].name == "Ananas")
        {
            Ananas.SetActive(true);
        }
        else
        {
            Ananas.SetActive(false);
        }

        dialoguePanel.SetActive(true);
        back.SetActive(false);
        cardPath = FindCard();

        StartCoroutine(ScrollText());
        
        if(FindObjectOfType<SearchInRoom>() != null&&FindObjectOfType<PlayerMove>() != null)
        {
            FindObjectOfType<PlayerMove>().canMove = false;
            FindObjectOfType<SearchInRoom>().canSearch = false;
        }
    }


    private IEnumerator ScrollText()
    {
        isScrolling = true;
        dialogueText.text = "";

        //TODO 要改成sentenceStruct[currentIndex].sentences.tochararray
        foreach (char letter in sentenceStruct[currentIndex].sentences.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeTime);
        }
        isScrolling = false;
        if(cardPath != null)
        {
            if(cardPath == "time")
            {
                newCardAnimationFinished = false;
                StartCoroutine(StartDialogue.instance.ViewSkillCardAni());
            }
            else if(cardPath == "End")
            {
               ScenceManager.instance.StartCoroutine(ScenceManager.instance.Fadeout());
            }
            else if(cardPath == "Door")
            {
                OpenDoor.instance.Open();
            }
            else
            {            
                newCardAnimationFinished = false;
                CardsManager.instance.GetNewCard(cardPath);
            }
        }

        //每句话结束后出现press space to continue
    }

    // public void CheckName()
    // {
    //     if(sentences[currentIndex].StartsWith("G-"))//gardener
    //     {
    //         npcImage.sprite = gardenerImage;
    //         nameText.text = "Gardener";
    //         sentences[currentIndex] = sentences[currentIndex].Replace("G-", "");
    //     }
    //     if (sentences[currentIndex].StartsWith("n-"))//player
    //     {
    //         nameText.text = "You";
    //         sentences[currentIndex] = sentences[currentIndex].Replace("n-", "");
    //     }
    // }
    //在指定句子结束后，加载指定卡牌
    //判断当前句子是否以指定文字开始,在给句子赋值的时候就可以在想生成卡牌的句子前面加上这个指定文字
    //ChessHistory-
    //FireEvent-
    //Gardener-
    //OldLady-
    //CashClue-
    //OldLadySoul-
    //OldLadyDead-
    //OnlySon-
    //SecondHusband-
    //ChessMap-

    public string FindCard()
    {
        string cardPath = null;
        if(sentenceStruct[currentIndex].sentences.StartsWith("ChessHistory-"))
        {
            sentenceStruct[currentIndex].sentences = sentenceStruct[currentIndex].sentences.Replace("ChessHistory-", "");
            cardPath = "CardsDatabase/Prop/Clue/Clue_Chess_History";
            return cardPath;
        }
        if (sentenceStruct[currentIndex].sentences.StartsWith("FireEvent-"))
        {
            sentenceStruct[currentIndex].sentences = sentenceStruct[currentIndex].sentences.Replace("FireEvent-", "");
            cardPath = "CardsDatabase/Prop/Clue/Clue_FireEvent";
            return cardPath;
        }
        if (sentenceStruct[currentIndex].sentences.StartsWith("Gardener-"))
        {
            sentenceStruct[currentIndex].sentences = sentenceStruct[currentIndex].sentences.Replace("Gardener-", "");
            cardPath = "CardsDatabase/Role/Role_Gardener";
            return cardPath;
        }
        if (sentenceStruct[currentIndex].sentences.StartsWith("OldLady-"))
        {
            sentenceStruct[currentIndex].sentences = sentenceStruct[currentIndex].sentences.Replace("OldLady-", "");
            cardPath = "CardsDatabase/Role/Role_Old Lady";
            return cardPath;
        }
        if (sentenceStruct[currentIndex].sentences.StartsWith("CashClue-"))
        {
            sentenceStruct[currentIndex].sentences = sentenceStruct[currentIndex].sentences.Replace("CashClue-", "");
            cardPath = "CardsDatabase/Prop/Clue/Clue_CashClue";
            return cardPath;
        }
        if (sentenceStruct[currentIndex].sentences.StartsWith("OldLadySoul-"))
        {
            sentenceStruct[currentIndex].sentences = sentenceStruct[currentIndex].sentences.Replace("OldLadySoul-", "");
            cardPath = "CardsDatabase/Role/Role_Old Lady Soul";
            return cardPath;
        }
        if (sentenceStruct[currentIndex].sentences.StartsWith("OldLadyDead-"))
        {
            sentenceStruct[currentIndex].sentences = sentenceStruct[currentIndex].sentences.Replace("OldLadyDead-", "");
            cardPath = "CardsDatabase/Prop/Truth/Truth_OldLadyIsDead";
            return cardPath;
        }
        if (sentenceStruct[currentIndex].sentences.StartsWith("OnlySon-"))
        {
            sentenceStruct[currentIndex].sentences = sentenceStruct[currentIndex].sentences.Replace("OnlySon-", "");
            cardPath = "CardsDatabase/Role/Role_Only Son";
            return cardPath;
        }
        if (sentenceStruct[currentIndex].sentences.StartsWith("SecondHusband-"))
        {
            sentenceStruct[currentIndex].sentences = sentenceStruct[currentIndex].sentences.Replace("SecondHusband-", "");
            cardPath = "CardsDatabase/Role/Role_Second Husband";
            return cardPath;
        }
        if (sentenceStruct[currentIndex].sentences.StartsWith("ChessMap-"))
        {
            sentenceStruct[currentIndex].sentences = sentenceStruct[currentIndex].sentences.Replace("ChessMap-", "");
            cardPath = "CardsDatabase/Prop/Clue/Clue_ChessMapInfo";
            return cardPath;
        }
        //skill card
        if(sentenceStruct[currentIndex].sentences.StartsWith("Time-"))
        {
            sentenceStruct[currentIndex].sentences = sentenceStruct[currentIndex].sentences.Replace("Time-", "");
            // cardPath = "CardsDatabase/Prop/Skill/Skill_MakeFace";
            // return cardPath;
            //给cardpath一个标记，只需要展示一下就可以
            cardPath = "time";
            return cardPath;
        }

        //end game
        if(sentenceStruct[currentIndex].sentences.StartsWith("End-"))
        {
            sentenceStruct[currentIndex].sentences = sentenceStruct[currentIndex].sentences.Replace("End-", "");
            cardPath = "End";
            return cardPath;
        }

        if(sentenceStruct[currentIndex].sentences.StartsWith("Door-"))
        {
            //园丁说完话 开门 园丁进
            sentenceStruct[currentIndex].sentences = sentenceStruct[currentIndex].sentences.Replace("Door-", "");
            cardPath = "Door";
            return cardPath;
        }
        return cardPath;
    }
}
