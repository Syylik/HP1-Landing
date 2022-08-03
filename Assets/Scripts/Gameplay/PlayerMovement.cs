using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0.1f, 15f)] float speed;
    private Touch firstTouch;
    private Vector2 touchPos;
    private Vector2 dir;

    private enum Facing { FORWARD, LEFT, RIGHT }
    private Facing facing;

    private Rigidbody2D rb;
    private Animator anim;

    public static readonly int isMoveAnim = Animator.StringToHash("isMoving");
    public static readonly int dieAnim = Animator.StringToHash("die");
    private void Start()
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
            anim.SetBool(isMoveAnim, true);
            
            ChangeFacing();
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        if(firstTouch.phase == TouchPhase.Ended)
        {
            facing = Facing.FORWARD;
            anim.SetBool(isMoveAnim, false);
            rb.velocity = Vector2.zero;
        }
    }
    private void ChangeFacing()
    {
        if(dir.x < 0) facing = Facing.LEFT;
        if(dir.x > 0) facing = Facing.RIGHT;
        var Scale = transform.localScale;
    
        if(facing == Facing.LEFT)
        {
            if(Scale.x < 0) Scale.x *= -1f;
        }
        else if(facing == Facing.RIGHT)
        {
            if(Scale.x > 0) Scale.x *= -1f;
        }

        transform.localScale = Scale;
    }
    public void PlayDieAnim() => anim.SetTrigger(dieAnim);
}
