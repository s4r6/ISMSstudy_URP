using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DocsWindow : MonoBehaviour
{
    public static DocsWindow instance;

    [SerializeField]
    private Transform[] Pages;

    private int nowPage = 0;

    private const bool MOVERIGHT = true;
    private const bool MOVELEFT = false;



    // Start is called before the first frame update
    void Start()
    {
        //インスタンスの準備
        if (instance == null)
        {
            instance = this;
        }

        nowPage = 0;
        for(int i=0;i< Pages.Length; i++)
        {
            if (i != nowPage)
            {
                Pages[i].gameObject.SetActive(false);
            }
            
        }

        /*this.gameObject.transform.localScale = new Vector3(0, 0, 0);    //サイズ初期化
        LeftPos = DeltaLeft.localPosition;
        RightPos = DeltaRight.localPosition;*/
        //this.gameObject.SetActive(false);
        

    }
    public IEnumerator DisplayDocumentWindow()   //ウィンドウの表示
    {
        this.gameObject.SetActive(true);    //ウィンドウ有効化
        StartCoroutine(DisplayWindow.AppearAnimation(this.gameObject)); //ウィンドウをだんだん大きく表示する
        //DeltaAnimation();   //ドキュメントウィンドウの矢印を表示 
        yield return null;
    }

    public IEnumerator HiddenDocmentWindow()
    {
        IEnumerator DisAppear = DisplayWindow.HiddenAnimation(this.gameObject); //ウィンドウ表示が非表示になるまで待機
        yield return DisAppear;
        this.gameObject.SetActive(false);
    }

    public bool ActionWindow()
    {
        if (GameInput.instance._BackAction.triggered == true)
        {
            StartCoroutine(HiddenDocmentWindow());
            return true;
        }

        /* 左スティックか十字キーの左右でページ送り。****************/

        if (0 < GameInput.instance.JujiKeyInput.x || 0 < GameInput.instance.moveInput.x)
        {
            GameInput.instance.rightButtonframe++;
            if (GameInput.instance.rightButtonframe % 60 == 1)//長押しにも対応できるような感じで。1秒に1回動く。
            {   //右入力
                PageChange(MOVERIGHT);

                AudioManager.instance.playSE(9);
            }

        }
        else if (GameInput.instance.JujiKeyInput.x < 0 || GameInput.instance.moveInput.x < 0)
        {
            GameInput.instance.leftButtonframe++;
            if (GameInput.instance.leftButtonframe % 60 == 1)
            {   //左入力
                PageChange(MOVELEFT);

                AudioManager.instance.playSE(9);
            }
        }
        else
        {
            GameInput.instance.rightButtonframe = 0; GameInput.instance.leftButtonframe = 0;
        }
        return false;
    }
    /*** ページ関数　 **********************************************************************/

    //引数trueで正方向・falseで負の方向にページ移動
    public void PageChange(bool dirFlag)
    {
        int nextPage = nowPage;


        if (dirFlag == MOVERIGHT)
        {
            nextPage++;
            if(nextPage> Pages.Length-1)
            {
                nextPage = 0;
            }
        }
        else
        {
            nextPage--;
            if (nextPage < 0)
            {
                nextPage = Pages.Length - 1;
            }
        }
        Pages[nowPage].gameObject.SetActive(false);
        Pages[nextPage].gameObject.SetActive(true);

        nowPage = nextPage;

    }


    /*[SerializeField]
    private Transform DeltaLeft, DeltaRight;
    private Vector3 LeftPos, RightPos, movePoint;
    */
    //左右に出る矢印のアニメーション
    /*private void DeltaAnimation()
    {
        movePoint.x = activeFrameCount % 30;

        DeltaLeft.localPosition = LeftPos + movePoint;
        DeltaRight.localPosition = RightPos - movePoint;
    }
    */
}
