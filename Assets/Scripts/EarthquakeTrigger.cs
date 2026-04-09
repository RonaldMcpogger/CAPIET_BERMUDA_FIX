using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EarthquakeTrigger : MonoBehaviour
{
    [SerializeField] public GameObject trigger;
    [SerializeField] private GameObject player;

    [SerializeField] bool start = false;

    [SerializeField] bool repeat = false; //whether to repeat or if it just a one time thing
    [SerializeField] bool decrease = false; //whether to decrease the strength of the quakes each cycle
    [SerializeField] bool up = true; //timer ticking up (timer++) 
    [SerializeField] bool once = false;

    [SerializeField] float timerMax = 2; //max timer
    [SerializeField] float delayFactor = 2; //how long the delay until the next set of vibrations compared to the length of the quake
    [SerializeField] float timer = 0; //value to be incremented by delta time (DO NOT CHANGE THIS!)
    [SerializeField] float intensity = 2.0f; //intensity of the screen shake
    [SerializeField] float motorIntensity = 0.4f; //high frequency of the controller vibration

    // Start is called before the first frame update
    void Start()
    {
        start = false;
        player = GameObject.FindGameObjectWithTag("Player");
        trigger.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!start && trigger.GetComponent<Collider>().bounds.Intersects(player.GetComponent<Collider>().bounds))
        {
            start = true;
            trigger.SetActive(false);
        }

        if (start)
        {
            if (!once) //At the Start, enable vibrations and screenshake.
            {
                if (Gamepad.current != null)
                    Gamepad.current.SetMotorSpeeds(0, motorIntensity);

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
                    timer *= delayFactor;
                }
            }
            else if (!up)//when timer is up, the timer goes again but this time as a cooldown, and restarts the screenshakes when everything is over.
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    if (!repeat)
                        start = false;

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
}
