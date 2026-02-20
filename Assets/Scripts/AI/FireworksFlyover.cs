using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksFlyover : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.forward * Time.deltaTime * speed;
    }
}
