using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    Gridling grid;

    List<Node> openList = new List<Node>();
    List<Node> closeList = new List<Node>();

    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 goalPosition;

    Node startNode;
    Node goalNode;
    Node currentNode;

    void Start()
    {
        grid = GetComponent<Gridling>();

        startNode = grid.GetNode(startPosition);
        goalNode = grid.GetNode(goalPosition);

#if ASTAR_DEBUG
        startNode.NodeGO.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
        goalNode.NodeGO.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);
#endif

        openList.Add(startNode);

        //need to go to the next node to progress
        
        while (true)
        {
            openList.Sort();
            currentNode = openList[0];

            openList.Remove(currentNode);
            closeList.Add(currentNode);

            if (currentNode == goalNode)
            {
                return;
            }


            // Find nabour
            List<Node> neighbours = new List<Node>();


            //right neighbour
            Vector3Int rightNodeGridPosition = currentNode.GridPosition + new Vector3Int(1, 0, 0);
            if (rightNodeGridPosition.x < grid.gridCountX)
            {
                Node rightNode = grid.GetNode(grid.GridToWorldPosition(rightNodeGridPosition));
                neighbours.Add(rightNode);
            }

            //left neighbour
            Vector3Int leftNodeGridPosition = currentNode.GridPosition + new Vector3Int(-1, 0, 0);
            if (leftNodeGridPosition.x >= 0)
            {
                Node lefttNode = grid.GetNode(grid.GridToWorldPosition(leftNodeGridPosition));
                neighbours.Add(lefttNode);
            }

            //top neighbour
            Vector3Int topNodeGridPosition = currentNode.GridPosition + new Vector3Int(0, 0, 1);
            if (topNodeGridPosition.z < grid.gridCountZ)
            {
                Node topNode = grid.GetNode(grid.GridToWorldPosition(topNodeGridPosition));
                neighbours.Add(topNode);
            }

            //bottom neighbour
            Vector3Int bottomNodeGridPosition = currentNode.GridPosition + new Vector3Int(0, 0, -1);
            if (bottomNodeGridPosition.z >= 0)
            {
                Node bottomtNode = grid.GetNode(grid.GridToWorldPosition(bottomNodeGridPosition));
                neighbours.Add(bottomtNode);
            }

#if ASTAR_DEBUG
            for (int i = 0; i < neighbours.Count; i++)
            {
                neighbours[i].NodeGO.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
            }
#endif
        }
    }

    void CalculateNextDecision(Node node1, Node node2) 
    {
            
    }

}