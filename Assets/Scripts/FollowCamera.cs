using System;
using System.Numerics;
using System.Timers;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private Transform _cam;

    [SerializeField] [Range(0, 1)] private float _lerpFactor = 0.5f;

    private void Start()
    {
        _cam = transform;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 camPosition = _cam.position;
        Vector3 playerPosition = _player.position;
        Vector3 playerWithOffset = new Vector3(playerPosition.x, playerPosition.y, camPosition.z);

        camPosition = Vector3.Lerp(camPosition, playerWithOffset, _lerpFactor * Time.deltaTime);
        _cam.position = camPosition;
    }
}
