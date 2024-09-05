using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISMS.Presenter.Detail
{
    public enum SystemCode
    {
        Locked,
        Surveyed,
        FileIOFailed
    }
    public class SystemMessage : MonoBehaviour
    {
        static Dictionary<SystemCode, string> message = new()
        {
            [SystemCode.Locked] = "�����������Ă��܂��B",
            [SystemCode.Surveyed] = "���X�N�����ς݃I�u�W�F�N�g�ł��B",
            [SystemCode.FileIOFailed] = "�t�@�C���o�͂ł��܂���ł����B"
        };
    }
}
