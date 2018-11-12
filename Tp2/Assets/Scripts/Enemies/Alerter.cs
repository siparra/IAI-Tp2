using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alerter : MonoBehaviour, IObservable {
    public FSM<Feed> stateMachine;

    public LOS los;

    public Transform target;

    public GameObject explosionParticle;
    public GameObject spotLight;
    //------------- PATHFINDING --------------------
    public Node initialNode;
    public Node finalNode;

    public Node nodoRata;

    public List<Node> listaDeNodos = new List<Node>();
    private int currentIndex;

    //-----Observers-------
    private List<IObserver> _listOfObservers = new List<IObserver>();

    public float threshold;
    public float speed;

    public bool finished;
    public AlerterManager alertManager;


    void Start()
    {
        var patrol = new AlerterPatrolState<Feed>(listaDeNodos,speed,threshold,initialNode,finalNode,this.transform,this,alertManager);

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
            //Trigger("HeroInSight");//Vio al Hero y Avisa!

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

    public void AddObserver(IObserver obs)
    {
        if (!_listOfObservers.Contains(obs))
        {
            _listOfObservers.Add(obs);
        }
    }

    public void RemoveObserver(IObserver obs)
    {
        if (_listOfObservers.Contains(obs))
        {
            _listOfObservers.Remove(obs);
        }
    }

    public void Trigger(string triggermessage)
    {
        foreach (var observer in _listOfObservers)
        {
            observer.OnNotify(triggermessage);
        }
    }
}
