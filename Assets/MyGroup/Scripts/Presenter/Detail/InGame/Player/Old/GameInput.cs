using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput instance;
    /*[SerializeField]
    private Camera CameraMain = null;

    [SerializeField]
    private Transform CameraL = null;

    [SerializeField]
    private Transform CameraR = null;

    [SerializeField]
    private Transform CameraFollower = null;

    [SerializeField]
    private Transform WebCameraSet = null;

    [SerializeField]
    private Transform DebugWindow = null;
    */

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        InputInit();
    }

    private void Update()
    {
        Debug.Log("�C���v�b�gnow");
        InputUpdate();
    }
    /*** ���͎擾�ϐ� **********************************************************************************/

    /* �ȉ��@InputSystem�p�̕ϐ� ***/
    public InputAction _MoveAction, _LookAction, _DecisionAction,
        _BackAction, _RiskAction, _GimicAction, _ArchivesAction,
        _JujiKeyAction, _AnyAction, _CameraResetAction;

    public InputAction _L2Action, _R2Action, _R1Action, _DebugMode;
    public Vector2 moveInput, lookInput, JujiKeyInput;

    public float rightButtonframe;  //�\���L�[�̉E��������Ă���t���[����
    public float leftButtonframe;  //�\���L�[����������Ă���t���[����

    public bool debugModeFlag = false;
    [SerializeField] public float turnSpeed = (float)120;
    private void InputInit()
    {
        var pInput = GetComponent<PlayerInput>();
        //���݂̃A�N�V�����}�b�v���擾�B
        //������Ԃ�PlayerInput�R���|�[�l���g��inspector��DefaultMap
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        _MoveAction = actionMap["Move"];
        _LookAction = actionMap["Look"];
        _DecisionAction = actionMap["Decision"];
        _BackAction = actionMap["Back"];
        _RiskAction = actionMap["Risk"];
        _ArchivesAction = actionMap["Archives"];
        _GimicAction = actionMap["Gimic"];
        _JujiKeyAction = actionMap["JujiKey"];
        _AnyAction = actionMap["Any"];
        _CameraResetAction = actionMap["CameraReset"];
        _L2Action = actionMap["L2Action"];
        _R2Action = actionMap["R2Action"];
        _R1Action = actionMap["R1Action"];
        _DebugMode = actionMap["DebugMode"];

        debugModeFlag = false;

    }

    //���͌n�̍X�V
    void InputUpdate()
    {
        //���t���[���e���͂̒l��ǂݎ��
        moveInput = _MoveAction.ReadValue<Vector2>();
        lookInput = _LookAction.ReadValue<Vector2>();
        JujiKeyInput = _JujiKeyAction.ReadValue<Vector2>();
    }



    public bool L2Pressed()
    {
        return _L2Action.IsPressed();
    }

    public bool R2Pressed()
    {
        return _R2Action.IsPressed();
    }

    public bool R1Triggered()
    {
        return _R1Action.triggered;
    }

    public bool GetDebugMode()
    {
        return debugModeFlag;
    }
}
