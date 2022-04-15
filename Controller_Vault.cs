using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_Vault : MonoBehaviour
{
    public static Controller_Vault Instance;

    [SerializeField] private List<GameObject> list_game_object = new List<GameObject>();
    [SerializeField] private Camera OrthoG;
    [SerializeField] private GameObject[] level;
    [SerializeField] private GameObject finish_line;
    [SerializeField] private GameObject Spawner;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private Animator myAnimator;

    [SerializeField] private float forwardSpeed;
    [SerializeField] private float sensivity;

    public int levelCount = 1;

    private CubeColor currentColor = CubeColor.Red;
    private CubeColor cubeColor;
    private Rigidbody rb;
    private Vector3 mousePosition;
    private Vector3 firstPosition;
    private Vector3 difference;
    private bool finish_time = false;
    private float distance;
    private float height = 0.1f;
    private float jumping_Force = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        firstPosition = Vector3.Lerp(firstPosition, mousePosition, .1f);
        if (Input.GetMouseButtonDown(0))
            MouseDown(Input.mousePosition);

        else if (Input.GetMouseButtonUp(0))
            MouseUp();

        else if (Input.GetMouseButton(0))
            MouseHold(Input.mousePosition);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.5f, 3.5f), transform.position.y, transform.position.z);
    }
    private void FixedUpdate()
    {
        rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(difference.x, rb.velocity.y, forwardSpeed), .2f);

        if (state)
        {
            distance = list_game_object[list_game_object.Count - 1].gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            if (distance < 0.001f)
            {
                state = false;
                Debug.Log("SON NESNE DURDU.");

                Invoke("NextLevel", 11f);
            }
        }
    }
    bool state = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("plate"))
        {
            cubeColor = other.GetComponent<Cube>().CubeColor;

            other.gameObject.GetComponent<Collider>().enabled = false;

            if (currentColor == cubeColor)
            {
                list_game_object.Add(other.gameObject);
                CanvasManager.Instance.plateCountAdd();

                for (int i = 0; i < list_game_object.Count; i++)
                {
                    list_game_object[i].transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + height * (i + 1), gameObject.transform.position.z);
                    list_game_object[i].transform.SetParent(Spawner.transform);
                }
            }
            else
            {
                if (list_game_object.Count <= 1)
                {
                    Debug.Log(Constants.GAME_OVER);
                    Time.timeScale = 0;
                }
                else
                {
                    Destroy(list_game_object[list_game_object.Count - 1]);
                    Destroy(other.gameObject);

                    list_game_object.RemoveAt(list_game_object.Count - 1);
                    CanvasManager.Instance.plateCountRemove();
                }
            }
        }
        else if (other.gameObject.CompareTag("wall"))
        {
            cubeColor = other.GetComponent<Cube>().CubeColor;
            currentColor = other.GetComponent<Cube>().CubeColor;
            //Debug.Log(currentColor.ToString());

            for (int i = 0; i < list_game_object.Count; i++)
            {
                list_game_object[i].GetComponent<Renderer>().material = MaterialManager.Instance.GetMaterial(cubeColor);
            }

            //Debug.Log("Wall is pas :))");
        }
        else if (other.gameObject.CompareTag("finish_line"))
            finish_time = true;
        else if (other.gameObject.CompareTag("force_line"))
        {
            myAnimator.SetBool("forceLine", true);

            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            //myAnimator.SetFloat("distance", distance);

            for (int i = 0; i < list_game_object.Count; i++)
            {
                list_game_object[i].AddComponent<Rigidbody>();
                list_game_object[i].gameObject.GetComponent<Rigidbody>().AddForce(((transform.up / 2) + -transform.forward) * i * jumping_Force * 2);

                list_game_object[i].gameObject.GetComponent<BoxCollider>().enabled = true;
                list_game_object[i].gameObject.GetComponent<BoxCollider>().isTrigger = false;

                state = true;
            }
            rb.isKinematic = true;
        }
    }

    //END LINE.

    private void MouseDown(Vector3 inputPos)
    {
        mousePosition = OrthoG.ScreenToWorldPoint(inputPos);
        firstPosition = mousePosition;

        if (finish_time)
            jumping_Force++;
    }
    private void MouseHold(Vector3 inputPos)
    {
        mousePosition = OrthoG.ScreenToWorldPoint(inputPos);
        difference = mousePosition - firstPosition;
        difference *= sensivity;
    }
    private void MouseUp()
    {
        difference = Vector3.zero;
    }

    private void NextLevel()
    {
        LevelManager.Instance.LevelIndex++;
        SceneManager.LoadScene(0);
    }
}