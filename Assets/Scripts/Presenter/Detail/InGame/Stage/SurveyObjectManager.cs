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
    /// <summary>
    /// �X�e�[�W�f�[�^���e�I�u�W�F�N�g�ɐݒ�
    /// �q�I�u�W�F�N�g��BaseSurveyObject�N���X�Ɋe�l��ݒ�
    /// </summary>
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

        async void Start()
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
                    .Subscribe(async x =>
                    {
                        if(x == true)
                        {
                            DengerObjNum--;
                            if (DengerObjNum == 0)
                            {
                                Debug.Log("�I��");
                                await UniTask.WaitUntil(() => _player.CurrentPlayerState.Value == PlayerState.Explore);     //���X�N������Ԃ��I�����ĒT���X�e�[�g�ɖ߂�������
                                _player.ChangeCurrentPlayerState(PlayerState.Result);   //���U���g�\��
                            }
                        }
                        else
                        {
                            _player.AddInCorrectCount();    //���s�����񐔂����Z
                        }
                    }).AddTo(this);
            }
            DengerObjNum = _objDic.GetDengerObjNum();   //�댯�ȃI�u�W�F�N�g�̐����擾
            _player.ChangeCurrentPlayerState(PlayerState.Wait);
        }
    }
}
