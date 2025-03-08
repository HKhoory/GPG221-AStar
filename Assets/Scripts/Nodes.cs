using System;
using UnityEngine;


public class Nodes : IComparable
{

    public Nodes nextPath; //defines the next node the AI will travel to using the A* algorithm

    public bool isBlocked; //determines if the AI can walk in this node
    public int version; //stores the number of iterations the node went through calculating the f cost

    public Vector3 worldPosition { get; set; }
    public Vector3Int gridPosition { get; set; }

    public int gCost, hCost, fCost;



    GameObject instantiation;

    public GameObject Instantiation //for external function use to modify values
    {
        get
        {
            return instantiation;
        }

        set
        {

            instantiation = value;

        }

    }


    //private void Start()
    //{
     //Physics.BoxCast(); //this function
    //}


    public Nodes(Vector3 poistion)
    {
        worldPosition = poistion;
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            isBlocked = true;
        }
    }
    */

    public int CompareTo(object obj)
    {
        Nodes nodes = (Nodes)obj;

        if (nodes.fCost > fCost)
        {
            return -1;
        }
        else return 1;
    }

}
