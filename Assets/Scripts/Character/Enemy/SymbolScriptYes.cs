using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolScriptYes : MonoBehaviour
{

    [SerializeField] Transform Camera;

    void Update()
    {
        transform.LookAt(Camera);
    }
}
