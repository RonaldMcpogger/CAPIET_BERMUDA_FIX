using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScreenShake : MonoBehaviour
{
    GlobalScreenShake Instance;
    // Start is called before the first frame update
    [SerializeField] Camera cam;
    public AnimationCurve ShakeStr;
    public bool start = false;
    public float duration = 1.0f;

    private Vector3 originalPos;
    private float magnitude = 0.1f;
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
            cam = Camera.main;
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
            cam.transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cam.transform.localPosition = originalPos;
    }
}
