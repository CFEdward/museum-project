using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private int collectiblesCollected = 0;

    public int Collectibles
    { get { return collectiblesCollected; } 
      set { collectiblesCollected = value; 
            Debug.LogFormat("Collectibles: {0}", collectiblesCollected);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
