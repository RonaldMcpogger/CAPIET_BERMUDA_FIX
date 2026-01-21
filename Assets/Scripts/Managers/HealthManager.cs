using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    // Vignette UI
    [SerializeField] Image Vignette;
    [SerializeField]private float Health = 100f;
    [SerializeField]private bool isOutofO2 = false;
    private Color vec4 = new Color(0f, 0f, 0f, 0f);
    private bool cooldownDmg = false;
    private bool cooldownHeal = false;
    HealthStatus currentHealthStatus;
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
            currentHealthStatus = HealthStatus.Healthy;
            Vignette.color = Color.Lerp(Vignette.color, new Color(0f, 0f, 0f, 0.0f), Time.deltaTime*2);
        }
        else if (health <= 70 && health > 50f)
        {
            currentHealthStatus = HealthStatus.Injured1;
            Vignette.color = Color.Lerp(Vignette.color, new Color(0f, 0f, 0f, 0.50f), Time.deltaTime * 2);
        }
        else if(health <= 50 && health > 30f)
        {
            currentHealthStatus = HealthStatus.Injured1;
            Vignette.color = Color.Lerp(Vignette.color, new Color(0f, 0f, 0f, 0.70f), Time.deltaTime*2);
        }
        else if(health <= 30f)
        {
            currentHealthStatus = HealthStatus.Critical;
            Vignette.color = Color.Lerp(Vignette.color, new Color(0f, 0f, 0f, 0.95f), Time.deltaTime * 2);
        }
    }

    //Managers
    public static HealthManager Instance { get; private set; }
    // Update is called once per frame

    void Start()
    {
        
    }
    void Update()
    {
        if(cooldownDmg == false && isOutofO2)
        {
            StartCoroutine(DamageTimer());
        }
        else if(cooldownDmg == false && !isOutofO2)
        {
            StopCoroutine(DamageTimer());
            Debug.Log("02 recovirng");
         
                Health = 100f;
            
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

            

            takeDamage(10f);
        GlobalScreenShake.Instance.TriggerShake(0.3f, 0.3f);

        cooldownDmg = false;
    }
  
}
