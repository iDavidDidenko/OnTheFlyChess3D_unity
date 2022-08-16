using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    private float fixedDeltaTime;
    void Awake()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = 0.03f;
        
    }

    void Update()
    {
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        Debug.Log("hii");
    }


}
