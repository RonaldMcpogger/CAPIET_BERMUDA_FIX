using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireworksFlyover : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    [SerializeField] bool decrease = true;
    [SerializeField] bool up = true;
    [SerializeField] bool once = false;
    [SerializeField] float timerMax = 10;
    [SerializeField] float timer = 0;
    [SerializeField] float intensity = 2.0f;
    [SerializeField] float motorIntensity = 0.4f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.forward * Time.deltaTime * speed;
        
        if (!once) //At the Start, enable vibrations and screenshake.
        {
            if (Gamepad.current != null)
                Gamepad.current.SetMotorSpeeds(0, 0.4f);

            GlobalScreenShake.Instance.TriggerShake(timerMax, intensity);
            once = true;
        }

        if (up && timer <= timerMax) //Continues screenshake and vibrations until timer is up
        {
            timer += Time.deltaTime;
            if (timer > timerMax)
            {
                Gamepad.current.SetMotorSpeeds(0, 0.0f);
                up = false;
            }
        }
        else if (!up)//when timer is up, the timer goes again but this time as a cooldown, and restarts the screenshakes when everything is over.
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0.0f;
                up = true;
                once = false;

                if (decrease)// halves the intensity of the screenshake and vibrations each loop for the first fireworks monster
                {
                    intensity /= 2;
                    motorIntensity /= 2;
                }

            }
        }

    }
}
