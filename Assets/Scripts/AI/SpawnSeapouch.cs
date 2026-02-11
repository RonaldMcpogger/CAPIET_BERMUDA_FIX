using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSeapouch : MonoBehaviour
{
    [SerializeField] private GameObject seapouchEnemy;
    [SerializeField] private GameObject spawnTrigger;
    [SerializeField] private GameObject despawnTrigger;
    [SerializeField] private GameObject player;

    [SerializeField] private float maxTimer = 0;
    [SerializeField] private float currentTimer = 0;

    bool spawned = false;


    // Start is called before the first frame update
    void Start()
    {
        currentTimer = 0;
        seapouchEnemy.SetActive(false);
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= maxTimer && !spawned && spawnTrigger.GetComponent<Collider>().bounds.Intersects(player.GetComponent<Collider>().bounds))
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
