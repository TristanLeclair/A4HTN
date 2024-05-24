using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Code.Scripts.Managers;
using Code.Source.Player;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private float _timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Players")) return;
        if (_timer < 3f) _timer += Time.deltaTime;
        else
        {
            _timer = 0f;
            WorldState.Instance.GrabTreasure();
            other.GetComponent<Player>().GrabTreasure();
        }
    }
}
