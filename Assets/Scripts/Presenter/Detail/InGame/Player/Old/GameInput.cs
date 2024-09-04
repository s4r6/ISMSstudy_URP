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
        Debug.Log("インプットnow");
        InputUpdate();
    }
    /*** 入力取得変数 **********************************************************************************/

    /* 以下　InputSystem用の変数 ***/
    public InputAction _MoveAction, _LookAction, _DecisionAction,
        _BackAction, _RiskAction, _GimicAction, _ArchivesAction,
        _JujiKeyAction, _AnyAction, _CameraResetAction;

    public InputAction _L2Action, _R2Action, _R1Action, _DebugMode;
    public Vector2 moveInput, lookInput, JujiKeyInput;

    public float rightButtonframe;  //十字キーの右が押されているフレーム数
    public float leftButtonframe;  //十字キー左が押されているフレーム数

    public bool debugModeFlag = false;
    [SerializeField] public float turnSpeed = (float)120;
    private void InputInit()
    {
        var pInput = GetComponent<PlayerInput>();
        //現在のアクションマップを取得。
        //初期状態はPlayerInputコンポーネントのinspectorのDefaultMap
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
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

    //入力系の更新
    void InputUpdate()
    {
        //毎フレーム各入力の値を読み取る
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
