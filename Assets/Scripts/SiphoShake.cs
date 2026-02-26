using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiphoShake : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Collider shakeTrigger;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (shakeTrigger.GetComponent<Collider>().bounds.Intersects(playerObject.GetComponent<Collider>().bounds))
        {
            GlobalScreenShake.Instance.TriggerShake(10, 1);
        }
    }
}
