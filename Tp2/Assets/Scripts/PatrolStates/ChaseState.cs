using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState<T> : State<T>
{
    private Transform _modelTransform;
    private Transform _target;
    private float _speed;

    public ChaseState(Transform pModelTransform, Transform pTarget, float pSpeed)
    {
        _modelTransform = pModelTransform;
        _target = pTarget;
        _speed = pSpeed;
    }

    public override void Update()
    {
        Debug.Log("Patrol esta en estado chase");

        var direction = (_target.position - _modelTransform.position).normalized;

        var smooth = 100.5f;
        var lookRotation = Quaternion.LookRotation(_target.position - _modelTransform.position);

        _modelTransform.position += direction * (_speed * 3) * Time.deltaTime;
        _modelTransform.rotation = Quaternion.RotateTowards(_modelTransform.rotation, lookRotation, Time.deltaTime * smooth);

    }

}
