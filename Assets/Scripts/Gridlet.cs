using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridlet : MonoBehaviour
{

    [SerializeField] private GameObject node; //takes the prefab

    Nodes[] grid;

    [SerializeField] public int rows, cols;
    [SerializeField] public float nodeLength, nodeWidth; //ho

    [SerializeField] int totalNodes;
    //has their sizes as well

    // Start is called before the first frame update
    void Start()
    {
        totalNodes = rows * cols;

        grid = new Nodes[totalNodes];

        for (int z = 0; z < cols; z++)
        {
            for (int x = 0; x < rows; x++)
            {
                int gridSpawn = x + z * rows;

                Vector3 nodePosition = new Vector3(x * nodeLength, 0, z * nodeWidth);

                bool isBlocked = Physics.CheckBox(nodePosition, nodePosition, Quaternion.identity, 3);

                grid[gridSpawn] = new Nodes(nodePosition); //you need to do it here man
                grid[gridSpawn].Instantiation = Instantiate(node, nodePosition, node.transform.rotation); //need to make a constructor for the prefab
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
