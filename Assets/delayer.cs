using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delayer:MonoBehaviour
{
    private void Start() 
    {
        Destroy(this.gameObject,2.0f);
    }
}
