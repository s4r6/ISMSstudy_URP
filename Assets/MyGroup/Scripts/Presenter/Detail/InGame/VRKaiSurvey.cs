using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRKaiSurvey : MonoBehaviour
{
    private Transform _cashIconTransform;

    private Vector3 startScale;     // Unity上で事前に調整した表示位置を保存するやつ

    private Vector3 hitObjPosition;

    private string hitObjName = "none";

    
    [SerializeField]
    private Transform UICanvas = null;


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
        bool activeFlag = PlayerMove.instance.GetIconFlag();
        if (activeFlag == true)
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
