using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float contador;

	void Start ()
    {
        contador = 0f;
	}
	
	void Update ()
    {
        contador += Time.deltaTime;
        if(contador > 3)
        {
            Destroy(this.gameObject);
        }
	}
}
