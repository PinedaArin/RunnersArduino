using UnityEngine;
using System.Collections;
using DentedPixel;
using Boo.Lang;
using UnityEngine.UI;
public class RaceGameManager : MonoBehaviour
{
    public static RaceGameManager Instance;
    public Text textTime;
    public Transform[] pathTransforms;

    private LTBezierPath path;

    private GameObject avatar1;
    private GameObject avatar2;
    private GameObject avatar3;

    private float progressAvatar_1;
    private float progressAvatar_2;
    private float progressAvatar_3;

    private int time = 30;

    void OnEnable()
    {
        SetUpTrack();

    }

    private void SetUpTrack()
    {
        List<Vector3> pathPoints = new List<Vector3>();

        foreach (Transform point in pathTransforms)
        {
            pathPoints.Add(point.position);
            point.gameObject.SetActive(false);
        }

        // create the path
        path = new LTBezierPath(new Vector3[] { pathPoints[0], pathPoints[2], pathPoints[1], pathPoints[3],
                                                pathPoints[3], pathPoints[5], pathPoints[4], pathPoints[6],
                                                pathPoints[6], pathPoints[8], pathPoints[7], pathPoints[9],
                                                pathPoints[9], pathPoints[11], pathPoints[10], pathPoints[12],
                                                pathPoints[12], pathPoints[14], pathPoints[13], pathPoints[15],
                                                pathPoints[15], pathPoints[17], pathPoints[16], pathPoints[18],
                                               pathPoints[18], pathPoints[20], pathPoints[19], pathPoints[21],
                                               pathPoints[21], pathPoints[23], pathPoints[22], pathPoints[24],

        });
    }

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
    }

    void Start()
    {
        
        avatar1 = GameObject.Find("Avatar1");
        avatar2 = GameObject.Find("Avatar2");
        avatar3 = GameObject.Find("Avatar3");
       
    }


    void Update()
    {


        IncrementProgress();

        if (progressAvatar_1 > 1.0f || progressAvatar_2 > 1.0f || progressAvatar_3 > 1.0f)
        {
            ResetAvatarProgress();
            //OnRaceFinished();

        }
        
       

        path.place(avatar1.transform, progressAvatar_1);
        path.place(avatar2.transform, progressAvatar_2);
        path.place(avatar3.transform, progressAvatar_3);

        avatar1.transform.rotation = (Quaternion.identity);
        avatar2.transform.rotation = (Quaternion.identity);
        avatar3.transform.rotation = (Quaternion.identity);
    }

    private void IncrementProgress()
    {
        if (PlayerInputManager.Instance.GetPlayerInput(1) == 1)
        {
            progressAvatar_1 += Time.deltaTime * 0.5f;
        }

        if (PlayerInputManager.Instance.GetPlayerInput(2) == 1)
        {
            progressAvatar_2 += Time.deltaTime * 0.5f;

        }
        if (PlayerInputManager.Instance.GetPlayerInput(3) == 1)
        {
            progressAvatar_3 += Time.deltaTime * 0.5f;

        }
    }

    public void ResetAvatarProgress ()
    {
        progressAvatar_1 = 0;
        progressAvatar_2 = 0;
        progressAvatar_3 = 0;
    }


    void OnDrawGizmos()
    {
        // Debug.Log("drwaing");
        if (path != null)
            OnEnable();
        Gizmos.color = Color.red;
        if (path != null)
            path.gizmoDraw(); // To Visualize the path, use this method
    }

    private void OnRaceFinished()
    {
        StopAllCoroutines();
        ResetAvatarProgress();
        time = 30;
        textTime.text = "30";
        GameManager.Instance.RaceFinished();

    }

    public IEnumerator Timmer()
    {
        for (int i = 0; i < 31; i++)
        {
            yield return new WaitForSeconds(1);
            textTime.text = time.ToString();
            time--;
        }
        if (time == -1)
        {
            OnRaceFinished();
        }

    }

    public void OnGameStarted()
    {
        StartCoroutine(Timmer());
    }

}

