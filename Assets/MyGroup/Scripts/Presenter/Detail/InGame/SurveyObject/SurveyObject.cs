using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISMS.Presenter.Detail.Stage
{
    public class SurveyObject : BaseSurveyObject
    {
        public string Name => _name;
        public string Discribe => _discribe;
        public bool Risk => _risk;

        private void Awake()
        {
            _name = this.gameObject.name;
        }
        private void Start()
        {
            GetSelfInfo();
        }
    }
}
