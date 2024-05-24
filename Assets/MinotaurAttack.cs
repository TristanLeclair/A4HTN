using System;
using Code.Source;
using Code.Source.Player;
using UnityEngine;

public class MinotaurAttack : MonoBehaviour
{
    public float fadeRate = 0.001f;

    private Renderer _renderer;
    private float _radius;

    private void Awake()
    {
        _radius = GameVars.Instance.minotaurRadius;
        // Set object radius
        transform.localScale = new Vector3(_radius, 1, _radius);

        _renderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // destroy attack after 0.5 seconds
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    private void Update()
    {
        // make attack fade out but never go below 0 alpha
        var color = _renderer.material.color;
        color.a = Math.Max(color.a - fadeRate, 0);
        _renderer.material.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Players")) return;

        Debug.Log("Hit player");
        other.gameObject.GetComponent<Hittable>().Hit();
    }
}