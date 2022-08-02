using UnityEngine;

public class Coin : MonoBehaviour
{
    public void Collect()
    {
        GameManager.instance.AddCoin();
        Destroy(gameObject);
    }
}
