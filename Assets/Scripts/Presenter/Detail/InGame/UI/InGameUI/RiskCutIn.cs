using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

namespace ISMS.Presenter.Detail.UI
{
    /// <summary>
    /// リスク発見時に表示するカットインを管理するクラス
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
            _dengerMarkStartPos = _dengerMark.transform.localPosition;  //危険マークの最初の位置を保存
            _textStartPos = _discoverText.transform.localPosition;  //リスク発見テキストの最初の位置を保存
            _loupeStartPos = _loupeMark.transform.localPosition;    //ルーペの最初の位置を保存

            _loupeMarkAnimation = DOTween.Sequence()    //ルーペの移動アニメーションの後に、危険マークと共に移動するアニメーションを行う
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

            _dengerMarkAnimation = DOTween.Sequence()   //危険マークを拡大・縮小するアニメーション
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

        void InitPos()  //位置の初期化
        {
            _dengerMark.transform.localPosition = _dengerMarkStartPos;
            _discoverText.transform.localPosition = _textStartPos;
            _loupeMark.transform.localPosition = _loupeStartPos;
        }

        void Update()   //背景をスライドする
        {
            if (_material != null)
            {
                var x = Mathf.Repeat(Time.time * _offsetSpeed.x, MAX_OFFSET);
                var y = Mathf.Repeat(Time.time * _offsetSpeed.y, MAX_OFFSET);
                var offset = new Vector2(x, y);
                _material.SetTextureOffset(PROPERTY_NAME, offset);
            }
        }

        //カットインアニメーションを行う
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
