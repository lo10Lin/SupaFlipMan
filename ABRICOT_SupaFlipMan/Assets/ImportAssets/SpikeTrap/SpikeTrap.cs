using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour {

    public Animator spikeTrapAnim;
    [SerializeField] private float openTimer1 = 0;
    [SerializeField] private float closeTimer = 2;
    [SerializeField] private float openTimer2 = 2;
    void Awake()
    {
        spikeTrapAnim = GetComponent<Animator>();
        StartCoroutine(OpenCloseTrap());
    }

    public void Continue()
    {
        StartCoroutine(OpenCloseTrap());
    }

    IEnumerator OpenCloseTrap()
    {
        yield return new WaitForSeconds(openTimer1);

        spikeTrapAnim.SetTrigger("close");
        yield return new WaitForSeconds(closeTimer);

        spikeTrapAnim.SetTrigger("open");
        yield return new WaitForSeconds(openTimer2);

        StartCoroutine(OpenCloseTrap());

    }
}