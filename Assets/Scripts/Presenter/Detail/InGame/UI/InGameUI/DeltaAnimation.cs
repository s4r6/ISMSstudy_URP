using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ISMS.Presenter.Detail
{
    /// <summary>
    /// ���������\��UI�̍��E�ɕ\������遤�̃A�j���[�V������ݒ�
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

            anim = this.gameObject.transform.DOLocalMoveX(MoveEndPos, MoveTime) //���E�ɓ����A�j���[�V����
                .SetLoops(-1, LoopType.Restart);
        }

        private void OnDisable()
        {
            anim.Pause();
        }
    }
}
