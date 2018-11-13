using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState<T>:State<T>
{

    //TODO: hacer la animacion con rotacion para el alert
    private GameObject _spotLight;
    private Patrol _patrol;
    private float _contador = 0f;
    public AlertState( GameObject pSpotLight, Patrol sPatrol)
    {
        _spotLight = pSpotLight;
        _patrol = sPatrol;
    }

    public override void Enter()
    {
        _spotLight.SetActive(true);
    }

    public override void Update()
    {
        Debug.Log("Actualmente en Alert State");
        _contador += Time.deltaTime;
        if (_contador > 5f)
        {
            _patrol.stateMachine.Feed(Feed.NoHayEnemigos);
            _contador = 0f;
        }
    }

    public override void Exit()
    {
        _spotLight.SetActive(false);
    }
}
