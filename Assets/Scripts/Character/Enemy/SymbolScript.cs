using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolScript : MonoBehaviour
{

    public Transform Cam;


    // Update is called once per frame
    void LateUpdate()
    {
      transform.LookAt(Cam); 
    }
}
