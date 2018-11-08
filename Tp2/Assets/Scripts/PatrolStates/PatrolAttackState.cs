using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAttackState<T> : State<T>
{

    private Transform _modelTransform;

    public PatrolAttackState( Transform modelTransform)
    {

        _modelTransform = modelTransform;
    }

    public override void Enter()
    {
        Explode();
    }

    public override void Update()
    {
       
        Debug.Log("Actualmente en estado PatrolAttackState");
    }


    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(_modelTransform.position, 5f);

        foreach (var collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            var hero = collider.GetComponent<Hero>();
            if(rb != null)
            {
                rb.AddExplosionForce(1000, _modelTransform.position, 5f);
            }
            if (hero != null)
            {
                hero.Trigger("Explosion Damage");
            }

        }
        
    }

}
