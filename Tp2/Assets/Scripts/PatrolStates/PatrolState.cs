using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState<T>: State<T>
{
    private List<Transform> _waypoints = new List<Transform>();
    private float _threshold;
    private float _speed;
    private Transform _modelTransform;

    private int _currentIndex;

    public PatrolState(List<Transform> wp, float thr, float spd, Transform tr)
    {
        _waypoints = wp;
        _threshold = thr;
        _speed = spd;
        _modelTransform = tr;
    }


    public override void Enter()
    {
        Debug.Log("Entrando en estado Patrol");
       // _currentIndex = 0;
    }

    public override void Update()
    {
        Debug.Log("Actualmente Patrullando");

        var current = _waypoints[_currentIndex];
        var direction = (current.position - _modelTransform.position).normalized;
        var distance = Vector3.Distance(_modelTransform.position, current.position);
        var smooth = 100.5f;
        var lookRotation = Quaternion.LookRotation(current.position - _modelTransform.position);

        _modelTransform.position += direction * _speed * Time.deltaTime;
        _modelTransform.rotation = Quaternion.RotateTowards(_modelTransform.rotation, lookRotation, Time.deltaTime * smooth);


        //se puede hacer que cuando llega al wp se quede unos segundos en patrol mirando
        if (distance < _threshold)
        {
            _currentIndex++;

            if (_currentIndex == _waypoints.Count)
                _currentIndex = 0;
        }

    }

 

    public override void Exit()
    {
        
    }

}
