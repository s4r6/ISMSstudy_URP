using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTCVRSurveyIcon : MonoBehaviour
{

    private Transform _cashIconTransform;

    private Vector3 startScale;     // Unity上で事前に調整した表示位置を保存するやつ

    private Vector3 hitObjPosition;

    private string hitObjName = "none";

    //なぜかnullの値を使った方がちゃんと動作するのでnullにしてる。
    [SerializeField]
    private Camera _cameraMain = null;

    [SerializeField]
    private Camera _cameraL = null;

    [SerializeField] private Transform VRcanvas;
   

    /*** Unity側で自動で呼ばれる関数群 ******************************************************************/

    // Start is called before the first frame update
    void Start()
    {
        _cashIconTransform = this.transform;  //アタッチされたオブジェクトのtransformをキャッシュ

        startScale = _cashIconTransform.localScale;          //Unity上で調整した大きさを保存する。

        hitObjName = "none";
    }



    // Update is called once per frame
    void Update()
    {

        /*** プレイヤーの視界判定に調査オブジェクトがある場合、オブジェクトの上にアイコンを表示する ***/

        //プレイヤーの視界判定にぶつかったオブジェクトの名前を得る
        hitObjName = CameraRayCast.instance.GetHitObjectName();


        //視界に調査対象オブジェクトがある("none"ではない)場合、座標計算と表示。
        if (hitObjName != "none")//
        {
            //調査対象オブジェクトのカメラ上の位置を計算

            //座標を取得。GameObject.Findの処理が重いらしいので不採用
            //hitObjPosition = GameObject.Find(hitObjName).transform.position;  

            //オブジェクトの座標と高さを取得
            hitObjPosition = CameraRayCast.instance.GetRayHitObjPosition();
            float objSizeY = CameraRayCast.instance.GetRaycastHitObjSizeY();

            Vector3 yVec = new Vector3(0, 0.5f, 0);
            //Vector3 objPos = hitObjPosition + objSizeY * yVec;


            //Vector2 rectPos = RectTransformUtility.WorldToScreenPoint(_cameraMain, hitObjPosition + y * objSizeY);
            Vector3 rectPos = hitObjPosition + yVec * objSizeY;
            Vector3 pos = new Vector3(rectPos.x, rectPos.y, VRcanvas.position.z);
            _cashIconTransform.localPosition = pos;

            

            //表示をアクティブにする
            OnWindow();


        }
        //映っていない時は表示を隠す
        else
        {
            //myRectTfm.position = startPosition;
            OffWindow();
        }



    }


    // 表示の切り替えをするやつら。
    private void OnWindow()
    {
        _cashIconTransform.localScale = startScale* ScaleCalibration();
    }

    private void OffWindow()
    {
        _cashIconTransform.localScale = Vector3.zero;
    }


  
    private float ScaleCalibration()
    {

        float Amax = CameraRayCast.instance.rayRange;
        float Amin = 0.0f;
        float Bmax = 0.5f;
        float Bmin = 1.3f;

        float nowA = CameraRayCast.instance.GetRayHitDistance();

        float calibration = Bmin + (Bmax - Bmin) * (nowA - Amin) / (Amax - Amin);


        return calibration;

        ////float calibration =  1.5f * CameraRayCast.instance.GetRayHitDistance()/ CameraRayCast.instance.rayRange;

        //myRectTfm.localScale = startScale * calibration;
    }



}
