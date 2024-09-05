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
            await _stageDataLoader.GetStageData();  //�f�[�^���_�E�����[�h
            var parent = this.transform;
            _objDic = await _stageDataLoader.GetObjectData(0);  //�_�E�����[�h�����f�[�^����I�u�W�F�N�g�̏����擾
            foreach (Transform child in parent)
            {
                var _surveyObj = child.gameObject.GetComponent<BaseSurveyObject>();
                _surveyObj._obj = _objDic.GetObject(_surveyObj.name);   //�e�I�u�W�F�N�g�̃f�[�^���擾���ăZ�b�g
                _surveyObj.CorrectFlag
                    .SkipLatestValueOnSubscribe()   //�o�^�������̏����l��Push�𖳎�
                    .Subscribe(x =>
                    {
                        if(x == true)
                        {
                            DengerObjNum--;
                            if (DengerObjNum == 0)
                            {
                                Debug.Log("�I��");
                                _player.ChangeCurrentPlayerState(PlayerState.Result);
                            }
                        }
                        else
                        {
                            _player.AddInCorrectCount();    //���s�����񐔂����Z
                        }
                    }).AddTo(this);
            }
            DengerObjNum = _objDic.GetDengerObjNum();   //�댯�ȃI�u�W�F�N�g�̐����擾
        }
    }
}
