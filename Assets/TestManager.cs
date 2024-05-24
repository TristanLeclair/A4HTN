using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject minotaurAttackPrefab;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // on click, spawn attack
        if (!Input.GetMouseButtonDown(0)) return;

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (!Physics.Raycast(ray, out var hit)) return;

        var attack = Instantiate(minotaurAttackPrefab, hit.point,
            Quaternion.identity);
    }
}