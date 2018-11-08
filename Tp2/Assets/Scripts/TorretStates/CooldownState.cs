using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownState<T> : State<T>
{
    public override void Enter()
    {
        Debug.Log("Entrando en Estado ColldownState");
    }

    public override void Update()
    {
        Debug.Log("Actualmente en Estado ColldownState");
    }
    public override void Exit()
    {
        Debug.Log("Saliendo de estado ColldownState");
    }

}
