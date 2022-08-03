using UnityEngine;

public class BgMover : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform backTranform;
    private float backSize;
    private float backPos;
    void Start()
    {
        backTranform = GetComponent<Transform>();
        backSize = GetComponent<SpriteRenderer>().bounds.size.y;
    }
    void Update() => Move();
    public void Move()
    {
        backPos += speed * Time.deltaTime;
        backPos = Mathf.Repeat(backPos, backSize);
        backTranform.position = new Vector3(0, backPos, 0);
    }
}