using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaAngelMenuRandomizer : MonoBehaviour
{

    float delay;
    Animator animator;
    bool activatable = true;
    // Start is called before the first frame update
    void Start()
    {
        delay = Random.Range(2f, 8f);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;

        if (delay <= 0 && activatable)
        {
            animator.Play("SeaAngelMenu");
            activatable = false;

            StartCoroutine("createDelay");
        }
    }

    IEnumerator createDelay()
    {
        yield return new WaitForSeconds(6);

        activatable = true;
        delay = Random.Range(2f, 8f);
    }
}
