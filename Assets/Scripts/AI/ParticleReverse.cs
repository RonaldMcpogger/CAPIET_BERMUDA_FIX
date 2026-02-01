using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleReverse : MonoBehaviour
{
    bool start = false;
    public float defaultTimer = 8;
    public float currentTimer = 0;
    ParticleSystem particleSystem = null;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start || currentTimer - Time.deltaTime <= 0)
        {
            start = false;
            currentTimer = defaultTimer;
        }

        currentTimer -= Time.deltaTime;
        particleSystem.Simulate(currentTimer, false, false);
    }

    public void startPull()
    {
        start = true;
    }

}
