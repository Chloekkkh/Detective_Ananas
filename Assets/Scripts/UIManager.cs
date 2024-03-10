using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Button thinkButton;
    public Button thinkBackButton;
    public Button settingButton;
    public Button settingBackButton;
    public Button backButton;
    public Button newcardContinueButton;

    //fusion
    public Button fusionButton;

    public GameObject thinkPanel;
    //public bool canFusion = false;
    public GameObject settingPanel;

    //audio set
    public AudioMixerGroup audioMixer;

    //dialogue Back
    public Button dialogueBackButton;


    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {

        thinkButton.onClick.AddListener(Think);
        thinkBackButton.onClick.AddListener(ThinkBack);
        // settingButton.onClick.AddListener(Setting);
        // settingBackButton.onClick.AddListener(SettingBack);

        fusionButton.onClick.AddListener(ThinkFusion);

        newcardContinueButton.onClick.AddListener(NewCardContinue);

        dialogueBackButton.onClick.AddListener(DialogueBack);


    }
    public void Think()
    {
        thinkPanel.SetActive(true);
        AudioManager.Play("Click");
    }
    public void ThinkBack()
    {
        thinkPanel.SetActive(false);
        CardFusionManager.instance.CloseFusion();
        AudioManager.Play("Click");
    }
    // public void Setting()
    // {
    //     settingPanel.SetActive(true);
    //     AudioManager.Play("Click");
    // }
    // public void SettingBack()
    // {
    //     settingPanel.SetActive(false);
    //     AudioManager.Play("Click");
    // }
    public void NewCardContinue()
    {
        CardsManager.instance.newCardPanel.SetActive(false);
        AudioManager.Play("Click");
    }
    public void ThinkFusion()
    {
        CardFusionManager.instance.Fusion();
        //AudioManager.Play("Click");
    }

    public void DialogueBack()
    {
        DialogueManager.instance.dialoguePanel.SetActive(false);
        AudioManager.Play("Click");
        if(FindObjectOfType<SearchInRoom>() != null&&FindObjectOfType<PlayerMove>() != null)
        {
            FindObjectOfType<PlayerMove>().canMove = true;
            FindObjectOfType<SearchInRoom>().enabled = true;
        }
    }

}
