using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContextSteering
{
    public abstract class SteeringBehaviour : MonoBehaviour
    {
        public abstract void GetSteering(MovementContext context);
    }
}
