using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlerterPatrolState<T> : State<T> {
    public List<Node> _listaDeNodos = new List<Node>();
    public int _currentIndex;
    
    private float _speed;
    private float _threshold;

    private bool _finished;
    private bool _loop;

    private Node _initialNode;
    private Transform _modelTransform;
    private Alerter _alerter;
    private AlerterManager _alertManager;

    public AlerterPatrolState(List<Node> nodos, float speed, float threshold, Node initialNode, Node finalNode, Transform modelTransform, Alerter alerter, AlerterManager alertManager)
    {
        foreach(Node n in nodos)
        {
            _listaDeNodos.Add(n);
        }
        
        _speed = speed;
        _threshold = threshold;
        _initialNode = initialNode;
        _modelTransform = modelTransform;
        _alerter = alerter;
        
        _alertManager = alertManager;
        _alerter.nodoAlerta = finalNode;
        _currentIndex = 0;
        this.Enter(); //Posible Cambio!
    }

    public bool satisfy(Node node)
    {
        if (node.Equals(_alerter.nodoAlerta))//CAMBIAR DESPUES
            return true;
        else
            return false;
    }

    public List<Node> ExpandBFS(Node current)
    {
        return current.vecinos;
    }

    public override void Enter()
    {
        _listaDeNodos = BFS.Run(_initialNode, satisfy, ExpandBFS); // Como llamar al BFS una vez que actualizamos el nodoRata?
        _finished = false;
        _loop = true;
        Debug.Log("Loop in Enter: "+ _loop);
    }

    public override void Update()
    {
        if (_finished) return;

        Debug.Log("Actualmente Patrullando");

        var current = _listaDeNodos[_currentIndex];
        var direction = (current.transform.position - _modelTransform.position).normalized;
        var distance = Vector3.Distance(_modelTransform.position, current.transform.position);
        var smooth = 100.5f;
        var lookRotation = Quaternion.LookRotation(current.transform.position - _modelTransform.position);

        _modelTransform.position += direction * _speed * Time.deltaTime;
        _modelTransform.rotation = Quaternion.RotateTowards(_modelTransform.rotation, lookRotation, Time.deltaTime * smooth);


        //se puede hacer que cuando llega al wp se quede unos segundos en patrol mirando
        if (distance < _threshold)
        {
            Debug.Log("distance es menor a threshold!");
            _currentIndex++;

            if (_currentIndex == _listaDeNodos.Count)
            {
                Debug.Log("Loop: "+ _loop);
                Debug.Log("current Index: " + _currentIndex);
                if (_loop) _currentIndex = 0;
                else _finished = true;
            }
                
        }

        if (_alerter.estaAlerta)
        {
            RecalcularCamino();
        }

    }

    public override void Exit()
    {
        //Esto lo cambiamos. Actualizar el current node del alert manager.
        //_alertManager.currentNode = _listaDeNodos[_currentIndex];
        _alerter.currentIndex = _currentIndex;
        _alerter.Trigger("HeroInSight");
    }

    private List<Node> RecalcularCamino()
    {
        _listaDeNodos = BFS.Run(_initialNode, satisfy, ExpandBFS);
        _alerter.estaAlerta = false;

        return _listaDeNodos;
    }
}
