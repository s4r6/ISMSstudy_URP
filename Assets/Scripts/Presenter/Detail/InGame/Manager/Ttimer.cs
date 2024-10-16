using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ISMS.Presenter.Detail.Manager
{
    public class Timer
    {
         ReactiveProperty<float> _time = new FloatReactiveProperty();
        public IReadOnlyReactiveProperty<float> Time => _time;

        public Timer(float time = default)
        {
            if (time < 0)
                Debug.LogError("不正な開始時間です");

            _time.Value = time;
        }

        public void StartTimer()
        {

        }

        public void StopTimer()
        {

        }
    }
}
