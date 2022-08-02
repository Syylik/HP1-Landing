using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float shieldTime;
    private float shieldTimeLeft;

    [SerializeField] private Image shieldTimeImage;
    [SerializeField] private GameObject shield;
    private void Update()
    {
        if(shieldTimeLeft > 0) shieldTimeImage.fillAmount = shieldTimeLeft / shieldTime;
        else shieldTimeImage.gameObject.SetActive(false);
    }
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1.1f);
        gameObject.SetActive(false);
        GameManager.instance.Lose();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Meteor>(out Meteor meteor))
        {
            meteor.Explode();
            StartCoroutine(Die());
        }
        else if(collision.TryGetComponent<Coin>(out Coin coin)) coin.Collect();
        else if(collision.TryGetComponent<Shield>(out Shield shieldComponent))
        {
            shieldTimeLeft = shieldTime;
            shieldTimeImage.gameObject.SetActive(true);
            shieldComponent.CollectBonus();
        }
    }
}
