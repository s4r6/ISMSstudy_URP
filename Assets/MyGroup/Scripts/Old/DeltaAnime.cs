using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaAnime : MonoBehaviour
{

    [SerializeField]
    private Transform DeltaLeft, DeltaRight;
    private Vector3 LeftPos, RightPos,movePoint;

    private int activeCount;

    // Start is called before the first frame update
    void Start()
    {
        activeCount = 0;
        LeftPos = DeltaLeft.localPosition;
        RightPos = DeltaRight.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(ArchivesManager.instance.GetArchivesCount()< 2 )
        {
            DeltaLeft.gameObject.SetActive(false);
            DeltaRight.gameObject.SetActive(false);
        }
        else
        {
            DeltaLeft.gameObject.SetActive(true);
            DeltaRight.gameObject.SetActive(true);
        }

        activeCount++;
        movePoint.x = activeCount % 30;

        DeltaLeft.localPosition = LeftPos+ movePoint;
        DeltaRight.localPosition = RightPos - movePoint;
    }
}
