using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable
{
    void AddObserver(IObserver obs);
    void RemoveObserver(IObserver obs);
    void Trigger(string triggermessage);
}
