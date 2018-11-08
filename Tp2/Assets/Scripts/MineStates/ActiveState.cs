using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveState<T> : State<T>
{
    private Mine _mine;
    private GameObject _redLight;
    private bool _activateLight = false;

   public bool ableToBoom = true;

    public ActiveState(Mine pMine, GameObject pRedLight)
    {
        _mine = pMine;
        _redLight = pRedLight;
    }

    public override void Enter()
    {
        _activateLight = true;
    }

    public override void Update ()
    {
        if (_activateLight == true)
        {
            _activateLight = false;
            _redLight.SetActive(true);
            _mine.StartCoroutine(ActivateLight());
        }

        if (ableToBoom)
        {
            _mine.StartExplodeCR();
        }

    }

    IEnumerator ActivateLight()
    {
        yield return new WaitForSeconds(0.5f);
        _redLight.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _redLight.SetActive(true);
        _activateLight = true;
    }

    public override void Exit()
    {
        _mine.StopCoroutine(ActivateLight());
        _redLight.SetActive(false);
    }

}
