using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlerterManager : MonoBehaviour, IObserver {

    public List<Alerter> listOfEnemies;
    public Node currentNode;

    public void Awake()
    {
        foreach(Alerter alerter in listOfEnemies)
        {
            alerter.AddObserver(this);
        }
    }

    public void OnNotify(string message)
    {
        if(message == "HeroInSight")
        {
            foreach (Alerter alerter in listOfEnemies)
            {
                alerter.nodoRata = currentNode; //Actualiza el nodo Rata para los Enemigos en estado PATROL
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
