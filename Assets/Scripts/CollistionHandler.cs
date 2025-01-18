using UnityEngine;
using UnityEngine.SceneManagement;

public class CollistionHandler : MonoBehaviour
{
    [SerializeField] float levelDelayLoad= 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisabled = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

  
    void Update()
    {
        CheatCode();
    }

     void CheatCode()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle collision
        }
    }
    
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisabled) {return;} // 중복 방자 목적적

        switch (other.gameObject.tag)
        {
            case "Friendly":
            Debug.Log("시작");
            break;
            
            case "Finish":
            NextLevelSequence();
            break;

            default:
            StartCrashSequence();   
            break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelDelayLoad);
    }

    void NextLevelSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelDelayLoad);
    }
    void LoadNextLevel()
    {
        // 인덱스 다음 레벨을 불러온다.
        // 만약 다음 씬 인덱스와 씬 매니저의 씬 갯수가 같을 경우
        // 처음 씬으로 돌아온다.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        // SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void ReloadLevel()
    {
        // 가독성을 높이기 위해 적용
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
