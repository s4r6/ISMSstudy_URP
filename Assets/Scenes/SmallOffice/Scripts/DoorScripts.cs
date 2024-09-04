using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScripts : GimicObjBase
{
    private Transform _cashThisTransform;

    Vector3 startPosition = new Vector3(-90f, 180f, -180f);
    Vector3 afterPosition = new Vector3(-90f, 180f, -70f);

    bool openFlag = false;


    public bool LockFlag = false;


    // Start is called before the first frame update
    void Start()
    {
        _cashThisTransform = this.transform;
        _cashThisTransform.localEulerAngles = startPosition;
    }

    // Update is called once per frame

    public override void Action()
    {
        if (LockFlag == false)
        {
            openFlag = !openFlag;
            if (openFlag == true)
            {
                _cashThisTransform.localEulerAngles = afterPosition;
                AudioManager.instance.playSE(4);
            }
            else
            {
                _cashThisTransform.localEulerAngles = startPosition;
                AudioManager.instance.playSE(5);

            }
            

        }
        else
        {
            MessageManager.instance.SetMessage("åÆÇ™Ç©Ç©Ç¡ÇƒÇ¢Ç‹Ç∑ÅB");
            AudioManager.instance.playSE(3);

        }

    }
}
