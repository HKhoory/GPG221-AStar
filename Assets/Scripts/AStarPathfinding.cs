using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{

    Gridlet grids;

    List<Nodes> pathList = new List<Nodes>();

    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 goalPosition;

    Nodes startNode;
    Nodes goalNode;
    Nodes currentNode;

    //main algorithm
    //has lists to store nodes to know paths


    // Start is called before the first frame update
    void Start()
    {
        grids.GetComponent<Gridlet>();

        //startNode = 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
