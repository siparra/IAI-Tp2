using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveState<T> : State<T>
{
    private GameObject _redLight;

    public InactiveState(GameObject pRedLight)
    {
        _redLight = pRedLight;
    }
    public override void Enter()
    {
        _redLight.SetActive(false);  
    }
    public override void Update ()
    {
        Debug.Log("mina inactiva");
	}
}
