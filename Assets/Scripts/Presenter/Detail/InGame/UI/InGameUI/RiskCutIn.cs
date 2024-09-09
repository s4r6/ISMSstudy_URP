using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

namespace ISMS.Presenter.Detail.UI
{
    /// <summary>
    /// ���X�N�������ɕ\������J�b�g�C�����Ǘ�����N���X
    /// </summary>
    public class RiskCutIn : MonoBehaviour
    {
        const float MAX_OFFSET = 0.62f;
        const string PROPERTY_NAME = "_MainTex";

        [SerializeField]
        Vector2 _offsetSpeed;
        [SerializeField]
        Material _material;

        [SerializeField]
        GameObject _dengerMark;
        [SerializeField]
        GameObject _discoverText;
        [SerializeField]
        GameObject _loupeMark;

        Vector3 _dengerMarkStartPos;
        Vector3 _textStartPos;
        Vector3 _loupeStartPos;

        Sequence _dengerMarkAnimation;
        Sequence _loupeMarkAnimation;

        Subject<Unit> _animationEnd = new Subject<Unit>();
        public ISubject<Unit> OnEndAnimation => _animationEnd;
        void Awake()
        {
            _dengerMarkStartPos = _dengerMark.transform.localPosition;  //�댯�}�[�N�̍ŏ��̈ʒu��ۑ�
            _textStartPos = _discoverText.transform.localPosition;  //���X�N�����e�L�X�g�̍ŏ��̈ʒu��ۑ�
            _loupeStartPos = _loupeMark.transform.localPosition;    //���[�y�̍ŏ��̈ʒu��ۑ�

            _loupeMarkAnimation = DOTween.Sequence()    //���[�y�̈ړ��A�j���[�V�����̌�ɁA�댯�}�[�N�Ƌ��Ɉړ�����A�j���[�V�������s��
                .Append(_loupeMark.transform.DOLocalMoveX(_loupeStartPos.x * -1, 0.3f))
                .Append(_loupeMark.transform.DOLocalMoveX(0, 0.15f))
                .AppendInterval(0.3f)
                .Append(_loupeMark.transform.DOLocalMoveX(_loupeStartPos.x * -1, 0.15f))
                .Join(_dengerMark.transform.DOLocalMoveX(_loupeStartPos.x * -1, 0.15f))
                .OnComplete(() => 
                {
                    this.gameObject.SetActive(false);
                    InitPos();
                    _animationEnd.OnNext(default);
                })
                .SetAutoKill(false)
                .Pause()
                .SetLink(gameObject);

            _dengerMarkAnimation = DOTween.Sequence()   //�댯�}�[�N���g��E�k������A�j���[�V����
                .Append(_dengerMark.transform.DOScale(Vector3.one * 0.3f, 0.3f))
                .Append(_dengerMark.transform.DOScale(Vector3.one * 0.5f, 0.3f))
                .OnComplete(() => _loupeMarkAnimation.Restart())
                .SetAutoKill(false)
                .Pause()
                .SetLink(gameObject);
        }
        void Reset()
        {
            Debug.Log("reset");
            if (TryGetComponent(out Image image))
                _material = image.material;
        }

        void InitPos()  //�ʒu�̏�����
        {
            _dengerMark.transform.localPosition = _dengerMarkStartPos;
            _discoverText.transform.localPosition = _textStartPos;
            _loupeMark.transform.localPosition = _loupeStartPos;
        }

        void Update()   //�w�i���X���C�h����
        {
            if (_material != null)
            {
                var x = Mathf.Repeat(Time.time * _offsetSpeed.x, MAX_OFFSET);
                var y = Mathf.Repeat(Time.time * _offsetSpeed.y, MAX_OFFSET);
                var offset = new Vector2(x, y);
                _material.SetTextureOffset(PROPERTY_NAME, offset);
            }
        }

        //�J�b�g�C���A�j���[�V�������s��
        public void Animation()
        {
            _dengerMark.transform.localScale = Vector3.one * 0.7f;
            this.gameObject.SetActive(true);

            _dengerMarkAnimation.Restart();
        }

        void OnDestroy()
        {
            if (_material != null)
                _material.SetTextureOffset(PROPERTY_NAME, Vector2.zero);
        }
    }
}
