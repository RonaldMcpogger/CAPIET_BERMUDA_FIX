using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterUpdate : MonoBehaviour
{
    // Start is called before the first frame update

   public enum MeterType
    {
        OXYGEN,
        BATTERY,
        HEALTH,
        NULL
    }

    public MeterType meterType = MeterType.NULL;
    public Image bar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(meterType == MeterType.OXYGEN)
        {
            bar.fillAmount = ItemManager.Instance.getOxygen() / 100f;

        }
        else if (meterType == MeterType.BATTERY)
        {
            bar.fillAmount = ItemManager.Instance.getBattLife() / 100f;

        }
      
    }
}
