using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScreenShake : MonoBehaviour
{
    
    // Start is called before the first frame update
    [SerializeField] Camera cam;
    public AnimationCurve ShakeStr;
    public bool start = false;
    public float duration = 1.0f;

    private Vector3 originalPos;
    private float magnitude = 0.1f;


    public static GlobalScreenShake Instance { get; private set; }
    private void Awake()
    {
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

    private void Start()
    {
        if (cam == null)
        {
            cam = GameObject.Find("TexCam").GetComponent<Camera>();
        }
        originalPos = cam.transform.localPosition;
    }

    private void Update()
    {
        if(start)
        {
            Debug.Log("Screen Shake Triggered");
            start = false;
          StartCoroutine(Shake(duration, magnitude));

        }
        if(cam == null)
        {
            cam = GameObject.Find("TexCam").GetComponent<Camera>();
        }
    }

    public void TriggerShake(float dur, float mag)
    {
        duration = dur;
        magnitude = mag;
        start = true;
    }
    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            float strength = ShakeStr.Evaluate(elapsed / duration);
            cam.transform.localPosition = new Vector3(x, y, originalPos.z) * strength;
            elapsed += Time.deltaTime;
            yield return null;
        }
        cam.transform.localPosition = originalPos;
    }
}
