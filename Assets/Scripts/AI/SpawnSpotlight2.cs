using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpotlight2 : MonoBehaviour
{
    [SerializeField] private GameObject spotlightEnemy;
    [SerializeField] private GameObject spawnTrigger;
    [SerializeField] private GameObject despawnTrigger;
    [SerializeField] private GameObject player;
    bool spawned = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spotlightEnemy.SetActive(false);
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!spawned && spawnTrigger.GetComponent<Collider>().bounds.Intersects(player.GetComponent<Collider>().bounds))
        {
            spotlightEnemy.SetActive(true);
            spawned = true;
        }

        if (spawned && despawnTrigger.GetComponent<Collider>().bounds.Intersects(player.GetComponent<Collider>().bounds))
        {
            spotlightEnemy.SetActive(false);
        }

    }
}
