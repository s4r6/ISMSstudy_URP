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
        //�C���X�^���X�̏���
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

        /*this.gameObject.transform.localScale = new Vector3(0, 0, 0);    //�T�C�Y������
        LeftPos = DeltaLeft.localPosition;
        RightPos = DeltaRight.localPosition;*/
        //this.gameObject.SetActive(false);
        

    }
    public IEnumerator DisplayDocumentWindow()   //�E�B���h�E�̕\��
    {
        this.gameObject.SetActive(true);    //�E�B���h�E�L����
        StartCoroutine(DisplayWindow.AppearAnimation(this.gameObject)); //�E�B���h�E�����񂾂�傫���\������
        //DeltaAnimation();   //�h�L�������g�E�B���h�E�̖���\�� 
        yield return null;
    }

    public IEnumerator HiddenDocmentWindow()
    {
        IEnumerator DisAppear = DisplayWindow.HiddenAnimation(this.gameObject); //�E�B���h�E�\������\���ɂȂ�܂őҋ@
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

        /* ���X�e�B�b�N���\���L�[�̍��E�Ńy�[�W����B****************/

        if (0 < GameInput.instance.JujiKeyInput.x || 0 < GameInput.instance.moveInput.x)
        {
            GameInput.instance.rightButtonframe++;
            if (GameInput.instance.rightButtonframe % 60 == 1)//�������ɂ��Ή��ł���悤�Ȋ����ŁB1�b��1�񓮂��B
            {   //�E����
                PageChange(MOVERIGHT);

                AudioManager.instance.playSE(9);
            }

        }
        else if (GameInput.instance.JujiKeyInput.x < 0 || GameInput.instance.moveInput.x < 0)
        {
            GameInput.instance.leftButtonframe++;
            if (GameInput.instance.leftButtonframe % 60 == 1)
            {   //������
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
    /*** �y�[�W�֐��@ **********************************************************************/

    //����true�Ő������Efalse�ŕ��̕����Ƀy�[�W�ړ�
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
    //���E�ɏo����̃A�j���[�V����
    /*private void DeltaAnimation()
    {
        movePoint.x = activeFrameCount % 30;

        DeltaLeft.localPosition = LeftPos + movePoint;
        DeltaRight.localPosition = RightPos - movePoint;
    }
    */
}
