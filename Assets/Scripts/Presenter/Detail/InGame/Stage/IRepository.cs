using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISMS.Data;
using Cysharp.Threading.Tasks;

namespace ISMS.Presenter.Detail.Stage
{
    /// <summary>
    /// �X�e�[�W�f�[�^�̎擾�����������O���Ɍ��J
    /// </summary>
    public interface IRepository
    {
        UniTask GetStageData();
        UniTask<ObjectDictionary> GetObjectData(int StageNum);
    }
}
