using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace ISMS.Domain.Entity
{
    public class StageEntity : CleanArchtecture.IEntity
    {
        private int RiskObjNum;//読み込んだステージデータにあるリスクの数
        private List<string> SurveyObjList;   // CSVファイルから読み込んだ調査オブジェクトのデータ格納.
        private int SurveyingObjIndex;    //調査中のオブジェトのインデックス番号
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
