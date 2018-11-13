using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlerterChaseState<T> : State<T>
{
    private Transform _modelTransform;
    private Transform _target;
    private Hero _heroScript;
    private float time = 2f;

    private float _speed;

    public AlerterChaseState(Transform pModelTransform, Transform pTarget, float pSpeed)
    {
        _modelTransform = pModelTransform;
        _target = pTarget;
        _speed = pSpeed;

        _heroScript = _target.GetComponent<Hero>();
    }

    /// <summary>
    /// 10/11/2018 -- Refactor del codigo para soportar Steering Behaviours Pursuit. Le sumo el guessPosition para adivinar la posicion
    /// a en la que va a estar el Hero.
    /// </summary>
    public override void Update()
    {
        Debug.Log("Patrol esta en estado AlerterChaseState");

        var guessPosition = _target.transform.position + _heroScript.Velocity * time;

        var direction = (guessPosition - _modelTransform.position).normalized;

        var smooth = 100.5f;
        var lookRotation = Quaternion.LookRotation(_target.position - _modelTransform.position);

        _modelTransform.position += direction * (_speed * 2.75f) * Time.deltaTime;
        _modelTransform.rotation = Quaternion.RotateTowards(_modelTransform.rotation, lookRotation, Time.deltaTime * smooth);

    }

}
