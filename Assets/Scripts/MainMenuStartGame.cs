using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenuStartGame : MonoBehaviour
{
    [SerializeField] public GameObject doorWithAnimator;
    [SerializeField] public GameObject blackScreenPrefab;
    [SerializeField] public Transform pointToGo;

    [SerializeField] private GameObject[] otherObjectToDeactivate;

    [Header("Audio")] 
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip startClip;
    [SerializeField] private AudioClip doorOpenClip;
    [SerializeField] private AudioClip swooshClip;
    [SerializeField] private AudioSource playerSource;

    [SerializeField] public int savedLevelNumber = 1;

    public void StartGame()
    {
        Debug.Log("current saved level" + " " + PlayerPrefs.GetInt("savedLevelNumber"));

        for (int i = 0; i < otherObjectToDeactivate.Length; i++)
        {
            otherObjectToDeactivate[i].SetActive(false);
        }
        gameObject.transform.DOMove(pointToGo.position, 4f).SetEase(Ease.Linear);
        source.PlayOneShot(startClip);
        Invoke("OpenTheDoor",2f);
        Invoke("BlackScreenInstantiate",3f);
        Invoke("LoadLevel",5f);
        playerSource.PlayOneShot(swooshClip);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("savedLevelNumber"));
    }

    public void OpenTheDoor()
    {
        doorWithAnimator.GetComponent<Animator>().SetBool("Open",true);
        source.PlayOneShot(doorOpenClip);
    }

    public void BlackScreenInstantiate()
    {
        Instantiate(blackScreenPrefab);
    }
}
