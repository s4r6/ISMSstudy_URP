using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageManager : MonoBehaviour
{
    public TextMeshProUGUI MessageTMP;
    public static MessageManager instance;

    bool activeFlag;
    float colorAlpha;
    int activeCount;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        activeFlag = false;
        colorAlpha = 0;
        MessageTMP.color = new Color(255, 255, 255, colorAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (activeFlag == true)
        {
            if (activeCount <= 60)
            {
                colorAlpha -= 0.4f / 60.0f;
            }
            else
            {
                colorAlpha -= 1.0f / 30.0f;
            }
            
            if (colorAlpha < 0)
            {
                colorAlpha = 0;
                activeFlag = false;
            }
            MessageTMP.color = new Color(255, 255, 255, colorAlpha);
            activeCount++;
        }
        else
        {

        }

    }

    public void SetMessage(string s)
    {
        MessageTMP.text = s;
        activeFlag = true;
        colorAlpha = 1; //’l‚ð‘å‚«‚­‚·‚ê‚ÎAÁ‚¦‚éŽžŠÔ‚ª’·‚­‚È‚é
        activeCount = 0;
    }


}
