using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISMS.Data
{
    public class Explanation
    {
        public readonly string _explanation;

        public Explanation(string explanation)
        {
            if (explanation == null)
                Debug.LogError("�}�X�^�f�[�^�ɉ�����݂�܂���");

            _explanation = explanation;
        }
    }
}

