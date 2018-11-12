using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlerterAlarmState<T> : State<T> {
    private Alerter _alerter;
    private Hero target;

    public AlerterAlarmState(Alerter alerter,Hero target)
    {
        _alerter = alerter;
    }

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        
    }
}
