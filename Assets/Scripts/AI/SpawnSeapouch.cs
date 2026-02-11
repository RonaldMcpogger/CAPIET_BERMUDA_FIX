using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSeapouch : MonoBehaviour
{
    [SerializeField] private GameObject seapouchEnemy;
    [SerializeField] private GameObject spawnTrigger;
    [SerializeField] private GameObject despawnTrigger;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject coordinates;

    [SerializeField] private bool special = false;

    bool spawned = false;


    // Start is called before the first frame update
    void Start()
    {
        seapouchEnemy.SetActive(false);
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (special && coordinates.activeSelf == false)
            special = false;

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
