using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    [Header("要關閉的石像們")]
    public GameObject[] stones;

    // OTE
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "綿羊")
        {
            stones[0].SetActive(false);
            stones[1].SetActive(false);
        }
    }
}
