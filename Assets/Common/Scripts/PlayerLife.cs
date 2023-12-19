using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] AudioSource deathSound;
    public static float Hp = 200;
    private bool dead = false;

    [SerializeField]
    private Text hpText;
    [SerializeField]
    private float damageAmount = 10f;
    [SerializeField]
    private float period = 1f;

   
    private void Start()
    {
        
        
            Hp = PlayerPrefs.GetFloat("PlayerHealth",200);

        if (Hp <= 0) Hp = 200;

        StartCoroutine(DamagePlayer());
    }

    private void OnDestroy()
    {

        PlayerPrefs.SetFloat("PlayerHealth", Hp);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        hpText.text = $"HP: {Hp}";
        if (transform.position.y < -10f && !dead)
        {
            Die();
        }
        if (Hp <= 0)
        {
            Die();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<PlayerMovement>().enabled = false;
        dead = true;
        deathSound.Play();

        Invoke(nameof(ReloadLevel), 1.3f);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator DamagePlayer()
    {
        while (Hp > 0)
        {
            yield return new WaitForSeconds(period);
            Hp -= damageAmount;
            Debug.Log($"HP: {Hp}");
        }
    }
}