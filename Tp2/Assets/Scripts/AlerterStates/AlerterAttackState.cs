using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlerterAttackState<T> : State<T>
{

    private Transform _modelTransform;
    private FSM<Feed> _stateMachine;
    private GameObject _explosion;

    public AlerterAttackState(Transform modelTransform, FSM<Feed> stateMachine, GameObject explosionParticle)
    {

        _modelTransform = modelTransform;
        _stateMachine = stateMachine;
        _explosion = explosionParticle;
    }

    public override void Enter()
    {
        Explode();
        _stateMachine.Feed(Feed.BOOOOM);
    }

    public override void Update()
    {
        Debug.Log("Actualmente en estado AlerterAttackState");
    }


    void Explode()
    {
        GameObject.Instantiate(_explosion,_modelTransform.position,_modelTransform.rotation);
        Collider[] colliders = Physics.OverlapSphere(_modelTransform.position, 5f);

        foreach (var collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            var hero = collider.GetComponent<Hero>();
            if (rb != null)
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

