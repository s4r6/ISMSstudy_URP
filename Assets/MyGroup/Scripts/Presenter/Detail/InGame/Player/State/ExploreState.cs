using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerState
{
    public class ExploreState : PlayerStateBase
    {
        public override void OnUpdate(PlayerState owner)
        {
            Debug.Log("探索モード");
            PlayerMove.instance.Move(); //移動処理

            //カメラリセットのボタンが押されたら
            if (GameInput.instance._CameraResetAction.triggered == true)
            {
                Debug.Log("カメラリセット");
                PlayerMove.instance.ResetCamera();  //カメラリセット
            }

            //マニュアル表示ボタンが押されたら
            if (GameInput.instance._R1Action.triggered == true)
            {
                Debug.Log("ドキュメント表示");
                owner.ChangeState(Documentstate);
            }

            if (GameInput.instance._DecisionAction.triggered == true)
            {
                Debug.Log("決定ボタン入力");
                //視界に調べられるオブジェクトがある時、詳細情報を表示するフェーズに移行。
                if (CameraRayCast.instance.GetHitObjectName() != "none")
                {
                    Debug.Log("詳細表示");
                    owner.ChangeState(DetailInfostate);
                }
            }

            if (GameInput.instance._GimicAction.triggered == true)
            {
                string gimicName = CameraRayCast.instance.GetHitGimicName();

                //視界にギミックを発動できるオブジェクトがある時
                if (gimicName != "none")
                {
                    //対象のギミックオブジェを探して、そいつの持つアクションを実行する
                    GameObject.Find(gimicName).GetComponent<GimicObjBase>().Action();
                }
            }

            // アーカイブ画面に移行
            if (GameInput.instance._ArchivesAction.triggered == true)
            {
                ArchivesManager.instance.InfoOn();
                owner.ChangeState(Archivesstate);
            }
        }
    }
}
