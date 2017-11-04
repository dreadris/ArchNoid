using UnityEngine;

public class MoveRestrictor : MonoBehaviour, IMoveRestrictor
{
    [SerializeField]
    private Collider2D _collider;

    private RaycastHit2D[] raycastHits = new RaycastHit2D[1];
    [SerializeField]
    private float raycastDistance = 20f;


    public Vector2 Restrict(Vector2 newPosition, Vector2 oldPosition)
    {
#if DEBUG_MOVE_RESTRICTOR
        var rayStart = new Vector3(_collider.transform.position.x, _collider.transform.position.y, _collider.transform.position.z);
        var rayEnd = new Vector3(newPosition.x, newPosition.y, _collider.transform.position.z);
        Debug.DrawRay(rayStart, rayEnd - rayStart);
#endif
        if (_collider.bounds.Contains(new Vector3(newPosition.x, newPosition.y, _collider.bounds.center.z)))
            return newPosition;
        else
        {
            var rayCastDirection = (oldPosition - newPosition).normalized;
            var numContacts = Physics2D.RaycastNonAlloc(newPosition, rayCastDirection, raycastHits, raycastDistance, 1 << _collider.gameObject.layer);

            if (numContacts > 0)
                return raycastHits[0].point;
            else
            {
                var restrictedPosition = _collider.bounds.ClosestPoint(new Vector3(newPosition.x, newPosition.y, _collider.bounds.center.z));
                return new Vector2(restrictedPosition.x, restrictedPosition.y);
            }
        }
    }

}
