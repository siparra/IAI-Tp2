using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingBFS : MonoBehaviour {
    public Node initialNode;
    public Node finalNode;

    public List<Node> listaDeNodos = new List<Node>();
    private int currentIndex;
    public float speed;
    public float threshold;
    public bool loop;

    public bool finished;

    // Use this for initialization
    void Start () {
        listaDeNodos = BFS.Run(initialNode, satisfy, ExpandBFS);
        foreach(Node node in listaDeNodos)
        {
            Debug.Log("node: " + node.name);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (finished) return;

        var current = listaDeNodos[currentIndex];
        var direction = (current.transform.position - transform.position).normalized;
        var distance = Vector3.Distance(transform.position, current.transform.position);
        transform.position += direction * speed * Time.deltaTime;

        if(distance < threshold)
        {
            currentIndex++;
            if ( currentIndex == listaDeNodos.Count)
            {
                if (loop) currentIndex = 0;
                else finished = true;
            }
        }


    }

    /// <summary>
    /// Aca arranca BFS
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool satisfy(Node node)
    {
        if (node.Equals(finalNode))
            return true;
        else
            return false;
    }

    public List<Node> ExpandBFS(Node current)
    {
        return current.vecinos;
    }
    //Aca termina BFS

    /// <summary>
    /// Esto es para Astar
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public float Euristic(Node node)
    {
        var euristica = Vector3.Distance(node.transform.position, finalNode.transform.position);

        return euristica;
    }

    //public List<Tuple<Node, float>> ExpandAStar(Node node)
    //{
    //    var 
    //}
}
