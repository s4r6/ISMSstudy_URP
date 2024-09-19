using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISMS.Presenter.Detail
{
    public class DebugRotation : MonoBehaviour
    {
        [SerializeField]
        GameObject RotationObj;

        // Update is called once per frame
        void Update()
        {
            Debug.Log($"{RotationObj.name}'s Rotation : {RotationObj.transform.rotation} ");
        }
    }
}
