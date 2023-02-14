using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentCombatAI : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private LayerMask targetLayer;

    [SerializeField]
    private float attackRadius = 1.25f;

    [SerializeField]
    private float tryAttackDelay = 1f, attackDelay = 0.3f;

    public UnityEvent<Transform> OnAttackBegin;

    public UnityEvent OnAttackEnd;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TryAttack", 0, tryAttackDelay);
    }

    private void TryAttack()
    {
        Collider2D targetCollider =
                Physics2D.OverlapCircle(transform.position, attackRadius, targetLayer);
        if(targetCollider!= null)
        {
            target = targetCollider.transform;
            StartCoroutine(AttackCo());
        }
    }

    private IEnumerator AttackCo()
    {
        OnAttackBegin?.Invoke(target);
        yield return new WaitForSeconds(attackDelay);
        OnAttackEnd?.Invoke();
        yield return null;
    }
}
