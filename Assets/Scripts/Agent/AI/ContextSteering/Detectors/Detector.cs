using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Firstly, you should depend what vars does the ai concern, then assign these vars in MovementContext</para>
/// <para>Then use Detectors to set these vars at runtime</para>
/// <para>Secondly, you should depend custom behaviour to update (danger, interest) vector lists</para>
/// <para>While custom behaviours mostly are based on vars in MovementContext</para>
/// <para>There is an implicit agreement that you should perform "update vector list" in each behaviours</para>
/// <para>But not "store vector lists from several behaviours and handle them at last"</para>
/// <para>Finally, you call ContextSolver to handle (danger, interest) vector lists, return move dir</para>
/// <para>There is eventually a script, such as AgentAI, to control when to detect, to call ContextSolver, to move, to attack</para>
/// </summary>
namespace ContextSteering
{
    public abstract class Detector : MonoBehaviour
    {

        public abstract void Detect(MovementContext context);
    }
}