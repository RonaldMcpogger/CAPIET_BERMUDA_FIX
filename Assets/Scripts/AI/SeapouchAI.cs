using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeapouchAI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pullRadiusSphere;
    [SerializeField] private GameObject deathRadiusSphere;
    [SerializeField] private GameObject particleSystem;
    [SerializeField] private GameObject seaUrchinRadius;
    [SerializeField] private GameObject seaUrchin;
    [SerializeField] private CharacterController characterController;

    [SerializeField] private float pullStrength = 5;
    [SerializeField] private float maxPullTime = 5;
    [SerializeField] private float pullTime = 0;

    [SerializeField] private bool final = false;
    [SerializeField] private float extraRestTime = 3.5f;
    [SerializeField] private bool cooldown = false;

    [SerializeField] private float seaUrchinStuckTime = 5;
    [SerializeField] private float seaUrchinPullStrengthMultiplier = 4;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterController = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Tries to pull the player in
        Pull();

        if(!cooldown) //While the player is being pulled in, have the gulper eel face the player.
        {
            Vector3 direction = transform.position - player.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1).eulerAngles;
            transform.rotation = Quaternion.Euler(-90, 0, 180 + rotation.y);
        }
        else //disable the particle system if it isn't sucking in the player
            particleSystem.SetActive(false);

        if (player.GetComponent<Collider>().bounds.Intersects(deathRadiusSphere.GetComponent<Collider>().bounds))
        {
            Debug.Log("Player Dead!");
            Gamepad.current.SetMotorSpeeds(0f, 2f);
            Gamepad.current.SetMotorSpeeds(0, 0);
        }
        if (final && seaUrchin.GetComponent<Collider>().bounds.Intersects(deathRadiusSphere.GetComponent<Collider>().bounds))
        {
            pullTime = maxPullTime * 2 + extraRestTime;
            cooldown = true;
            seaUrchin.SetActive(false);
            final = false;
        }

    }

    public void Pull()
    {
        if (pullRadiusSphere.GetComponent<Collider>().bounds.Intersects(player.GetComponent<Collider>().bounds) && pullTime < maxPullTime && !cooldown)
        {//IF THE PLAYER IS BEING PULLED IN:
            if (particleSystem.activeSelf == false)
            {
                particleSystem.SetActive(true);
                particleSystem.GetComponent<ParticleReverse>().startPull();
            }

            if (final && pullTime >= seaUrchinStuckTime && seaUrchinRadius.GetComponent<Collider>().bounds.Intersects(seaUrchin.GetComponent<Collider>().bounds))
            {
                seaUrchin.transform.position += (seaUrchinPullStrengthMultiplier * pullStrength * Vector3.Normalize((deathRadiusSphere.transform.position - seaUrchin.transform.position)) * Time.deltaTime);
            }
            //Put Vibration Stuff Here:
            Gamepad.current.SetMotorSpeeds(0, 0.4f * pullTime);

                //Screen Shake
            GlobalScreenShake.Instance.TriggerShake(1.0f, 3.0f);

            //Actual Pulling
            characterController.Move(pullStrength * Vector3.Normalize((deathRadiusSphere.transform.position - player.transform.position)) * Time.deltaTime);

            pullTime += Time.deltaTime;
            if (pullTime >= maxPullTime)
            {
                pullTime += extraRestTime;
                cooldown = true;
            }

        }
        else
        {
            pullTime -= Time.deltaTime;
            if (pullTime <= 0)
            {
                pullTime = 0;
                cooldown = false;

                Gamepad.current.SetMotorSpeeds(0, 0);
            }
        }


    }

}
