using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenuStartGame : MonoBehaviour
{
    [SerializeField] public GameObject doorWithAnimator;
    [SerializeField] public GameObject blackScreenPrefab;
    [SerializeField] public Transform pointToGo;

    [SerializeField] private GameObject[] otherObjectToDeactivate;

    public void StartGame()
    {
        for (int i = 0; i < otherObjectToDeactivate.Length; i++)
        {
            otherObjectToDeactivate[i].SetActive(false);
        }
        gameObject.transform.DOMove(pointToGo.position, 4f).SetEase(Ease.Linear);
        Invoke("OpenTheDoor",2f);
        Invoke("BlackScreenInstantiate",3f);
        Invoke("LoadLevel",5f);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenTheDoor()
    {
        doorWithAnimator.GetComponent<Animator>().SetBool("Open",true);
    }

    public void BlackScreenInstantiate()
    {
        Instantiate(blackScreenPrefab);
    }
}
