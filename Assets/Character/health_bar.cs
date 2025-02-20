using UnityEngine;
using UnityEngine.UI;

public class health_bar : MonoBehaviour
{
    public GameObject[] hearts;
    public void updateHearts(int current_heart)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (current_heart <= i)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
}
