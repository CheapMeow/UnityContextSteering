using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ContextSteering
{
    public class ContextSolver : MonoBehaviour
    {
        public Vector2 GetDirectionToMove(List<SteeringBehaviour> behaviours, MovementContext context)
        {
            Array.Clear(context.danger, 0, context.danger.Length);
            Array.Clear(context.interest, 0, context.interest.Length);
            Array.Clear(context.result, 0, context.result.Length);

            //Loop through each behaviour
            foreach (SteeringBehaviour behaviour in behaviours)
            {
                behaviour.GetSteering(context);
            }

            //get the average direction
            Vector2 outputDirection = Vector2.zero;
            for (int i = 0; i < 8; i++)
            {
                context.result[i] = context.interest[i] - context.danger[i];
                outputDirection += Directions.eightDirections[i] * context.result[i];
            }

            outputDirection.Normalize();

            //return the selected movement direction
            return outputDirection;
        }
    }
}
