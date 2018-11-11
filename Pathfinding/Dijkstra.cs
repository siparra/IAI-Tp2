using System.Collections.Generic;
using System;

public static class Dijkstra
{

    public static List<Node> Run<Node>
        (
            Node start,
            Func<Node, bool> satisfies,
            Func<Node, Dictionary<Node, float>> expand
        )
    {
        //Nodos sin visitar
        var open = new PriorityQueue<Node>();
        //Arranca con el primer nodo
        open.Enqueue(start, 0);

        //Nodos ya visitados
        var closed = new HashSet<Node>();

        //Diccionario de padres, Key: Hijo, Value: Padre
        var parents = new Dictionary<Node, Node>();

        //Diccionarios de costos, Key: Nodo, Value: costo tentativo para llegar
        var costs = new Dictionary<Node, float>();
        costs[start] = 0;

        while (!open.IsEmpty)//Todavia haya nodos para chequear
        {
            var current = open.Dequeue();

            if (satisfies(current))//Si el nodo cumple la condicion
            {
                return ConstructPath(current, parents);//Devolvemos el camino a ese nodo
            }

            var currentCost = costs[current];

            //Ponemos al current en el closed asi no lo volvemos a chequear
            closed.Add(current);

            //Para cada hijo del current
            foreach (var childPair in expand(current))
            {
                var child = childPair.Key;
                var childCost = childPair.Value;

                //Si el nodo ya lo habimos procesado lo salteamos
                if (closed.Contains(child)) continue;

                var tentativeCost = currentCost + childCost;
                if (costs.ContainsKey(child) && tentativeCost > costs[child]) continue;

                parents[child] = current;//Le seteamos el padre

                costs[child] = tentativeCost;
                open.Enqueue(child, tentativeCost);//Lo agregamos a la cola
            }

        }

        //Si ningun nodo cumplio la condicion
        return null;
    }

    private static List<Node> ConstructPath<Node>(Node end, Dictionary<Node, Node> parents)
    {
        //Conocemos el final del camino y de donde venimos por los parents
        //Vamos a armar el camino del final al inicio y despues lo revertimos

        var path = new List<Node>();
        path.Add(end);//Emppezamos con el ultimo

        //Mientras el ultimo nodo tenga un padre
        while (parents.ContainsKey(path[path.Count - 1]))
        {
            var lastNode = path[path.Count - 1];//El ultimo nodo

            //Agregamos el padre del ultimo nodo al final
            path.Add(parents[lastNode]);
        }

        path.Reverse();//Lo damos vuenta
        return path;
    }

}
