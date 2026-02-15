using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpotlightAI : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject spotLightRadius;
    [SerializeField] Image targetImage;
    [SerializeField] GameObject deathScreen;

    [SerializeField] float detectionRange = 10f;
    [SerializeField] float speed = 1.32f;


    [SerializeField] float deathTimer = 3f;
    float currentDeathTimer = 0.0f;

    [SerializeField] float idleTimer = 10f;
    float currentIdleTimer = 0.0f;
    bool chase = false;

    // Start is called before the first frame update
    void Start()
    {
        this.playerObject = GameObject.FindWithTag("Player");
        this.deathScreen.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject != null) 
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);

            if(distanceToPlayer <= detectionRange) //when player is in detection range
            {
                chase = false;
                currentIdleTimer = 0f;

                Vector3 direction = transform.position - playerObject.transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1).eulerAngles;
                transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

                if (this.playerObject.GetComponent<Collider>().bounds.Intersects(this.spotLightRadius.GetComponent<Collider>().bounds))
                {

                    if (currentDeathTimer < deathTimer * 0.75f)
                        this.transform.position -= this.transform.forward * Time.deltaTime * speed / 2;

                    playerObject.GetComponent<Movement>().speed = 0.66f;
                    currentDeathTimer += Time.deltaTime;
                    targetImage.GetComponent<Image>().color = new Color(1f, 0f, 0f, Mathf.Clamp01(currentDeathTimer / deathTimer));
                    if (currentDeathTimer >= deathTimer)
                    {
                        Debug.Log("Player Killed by Spotlight"); //Insert Death Screen Here
                        this.deathScreen.active = true;
                        Gamepad.current.SetMotorSpeeds(0f, 2f);
                        Gamepad.current.SetMotorSpeeds(0, 0);
                    }
                }
                else
                {
                    playerObject.GetComponent<Movement>().speed = playerObject.GetComponent<Movement>().defaultSpeed;
                    if (currentDeathTimer > 0f)
                    {
                        currentDeathTimer -= Time.deltaTime;
                        if(currentDeathTimer < 0f)
                        {
                            currentDeathTimer = 0f;
                        }

                        targetImage.GetComponent<Image>().color = new Color(1f, 0f, 0f, Mathf.Clamp01(currentDeathTimer / deathTimer));
                    }

                        this.transform.position -= this.transform.forward * Time.deltaTime * speed;


                }
            }
            else
            {
                if(chase) //moves towards player's current position
                {
                    Vector3 direction = transform.position - playerObject.transform.position;
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1).eulerAngles;
                    transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

                    this.transform.position -= this.transform.forward * Time.deltaTime * speed/2;

                    currentIdleTimer -= Time.deltaTime;
                    if (currentIdleTimer < 0)
                    {
                        chase = false;
                        currentIdleTimer = 0f;
                    }
                }
                else
                {
                    currentIdleTimer += Time.deltaTime;
                    if (currentIdleTimer >= idleTimer)
                    {
                        chase = true;
                    }
                }

            }
        }
    }
}
