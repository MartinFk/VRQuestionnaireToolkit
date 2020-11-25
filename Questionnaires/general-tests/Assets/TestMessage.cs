using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMessage : MonoBehaviour
{
    public bool toggle;

    private void OnValidate()
    {
        if (toggle)
        {
            Debug.Log("toggle is on.");
        }
        else
        {
            Debug.Log("toggle is off.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
