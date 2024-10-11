using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.PLAYER_TAG))
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        ScoreManager.Instance.AddCoins(coinValue);
        Destroy(gameObject);
    }
}
