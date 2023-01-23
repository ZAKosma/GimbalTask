using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum GimbalModes
{
    //This enum represents the current gimbal controls we want to use
    //NOTE: All is not currently utilized
    None = 0,
    Transform = 1,
    Rotate = 2,
    Scale = 3,
    All = 4
}

public class Gimbal : MonoBehaviour
{
    #region -- Class Variables --
    // Variables to store the gimbal controls
    //Transform
    public Transform xGimbalTransform;
    public Transform yGimbalTransform;
    public Transform zGimbalTransform;
    //Rotate
    public Transform xGimbalRotate;
    public Transform yGimbalRotate;
    public Transform zGimbalRotate;
    //Scale
    public Transform xGimbalScale;
    public Transform yGimbalScale;
    public Transform zGimbalScale;

    // Variables to store the initial mouse position and the offset from the gimbal control
    private Vector3 initialMousePos;
    private Vector3 offset;

    // Variable to store the selected gimbal control
    private Transform selectedGimbal;
    public GimbalModes currentMode;
    #endregion

    private void Start()
    {
        //Sets our visual gimbal controls to match the selected mode in the Inspector
        switch (currentMode)
        {
            case GimbalModes.None:
                SetTransformGimbalActive(false);
                SetRotateGimbalActive(false);
                SetScaleGimbalActive(false);
                break;
            case GimbalModes.Transform:
                SetTransformGimbalActive(true);
                SetRotateGimbalActive(false);
                SetScaleGimbalActive(false);
                break;
            case GimbalModes.Rotate:
                SetTransformGimbalActive(false);
                SetRotateGimbalActive(true);
                SetScaleGimbalActive(false);
                break;
            case GimbalModes.Scale:
                SetTransformGimbalActive(false);
                SetRotateGimbalActive(false);
                SetScaleGimbalActive(true);
                break;
            default: //GimbalModes.All
                Debug.LogError("Can't set mode to All as it is not currently implemented. Please use Q, W, E, or R to select modes.");
                break;
        }
    }

    void Update()
    {
        #region -- GimbalControl Select --
        /*
         *  These statements control what gimbal we are looking to effect
         *  Q for none, W for transform, E for rotate, and R for scale
         *  Above hotkeys are based on the Unity default hotkeys
         */
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentMode = GimbalModes.None;
            
            SetTransformGimbalActive(false);
            SetRotateGimbalActive(false);
            SetScaleGimbalActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            currentMode = GimbalModes.Transform;
            
            SetTransformGimbalActive(true);
            SetRotateGimbalActive(false);
            SetScaleGimbalActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            currentMode = GimbalModes.Rotate;
            
            SetTransformGimbalActive(false);
            SetRotateGimbalActive(true);
            SetScaleGimbalActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            currentMode = GimbalModes.Scale;
            
            SetTransformGimbalActive(false);
            SetRotateGimbalActive(false);
            SetScaleGimbalActive(true);
        }
        #endregion

        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            
            // Store the initial mouse position
            initialMousePos = Input.mousePosition;

            // Raycast to check if a gimbal control is selected
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object is a gimbal control
                if (hit.transform.CompareTag("GimbalController"))
                {
                    selectedGimbal = hit.transform;
                    offset = selectedGimbal.position - transform.position;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            // Calculate the mouse movement delta
            // NOTE: Gimbal manipulation does not currently account for the cameras position relative to the object
            Vector3 mouseDelta = Input.mousePosition - initialMousePos;

            // Check if a gimbal control is selected
            if (selectedGimbal != null)
            {
                // Perform the appropriate transformation based on the selected gimbal control

                #region -- Transforms --
                if (currentMode == GimbalModes.Transform)
                {
                    if (selectedGimbal == xGimbalTransform)
                    {
                        Vector3 newPos = transform.position +
                                         transform.forward * ((mouseDelta.x + mouseDelta.y) * 0.01f);
                        transform.position = newPos;
                    }
                    else if (selectedGimbal == yGimbalTransform)
                    {
                        Vector3 newPos = transform.position + transform.up * ((mouseDelta.x + mouseDelta.y) * 0.01f);
                        transform.position = newPos;
                    }
                    else if (selectedGimbal == zGimbalTransform)
                    {
                        /* NOTE:
                         Multiplied by negative 1 to fix left/right up/down mouse controls as positive and negative values in Z axis
                            
                            Without multiplying by negative 1 moving the indicator left will result in a positive value change
                            This fix makes the Z axis gimbal perform as expected
                            
                            I believe this is due to how transform.right behaves, but I would need to investigate further to determine
                        */
                        Vector3 newPos = transform.position +
                                         transform.right * (-1 * ((mouseDelta.x + mouseDelta.y) * 0.01f));
                        transform.position = newPos;
                    }
                }

                #endregion
                
                #region -- Rotations --
                else if (currentMode == GimbalModes.Rotate)
                {
                    if (selectedGimbal == xGimbalRotate)
                    {
                        transform.RotateAround(transform.position, transform.right, mouseDelta.y * 0.1f);
                    }
                    else if (selectedGimbal == yGimbalRotate)
                    {
                        transform.RotateAround(transform.position, transform.up, -mouseDelta.x * 0.1f);
                    }
                    else if (selectedGimbal == zGimbalRotate)
                    {
                        transform.RotateAround(transform.position, transform.forward, mouseDelta.y * 0.1f);
                    }

                    
                }
                #endregion
                
                #region -- Scales --
                //NOTE: Currently the gimbal controls will scale with the object as the gimbal controls are child objects
                else if (currentMode == GimbalModes.Scale)
                {
                    if (selectedGimbal == xGimbalScale)
                    {
                        //NOTE: For the X and Z axis scaling takes place in the opposing axis
                        // This reflects the Unity editor's scaling controls
                        // This is more intuitive than scaling in the proper axis
                        transform.localScale =
                            new Vector3(transform.localScale.x,
                                transform.localScale.y, transform.localScale.z + ((mouseDelta.x + mouseDelta.y) * 0.01f));
                    }
                    else if (selectedGimbal == yGimbalScale)
                    {
                        transform.localScale =
                            new Vector3(transform.localScale.x,
                                transform.localScale.y + ((mouseDelta.x + mouseDelta.y) * 0.01f), transform.localScale.z);
                    }
                    else if (selectedGimbal == zGimbalScale)
                    {
                        //NOTE: For the X and Z axis scaling takes place in the opposing axis
                        // This reflects the Unity editor's scaling controls
                        // This is more intuitive than scaling in the proper axis
                        transform.localScale =
                            new Vector3(transform.localScale.x + ((mouseDelta.x + mouseDelta.y) * 0.01f),
                                transform.localScale.y, transform.localScale.z);
                    }
                }
                #endregion


                // Update the initial mouse position
                initialMousePos = Input.mousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Reset the selected gimbal control
            selectedGimbal = null;
        }
    }

    #region -- Utility functions --
    //Functions that enable or disable our gimbal control objects
    private void SetTransformGimbalActive(bool enabled = true)
    {
        xGimbalTransform.gameObject.SetActive(enabled);
        yGimbalTransform.gameObject.SetActive(enabled);
        zGimbalTransform.gameObject.SetActive(enabled);
    }
    private void SetRotateGimbalActive(bool enabled = true)
    {
        xGimbalRotate.gameObject.SetActive(enabled);
        yGimbalRotate.gameObject.SetActive(enabled);
        zGimbalRotate.gameObject.SetActive(enabled);
    }
    private void SetScaleGimbalActive(bool enabled = true)
    {
        xGimbalScale.gameObject.SetActive(enabled);
        yGimbalScale.gameObject.SetActive(enabled);
        zGimbalScale.gameObject.SetActive(enabled);
    }
    #endregion
}