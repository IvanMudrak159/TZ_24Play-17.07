using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject _losePanel;

    private void Awake()
    {
        _player.OnLose.AddListener(OnLose);
    }

    private void OnLose()
    {
        _losePanel.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
