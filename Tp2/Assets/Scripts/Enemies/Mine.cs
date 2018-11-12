using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private FSM<Feed> stateMachine;
    public LOS los;

    public Transform target;

    //private bool _ableToBoom = true;
    private bool _stillInSight = true;

    public GameObject redLight;
    public GameObject explosionParticleSys;

    public ActiveState<Feed> active;

	void Start ()
    {
        var inactive = new InactiveState<Feed>(redLight);
        active = new ActiveState<Feed>(this, redLight);
        var explode = new ExplodeState<Feed>(explosionParticleSys, this);

        inactive.AddTransition(Feed.EnemigoEntraEnLOS, active);

        active.AddTransition(Feed.EnemigoSaleDeLOS, inactive);
        active.AddTransition(Feed.BOOOOM, explode);

        stateMachine = new FSM<Feed>(inactive);

        los = GetComponent<LOS>();

    }
	
	void Update ()
    {
        stateMachine.Update();

        if (los.IsInSight(target))
        {
            stateMachine.Feed(Feed.EnemigoEntraEnLOS);
            _stillInSight = true;
        }

        if (!los.IsInSight(target))
        {
            _stillInSight = false;
            redLight.SetActive(false);
            stateMachine.Feed(Feed.EnemigoSaleDeLOS);
        }
	}

    public void StartExplodeCR()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        active.ableToBoom = false;
        yield return new WaitForSeconds(3f);
        if (_stillInSight == true)
        {
            stateMachine.Feed(Feed.BOOOOM);
            this.GetComponent<MeshRenderer>().enabled = false;

        }
        active.ableToBoom = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var hero = other.gameObject.GetComponent<Hero>();
        if (hero != null)
        {
            stateMachine.Feed(Feed.BOOOOM);
            this.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
