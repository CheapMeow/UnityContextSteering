using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContextSteering
{
    public class TargetDetector : Detector
    {
        [SerializeField]
        private float targetDetectionRange = 5;

        [SerializeField]
        private LayerMask obstaclesLayerMask, targetLayerMask;

        [SerializeField]
        private bool showGizmos = false;

        //gizmo parameters
        private List<Transform> colliders;

        public override void Detect(MovementContext context)
        {
            //Find out if targets is near
            Collider2D[] targetColliders =
                Physics2D.OverlapCircleAll(transform.position, targetDetectionRange, targetLayerMask);

            colliders = new List<Transform>();

            if (targetColliders != null)
            {
                foreach (Collider2D targetCollider in targetColliders)
                {
                    //Check if you see the target
                    Vector2 direction = (targetCollider.transform.position - transform.position).normalized;
                    RaycastHit2D[] hits =
                        Physics2D.RaycastAll(transform.position, direction, targetDetectionRange, obstaclesLayerMask | targetLayerMask);

                    bool canSee = true;

                    foreach(RaycastHit2D hit in hits)
                    {
                        if (1 << hit.collider.gameObject.layer == obstaclesLayerMask.value)
                        {
                            canSee = false;
                            break;
                        }
                    }

                    if(canSee)
                        colliders.Add(targetCollider.transform);
                }
            }

            context.targets = colliders;
        }

        private void OnDrawGizmosSelected()
        {
            if (showGizmos == false)
                return;

            Gizmos.DrawWireSphere(transform.position, targetDetectionRange);

            if (colliders == null)
                return;
            Gizmos.color = Color.magenta;
            foreach (var item in colliders)
            {
                Gizmos.DrawSphere(item.position, 0.3f);
            }
        }
    }
}