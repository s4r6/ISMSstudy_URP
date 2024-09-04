using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRayCast : MonoBehaviour
{

    public static CameraRayCast instance;

    [SerializeField]
    private Camera _cameraMain = null;

    [SerializeField]
    private Transform _cameraFollower = null;

    [SerializeField]
    private LayerMask layerMask;

   

    private bool modeVR = false;


    private float RayHitDistance;

    private string hitObjName = "none";
    private string hitGimicName = "none";

    private float hitObjSizeY;


    private Vector3 rootTfmPosition;


    private Vector3 hitGimicPosition;
    private float gimicSizeY;

    /*** Unity側で自動で呼ばれる関数群 ******************************************************************************/

    // Start is called before the first frame update
    void Start()
    {
        /*if (instance == null)
        {
            instance = this;
        }*/


    }

    // Update is called once per frame
    void Update()
    {
        //VRモードかどうか調べる処理。
        if (_cameraMain.enabled == true)
        {
            modeVR = false;
        }
        else
        {
            modeVR = true;
        }

        //プレイヤーの側からレイキャストの許可があれば当たり判定の処理をする。
        //bool rayCastFlag = PlayerMove.instance.GetRayCastFlag();
        /*if (rayCastFlag == true)
        {
          
            RayHitProcess(_cameraFollower);
        }*/

    }

    RaycastHit hit;     // Rayを飛ばして当たったオブジェクトの情報を格納する変数
    Vector3 rayOrigin, rayDir;
    public float rayRange = 60;
    private int rayHitID;
    public float rayRadius = 2;

    // Rayを飛ばしてオブジェクトとの衝突を調べる処理
    void RayHitProcess(Transform CameraTransform)
    {
        // Rayの原点をカメラの位置に(localだと挙動がおかしくなるので注意)
        rayOrigin = CameraTransform.position;

        // Rayを発射する方向をカメラが向いている方向に設定
        rayDir = CameraTransform.forward;

        //レイを飛ばす
       
        // 球形のRayを飛ばし、何かしらのオブジェクトに当たるか判定。
        if (Physics.SphereCast(rayOrigin, rayRadius, rayDir, out hit, rayRange, layerMask) == true)
        {
            //現在のレイが当たったオブジェクトのインスタンスIDを取得。
            int nowHitObjID = hit.transform.GetInstanceID();

            //前ループと違うオブジェクトに当たっている場合,タグ処理をする。
            if (rayHitID != nowHitObjID)
            {
                rayHitID = nowHitObjID;


                string hitTag = hit.collider.gameObject.tag;
                //Debug.Log(hitTag);

                // 当たったオブジェクトのタグで分岐処理
                switch (hitTag)
                {

                    case "RootObj":
                        /* 調べるアイコンに必要な情報を取得する ********************************/

                        //ルートに当たった場合は、自分の情報から 名前・座標・高さを取得
                        hitObjName = hit.transform.name;

                        rootTfmPosition = hit.transform.position;

                        BoxCollider bc = hit.transform.gameObject.GetComponent<BoxCollider>();

                        hitObjSizeY = bc.bounds.size.y; // 当たり判定の大きさを取得

                        //ギミックオブジェではないと設定
                        hitGimicName = "none";
                        break;

                    case "SurveyObj":

                        /* 調べるアイコンに必要な情報を取得する ********************************/
                        RootInfoGetProcess();

                        //ギミックオブジェではないと設定
                        hitGimicName = "none";

                        break;

                    case "ChildSurveyObj":

                        RootInfoGetProcess();

                        //ギミックオブジェではないと設定
                        hitGimicName = "none";
                        break;

                    case "GimicSurveyObj":
                        /* 調べるアイコンに必要な情報を取得する **********************************************/

                        RootInfoGetProcess();

                        /* ギミックアイコン用の座標処理 *****************************************************/

                        //ギミックオブジェの名前・座標・高さを取得する。
                        hitGimicName = hit.transform.name;
                        hitGimicPosition = hit.transform.position;

                        bc = hit.transform.gameObject.GetComponent<BoxCollider>();
                        gimicSizeY = bc.bounds.size.y;
                        break;


                    case "ChildGimicSurveyObj":

                        /* 調べるアイコンに必要な情報を取得する **********************************************/
                        RootInfoGetProcess();

                        /*ギミックアイコン用の座標処理 * ****************************************************/
                        //子供オブジェクトの場合親(1つ上のオブジェクト)の情報を格納する
                        Transform parent = hit.transform.parent;
                        hitGimicName = parent.name;
                        hitGimicPosition = parent.position;

                        bc = hit.transform.gameObject.GetComponent<BoxCollider>();
                        gimicSizeY = bc.bounds.size.y;          //ギミックオブジェクトの高さを取得。
                        break;

                    default:
                        //壁などに当たった場合、何もなしとする。
                        UnSurveyProcess();
                        break;
                }

            }

            //前ループのレイと同じオブジェクトに当たっている場合、アイコンの表示位置などの処理をしない
            else
            {
               
            }

        }
        else
        {// 当たってなければ何もなし
            UnSurveyProcess();

            rayHitID = 0;
        }


    }


    //レイが当たったオブジェクトのRootから調べるアイコンに必要な情報を取得する処理
    void RootInfoGetProcess()
    {
        Transform thisRoot = hit.transform.root;

        //当たったオブジェクトの名前・座標・高さを格納する
        hitObjName = thisRoot.name;

        rootTfmPosition = thisRoot.position;  // 当たったオブジェクトの一番上の親の座標を格納する

        BoxCollider bc = thisRoot.gameObject.GetComponent<BoxCollider>();   // Rootの当たり判定（高さ情報を取得）

        //BoxCollider bc = thisRoot.

        hitObjSizeY = bc.bounds.size.y; // 当たり判定の大きさを取得

        //hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;

    }


    //調べるオブジェクトじゃない時の処理纏め
    void UnSurveyProcess()
    {
        hitObjName = "none";
        hitGimicName = "none";   //ギミックではないって
        hitObjSizeY = 0;

    }


    // デバッグ用のヤツ
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;//
        Gizmos.DrawSphere(rayOrigin + rayDir * rayRange, rayRadius);
        Gizmos.DrawRay(rayOrigin, rayDir * rayRange);
        //Gizmos.DrawCube(rayOrigin + rayDir * rayRange, boxCastSize);
    }



    /*** ゲッター(他の関数から呼び出して、値を取得するやつ) **************************************************************************/

    // Rayが当たっているオブジェクトの名前を返す。 
    public string GetHitObjectName()
    {
        return hitObjName;
    }

    public bool GetModeVR()
    {
        return modeVR;

    }

    //Rayが当たったオブジェクトのY軸方向の大きさを返す。
    public float GetRaycastHitObjSizeY()
    {
        return hitObjSizeY;
    }

    public Vector3 GetRayHitObjPosition()
    {
        return rootTfmPosition;
    }

    public float GetRayHitDistance()
    {
        return hit.distance;
    }


    /*** ギミック系のゲッター *******************************/
    // Rayが当たっているギミック付きオブジェの名前を返す。
    public string GetHitGimicName()
    {
        return hitGimicName;
    }

    public Vector3 GetHitGimicPosition()
    {
        return hitGimicPosition;
    }

    public float GetGimicSizeY()
    {
        return gimicSizeY;
    }

}
