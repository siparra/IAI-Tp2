using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSystem : MonoBehaviour {

    private float contador = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        contador += Time.deltaTime;
        if (contador > 3.9)
        {
            Destroy(this.gameObject);
        }
		
	}
}
