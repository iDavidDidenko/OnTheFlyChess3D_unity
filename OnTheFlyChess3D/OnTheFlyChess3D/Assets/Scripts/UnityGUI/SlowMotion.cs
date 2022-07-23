using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    
    void Start()
    {
        Time.timeScale = 0.001f;
        Debug.Log("hii");
    }


}
