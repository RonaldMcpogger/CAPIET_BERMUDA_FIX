using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeapouchAI : MonoBehaviour
{
    public GameObject player;
    public GameObject pullRadiusSphere;
    public GameObject deathRadiusSphere;
    public GameObject particleSystem;
    public CharacterController characterController;

    public float pullStrength = 5;
    public float maxPullTime = 5;
    public float pullTime = 0;
    public float extraRestTime = 3.5f;
    public bool cooldown = false;
    // Start is called before the first frame update
    void Start()
    {
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

            //Put Vibration Stuff Here:

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
            }
        }


    }

}
