using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISMS.Presenter.Detail.Stage
{
    public abstract class BaseSurveyObject : MonoBehaviour
    {
        protected string _name;
        protected string _discribe;
        protected bool _risk;

        protected Vector3 SelfObjSize;

        [SerializeField]
        protected float IconYoffset = 0.5f;
        [SerializeField]
        protected GameObject SurveyIcon;
        protected Vector3 IconDefaultScale;

        private bool IsWatched = false;

        private void Awake()
        {
            SelfObjSize = this.gameObject.GetComponent<BoxCollider>().bounds.size;  //自身のサイズ取得
            IconDefaultScale = SurveyIcon.transform.localScale;
        }

        private void Update()
        {
            
            
        }

        protected virtual void GetSelfInfo()
        {

        }

        public void DisPlaySurveyIcon(float distance)
        {
            var IconPos = this.gameObject.transform.position + new Vector3(0, IconYoffset, 0) * SelfObjSize.y;  //アイコンの表示位置
            SurveyIcon.transform.position = IconPos;

            if (SurveyIcon.activeSelf) return;
                SurveyIcon.SetActive(true);
        }

        private float ScaleCalibration(float distance)
        {
            float Amin = 0.0f;
            float Bmin = 0.5f;
            float Bmax = 1.3f;


            float calibration = Bmin + 0.8f * (distance / 60);
            return calibration;
        }

    }
}
