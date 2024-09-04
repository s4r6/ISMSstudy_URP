using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISMS.Data;
using Zenject;
using Cysharp.Threading.Tasks;

namespace ISMS.Presenter.Detail.Stage
{
    public class SurveyObjectManager : MonoBehaviour
    {
        [SerializeField]
        int GetStageNum = 0;
        ObjectDictionary _objDic;
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
                _surveyObj._obj = _objDic.GetObject(_surveyObj.name);
            }
        }





    }
}
