using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterSubmarine : MonoBehaviour, HitboxScript
{
    [SerializeField] GameObject player;
    bool activated;

    void Awake()
    {
        activated = false;
    }

    public void entered()
    {
        player.GetComponentInChildren<HitboxUI>().setUIActive("Enter Ship");
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
            ItemManager.Instance.inSub = true; // set the trigger to true meaning player is inside submarine
           player.GetComponentInChildren<HitboxUI>().startFade("SubmarineTest");
      


        }
    }

}
