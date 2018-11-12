using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolWonder : MonoBehaviour
{

    public FSM<Feed> stateMachine;

    public LOS los;

    public Transform target;
    public float contador = 0f;
    public Animator animator;

    public List<Transform> waypoints = new List<Transform>();
    public float threshold;
    public float speed;

    public GameObject explosionParticle;
    public GameObject spotLight;

    void Start()
    {
        animator = GetComponent<Animator>();

        var patrol = new WonderState<Feed>(threshold, speed, this.transform, this.GetComponent<PatrolWonder>());
        var chase = new ChaseState<Feed>(this.transform, target, speed);
        var alert = new AlertState<Feed>(spotLight, this.GetComponent<Patrol>());
        var meleeattack = new PatrolAttackState<Feed>(this.transform);

        patrol.AddTransition(Feed.EnemigoEntraEnLOS, chase);

        chase.AddTransition(Feed.EnemigoSaleDeLOS, alert);
        chase.AddTransition(Feed.EntraEnRangoDeAtaque, meleeattack);

        alert.AddTransition(Feed.EnemigoEntraEnLOS, chase);
        alert.AddTransition(Feed.NoHayEnemigos, patrol);

        meleeattack.AddTransition(Feed.SaleDeRangoDeAtaque, chase);
        meleeattack.AddTransition(Feed.EnemigoSaleDeLOS, alert);

        stateMachine = new FSM<Feed>(patrol);

        los = GetComponent<LOS>();

    }


    void Update()
    {
        stateMachine.Update();

        var distance = Vector3.Distance(transform.position, target.position);

        if (los.IsInSight(target))
        {
            stateMachine.Feed(Feed.EnemigoEntraEnLOS);

            if (distance < 1.5f)
            {
                stateMachine.Feed(Feed.EntraEnRangoDeAtaque);
                Instantiate(explosionParticle, transform.position, transform.rotation);
                Destroy(gameObject);

            }
        }

        if (!los.IsInSight(target))
        {
            stateMachine.Feed(Feed.EnemigoSaleDeLOS);
        }
    }

}
