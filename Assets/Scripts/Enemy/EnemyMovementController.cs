using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    public Transform enemy;
    public Transform startPosition;
    public Transform endPosition;

    [SerializeField] private float constantSpeed = 1.0f;

    private void Update()
    {
        try
        {
            float step = constantSpeed * Time.deltaTime;
            enemy.position = Vector2.MoveTowards(enemy.position, CurrentMovementTarget(), step);

            if (Vector2.Distance(enemy.position, endPosition.position) < 0.01f)
            {
                SwapTargets();
            }
            else if (Vector2.Distance(enemy.position, startPosition.position) < 0.01f)
            {
                SwapTargets();
            }

        }
        catch (System.Exception) {

        
        }
    }

    Vector2 CurrentMovementTarget()
    {
        if (Vector2.Distance(enemy.position, startPosition.position) < 0.01f)
        {
            return endPosition.position;
        }
        else
        {
            return startPosition.position;
        }
    }

    void SwapTargets()
    {
        Vector2 temp = startPosition.position;
        startPosition.position = endPosition.position;
        endPosition.position = temp;
    }

    private void OnDrawGizmos()
    {
        if (enemy != null && startPosition != null && endPosition != null)
        {
            Gizmos.DrawLine(enemy.transform.position, startPosition.position);
            Gizmos.DrawLine(enemy.transform.position, endPosition.position);
        }
    }

}

