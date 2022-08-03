using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float shieldTime;
    private float shieldTimeLeft;
    private bool needPlayShieldSound = false;
    [SerializeField] private AudioSource shieldGetSound;
    [SerializeField] private AudioSource shieldBrokeSound;

    [SerializeField] private Image shieldTimeImage;
    [SerializeField] private GameObject shield;

    private bool shieldEnabled = false;

    [SerializeField] private AudioSource deadSound;

    private void Update()
    {
        if(shieldEnabled) shieldTimeLeft -= Time.deltaTime;
        if(shieldTimeLeft > 0) shieldTimeImage.fillAmount = shieldTimeLeft / shieldTime;
        else
        {
            if (needPlayShieldSound)
            {
                shieldBrokeSound.pitch = Random.Range(0.9f, 1.1f);
                shieldBrokeSound.Play();
                needPlayShieldSound = false;
            }

            shieldEnabled = false;
            shieldTimeImage.gameObject.SetActive(false);
            shield.SetActive(false);
        }
    }
    private IEnumerator Die()
    {
        deadSound.Play();
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
            shieldGetSound.pitch = Random.Range(0.9f, 1.1f);
            shieldGetSound.Play();
            if (!shieldEnabled)
            {
                

                shieldEnabled = true;
                needPlayShieldSound = true;
                shieldTimeLeft = shieldTime;
                shieldTimeImage.gameObject.SetActive(true);
                shield.SetActive(true);
            }
            else shieldTimeLeft = shieldTime;
            shieldBonus.CollectBonus();
        }
    }
}
