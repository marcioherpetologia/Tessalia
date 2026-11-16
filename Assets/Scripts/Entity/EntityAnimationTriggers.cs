using System;
using UnityEngine;

public class EntityAnimationTriggers : MonoBehaviour
{
    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Player>();
    }

    private void CurrentStateTrigger()
    {
        entity.CallAnimationTrigger();
    }
}
