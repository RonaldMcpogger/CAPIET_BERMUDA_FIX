using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsopodRandomizer : MonoBehaviour
{
    Animator animator;
    bool activatable;
    float delay;
    // Start is called before the first frame update
    void Start()
    {
        //delay = Random.Range(34f, 72f);
        delay = Random.Range(1f, 12f);
        activatable = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;

        if (delay <= 0 && activatable)
        {
            animator.Play("IsopodMenu2");
            activatable = false;

            StartCoroutine("createDelay");
        }
    }

    IEnumerator createDelay()
    {
        yield return new WaitForSeconds(12f);

        delay = Random.Range(34f, 72f);
        activatable = true;
    }
}
