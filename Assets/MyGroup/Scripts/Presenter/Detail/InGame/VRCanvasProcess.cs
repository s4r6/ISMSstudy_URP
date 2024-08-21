using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCanvasProcess : MonoBehaviour
{
    [SerializeField]
    private Transform IconCanvas;
    
    [SerializeField]
    private float IconVRModeScale=1.0f;

    private Vector3 IconCanvasStartScale;

    [SerializeField]
    private Transform WebCameraSet;

    //** アイコン以外のUIキャンバス *********************************/
    private Transform _cashCanvas;

    [SerializeField]
    private float VRModeScale = 1.0f;

    private Vector3 startScale;


    private Vector3 canvasPos;

    [SerializeField]
    private Vector3 DefoCanvasPos = new Vector3(0, 0, 5);

    [SerializeField]
    private Vector3 VRCanvasPos = new Vector3(0, 0, 7);

    [SerializeField]
    private Vector3 CameraPos = new Vector3(0, 12, 0);

    [SerializeField]
    private Vector3 VRCameraPos= new Vector3(0, 10, 0);


    // Start is called before the first frame update
    void Start()
    {
        _cashCanvas = this.transform;
        startScale = _cashCanvas.localScale;

        IconCanvasStartScale = IconCanvas.localScale;

        //ManualWindowStartScale = ManualWindow.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        /*bool modeVR = CameraRayCast.instance.GetModeVR();

        //VRモードの時、キャンバスを小さくする
        if (modeVR == true)

        {
            _cashCanvas.localPosition = VRCanvasPos;

            _cashCanvas.localScale = startScale * VRModeScale;
            
            IconCanvas.localScale = IconCanvasStartScale * IconVRModeScale;

            WebCameraSet.localPosition = VRCameraPos;


        }
        else
        {
            _cashCanvas.localPosition = DefoCanvasPos;
            
            _cashCanvas.localScale = startScale;
            
            IconCanvas.localScale = IconCanvasStartScale;

            WebCameraSet.localPosition = CameraPos;

        }*/

    }
}
