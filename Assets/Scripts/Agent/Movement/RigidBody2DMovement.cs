using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidBody2DMovement : MonoBehaviour
{
    #region Move Var

    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 2;

    [SerializeField, Tooltip("Rot Speed")]
    float turnSpeed = 5;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 20f;

    [SerializeField, Tooltip("Deacceleration while stopped.")]
    float deceleration = 80f;

    [SerializeField, Tooltip("Stop when move input smaller than threshold")]
    float stopThreshold = 0.1f;

    [SerializeField, Tooltip("Don't rotate when face dir smaller than threshold")]
    float faceDirLengthThreshold = 0.1f;

    private Rigidbody2D rg;

    private CircleCollider2D circleCollider;

    private Vector2 velocity;

    public bool isMoving
    {
        get { return velocity != Vector2.zero; }
    }

    #endregion

    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void Move(Vector2 moveInput)
    {
        // Move Logic

        if (moveInput.magnitude >= stopThreshold)
            velocity = Vector2.MoveTowards(velocity, speed * moveInput, walkAcceleration * Time.fixedDeltaTime);
        else
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, deceleration * Time.fixedDeltaTime);

        if (velocity.magnitude <= stopThreshold)
            velocity = Vector2.zero;

        Vector2 curPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 nextPos = curPos + velocity * Time.fixedDeltaTime;

        // Block by same layer rigidbody as self layer

        Collider2D[] hits = Physics2D.OverlapCircleAll(nextPos, circleCollider.radius, 1 << gameObject.layer);

        foreach (Collider2D hit in hits)
        {
            // Ignore own collider. and trigger
            if (hit == circleCollider || hit.isTrigger == true)
                continue;

            Debug.Log(hit.gameObject);

            ColliderDistance2D colliderDistance = hit.Distance(circleCollider);

            // Ensure that we are still overlapping this collider.
            // The overlap may no longer exist due to another intersected collider
            // pushing us out of this one.
            if (colliderDistance.isOverlapped)
                nextPos += (colliderDistance.pointA - colliderDistance.pointB);
        }

        rg.velocity = Vector2.zero;
        rg.MovePosition(nextPos);
    }

    public void FaceToTarget(Vector2 target)
    {
        Vector3 faceDir = new Vector3(target.x - transform.position.x, target.y - transform.position.y, 0);

        if (faceDir.magnitude <= faceDirLengthThreshold)
            return;

        faceDir = faceDir.normalized;

        float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg;
        if (angle < 0)
            angle += 360;

        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.fixedDeltaTime);
    }

    public void FaceToTarget(Transform target)
    {
        Vector3 faceDir = target.position - transform.position;
        faceDir.z = 0;

        if (faceDir.magnitude <= faceDirLengthThreshold)
            return;

        faceDir = faceDir.normalized;

        float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg;
        if (angle < 0)
            angle += 360;

        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.fixedDeltaTime);
    }

    public void FaceToDir(Vector2 faceDir)
    {
        if (faceDir.magnitude <= faceDirLengthThreshold)
            return;

        faceDir = faceDir.normalized;

        float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg;
        if (angle < 0)
            angle += 360;

        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.fixedDeltaTime);
    }
}