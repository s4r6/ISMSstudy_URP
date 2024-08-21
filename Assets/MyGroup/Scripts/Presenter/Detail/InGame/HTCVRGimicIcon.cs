using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTCVRGimicIcon : MonoBehaviour
{
    /*private Transform _cashIconTransform;

    private Vector3 startScale;     // Unity上で事前に調整した表示位置を保存するやつ
    
    private Vector3 hitObjPosition;

    private string hitGimicName = "none";

    [SerializeField] private Transform VRcanvas;

    //なぜかnullの値を使った方がちゃんと動作するのでnullにしてる。
    private Camera _camera = null;

    // Start is called before the first frame update
    void Start()
    {


        _cashIconTransform = this.transform;

        startScale = _cashIconTransform.localScale;          //Unity上で調整した大きさを保存する。

        hitGimicName = "none";
      

    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの視界判定にぶつかったギミックオブジェクトの名前を得る
        //hitGimicName = CameraRayCast.instance.GetHitGimicName();


        //視界に調査対象オブジェクトがある("none"ではない)場合、
        //アイコンの表示位置を計算、自身の表示をアクティブにする。
        if (hitGimicName != "none")//
        {
            //調査対象オブジェクトのカメラ上の位置を計算

            /*  オブジェクトの中心と高さで計算するパターン***/
            //hitObjPosition = GameObject.Find(hitObjName).transform.position;  //座標を取得。GameObject.Findの処理が重いらしいので不採用にした

            //オブジェクトの座標・高さを取得
            /*hitObjPosition = CameraRayCast.instance.GetHitGimicPosition();     
            float objSizeY = CameraRayCast.instance.GetGimicSizeY();
            */
            ////座標を計算し代入
            //Vector3 pos = new Vector3(hitObjPosition.x, hitObjPosition.y + objSizeY / 2, hitObjPosition.z);

            //Vector3 y = new Vector3(0, 0.5f, 0);

            
            /*Vector3 rectPos = hitObjPosition + y * objSizeY;
            Vector3 pos = new Vector3(rectPos.x, rectPos.y,VRcanvas.position.z);

            _cashIconTransform.localPosition = pos;

            _cashIconTransform.localScale = startScale* ScaleCalibration();

            //表示をアクティブにする
            ActiveDisplay();


        }
        //映っていない時は表示を隠す
        else
        {
            //myRectTfm.position = startPosition;
            InActiveDisplay();
        }


    }


    // 表示の切り替えをするやつら。
    private void ActiveDisplay()
    {

        //_cashIconTransform.localScale = startScale * ScaleCalibration();
        //_cashIconTransform.localScale = startScale;

    }

    private void InActiveDisplay()
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
    }*/
}
