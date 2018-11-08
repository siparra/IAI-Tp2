using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeState<T> : State<T>
{
    private GameObject _expParticleSys;
    private Mine _model;

    public ExplodeState(GameObject pexpParticleSys, Mine model)
    {
        _expParticleSys = pexpParticleSys;
        _model = model;
    }
    public override void Enter()
    {
        _model.StartCoroutine(ExploisonPS());
        Explode();
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(_model.gameObject.transform.position, 5f);

        foreach (var collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            var hero = collider.GetComponent<Hero>();
            if (rb != null)
            {
                rb.AddExplosionForce(1000, _model.gameObject.transform.position, 5f);
                hero.Trigger("Explosion Damage"); 
            }

        }
    }

    IEnumerator ExploisonPS()
    {
        var ps = GameObject.Instantiate(_expParticleSys, _model.gameObject.transform.position, _model.gameObject.transform.rotation);
        yield return new WaitForSeconds(1f);
        Debug.Log("entra despues de 1 sec");
        GameObject.Destroy(ps);
        GameObject.Destroy(_model.gameObject);
    }


}
