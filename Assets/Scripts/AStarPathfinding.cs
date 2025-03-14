using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{

    //he has a public delegate void 
    // and a event? for the AI to move thing

    [SerializeField] Gridlet grids;

    List<Nodeling> pathList = new List<Nodeling>();
    public List<Nodeling> finalPath = new List<Nodeling>();

    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 goalPosition;

    Nodeling startNode;
    Nodeling goalNode;
    Nodeling currentNode;

    [SerializeField] private int versionNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        grids.GetComponent<Gridlet>();

        startNode = grids.GetNodePos(startPosition);
        goalNode = grids.GetNodePos(goalPosition);

#if ASTAR_DEBUG
        startNode.Instantiation.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.5f);
        goalNode.Instantiation.GetComponent<Renderer>().material.color = new Color(0, 0.3f, 0.5f, 0.5f);
#endif

        pathList.Add(startNode);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        A_StarAlgorithm();
    }

    private void A_StarAlgorithm()
    {
        if (goalNode.IsBlocked || goalNode == null) return;

        if (pathList.Count > 0)
        {
            pathList.Sort();
            currentNode = pathList[0];

            pathList.Remove(currentNode);
            currentNode.Version = versionNumber;

            if (currentNode == goalNode)
            {
                FinalPath(goalNode);
                Debug.Log("Path found");

#if ASTAR_DEBUG
                for (int i = 0; i < finalPath.Count; i++)
                {
                    finalPath[i].Instantiation.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f);
                }
#endif

                finalPath.Reverse();
                //call the AI to move
                pathList.Clear();
                versionNumber++;
                return;

            }

            List<Nodeling> neighbours = new List<Nodeling>();

            //right
            Vector3Int rightGridPos = currentNode.gridPosition + new Vector3Int(1, 0, 0);

            if (rightGridPos.x < grids.rows)
            {
                Nodeling rightNode = grids.GetNodePos(grids.GridToWorldPos(rightGridPos));
                neighbours.Add(rightNode);
            }

            //left
            Vector3Int leftGridPos = currentNode.gridPosition + new Vector3Int(-1, 0, 0);

            if (leftGridPos.x >= 0)
            {
                Nodeling leftGrid = grids.GetNodePos(grids.GridToWorldPos(leftGridPos));
                neighbours.Add(leftGrid);
            }

            //up
            Vector3Int upGridPos = currentNode.gridPosition + new Vector3Int(0, 0, 1);

            if (upGridPos.z < grids.cols)
            {
                Nodeling upGrid = grids.GetNodePos(grids.GridToWorldPos(upGridPos));
                neighbours.Add(upGrid);
            }

            //down
            Vector3Int downGridPos = currentNode.gridPosition + new Vector3Int(0, 0, -1);

            if (downGridPos.z >= 0)
            {
                Nodeling downGrid = grids.GetNodePos(grids.GridToWorldPos(downGridPos));
                neighbours.Add(downGrid);
            }


            for (int i = 0; i < neighbours.Count; i++)
            {

                if (neighbours[i].IsBlocked || neighbours[i].version == versionNumber)
                {
                    continue;
                }

#if ASTAR_DEBUG
                neighbours[i].Instantiation.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.5f);
#endif

                int movementPath = CalculateDistance(neighbours[i].gridPosition, currentNode.gridPosition) + currentNode.GCost;
                neighbours[i].Version = versionNumber;

                if (movementPath < neighbours[i].GCost || !pathList.Contains(neighbours[i]))
                {
                    neighbours[i].GCost = movementPath;
                    neighbours[i].HCost = CalculateDistance(neighbours[i].gridPosition, goalNode.gridPosition);
                    

                    if (neighbours[i].NextNode != currentNode)
                    neighbours[i].NextNode = currentNode;

                    

                    if (!pathList.Contains(neighbours[i]))
                    {
                        pathList.Add(neighbours[i]);
                    }
                    else if (neighbours[i].NextNode == null)
                    {
                        Debug.Log("WARNING"); //for debugging if NextNode got overwritten
                    }
                }
                
            }

        }

    }

    private void FinalPath(Nodeling node)
    {
        while (node != null)
        {
            if (finalPath.Contains(node))
            {
                break;
            }
            finalPath.Add(node);
            node = node.NextNode;
        }
    }

    private int CalculateDistance(Vector3Int grid1, Vector3Int grid2)
    {
        return Mathf.Abs(grid1.x - grid2.x) + Mathf.Abs(grid1.y - grid2.y) + Mathf.Abs(grid1.z - grid2.z);
    }


}
