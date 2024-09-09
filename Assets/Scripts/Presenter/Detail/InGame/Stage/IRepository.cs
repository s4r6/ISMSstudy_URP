using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISMS.Data;
using Cysharp.Threading.Tasks;

namespace ISMS.Presenter.Detail.Stage
{
    /// <summary>
    /// ステージデータの取得部分だけを外部に公開
    /// </summary>
    public interface IRepository
    {
        UniTask GetStageData();
        UniTask<ObjectDictionary> GetObjectData(int StageNum);
    }
}
