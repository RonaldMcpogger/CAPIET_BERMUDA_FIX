using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    float target1, target2;
    // Start is called before the first frame update
    void Start()
    {
        target1 = -.3f;
        target2 = .3f;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(0,1,0);
        
    }
}
