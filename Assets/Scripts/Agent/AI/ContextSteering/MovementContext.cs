using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContextSteering
{
    public class MovementContext : MonoBehaviour
    {
        public float[] interest = new float[8];
        public float[] danger = new float[8];
        public float[] result = new float[8];

        public List<Transform> targets = null;
        public Collider2D[] obstacles = null;

        public Transform currentTarget;

        public Vector3 cachedPos;

        public bool hasReachedCachedPos = false;

        public int GetTargetsCount() => targets == null ? 0 : targets.Count;

        [SerializeField]
        private bool showDangerGizmos = false, showInterestGizmos = false, showResultGizmos = true;

        [SerializeField]
        private bool showCachedPosGizmos = true;

        [SerializeField]
        private float gizmosRayLength = 2f, gizmosCachedSphereRadius = 0.2f;

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                if (showDangerGizmos)
                {
                    Gizmos.color = Color.red;
                    for (int i = 0; i < danger.Length; i++)
                    {
                        Gizmos.DrawRay(
                            transform.position,
                            Directions.eightDirections[i] * danger[i] * gizmosRayLength
                            );
                    }
                }

                if (showInterestGizmos)
                {
                    Gizmos.color = Color.green;
                    for (int i = 0; i < interest.Length; i++)
                    {
                        Gizmos.DrawRay(
                            transform.position,
                            Directions.eightDirections[i] * interest[i] * gizmosRayLength
                            );
                    }
                }

                if (showResultGizmos)
                {
                    Gizmos.color = Color.blue;
                    for (int i = 0; i < result.Length; i++)
                    {
                        Gizmos.DrawRay(
                            transform.position,
                            Directions.eightDirections[i] * result[i] * gizmosRayLength
                            );
                    }
                }

                if (showCachedPosGizmos)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawSphere(cachedPos, gizmosCachedSphereRadius);
                }
            }

        }
    }

    public static class Directions
    {
        public static List<Vector2> eightDirections = new List<Vector2>{
            new Vector2(0,1).normalized,
            new Vector2(1,1).normalized,
            new Vector2(1,0).normalized,
            new Vector2(1,-1).normalized,
            new Vector2(0,-1).normalized,
            new Vector2(-1,-1).normalized,
            new Vector2(-1,0).normalized,
            new Vector2(-1,1).normalized
        };
    }
}