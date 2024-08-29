using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISMS.Data;
using Cysharp.Threading.Tasks;

namespace ISMS.Presenter.Detail.Stage
{
    public interface IRepository
    {
        
        UniTask<ObjectDictionary> GetObjectData(int StageNum);
    }
}
