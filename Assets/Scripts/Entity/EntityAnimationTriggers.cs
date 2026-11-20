using System;
using UnityEngine;

public class EntityAnimationTriggers : MonoBehaviour
{
    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    private void CurrentStateTrigger()
    {
        entity.CallAnimationTrigger();
        print("chamou");
    }
}
