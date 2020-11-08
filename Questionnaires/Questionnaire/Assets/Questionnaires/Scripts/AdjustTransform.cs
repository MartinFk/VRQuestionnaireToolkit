using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustTransform : MonoBehaviour
{
    void Update()
    {
        // Press + to scale up
        if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus))
            this.transform.localScale = Vector3.Scale(this.transform.localScale, new Vector3(1.1f, 1.1f, 1.0f));
        // Press - to scale down
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
            this.transform.localScale = Vector3.Scale(this.transform.localScale, new Vector3(0.9f, 0.9f, 1.0f));
        // Press 0 to reset
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
