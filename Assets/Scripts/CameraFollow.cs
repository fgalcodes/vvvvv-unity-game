using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;

    private GameObject _player;
    private Transform target;

    private Vector3 vel = Vector3.zero;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        target = _player.transform;
    }
    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
    }
}
