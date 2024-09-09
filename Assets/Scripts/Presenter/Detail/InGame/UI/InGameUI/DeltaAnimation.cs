using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ISMS.Presenter.Detail
{
    /// <summary>
    /// 説明資料表示UIの左右に表示される▽のアニメーションを設定
    /// </summary>
    public class DeltaAnimation : MonoBehaviour
    {
        [SerializeField]
        float MoveTime = 0.3f;
        [SerializeField]
        float MoveEndPos = 460;

        Tweener anim;
        Vector3 startPos;

        void Awake()
        {
            startPos = transform.localPosition;    
        }
        void OnEnable()
        {
            transform.localPosition = startPos;

            anim = this.gameObject.transform.DOLocalMoveX(MoveEndPos, MoveTime) //左右に動くアニメーション
                .SetLoops(-1, LoopType.Restart);
        }

        private void OnDisable()
        {
            anim.Pause();
        }
    }
}
