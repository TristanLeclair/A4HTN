using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LookAtCamera : MonoBehaviour
{
    private Camera _camera; 
    
    // Start is called before the first frame update
    private void Start()
    {
        Assert.IsNotNull(Camera.main, "Camera.main != null"); 
        _camera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        var transform1 = transform;
        transform1.LookAt(_camera.transform);
        transform1.rotation = _camera.transform.rotation;
    }
}
