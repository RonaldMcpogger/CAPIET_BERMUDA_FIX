using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnSpotlight : MonoBehaviour
{
    bool once = true;
    bool twice = true;
    [SerializeField] GameObject spotlightPrefab;
    [SerializeField] GameObject playerObject;
    [SerializeField] Collider despawnArea;
    // Start is called before the first frame update
    void Start()
    {
        spotlightPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Coords") == false && once && twice)
        {
            Debug.Log("Spawn!");
            once = false;
            spotlightPrefab.SetActive(true);
        }

        if(!once && twice)
        {
            if (despawnArea.bounds.Intersects(playerObject.GetComponent<Collider>().bounds))
            {
                spotlightPrefab.SetActive(false);
                twice = false;
            }
        }

    }
}
