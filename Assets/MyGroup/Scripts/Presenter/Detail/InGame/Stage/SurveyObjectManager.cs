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

        async UniTask Initialize()
        {
            var parent = this.transform;
            _objDic = await _stageDataLoader.GetObjectData(0);
            foreach (Transform child in parent)
            {
                var _surveyObj = child.gameObject.GetComponent<BaseSurveyObject>();
                _surveyObj._obj.Value = _objDic.GetObject(_surveyObj.name);
            }
        }





    }
}
