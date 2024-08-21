using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRKaiGimicIcon : MonoBehaviour
{
    private Transform _cashIconTransform;

    private Vector3 startScale;     // Unity上で事前に調整した表示位置を保存するやつ

    private Vector3 hitObjPosition;

    private string hitGimicName;

    [SerializeField]
    private Transform UICanvas = null;

    // Start is called before the first frame update
    void Start()
    {

        _cashIconTransform = this.transform;

        startScale = _cashIconTransform.localScale;  //Unity上で調整した大きさを保存する。

        hitGimicName = "none";

    }

    // Update is called once per frame
    void Update()
    {
        bool activeFlag = PlayerMove.instance.GetIconFlag();
        if (activeFlag == true)
        {
            //プレイヤーの視界判定にぶつかったギミックオブジェクトの名前を得る
            hitGimicName = CameraRayCast.instance.GetHitGimicName();


            //視界に調査対象オブジェクトがある("none"ではない)場合、
            //アイコンの表示位置を計算、自身の表示をアクティブにする。
            if (hitGimicName != "none")//
            {
                //調査対象オブジェクトのカメラ上の位置を計算

                /*  オブジェクトの中心と高さで計算するパターン***/
                
                //オブジェクトの座標・高さを取得
                hitObjPosition = CameraRayCast.instance.GetHitGimicPosition();
                float objSizeY = CameraRayCast.instance.GetGimicSizeY();

                //座標を計算し代入
                Vector3 pos = new Vector3(hitObjPosition.x, hitObjPosition.y + objSizeY / 2, hitObjPosition.z);
                _cashIconTransform.position = pos;

                //普通のUIと同じ角度にする。
                _cashIconTransform.eulerAngles = UICanvas.eulerAngles;


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
        else
        {
            OffWindow();
        }

    }


    // 表示の切り替えをするやつら。
    private void OnWindow()
    {
        _cashIconTransform.localScale = startScale;
    }

    private void OffWindow()
    {
        _cashIconTransform.localScale = Vector3.zero;
    }
}
