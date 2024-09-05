using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISMS.Data;
using Zenject;
using Cysharp.Threading.Tasks;
using ISMS.Presenter.Detail.Player;
using UniRx;

namespace ISMS.Presenter.Detail.Stage
{
    public class SurveyObjectManager : MonoBehaviour
    {
        [SerializeField]
        int GetStageNum;
        [SerializeField]
        PlayerCore _player;

        ObjectDictionary _objDic;
        int DengerObjNum; 

        [Inject]
        IRepository _stageDataLoader;

        public async UniTask Initialize()
        {
            await _stageDataLoader.GetStageData();  //データをダウンロード
            var parent = this.transform;
            _objDic = await _stageDataLoader.GetObjectData(0);  //ダウンロードしたデータからオブジェクトの情報を取得
            foreach (Transform child in parent)
            {
                var _surveyObj = child.gameObject.GetComponent<BaseSurveyObject>();
                _surveyObj._obj = _objDic.GetObject(_surveyObj.name);   //各オブジェクトのデータを取得してセット
                _surveyObj.CorrectFlag
                    .SkipLatestValueOnSubscribe()   //登録した時の初期値のPushを無視
                    .Subscribe(x =>
                    {
                        if(x == true)
                        {
                            DengerObjNum--;
                            if (DengerObjNum == 0)
                            {
                                Debug.Log("終了");
                                _player.ChangeCurrentPlayerState(PlayerState.Result);
                            }
                        }
                        else
                        {
                            _player.AddInCorrectCount();    //失敗した回数を加算
                        }
                    }).AddTo(this);
            }
            DengerObjNum = _objDic.GetDengerObjNum();   //危険なオブジェクトの数を取得
        }
    }
}
