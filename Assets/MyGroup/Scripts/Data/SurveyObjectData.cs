using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/SurveyObjectData")]
public class SurveyObjectData : ScriptableObject
{   
    [SerializeField]
    string ObjectName;
    [SerializeField, TextAreaAttribute(2,5)]
    string Describe;
    [SerializeField]
    int RiskFlag;
}
