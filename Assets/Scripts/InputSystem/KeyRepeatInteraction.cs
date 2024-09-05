using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class KeyRepeatInteraction : IInputInteraction
{
    // �{�^�����ŏ��ɉ�����Ă��烊�s�[�g�������n�܂�܂ł̎���[s]
    public float repeatDelay = 1f;

    // �{�^����������Ă���Ԃ̃��s�[�g�����̊Ԋu[s]
    public float repeatInterval = 0.3f;

    // �{�^����臒l�i0�̏ꍇ�̓f�t�H���g�ݒ�l���g�p�j
    public float pressPoint = 0;

    // �ݒ�l���f�t�H���g�l�̒l���i�[����t�B�[���h
    float PressPointOrDefault => pressPoint > 0 ? pressPoint : InputSystem.settings.defaultButtonPressPoint;
    float ReleasePointOrDefault => PressPointOrDefault * InputSystem.settings.buttonReleaseThreshold;

    // ���̃��s�[�g����[s]
    double _nextRepeatTime;

#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoadMethod]
#else
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
    public static void Initialize()
    {
        // �����Interaction��o�^����K�v������
        InputSystem.RegisterInteraction<KeyRepeatInteraction>();
    }

    public void Process(ref InputInteractionContext context)
    {
        // �ݒ�l�̃`�F�b�N
        if (repeatDelay <= 0 || repeatInterval <= 0)
        {
            Debug.LogError("initialDelay��repeatInterval��0���傫���l��ݒ肵�Ă��������B");
            return;
        }

        if (context.timerHasExpired)
        {
            // ���s�[�g�����ɒB������Ă�Performed�ɑJ��
            if (context.time >= _nextRepeatTime)
            {
                // ���s�[�g�����̎�����s������ݒ�
                _nextRepeatTime = context.time + repeatInterval;

                // ���s�[�g���̏���
                context.PerformedAndStayPerformed();

                // ���̃��s�[�g������Process���\�b�h���Ă΂��悤�Ƀ^�C���A�E�g��ݒ�
                context.SetTimeout(repeatInterval);
            }

            return;
        }

        switch (context.phase)
        {
            case InputActionPhase.Waiting:
                // �{�^���������ꂽ��Started�ɑJ��
                if (context.ControlIsActuated(PressPointOrDefault))
                {
                    // �{�^���������ꂽ���̏���
                    context.Started();
                    context.PerformedAndStayPerformed();

                    // ���s�[�g�����̏�����s������ݒ�
                    _nextRepeatTime = context.time + repeatDelay;

                    // ���̃��s�[�g������Process���\�b�h���Ă΂��悤�Ƀ^�C���A�E�g��ݒ�
                    context.SetTimeout(repeatDelay);
                }

                break;

            case InputActionPhase.Performed:
                // �{�^���������ꂽ��Canceled�ɑJ��
                if (!context.ControlIsActuated(ReleasePointOrDefault))
                {
                    // �{�^���������ꂽ���̏���
                    context.Canceled();
                }

                break;
        }
    }

    public void Reset()
    {
        _nextRepeatTime = 0;
    }
}
