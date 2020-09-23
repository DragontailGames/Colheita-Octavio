using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBird : MonoBehaviour
{
    public float velocityZ;
    public AnimationCurve myCurve;

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * velocityZ;
        transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)), transform.position.z);
        
    }
}
 

