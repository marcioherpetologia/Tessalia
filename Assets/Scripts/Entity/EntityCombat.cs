using System;
using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    
    public float damage = 10f;
    
    [Header("TargetDetection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    private Collider2D[] GetDetectionColliders()
    {
        return Physics2D.OverlapCircleAll(transform.position, targetCheckRadius, whatIsTarget);
    }

    public void PerformAttack()
    {
        foreach (var target in GetDetectionColliders())
        {
            EntityHealth targetHealth = target.GetComponent<EntityHealth>();
            targetHealth?.TakeDamage(damage,transform);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
