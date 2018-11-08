using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM <T>
{
    public State<T> current;

    //constructor
    public FSM(State<T> initialState)
    {
        //le cargo un estado como estado inicial
        current = initialState;
    }

    //este update lo voy a llamar desde el update de la entidad cuando creo la instancia de la State
    //machine
    public void Update()
    {
        //llamo el update del current state
        current.Update();
    }
    
    //aca pasa la magia
    public void Feed(T input)
    {
        //cargo la proxima transicion
        var next = current.GetTransition(input);

        //si el GetTransition devuelve una transicion hago el cambio de estado
        if(next != null)
        {
            current.Exit();
            current = next;
            current.Enter();
        }
    }
}
