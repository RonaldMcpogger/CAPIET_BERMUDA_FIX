using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.InputSystem;

public class Heartbeat : MonoBehaviour
{
    int baseBPM = 60;
    float BPM;
    float timeSinceLastBeat;
    // Start is called before the first frame update
    void Start()
    {
        setBPM();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastBeat += Time.deltaTime;
        if (ItemManager.Instance.getOxygen() > 30 && ItemManager.Instance.getOxygen() < 60)
        {
            setBPM();
        }
        else if (ItemManager.Instance.getOxygen() <= 30)
        {
            setBPM();
        }
        if (timeSinceLastBeat > (60 / BPM))
        {
            timeSinceLastBeat = 0;
            StartCoroutine(beat());
        }
    }
    
    void setBPM()
    {                   //2 - (current/max)
        BPM = baseBPM * (2.0f - (ItemManager.Instance.getOxygen() / 100f));
    }
    private IEnumerator beat()
    {
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(1.8f, 0.5f);
            Gamepad.current.SetMotorSpeeds(0, 0);
            yield return new WaitForSeconds(0.17f);
            Gamepad.current.SetMotorSpeeds(0.4f, 1.8f);
            Gamepad.current.SetMotorSpeeds(0, 0);
        }
    }
}
