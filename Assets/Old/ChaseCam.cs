﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ChaseCam : MonoBehaviour
{
    public float chaseDistance;
    public float chaseFloat;

    public float followResistance;

    private Camera cam;
    private Transform target;

    private bool initialized;

    [Header("Camera Reaction Shake Properties")]
    public float duration;
    public float strength;
    public int vibrado;
    public float randomness;

    private Tween currentTween;

    void Awake()
    {
        EventManager.CollisionReaction += CameraShake;
    }

    public void Init(Transform parent, Transform chaseTarget)
    {
        transform.SetParent(parent);
        target = chaseTarget;

        Vector3 targetPos = target.position;

        targetPos.z = targetPos.z - chaseDistance;
        targetPos.y = targetPos.y + chaseFloat;

        transform.position = targetPos;

        Quaternion lookRot = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = lookRot;

        initialized = true;
    }

    void Update()
    {
        if (initialized)
        {
            Vector3 targetPos = target.position;

            targetPos.z = targetPos.z - chaseDistance;
            targetPos.y = targetPos.y + chaseFloat;

            transform.position = Vector3.Lerp(transform.position, targetPos, followResistance);

            //Quaternion lookRot = Quaternion.LookRotation(target.position - transform.position);
            //lookRot = Quaternion.Euler(0, 0, -lookRot.eulerAngles.y);
            //transform.rotation = lookRot;
        }
    }

    void CameraShake()
    {
        currentTween = transform.DOShakePosition(duration, strength, vibrado, randomness);
    }
}
