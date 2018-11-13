using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlerterBrokenState<T> : State<T>
{
    private GameObject _smokeEffect;
    private Alerter _patrol;

    public AlerterBrokenState(GameObject smokeEffect, Alerter patrol)
    {
        _smokeEffect = smokeEffect;
        _patrol = patrol;
    }

    public override void Enter()
    {
        GameObject.Instantiate(_smokeEffect, _patrol.transform.position,_patrol.transform.rotation);
    }

    public override void Update()
    {
        Debug.Log("Actualmente en Broken State");
    }
}
