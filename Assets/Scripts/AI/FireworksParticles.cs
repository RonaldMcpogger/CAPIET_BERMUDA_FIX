using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksParticles : MonoBehaviour
{
    [SerializeField] private bool debug = false;
    [SerializeField] private float damage = 1;

    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Player"))
        {
            if(debug)
            Debug.Log("Player Hit!");
            HealthManager.Instance.takeDamage(damage);
        }
    }
}
