using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace ContextSteering
{
    public class AgentMovementAI : MonoBehaviour
    {
        [SerializeField]
        private MovementContext context;

        [SerializeField]
        private ContextSolver movementDirectionSolver;

        [SerializeField]
        private List<SteeringBehaviour> steeringBehaviours;

        [SerializeField]
        private List<Detector> detectors;

        [SerializeField]
        private float detectionDelay = 0.05f, solveDelay = 0.05f;

        [SerializeField]
        private float cachedPosReachThreshold = 0.5f, cachedPosRefreshThreshold = 0.1f;

        public UnityEvent<Vector2> OnMovementInput;

        [SerializeField]
        private Vector2 movementInput;

        [SerializeField]
        private bool movable = true;

        private void Start()
        {
            //Detecting Player and Obstacles around
            InvokeRepeating("PerformDetection", 0, detectionDelay);
            InvokeRepeating("SolveContext", 0, solveDelay);
        }

        private void PerformDetection()
        {
            foreach (Detector detector in detectors)
            {
                detector.Detect(context);
            }

            context.targets.OrderBy(target => (target.position - transform.position).magnitude);
            context.currentTarget = context.targets.FirstOrDefault();

            // if target pos change largely
            if (context.GetTargetsCount() > 0)
                if ((context.currentTarget.position - context.cachedPos).magnitude >= cachedPosRefreshThreshold)
                {
                    context.cachedPos = context.currentTarget.position;
                    context.hasReachedCachedPos = false;
                    return;
                }

            // if target reach pos
            if ((context.cachedPos - transform.position).magnitude >= cachedPosReachThreshold)
            {
                context.hasReachedCachedPos = false;
            }
            else
            {
                context.hasReachedCachedPos = true;
            }
        }

        private void SolveContext()
        {
            movementInput = movementDirectionSolver.GetDirectionToMove(steeringBehaviours, context);
            Vector2 verticalDir = Vector3.Cross(movementInput, Vector3.forward);
            movementInput = (movementInput + verticalDir).normalized;
        }

        private void FixedUpdate()
        {
            if (movable && !context.hasReachedCachedPos)
            {
                OnMovementInput?.Invoke(movementInput);
            }
        }

        public void SetMovable(bool state)
        {
            movable = state;
        }
    }
}