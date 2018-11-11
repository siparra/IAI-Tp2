using System.Collections.Generic;
using System;

public static class BFS
{

    public static List<Node> Run<Node>
        (
            Node start,
            Func<Node, bool> satisfies,
            Func<Node, List<Node>> expand
        )
    {
        //Nodos sin visitar
        var open = new Queue<Node>();
        //Arranca con el primer nodo
        open.Enqueue(start);

        //Nodos ya visitados
        var closed = new HashSet<Node>();

        //Diccionario de padres, Key: Hijo, Value: Padre
        var parents = new Dictionary<Node, Node>();

        while (open.Count > 0)//Todavia haya nodos para chequear
        {
            var current = open.Dequeue();//Obtenemos el primer nodo

            if (satisfies(current))//Si el nodo cumple la condicion
            {
                return ConstructPath(current, parents);//Devolvemos el camino a ese nodo
            }

            //Ponemos al current en el closed asi no lo volvemos a chequear
            closed.Add(current);

            //Para cada hijo del current
            foreach (var child in expand(current))
            {
                //Si el nodo ya lo habimos procesado lo salteamos
                if (closed.Contains(child)) continue;

                //Si el nodo estaba en la cola lo salteamos
                if (open.Contains(child)) continue;

                parents[child] = current;//Le seteamos el padre
                open.Enqueue(child);//Lo agregamos al final de la cola
            }

        }

        //Si ningun nodo cumplio la condicion
        return null;
    }

    private static List<Node> ConstructPath<Node>(Node end, Dictionary<Node, Node> parents)
    {
        //Conocemos el final del camino y de donde venimos por los parents
        //Vamos a armar el camino de final a inicio y despues lo revertimos

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
