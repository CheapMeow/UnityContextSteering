# UnityContextSteering

## Main Logic

Firstly, you should decide what vars does the ai concern, then assign these vars in MovementContext.

Then use Detectors to set these vars at runtime.

Secondly, you should decide custom Behaviour to update (danger, interest) vector lists.

While custom Behaviours mostly are based on vars in MovementContext.

Finally, you call ContextSolver to handle (danger, interest) vector lists, return move dir.

There is eventually a script, such as AgentAI, to control when to detect, to call ContextSolver, to move, to attack.

### Movement Context

Movement Context only contains float array that represent weights on eight ways.

```csharp
public float[] interest = new float[8];
public float[] danger = new float[8];
public float[] result = new float[8];
```

### Steering Behaviour

Simple Chase Bahaviour is get a target and go to it. It caculate a vector to target and input it into context solver.

```csharp
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
```

But if you input a vector vertical to `directionToTarget`, the result movement may not be a circle.

```csharp
Vector2 verticalDir = Vector3.Cross(directionToTarget, Vector3.forward);
```

Becuase when you dot eight ways and input vector, the result movement is a synthesis of eight vectors but not a decomposition of the origin vector.

So it shows what is difference between common movement and the way of context solver. You can't simply input a vector to context solver and expect movement is precisely along the input.

### Context Solver

Context Solver simply use interest and danger arrays to get movement direction. This should be a very general method.

```csharp
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
```

For clean code, Context Solver should output a normalized movement vector, there should be other controller script use this output to move. So things like smoothing movement is not duty of Context Solver.

## More Custom Behaviour

You should get custom context by detector, and use these context in custom behaviour.

Or you can control when to call context solver in a custom controller script, and so on.

For example, you want enemies can dodge player's projectiles, a natural way is to set projectiles to obstacle layer, so projectiles is incorporated into regular obstacle avoidance behaviorml. Also, you can set a box collider for player as player projectiles' track, and set this box collider to obstacle layer. Then when your player's mouse is aiming at enemies, they will fly away.

Further more, if you want to chase something, set it to target layer; if you want to avoid something, set it to obstacle layer. In the best form, you only consider two things: target and obstacles, then you only need two Steering Behaviours and two Detectors.

What is subtle is how to weight difference contributes to interest and danger array, and how to caculate, such as sum array up then normalize them, or simply get the maximum.

## Extend to 3D

Different 3D logic determine ways to extend. If game world is 3D and combat is in 2D, you only need 8 way directions, but if combat is 3D, such as space ship, you may need 14 way directions. So I don't implement 3D in this repository.

## Reference

1. http://www.gameaipro.com/GameAIPro2/GameAIPro2_Chapter18_Context_Steering_Behavior-Driven_Steering_at_the_Macro_Scale.pdf

2. https://youtu.be/6BrZryMz-ac

3. https://github.com/SunnyValleyStudio/Unity-2D-Context-steering-AI

4. https://github.com/sturdyspoon/unity-movement-ai