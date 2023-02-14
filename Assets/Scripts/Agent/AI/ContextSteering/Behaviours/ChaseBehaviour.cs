using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ContextSteering
{
    public class ChaseBehaviour : SteeringBehaviour
    {
        public override void GetSteering(MovementContext context)
        {
            Vector2 directionToTarget = context.cachedPos - transform.position;

            for (int i = 0; i < context.interest.Length; i++)
            {
                float result = Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]);
                if(context.interest[i] < result)
                    context.interest[i] = result;
            }
        }
    }
}