using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridling : MonoBehaviour
{
    [SerializeField] GameObject nodePrefab;

    Node[] grid;

    [SerializeField] public int gridCountX;
    [SerializeField] public int gridCountZ;

    [SerializeField] public int nodeWidth;
    [SerializeField] public int nodeHeight;

    int totalNodes;

    void Start()
    {
        totalNodes = gridCountX * gridCountZ;

        grid = new Node[totalNodes];

        for (int z = 0; z < gridCountZ; z++)
        {
            for (int x = 0; x < gridCountX; x++)
            {
                //int i = x + z * w;
                int i = x + z * gridCountX;

                Vector3 halfPoint = new Vector3((float)nodeWidth / 2.0f, 0, (float)nodeHeight / 2.0f);
                Vector3 worldPosition = new Vector3(x * nodeWidth + halfPoint.x, 0, z * nodeHeight + halfPoint.z);
                Vector3Int gridPosition = new Vector3Int(x, 0, z);
                bool isWalkable = true;

                grid[i] = new Node(worldPosition, gridPosition, isWalkable);

#if ASTAR_DEBUG
                grid[i].NodeGO = Instantiate(nodePrefab, worldPosition, nodePrefab.transform.rotation);
#endif
            }
        }
    }

    public Vector3Int GridToWorldPosition(Vector3Int gridPosition)
    {
        return new Vector3Int(gridPosition.x * nodeWidth, 0, gridPosition.z * nodeHeight);
    }

    public Vector3Int WorldToGridPosition(Vector3 worldPosition)
    {
        return new Vector3Int((int)worldPosition.x / nodeWidth, 0, (int)worldPosition.z / nodeHeight);
    }

    public Node GetNode(Vector3 worldPosition)
    {
        Vector3Int gridPosition = WorldToGridPosition(worldPosition);

        //int i = x + z * w;
        int i = gridPosition.x + gridPosition.z * gridCountX;
        return grid[i];
    }

    void Update()
    {

    }
}