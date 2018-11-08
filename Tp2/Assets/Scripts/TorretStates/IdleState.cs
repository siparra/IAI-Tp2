using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState<T> : State<T>
{
    public override void Enter()
    {
        Debug.Log("Enter en estado Idle");
    }

}
