using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridlet : MonoBehaviour
{

    [SerializeField] private GameObject node; //takes the prefab

    Nodeling[] grid;

    [SerializeField] public int rows, cols;
    [SerializeField] public float nodeLength, nodeWidth; //ho

    [SerializeField] int totalNodes;

    void Start()
    {
        totalNodes = rows * cols;

        grid = new Nodeling[totalNodes];

        for (int z = 0; z < cols; z++)
        {
            for (int x = 0; x < rows; x++)
            {
                int i = x + z * rows;

                Vector3 midPoint = new Vector3(nodeWidth/2.0f, 0, nodeLength / 2.0f);
                Vector3 nodePosition = new Vector3(x * nodeLength + midPoint.x, 0, z * nodeWidth + midPoint.z);

                Vector3Int gridPosition = new Vector3Int(x, 0, z);

                bool isBlocked = Physics.CheckBox(nodePosition, midPoint);

                grid[i] = new Nodeling(nodePosition, gridPosition, isBlocked);
#if ASTAR_DEBUG
                grid[i].Instantiation = Instantiate(node, nodePosition, node.transform.rotation);
                if (grid[i].IsBlocked)
                {
                    grid[i].Instantiation.GetComponent<Renderer>().material.color = new Color(1, 0, 1, 0.5f);
                }
#endif

            }
        }

    }

    public Vector3Int WorldToGridPos(Vector3 worldPos)
    {
        return new Vector3Int((int)(worldPos.x / nodeWidth), 0, (int)(worldPos.z / nodeLength));

    }

    public Vector3Int GridToWorldPos(Vector3Int gridPos)
    {
        return new Vector3Int((int)(gridPos.x * nodeWidth), 0, (int)(gridPos.z * nodeLength));
    }

    public Nodeling GetNodePos(Vector3 worldPos)
    {
        Vector3Int gridPos = WorldToGridPos(worldPos);

        int i = gridPos.x + gridPos.z * rows;
        return grid[i];
    }
    

}
