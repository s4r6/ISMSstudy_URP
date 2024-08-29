using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISMS.Data
{
    public class Name
    {
        public readonly string _systemName;
        public readonly string _objName;

        public Name(string system, string obj)
        {
            if (system == null || obj == null)
                Debug.LogError("�}�X�^�f�[�^�̃V�X�e���l�[���܂��̓I�u�W�F�N�g�l�[�����݂�܂���");

            _systemName = system;
            _objName = obj;
        }
    }
}

