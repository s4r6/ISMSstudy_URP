using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DepthCameraManager : MonoBehaviour
{

    [SerializeField]
    private Camera _xrCameraMain = null;


    [SerializeField]
    private Camera _DepthCameraMain = null;

    [SerializeField]
    private Camera _DepthCameraL = null;

    [SerializeField]
    private Camera _DepthCameraR = null;

    [SerializeField]
    private Camera _UIDepthCamL = null;

    [SerializeField]
    private Camera _UIDepthCamR = null;


    private Transform _CameraLTfm = null;
    private Transform _CameraRTfm = null;

    private Transform _UIDepthLtfm, _UIDepthRtfm;

    private Vector3 LstartPos, RstartPos;
    private Vector3 LCameraPos, RCameraPos;




    // Start is called before the first frame update
    void Start()
    {
        //_DepthCameraMain.enabled = true;
        _DepthCameraL.enabled = false;
        _DepthCameraR.enabled = false;

        //_DepthCameraL.stereoTargetEye = StereoTargetEyeMask.Both;
        //_DepthCameraR.stereoTargetEye = StereoTargetEyeMask.Both;

        _CameraLTfm = _DepthCameraL.transform;
        _CameraRTfm = _DepthCameraR.transform;

        LstartPos = _DepthCameraL.transform.localPosition;
        RstartPos = _DepthCameraR.transform.localPosition;

        LCameraPos = LstartPos;
        RCameraPos = RstartPos;

        _UIDepthLtfm = _UIDepthCamL.transform;
        _UIDepthRtfm = _UIDepthCamR.transform;


    }

    // Update is called once per frame
    void Update()
    {
        if (_xrCameraMain.enabled == true)
        {
            _DepthCameraMain.enabled = true;
            _DepthCameraL.enabled = false;
            _DepthCameraR.enabled = false;

            _UIDepthCamL.enabled = false;
            _UIDepthCamR.enabled = false;
        }
        else
        {
            _DepthCameraMain.enabled = false;
            //_DepthCameraL.enabled = true;
            //_DepthCameraR.enabled = true;

            //_UIDepthCamL.enabled = true;
            //_UIDepthCamR.enabled = true;


        }

       

    }
}
