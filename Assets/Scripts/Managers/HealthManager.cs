using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    // Vignette UI
    [SerializeField] Image Vignette;
    [SerializeField]private float Health = 100f;
    [SerializeField]private bool isOutofO2 = false;
   
    private bool cooldownDmg = false;
    private bool cooldownHeal = false;
    enum  HealthStatus
    {
        Healthy,
        Injured1,
        Injured2,
        Critical
    }


   private void ConditionHealth(float health)
    {
        if(health > 70f)
        {
            Vignette.color = Color.Lerp(Vignette.color, new Color(Vignette.color.r, Vignette.color.g, Vignette.color.b, 0.0f), Time.deltaTime*2);
        }
        else if (health <= 70 && health > 50f)
        {
            Vignette.color = Color.Lerp(Vignette.color, new Color(Vignette.color.r, Vignette.color.g,Vignette.color.b, 0.10f), Time.deltaTime * 2);
        }
        else if(health <= 50 && health > 30f)
        {
            Vignette.color = Color.Lerp(Vignette.color, new Color(Vignette.color.r, Vignette.color.g, Vignette.color.b, 0.20f), Time.deltaTime*2);
        }
        else if(health <= 30f)
        {
            Vignette.color = Color.Lerp(Vignette.color, new Color(Vignette.color.r, Vignette.color.g, Vignette.color.b, 0.95f), Time.deltaTime * 2);
        }
    }

    //Managers
    public static HealthManager Instance { get; private set; }
    // Update is called once per frame

    void Start()
    {
        if(Vignette == null)
        {
            Vignette = GameObject.Find("HealthVignette").GetComponent<Image>();
            Vignette.color = new Color(Vignette.color.r, Vignette.color.g, Vignette.color.b, 0.0f);
        }
           
    }
    public void die()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        GameObject fader = GameObject.FindGameObjectWithTag("Player");
      

        WorldTransportManager.Instance.setDiedScene(SceneManager.GetActiveScene().name);

        //  SceneManager.LoadScene("DeathMenu");
        fader.GetComponentInChildren<HitboxUI>().startFade("DeathMenu");
        Health = 100;
        ItemManager.Instance.rechargeAll(100);
    }
    void Update()
    {
        if(Health <= 0)
        {
       
            die();

        }

        if (Vignette == null)
        {
            Vignette = GameObject.Find("HealthVignette").GetComponent<Image>();
        }

        if (cooldownDmg == false && isOutofO2)
        {
            if (ItemManager.Instance.inSub == false)
            {
                StartCoroutine(DamageTimer());
            }
            else
            {
                StopCoroutine(DamageTimer());
                if (Health < 100)
                {
                    Health += 1f * Time.deltaTime;
                }
            }
        }
        else if (cooldownDmg == false && !isOutofO2)
        {
            StopCoroutine(DamageTimer());



            if (Health < 100)
            {
                Health += 1f * Time.deltaTime;
            }

        }
        ConditionHealth(this.Health);
    }

    public void takeDamage(float damage)
    {
        Health -= damage;
      
    }

    private void Awake()
    {

        // If an instance already exists and it's not this one, destroy this duplicate.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // Otherwise, set this as the instance.
            Instance = this;
            // Optionally, make the object persistent across scene loads.
            DontDestroyOnLoad(this);
        }
    }

    public void setO2Status(bool status)
    {
 

        isOutofO2 = status;

      
    }


    IEnumerator DamageTimer()
    {

       
        cooldownDmg = true;

        yield return new WaitForSeconds(3);

            

        GlobalScreenShake.Instance.TriggerShake(0.3f, 0.3f);
        takeDamage(15f);

        cooldownDmg = false;
    }

   

}
