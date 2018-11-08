using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public LOS los;
    public Transform target;
    public bool activateLight = true;
    public GameObject greenLight;


    void Start ()
    {
        los = GetComponent<LOS>();
	}
	
	void Update ()
    {
        if (los.IsInSight(target))
        {
            var hero = target.gameObject.GetComponent<Hero>();
            hero.Trigger("Win");
        }

        if (activateLight == true)
        {
            activateLight = false;
            greenLight.SetActive(true);
            StartCoroutine(ActivateLight());
        }
    }

    IEnumerator ActivateLight()
    {
        yield return new WaitForSeconds(0.5f);
        greenLight.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        greenLight.SetActive(true);
        activateLight = true;
    }
}
