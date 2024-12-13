using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bida : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float force = 50f;
    [SerializeField] private float maxRange = 5f;
    
    private Vector2 distance;
    private Vector2 pointEnd;
    private Vector2 pointStart;
    
    public LineRenderer lineArrow;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        lineArrow.positionCount = 2;
        ResetLineArrow();
        lineArrow.enabled = false;
    }

    private void OnMouseDown()
    {
        ResetLineArrow();
        lineArrow.enabled = true;
    }

    private void OnMouseDrag()
    {
        pointStart = new Vector2(transform.position.x, transform.position.y);
        lineArrow.SetPosition(0, pointStart);
        
        pointEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        distance = pointEnd - pointStart;
        
        if (distance.magnitude > maxRange)
        {
            distance = distance.normalized * maxRange;
            lineArrow.SetPosition(1, pointStart + distance);
        }
        else
        {
            lineArrow.SetPosition(1, pointEnd);
        }
    }

    private void OnMouseUp()
    {
        rb.AddForce(-1 * distance * force, ForceMode2D.Impulse);
        lineArrow.enabled = false;
    }

    private void ResetLineArrow()
    {
        lineArrow.SetPosition(0, transform.position);
        lineArrow.SetPosition(1, transform.position);
    }
}
