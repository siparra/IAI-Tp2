using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hero : MonoBehaviour, IObservable {

    NavMeshAgent agent;

    public GameObject bloodParticleSys;

    public List<IObserver> observers = new List<IObserver>();

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
	}
	

	void Update ()
    {

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //TODO: hacer el refactor del movimiento del hero. El set destination es malisimo.
                agent.SetDestination(hit.point);          
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            var bloodEfect = Instantiate(bloodParticleSys, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            Trigger("Bullet Damage"); 
            Destroy(collision.gameObject);
            StartCoroutine(DestroyPS(bloodEfect));     
        }
    }

    IEnumerator DestroyPS(GameObject ps)
    {
        yield return new WaitForSeconds(1);
        Destroy(ps);
    }

    public void AddObserver(IObserver obs)
    {
        observers.Add(obs);
    }

    public void RemoveObserver(IObserver obs)
    {
        if (observers.Contains(obs))
        {
            observers.Remove(obs);
        }
    }

    public void Trigger(string triggermessage)
    {
        foreach (var observer in observers)
        {
            observer.OnNotify(triggermessage);
        }
    }
}

