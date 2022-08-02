using UnityEngine;

public class Meteor : MonoBehaviour
{

	[SerializeField, Range(-15f, 15f)] private float moveSpeed = 5f;
	[SerializeField, Range(5f, 50f)] private float frequency = 20f;
	[SerializeField, Range(0.1f, 10f)] private float magnitude = 0.5f;
    [SerializeField, Range(0f, 15f)] private float timeToDestroy = 10f;

	private bool facingRight = true;

	private Vector3 pos, localScale;
	private void Start()
	{
		Destroy(gameObject, timeToDestroy);
		pos = transform.position;
		localScale = transform.localScale;

		CheckWhereToFace();
	}
    private void Update()
    {
		if(facingRight) MoveRight();
		else MoveLeft();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Shield"))
        {
			Explode();
		}
    }
    public void Explode()
    {
		Destroy(gameObject);
	}
    void CheckWhereToFace()
	{
		if(pos.x < -0) facingRight = true;
		else if(pos.x > 0) facingRight = false;

		bool condition = (facingRight && localScale.x < 0) || (!facingRight && localScale.x > 0);
		if(condition) localScale.x *= -1;

		transform.localScale = localScale;
	}

	void MoveRight()
	{
		pos += transform.right * Time.deltaTime * moveSpeed;
		transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
		transform.Translate(Vector2.up * moveSpeed);
	}
	void MoveLeft()
	{
		pos -= transform.right * Time.deltaTime * moveSpeed;
		transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
		transform.Translate(Vector2.up * moveSpeed);
	}

}