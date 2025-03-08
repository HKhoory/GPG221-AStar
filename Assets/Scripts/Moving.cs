using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{

    [SerializeField] private bool hasReached;
    [SerializeField] private float speed;

    [SerializeField] private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        hasReached = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasReached)
        {
            _rb.velocity = new Vector3(speed * Time.deltaTime, _rb.velocity.y, 0);
        }
        else
        {
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        }
    }
}
