using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody rbody;

    private float x;
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        rbody.velocity = new Vector3(x * 3f, rbody.velocity.y);
    }
}
