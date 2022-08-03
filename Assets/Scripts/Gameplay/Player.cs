using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float shieldTime;
    private float shieldTimeLeft;

    [SerializeField] private Image shieldTimeImage;
    [SerializeField] private GameObject shield;
    private bool shieldEnabled = false;

    private void Update()
    {
        shieldTimeLeft -= Time.deltaTime;
        if(shieldTimeLeft > 0) shieldTimeImage.fillAmount = shieldTimeLeft / shieldTime;
        else
        {
            shieldEnabled = false;
            shieldTimeImage.gameObject.SetActive(false);
            shield.SetActive(false);
        }
    }
    private IEnumerator Die()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerMovement>().PlayDieAnim();
        yield return new WaitForSeconds(1.1f);
        GameManager.instance.Lose();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Meteor>(out Meteor meteor))
        {
            if (!shieldEnabled)
            {
                meteor.Explode();
                StartCoroutine(Die());
            }
        }
        else if(collision.TryGetComponent<Coin>(out Coin coin)) coin.Collect();
        else if(collision.TryGetComponent<ShieldBonus>(out ShieldBonus shieldBonus))
        {
            if (!shieldEnabled)
            {
                shieldEnabled = true;
                shieldTimeLeft = shieldTime;
                shieldTimeImage.gameObject.SetActive(true);
                shield.SetActive(true);
            }
            else shieldTimeLeft = shieldTime;
            shieldBonus.CollectBonus();
        }
    }
}
