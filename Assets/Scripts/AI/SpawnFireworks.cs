using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFireworks : MonoBehaviour
{
    [SerializeField] private GameObject fireworksEnemy;
    [SerializeField] private GameObject coordinates;


    // Start is called before the first frame update
    void Start()
    {
        fireworksEnemy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (coordinates.activeSelf == false)
            fireworksEnemy.SetActive(true);
    }
}
