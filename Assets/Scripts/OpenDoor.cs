using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor: MonoBehaviour
{
    public static OpenDoor instance;
    public GameObject doorOpen;
    public GameObject doorClose;
    public GameObject gardener;

    void Awake()
    {
        instance = this;
    }
    public void Open()
    {
        doorOpen.SetActive(true);
        doorClose.SetActive(false);
        //gardener.transform.position = new Vector3(0, 0, 0);

        //jimmy.hastalked = true;
        //ken.hastalked = true;
        //cardhascreated = true;
        //Card card = Resources.Load<Card>(cardPath);
        //card,isCreated = true;
    }
}
