using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayAllChildObject : MonoBehaviour
{
    void DisplayChildObject()
    {
        foreach(Transform child in this.gameObject.transform)
        {
            Debug.Log(child.name);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            DisplayChildObject();
        }
    }
}
