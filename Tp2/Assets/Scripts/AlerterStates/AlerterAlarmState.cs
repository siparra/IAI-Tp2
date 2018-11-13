using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlerterAlarmState<T> : State<T> {
    private AlerterPatrolState<Feed> _patroler;
    private List<Alerter> _listOfAlerters;

    public AlerterAlarmState(AlerterPatrolState<Feed> patrol, List<Alerter> listOfAlerters)
    {
        _patroler = patrol;
        _listOfAlerters = new List<Alerter>();
        foreach(Alerter alert in listOfAlerters)
        {
            _listOfAlerters.Add(alert);
        }
    }

    public override void Enter()
    {
        Alertar(_patroler._listaDeNodos[_patroler._currentIndex]); //LISTA de NODOS no contiene el nodo actual
    }

    public override void Update()
    {
        
    }

    public void Alertar(Node currentNode)
    {
        foreach (Alerter a in _listOfAlerters)
        {
            Debug.Log("Asignando Nodo Alerta");
            a.nodoAlerta = currentNode;
            a.estaAlerta = true;
        }
    }
}
