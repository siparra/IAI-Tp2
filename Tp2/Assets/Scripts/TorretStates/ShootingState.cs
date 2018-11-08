using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState<T> : State<T>
{
    private Transform transform;
    public Transform target;
    private Torreta  _torreta;

    public bool allowFire = true;
    public float contador;

    public ShootingState(Transform pCharacter, Transform pTarget, Torreta pTorreta)
    {
        this.transform = pCharacter;
        target = pTarget;
        _torreta = pTorreta;
    }

    public override void Enter()
    {
        Debug.Log("Entrando en Estado ShootingState");
    }

    public override void Update()
    {
        Rotate();

        if (allowFire == true)
        {
            _torreta.StartFireCr();
        }

        contador += Time.deltaTime;
        if (contador > 5)
        {
            _torreta.StartTransitionToCDCouroutine();
        }
    }

    public void Rotate()
    {
        float smooth = 40.5f;
        //float tiltAngle = 60.0f;
        //Vector3 enemyPos =  (target.position - transform.position);
        //var angleToTarget = Vector3.Angle(transform.forward, enemyPos) / 2;

        // Quaternion targetyPos = Quaternion.Euler(0, angleToTarget, 0);
        // transform.rotation = Quaternion.Slerp(transform.rotation, targetyPos, Time.deltaTime * smooth);

        var lookRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * smooth);


    }

}
