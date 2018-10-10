﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Remoting.Channels;
using UnityEngine;

public class Stacker : MonoBehaviour
{
    public GameObject[] shapes;
    private Stack stack;
    private string topShape;
    private int numShapesCleared;

    private void Awake()
    {
        stack = new Stack();
    }
    // Use this for initialization
    void Start()
    {
        stack.Push(gameObject);
        topShape = gameObject.tag;
        numShapesCleared = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            StackShape("Triangle");
        }
        if (Input.GetKeyDown("p"))
        {
            PopShape();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("incoming: " + other.tag);
        //Debug.Log("top: " + topShape);
        if (other.CompareTag(topShape))
        {
            PopShape();
        }
        else
        {
            //Debug.Log("STACKING");
            StackShape(other.tag);
        }
    }

    public int GetStackCount()
    {
        return stack.Count;
    }

    public int GetNumShapesCleared()
    {
        return numShapesCleared;
    }

    public void StackShape(string tag)
    {
        GameObject newShape;
        //Debug.Log("SEARCHING FOR: " + tag);

        foreach (GameObject shape in shapes)
        {
            if (shape.CompareTag(tag))
            {
                newShape = Instantiate(shape, transform.position, transform.rotation);
                newShape.transform.Translate(Vector3.up * transform.localScale.y * stack.Count);
                newShape.transform.SetParent(gameObject.transform);
                stack.Push(newShape);

                topShape = tag;
                numShapesCleared++;
                UpdateCollider();
            }
        }
    }

    public void PopShape()
    {
        if (stack.Count > 1)
        {
            GameObject toPop = (GameObject)stack.Pop();
            Destroy(toPop);

            topShape = ((GameObject)stack.Peek()).tag;
            UpdateCollider();
        }
    }

    private void UpdateCollider()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(1, stack.Count);
        GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.5f * (stack.Count - 1));
    }
}
