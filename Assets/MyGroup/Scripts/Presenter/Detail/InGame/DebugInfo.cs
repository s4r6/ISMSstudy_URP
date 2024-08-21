using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugInfo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI DepthPosText;

    [SerializeField]
    private Transform DepthL, DepthR;

    [SerializeField]
    private Transform CameraFolower;

    [SerializeField]
    private TextMeshProUGUI CameraAngleText;

    [SerializeField]
    private TextMeshProUGUI CameraWorldText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DepthPosText.text = "UIカメラL：" + DepthL.localPosition.x.ToString()+ 
            "\n"+ "UIカメラR：" + DepthR.localPosition.x.ToString();

        Vector3 angles = CameraFolower.localEulerAngles;
        CameraAngleText.text = "CameraLocalAngles\n" + "X:" + angles.x.ToString("f4") +" Y:" +angles.y.ToString("f4") + " Z:" + angles.z.ToString("f4");

        Vector3 angleW = CameraFolower.eulerAngles;
        CameraWorldText.text = "CameraWorldAngles\n" + "X:" + angleW.x.ToString("f4") + " Y:" + angleW.y.ToString("f4") + " Z:" + angleW.z.ToString("f4");


    }
}
