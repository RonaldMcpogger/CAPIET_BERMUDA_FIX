using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSeapouch : MonoBehaviour
{
    [SerializeField] private GameObject seapouchEnemy;
    [SerializeField] private GameObject spawnTrigger;
    [SerializeField] private GameObject despawnTrigger;
    [SerializeField] private GameObject player;

    bool spawned = false;


    // Start is called before the first frame update
    void Start()
    {
        seapouchEnemy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTrigger.GetComponent<Collider>().bounds.Intersects(player.GetComponent<Collider>().bounds))
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
