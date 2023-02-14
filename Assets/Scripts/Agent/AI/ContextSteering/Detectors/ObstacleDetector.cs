using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContextSteering
{
    public class ObstacleDetector : Detector
    {
        [SerializeField]
        private float detectionRadius = 2;

        [SerializeField]
        private LayerMask layerMask;

        [SerializeField]
        private bool showGizmos = true;

        Collider2D[] colliders;

        public override void Detect(MovementContext context)
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, layerMask);
            context.obstacles = colliders;
        }

        private void OnDrawGizmos()
        {
            if (showGizmos == false)
                return;
            if (Application.isPlaying && colliders != null)
            {
                Gizmos.color = Color.red;
                foreach (Collider2D obstacleCollider in colliders)
                {
                    Gizmos.DrawSphere(obstacleCollider.transform.position, 0.2f);
                }
            }
        }
    }
}