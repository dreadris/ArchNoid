using UnityEngine;

public interface IMoveRestrictor
{
    Vector2 Restrict(Vector2 newPosition, Vector2 oldPosition);
}
