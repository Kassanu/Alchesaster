using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    public GameObject victory;
    public GameObject dead;
    public GameObject floorsCleared;

    private void Start() {
        GameManager manager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        this.floorsCleared.GetComponent<TMPro.TextMeshProUGUI>().text = manager.floorsCleared.ToString();
        if (manager.playerDead) {
            this.dead.SetActive(true);
        } else {
            this.victory.SetActive(true);
        }
    }

    public void restartGame() {
        DestroyImmediate(GameObject.FindWithTag("GameController"));
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}