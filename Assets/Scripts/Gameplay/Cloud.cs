using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float randOffset;
    [SerializeField] private Sprite[] cloudSpriteVariations;

    private MovingObject move;
    private SpriteRenderer sp;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        move = GetComponent<MovingObject>();
    }
    private void Start()
    {
        sp.sprite = Utils.GetRandInArray(cloudSpriteVariations);
        if(Random.Range(0, 5) == 5) sp.sortingOrder = 5;
        else sp.sortingOrder = 0;
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, Random.Range(0f, 0.8f));

        transform.position = new Vector3(transform.position.x, transform.position.y + Random.Range(-randOffset, randOffset) ,transform.position.z);
        if(transform.position.x > 0) move.moveSpeed *= -1f;
    }
}
