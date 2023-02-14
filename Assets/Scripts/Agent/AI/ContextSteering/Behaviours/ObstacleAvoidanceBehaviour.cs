using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContextSteering
{
    public class ObstacleAvoidanceBehaviour : SteeringBehaviour
    {
        [SerializeField]
        private float radius = 2f, agentColliderSize = 0.6f;

        public override void GetSteering(MovementContext context)
        {
            foreach (Collider2D collider in context.obstacles)
            {
                Vector2 directionToCollider = collider.ClosestPoint(transform.position) - (Vector2)transform.position;
                float distanceToCollider = directionToCollider.magnitude;

                //calculate weight based on the distance Enemy<--->Obstacle
                float weight = distanceToCollider <= agentColliderSize ? 1 : (radius - distanceToCollider) / radius;

                Vector2 directionToColliderNormalized = directionToCollider.normalized;

                //Add obstacle parameters to the danger array
                for (int i = 0; i < Directions.eightDirections.Count; i++)
                {
                    float result = Vector2.Dot(directionToColliderNormalized, Directions.eightDirections[i]);

                    result = result * weight;

                    if (context.danger[i] < result)
                        context.danger[i] = result;
                }
            }
        }
    }
}