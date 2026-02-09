using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float timer = 10f;
    [SerializeField] public Material highlight;
    [SerializeField] public Material NonScannable;
    [SerializeField] private bool isScanning = false; // scan currently ongoing
    [SerializeField] private Collider ScanArea;

    //originMaterial store

    private readonly Dictionary<GameObject, Material[]> originalMaterials = new Dictionary<GameObject, Material[]>();



    // coroutine handle for the scanning timer
    private Coroutine scanCoroutine;



    void Start()
    {
        ScanArea=GameObject.Find("ScanArea").GetComponent<Collider>();
        if (ScanArea != null)
        {
            ScanArea.isTrigger = true;
        }
        //for the scanner item to work with trigger colission
        var rb = ScanArea.gameObject.GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = ScanArea.gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    public void StartScanning()
    {
        
        if (isScanning == false)
        {
            ItemManager.Instance.BatteryLife -= 25f;
            isScanning = true;
            RenderSettings.fog = false;

            if (scanCoroutine != null)
            {
                StopCoroutine(scanCoroutine);
            }
            scanCoroutine = StartCoroutine(ScanTimer());
        }
    }

    public void StopScanning()
    {
        isScanning = false;
        // restore original materials
        RenderSettings.fog = true;

      
        // stop the scanning timer coroutine if it's running
        if (scanCoroutine != null)
        {
            StopCoroutine(scanCoroutine);
            scanCoroutine = null;
        }
        this.Restore();
        this.gameObject.SetActive(false);
    }

    private void Restore()
    {
        foreach (var kvp in originalMaterials)
        {
            if (kvp.Key != null)
            {
                var rend = kvp.Key.GetComponent<Renderer>();
                if (rend != null)
                {
                    rend.materials = kvp.Value;
                }
            }
        }
        originalMaterials.Clear();
    }


    private void ScanSort(Collider other) //helper function to determienw which object is scanned
    {
        if (isScanning && other.gameObject.CompareTag("Items"))
        {
            Debug.Log("Item scanned");

            Renderer objRenderer = other.gameObject.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                Debug.Log("Item in Library" + other.gameObject);
                // Store original materials if not already stored
                if (!originalMaterials.ContainsKey(other.gameObject))
                {
                    originalMaterials[other.gameObject] = objRenderer.materials;
                }
                // Create a new array of materials with the highlight material
                Material[] highlightMaterials = new Material[objRenderer.materials.Length];
                for (int i = 0; i < highlightMaterials.Length; i++)
                {
                    highlightMaterials[i] = highlight;
                }
                // Apply the highlight materials
                objRenderer.materials = highlightMaterials;
            }
        }
        else if (isScanning && other.gameObject.CompareTag("Walls"))
        {
            Debug.Log("wall scanned");
            Renderer objRenderer = other.gameObject.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                if (!originalMaterials.ContainsKey(other.gameObject))
                {
                    originalMaterials[other.gameObject] = objRenderer.materials;
                }

                Material[] wallMaterials = new Material[objRenderer.materials.Length];
                for (int i = 0; i < wallMaterials.Length; i++)
                {
                    wallMaterials[i] = NonScannable;
                }
                objRenderer.materials = wallMaterials;
            }
        }
    }

    private void OnTriggerEnter(Collider other) /// test and recode this later
    {
       ScanSort(other);
    }

    private void OnTriggerStay(Collider other)
    {
        ScanSort(other);
    }


    private void OnTriggerExit(Collider other)
    {
        // Restore original materials when object exits scan area
        if (originalMaterials.ContainsKey(other.gameObject))
        {
            Renderer objRenderer = other.gameObject.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                objRenderer.materials = originalMaterials[other.gameObject];
            }
            originalMaterials.Remove(other.gameObject);
        }
    }


    private IEnumerator ScanTimer()
    {
        float elapsed = 0f;
        while (elapsed < timer)
        {
            // if scanning was turned off externally, stop early
            if (!isScanning)
            {
                scanCoroutine = null;
                yield break;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // time's up -> stop scanning
        Debug.Log("Scan complete");
        StopScanning();
    }
}
    // Update is called once per frame

