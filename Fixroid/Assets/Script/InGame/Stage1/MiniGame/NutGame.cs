using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutGame : MonoBehaviour
{
    public enum touchState { Ready, Start, Move, End };
    public touchState state = touchState.Ready;
    private Vector3 clickedPos;

    private AudioSource nutrotationSound;

    private BlurManager blurManager;
    private ProgressManager progressManager;
    private MiddleSaw middleSaw;

    private GameObject nut;
    private GameObject guide;
    private GameObject trace;
    private TrailRenderer traceTrail;
    private int successCount = 0;

    private Sprite pushedNut;

    private int currentNum = 0;
    private bool gamePlaying = false; 
    private List<int> answers = new List<int>();

    private void Awake()
    {
        nutrotationSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/MiniGame/NutGame/NutRotationSound");

        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main.GetComponent<Camera>();

        nut = transform.GetChild(0).gameObject;
        guide = transform.GetChild(1).gameObject;
        trace = transform.GetChild(2).gameObject;
        guide.SetActive(false);
        traceTrail = trace.GetComponentInChildren<TrailRenderer>();

        progressManager = ProgressManager.GetInstance;
        pushedNut = Resources.LoadAll<Sprite>("Stage/Stage1/MiniGame/NutGame")[1];
    }

    // Use this for initialization
    void Start ()
    {
        blurManager = UIController.GetInstance.BlurManager;
        middleSaw = SawController.GetInstance.MiddleSaw;
        StartCoroutine(Guide());
    }

    private IEnumerator Guide()
    {
        trace.SetActive(true);
        traceTrail.emitting = true;
        float step = -45.0f;

        Quaternion newQuaternion = Quaternion.identity;
        newQuaternion.eulerAngles = new Vector3(0, 0, step);
        trace.transform.rotation = newQuaternion;

        while (step < 45)
        {
            newQuaternion = Quaternion.identity;
            newQuaternion.eulerAngles = new Vector3(0, 0, step);
            trace.transform.rotation = newQuaternion;
            step += 2f;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSecondsRealtime(1.0f);
        guide.SetActive(true);
        gamePlaying = true;
        trace.SetActive(false);
        traceTrail.emitting = false;
    }

    private IEnumerator Rotation(bool isFinal)
    {
        float step = 0;
        UnityEngine.UI.Image nutSprite = nut.GetComponent<UnityEngine.UI.Image>();
        Sprite tempSprite = nutSprite.sprite;
        nutSprite.sprite = pushedNut;
        nutrotationSound.Play();
        guide.SetActive(false);

        while (step < 40)
        {
            nut.transform.Rotate(new Vector3(0, 0, 3));
            step += 1.0f;
            yield return new WaitForEndOfFrame();
        }

        nutSprite.sprite = tempSprite;

        yield return new WaitForSeconds(0.5f);

        if (!isFinal)
            StartCoroutine(Guide());
        else
        {
            middleSaw.StartRotate();
            blurManager.CloseBlur(BlurManager.RequestType.NutGame, BlurManager.EndType.MinigameSuccess);
            progressManager.NutGameEnd = true;
            Destroy(gameObject);
        }

        yield break;
    }


    // Update is called once per frame
    void Update()
    {
        if (!gamePlaying) return;
        CheckMouseState();

        if (Input.GetMouseButton(0))
        {
            if (state == touchState.Move)
            {
                Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray = new Ray2D(point, Vector2.zero);
                RaycastHit2D raycastHit = Physics2D.Raycast(ray.origin, ray.direction);

                if (raycastHit.collider != null)
                {
                    int result;
                    if (int.TryParse(raycastHit.collider.name, out result))
                    {
                        if (currentNum != result)
                        {
                            currentNum = result;
                            answers.Add(result);
                        }
                    }
                }
            }
        }
        else if (state == touchState.End)
        {
            gamePlaying = false;
            if (Checker())
            {
                if(successCount<2)
                {
                    StartCoroutine(Rotation(false));
                    successCount++;
                }
                else
                    StartCoroutine(Rotation(true));
                
                answers.Clear();
                state = touchState.Ready;
            }
            else
            {
                Destroy(gameObject);
                blurManager.CloseBlur(BlurManager.RequestType.NutGame, BlurManager.EndType.MinigameFail);
            }
        }
    }

    void CheckMouseState()
    {
        if (state == touchState.Ready)
        {
            if (Input.GetMouseButtonDown(0))
            {
                state = touchState.Start;
                clickedPos = Input.mousePosition;
            }
        }
        else if (state == touchState.Start)
        {
            if (clickedPos != Input.mousePosition)
                state = touchState.Move;

        }
        else if (state == touchState.Move)
        {
            if (Input.GetMouseButtonUp(0))
                state = touchState.End;
        }
    }

    bool Checker()
    {
        if (answers.Count != 6)
            return false;

        for (int i = 0; i < 6; i++)
            if (answers[i] != i + 1)
                return false;

        return true;
    }

}
