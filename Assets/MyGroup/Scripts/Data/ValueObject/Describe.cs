using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISMS.Data
{
    public class Describe
    {
        public readonly string _describe;

        public Describe(string describe)
        {
            if (describe == null)
                Debug.LogError("�}�X�^�f�[�^�̐��������݂�܂���");

            _describe = describe;
        }
    }
}

