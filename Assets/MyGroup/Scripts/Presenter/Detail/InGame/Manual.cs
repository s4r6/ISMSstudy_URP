using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manual : MonoBehaviour
{
    public static Manual instance;

    [SerializeField]
    private Transform BackGround = null;

    [SerializeField]
    private Transform ExploreWindow= null;

    [SerializeField]
    private Transform DetailInfo = null;

    [SerializeField]
    private Transform DetailInfoArchives = null;

    [SerializeField]
    private Transform DetailInfoMove = null;

    [SerializeField]
    private Transform BackCommand = null;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        WindowOff();
    }

    public void ExploreActive()
    {
        //BackGround.gameObject.SetActive(true); 
        ExploreWindow.gameObject.SetActive(true);
    }
    public void DetailActive()
    {
        //BackGround.gameObject.SetActive(true);
        DetailInfo.gameObject.SetActive(true);
        BackCommand.gameObject.SetActive(true);
        DetailInfoArchives.gameObject.SetActive(true);


    }
    public void ArchiveActive()
    {
        //BackGround.gameObject.SetActive(true);
        DetailInfo.gameObject.SetActive(true);
        BackCommand.gameObject.SetActive(true);
        DetailInfoMove.gameObject.SetActive(true);
    }

    public void WindowOff() 
    {
        BackGround.gameObject.SetActive(false);
        ExploreWindow.gameObject.SetActive(false);
        DetailInfo.gameObject.SetActive(false);
        BackCommand.gameObject.SetActive(false);
        DetailInfoMove.gameObject.SetActive(false);
        DetailInfoArchives.gameObject.SetActive(false);

    }

    public void BackCommandActive()
    {
        BackCommand.gameObject.SetActive(true);
    }


}
