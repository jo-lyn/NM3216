﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FallingShape : MonoBehaviour
{
    public Stacker stacker;
    public float fallSpeed;

    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fallSpeed = GameController.instance.GetFallSpeed();
        Vector2 direction = transform.rotation * new Vector2(0, -1 * fallSpeed);
        rb2d.MovePosition(rb2d.position + direction * Time.fixedDeltaTime);

    }

    void Update()
    {
        /**
        fallSpeed = 10.0f * Mathf.Exp(0.01f * Time.timeSinceLevelLoad);

        // Cap maximum speed at 40
        if (fallSpeed > 40.0f)
        {
            fallSpeed = 40.0f;

        }
        */
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        fallSpeed = 0;
        Destroy(gameObject);
    }
}
