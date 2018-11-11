using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node: MonoBehaviour {
    public List<Node> vecinos;

    private void Awake()
    {
        foreach(Node node in vecinos)
        {
            if (!node.vecinos.Contains(this)){
                node.vecinos.Add(this);
            }

        }
    }

    void OnDrawGizmos()
    {
        var position = this.transform.position;

        foreach (Node node in vecinos)
        {
            Gizmos.DrawLine(position, node.transform.position);
        }
    }
}
