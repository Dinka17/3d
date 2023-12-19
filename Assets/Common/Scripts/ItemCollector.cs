using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int coinsCollected = 0;
    [SerializeField]
    private Text coinsText;

    [SerializeField] AudioSource collectionSound;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Collectable"))
        {
            Destroy(collider.gameObject);
            coinsCollected++;
            PlayerLife.Hp += 10;
            coinsText.text = $"Coins: {coinsCollected}";
            collectionSound.Play();
        }
    }
}
