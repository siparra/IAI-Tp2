using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alerter : MonoBehaviour, IObservable {
    public FSM<Feed> stateMachine;

    public LOS los;

    public Transform target;

    public GameObject smokeEffect;
    public GameObject explosionParticle;
    //public GameObject spotLight;
    //------------- PATHFINDING --------------------
    public Node initialNode;
    public Node finalNode;

    public Node nodoAlerta;

    public List<Node> listaDeNodos = new List<Node>();
    public int currentIndex;

    //----- List of Alerters ----
    public List<Alerter> listOfAlerters;
    public bool estaAlerta;

    //-----Observers-------
    private List<IObserver> _listOfObservers = new List<IObserver>();

    public float threshold;
    public float speed;

    public bool finished;
    public AlerterManager alertManager;


    void Start()
    {
        var patrol = new AlerterPatrolState<Feed>(listaDeNodos,speed,threshold,initialNode,finalNode,this.transform,this,alertManager);
        var alarm = new AlerterAlarmState<Feed>(patrol, listOfAlerters);
        var chase = new AlerterChaseState<Feed>(this.transform, target, speed);
        //var alert = new AlertState<Feed>(spotLight, this.GetComponent<Patrol>());
        patrol.AddTransition(Feed.EnemigoEntraEnLOS, alarm);

        alarm.AddTransition(Feed.EnemigoEntraEnLOS, chase);

        //SERGIO!!! Le reordene esta parte, porque meleeattack triggerea el estado broken, 
        //y para eso necesito que la state machine este creada de antemano.
        stateMachine = new FSM<Feed>(patrol);

        var meleeattack = new AlerterAttackState<Feed>(this.transform,stateMachine,explosionParticle);
        var broken = new AlerterBrokenState<Feed>(smokeEffect,this);

        //chase.AddTransition(Feed.EnemigoSaleDeLOS, alert);
        chase.AddTransition(Feed.EntraEnRangoDeAtaque, meleeattack);

        //alert.AddTransition(Feed.EnemigoEntraEnLOS, chase);
        //alert.AddTransition(Feed.NoHayEnemigos, patrol);

        meleeattack.AddTransition(Feed.SaleDeRangoDeAtaque, chase);
        //meleeattack.AddTransition(Feed.EnemigoSaleDeLOS, alert);
        meleeattack.AddTransition(Feed.BOOOOM, broken);

        

        los = GetComponent<LOS>();

        estaAlerta = false;

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
                //Instantiate(explosionParticle, transform.position, transform.rotation);
                //Instantiate(smokeEffect, transform.position, transform.rotation);
                //Destroy(gameObject);

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
