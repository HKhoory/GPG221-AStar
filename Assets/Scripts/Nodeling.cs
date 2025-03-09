using System;
using TMPro;
using UnityEngine;


public class Nodeling : IComparable
{

    public Nodeling nextNode; //defines the next node the AI will travel to using the A* algorithm
    public bool IsBlocked { get; private set; } //determines if the AI can walk in this node
    public int version = 0; //stores the number of iterations the node went through calculating the f cost

    public Vector3 worldPosition { get; set; }
    public Vector3Int gridPosition { get; set; } //it was int

    private int gCost, hCost, fCost;

    //have the debug thingy in here

#if ASTAR_DEBUG

    GameObject instantiation;
    TMP_Text gCostText;
    TMP_Text hCostText;
    TMP_Text fCostText;
    TMP_Text versionText;

    public GameObject Instantiation //for external function use to modify values
    {
        get
        {
            return instantiation;
        }

        set
        {

            instantiation = value;
            gCostText = instantiation.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            hCostText = instantiation.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
            fCostText = instantiation.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>();
            versionText = instantiation.transform.GetChild(0).GetChild(3).GetComponent<TMP_Text>();

        }

    }
#endif


    public Nodeling(Vector3 poistion, Vector3Int gridPos, bool isBlocked)
    {
        worldPosition = poistion;
        gridPosition = gridPos;
        IsBlocked = isBlocked;
    }
    

    public int GCost
    {
        get { return gCost; }

        set {
#if ASTAR_DEBUG
            gCostText.text = value.ToString();
            fCostText.text = FCost.ToString();
#endif
            gCost = value; 
        }
    }

    public int HCost
    {
        get { return hCost; }

        set {

#if ASTAR_DEBUG
            hCostText.text = value.ToString();
            fCostText.text = FCost.ToString();
#endif
            hCost = value;
        }
    }

    public int FCost
    {
        get { return gCost + hCost; }

    }

    public int Version
    {
        get { return version; }

        set {  versionText.text = value.ToString();}

    }

    public int CompareTo(object obj)
    {
        Nodeling nodes = (Nodeling)obj;

        if (nodes.fCost > fCost)
        {
            return -1;
        }
        else return 1;
    }

}
