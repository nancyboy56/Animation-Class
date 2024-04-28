using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    [SerializeField]
    private int rate = 15;
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = rate;
    }

    
}
