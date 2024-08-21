using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace ISMS.Domain.Entity
{
    public class StageEntity : CleanArchtecture.IEntity
    {
        private int RiskObjNum;//�ǂݍ��񂾃X�e�[�W�f�[�^�ɂ��郊�X�N�̐�
        private List<string> SurveyObjList;   // CSV�t�@�C������ǂݍ��񂾒����I�u�W�F�N�g�̃f�[�^�i�[.
        private int SurveyingObjIndex;    //�������̃I�u�W�F�g�̃C���f�b�N�X�ԍ�
        public int stageID { get; private set; }
        //public string stageName { get; private set; } = null;
        ReactiveProperty<string> stageName = new ReactiveProperty<string>(null);
        public IObservable<string> OnChangeStageName => stageName;


        public StageEntity()
        {
            this.stageID = -1;
        }
        public void SetStageID(int id)
        {
            stageID = id;
        }

        public void SetStageName(string name)
        {
            stageName.Value = name;
        }
        
        



    }
}
