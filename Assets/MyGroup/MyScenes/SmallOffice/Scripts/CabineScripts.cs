using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabineScripts :GimicObjBase
{

    //float cabineStartX = 3.15f;
    [SerializeField]
    private float cabineMaxZ;
    
    bool cabineOpen = false;
    Vector3 startPosition;
    Vector3 openedPosition;

    private Transform _cashThisTransform;

    [SerializeField]
    private bool LockFlag = false;


    // Start is called before the first frame update
    void Start()
    {
        _cashThisTransform = this.transform;
        startPosition = this.transform.localPosition;
        openedPosition = startPosition + new Vector3(0, 0.0f, cabineMaxZ);
    }

   

    public override void Action()
    {
        if (LockFlag == false)
        {
            cabineOpen = !cabineOpen;
            if (cabineOpen == true)
            {
                _cashThisTransform.localPosition = openedPosition;
                AudioManager.instance.playSE(6);
            }
            else
            {
                _cashThisTransform.localPosition = startPosition;
                AudioManager.instance.playSE(7);
            }
            
        }
        else
        {
            MessageManager.instance.SetMessage("åÆÇ™Ç©Ç©Ç¡ÇƒÇ¢Ç‹Ç∑ÅB");
            AudioManager.instance.playSE(3);
        }
        
    }
}
