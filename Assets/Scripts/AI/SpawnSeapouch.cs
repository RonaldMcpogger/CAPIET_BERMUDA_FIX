using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnSeapouch : MonoBehaviour
{
    [SerializeField] private GameObject seapouchEnemy;
    [SerializeField] private GameObject spawnTrigger;
    [SerializeField] private GameObject despawnTrigger;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject coordinates;

    [SerializeField] private bool special = false;

    bool spawned = false;
    bool once = false;
    [SerializeField] float timerMax = 3;
    [SerializeField] float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        seapouchEnemy.SetActive(false);
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (special && coordinates.activeSelf == false && !once) //Shakes the Screen and Starts Controller Vibrations when the Coordinates are picked up
        {
            once = true;

            if (Gamepad.current != null)
                Gamepad.current.SetMotorSpeeds(0, 0.4f);

            GlobalScreenShake.Instance.TriggerShake(3.0f, 3.0f); //3 Second Duration, so it syncs with the vibrations
            special = false;
        }

        if(once && timer <= timerMax) //Stops Controller Vibration after the 3 second timer is up
        {
            timer += Time.deltaTime;
            if (timer > timerMax) Gamepad.current.SetMotorSpeeds(0, 0.4f); 
        }
            

        if (!special && !spawned && spawnTrigger.GetComponent<Collider>().bounds.Intersects(player.GetComponent<Collider>().bounds))
        {
            seapouchEnemy.SetActive(true);
            spawned = true;
        }

        if (spawned && despawnTrigger.GetComponent<Collider>().bounds.Intersects(player.GetComponent<Collider>().bounds))
        {
            seapouchEnemy.SetActive(false);
        }

    }
}
