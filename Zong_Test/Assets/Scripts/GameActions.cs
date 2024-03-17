using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameActions : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject stoneObject;
    [SerializeField] private GameObject instrumentObject;
    [SerializeField] private Transform stonePosition;
    [SerializeField] private Transform BoxAPosition;
    [SerializeField] private Transform BoxBPosition;
    [SerializeField] private Transform BoxCPosition;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private AudioSource WinSound;

    private GameObject canvasWorld0;
    private GameObject canvasWorld1;

    private int score;
    private bool isStoneCollected;

    void Start()
    {
        canvasWorld0 = GameObject.FindGameObjectWithTag("CanvasWorld0");
        canvasWorld1 = GameObject.FindGameObjectWithTag("CanvasWorld1");
        canvasWorld1.GetComponent<Canvas>().enabled = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone") && !isStoneCollected)
        {
            score += 10;
            pointsText.text = "Points: " + score.ToString();
            canvasWorld0.SetActive(false);
            canvasWorld1.GetComponent<Canvas>().enabled = true;
            collision.transform.parent = playerObject.transform;
//            instrumentObject.SetActive(true);
            isStoneCollected = true;
        }
        else if ((collision.gameObject.CompareTag("BoxA") || collision.gameObject.CompareTag("BoxB")) && isStoneCollected)
        {
            score += 10;
            pointsText.text = "Points: " + score.ToString();
            Debug.Log("Collided with " + collision.gameObject.tag);
            collision.transform.parent = null;
            Transform firstChild = collision.transform.GetChild(0);
            firstChild.gameObject.SetActive(true);
            Transform secondChild = collision.transform.GetChild(1);
            secondChild.gameObject.SetActive(true);
             // Play the WinSound
            WinSound.Play();

            if (collision.gameObject.CompareTag("BoxB"))
            {
                stoneObject.transform.parent = collision.transform;
                stoneObject.transform.position = BoxBPosition.transform.position;
                isStoneCollected = false;
            }
            else if (collision.gameObject.CompareTag("BoxA"))
            {
                stoneObject.transform.parent = collision.transform;
                stoneObject.transform.position = BoxAPosition.transform.position;
                isStoneCollected = false;
            }
        }
        else if (collision.gameObject.CompareTag("BoxC") && isStoneCollected)
        {
            WinSound.Play();
            stoneObject.transform.parent = collision.transform;
            stoneObject.transform.position = BoxCPosition.transform.position;
            playerObject.transform.position = stonePosition.position;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
