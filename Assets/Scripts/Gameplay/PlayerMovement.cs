using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0.1f, 15f)] float speed;
    private Touch firstTouch;
    private Vector2 touchPos;
    private Vector2 dir;

    private Rigidbody2D rb;
    private Animator anim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            firstTouch = Input.GetTouch(0);
            touchPos = Camera.main.ScreenToWorldPoint(firstTouch.position);  
            dir = touchPos - (Vector2)transform.position;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        if(firstTouch.phase == TouchPhase.Ended) rb.velocity = Vector2.zero;
    }
}
