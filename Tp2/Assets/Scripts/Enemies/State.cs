using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T>
{

    private Dictionary<T, State<T>> transitions = new Dictionary<T, State<T>>();

    //lleno el diccionario con la key y el state correspondiente cuando se llama la funcion
    public void AddTransition(T key, State<T> nextState)
    {
        transitions[key] = nextState;
    }

    //busca la transicion correspondiente pasandole una key
    public State<T> GetTransition(T key)
    {
        if (transitions.ContainsKey(key)) return transitions[key];
        else {Debug.Log("La transicion que se esta llamando no esta en el Diccionario"); return null; }
    }
		
    public virtual void Update() { }
    public virtual void Enter() { }
    public virtual void Exit() { }
	
}
