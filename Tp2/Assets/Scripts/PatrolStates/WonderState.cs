using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderState<T> : State<T> {

    private float _threshold;
    private float _speed;
    private Transform _modelTransform;
    private float _time = 4f;
    private float _radius = 10f;
    private Vector3 _current;
    private Vector3 _wanderTarget;
    private MonoBehaviour _patrolw;


    private int _currentIndex;

    public WonderState( float thr, float spd, Transform tr, MonoBehaviour pw)
    {
        _threshold = thr;
        _speed = spd;
        _modelTransform = tr;
        _patrolw = pw;
        _wanderTarget = tr.position;

        _patrolw.StartCoroutine(WonderRoutine());
    }


    public override void Enter()
    {
        Debug.Log("Entrando en estado Patrol");
        Vector2 offset = Random.insideUnitCircle;
        _current = _wanderTarget + new Vector3(offset.x, 0, offset.y) * _radius;
    }

    /// <summary>
    /// El movimiento aplica el wonder
    /// </summary>
    public override void Update()
    {
        Debug.Log("Actualmente Patrullando");

        var direction = (_current - _modelTransform.position).normalized;
        var distance = Vector3.Distance(_modelTransform.position, _current);
        var smooth = 100.5f;
        var lookRotation = Quaternion.LookRotation(_current - _modelTransform.position);

        if(distance > _threshold)
        {
            _modelTransform.position += direction * _speed * Time.deltaTime;
            _modelTransform.rotation = Quaternion.RotateTowards(_modelTransform.rotation, lookRotation, Time.deltaTime * smooth);

        }

    }

    public override void Exit()
    {

    }


    private IEnumerator WonderRoutine()
    {
        var curTime = 0f;

        while (true)
        {
            curTime += Time.deltaTime;

            if (curTime > _time)
            {
                curTime = 0;

                Vector2 offset = Random.insideUnitCircle;
                _current = _wanderTarget + new Vector3(offset.x, 0, offset.y) * _radius;

            }
            yield return null;
        }

    }
}
