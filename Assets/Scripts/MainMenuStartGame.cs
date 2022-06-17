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

    public void StartGame()
    {
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
        SceneManager.LoadScene(1);
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
