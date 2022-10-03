using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MovementSpeed = 1; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position +=
            new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * MovementSpeed;
    }
}
