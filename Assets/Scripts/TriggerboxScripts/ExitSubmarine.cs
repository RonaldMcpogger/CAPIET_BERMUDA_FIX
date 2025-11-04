using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSubmarine : MonoBehaviour, HitboxScript
{
    [SerializeField] GameObject player;
    bool activated;

    void Awake()
    {
        activated = false;

    }

    public void entered()
    {
        player.GetComponentInChildren<HitboxUI>().setUIActive("Exit Ship");
    }
    public void exited()
    {
        player.GetComponentInChildren<HitboxUI>().setUIInactive();
    }
    public void interact()
    {
        if (activated == false)
        {
            Debug.Log("Interact");
            activated = true;
            player.GetComponentInChildren<HitboxUI>().startFade("SeafloorTest");

        }
    }
}
