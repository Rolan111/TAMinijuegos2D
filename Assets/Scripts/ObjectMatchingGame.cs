using System;
using System.Collections;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class ObjectMatchingGame : MonoBehaviour
{
    public AudioSource source;
    public AudioClip correctClip, wrongClip, victoryClip, lostClip;
    public GameObject winScreen, wrongCorrectImage, lostScreen;
    public Sprite spriteHover;
    private Sprite spriteBase;


    private LineRenderer lineRenderer;
    [SerializeField] private int matchId;
    private bool isDragging;
    private Vector3 endPoint;
    private ObjectMatchForm objectMatchForm;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        spriteBase = GetComponent<Image>().sprite;
    }

    private void Update()
    {
        if (LifeCounter.instance.GetLifesRemaining() > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {

                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    Vector3 colliderCenterBottom = hit.collider.bounds.center;
                    colliderCenterBottom.y -= hit.collider.bounds.extents.y;
                    colliderCenterBottom.z = 0;
                    lineRenderer.SetPosition(0, colliderCenterBottom);
                }
            }
            if (isDragging)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                lineRenderer.SetPosition(1, mousePosition);
                endPoint = mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;

                RaycastHit2D hit = Physics2D.Raycast(endPoint, Vector2.zero);
                if (hit.collider != null && hit.collider.TryGetComponent(out objectMatchForm) && matchId == objectMatchForm.Get_ID())
                {

                    BoxCollider2D infoCardCollider = objectMatchForm.GetBoxCollider2D();

                    Vector2 topLeftWorld = infoCardCollider.transform.TransformPoint(infoCardCollider.bounds.min);
                    Vector2 topRightWorld = infoCardCollider.transform.TransformPoint(new Vector2(infoCardCollider.bounds.max.x, infoCardCollider.bounds.min.y));

                    Vector2 topCenter = (topLeftWorld + topRightWorld) / 2f;
                    topCenter.y += infoCardCollider.bounds.extents.y;

                    lineRenderer.SetPosition(1, topCenter);

                    hit = Physics2D.Raycast(topCenter, Vector2.zero);

                    LifeCounter.instance.Match();
                    this.enabled = false;

                    if (LifeCounter.instance.CheckWin())
                    {
                        Win();
                    }
                    else
                    {
                        Match();
                    }



                }
                else if (hit.collider != null && hit.collider.TryGetComponent(out objectMatchForm) && matchId != objectMatchForm.Get_ID())
                {
                    endPoint = Vector2.zero;
                    lineRenderer.positionCount = 0;
                    NotMatch();
                }

                lineRenderer.positionCount = 2;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (!LifeCounter.instance.CheckWin())
        {
            this.gameObject.GetComponent<Image>().sprite = spriteHover;
        }
    }

    private void OnMouseExit()
    {
        this.gameObject.GetComponent<Image>().sprite = spriteBase;
    }
    private void Win()
    {
        winScreen.SetActive(true);
        PlaySound(victoryClip);
    }

    private void Lost()
    {
        lostScreen.SetActive(true);
        PlaySound(lostClip);
    }



    private void Match()
    {
        PlaySound(correctClip);
        wrongCorrectImage.gameObject.transform.Find("Correct").gameObject.SetActive(true);
        StartCoroutine(HideCorrectWrongIcon("Correct"));
    }

    private void NotMatch()
    {
        PlaySound(wrongClip);
        wrongCorrectImage.gameObject.transform.Find("Wrong").gameObject.SetActive(true);
        StartCoroutine(HideCorrectWrongIcon("Wrong"));
    }


    private IEnumerator HideCorrectWrongIcon(String name)
    {
        yield return new WaitForSeconds(1);
        wrongCorrectImage.gameObject.transform.Find(name).gameObject.SetActive(false);
        if (name == "Correct")
        {
            if (LifeCounter.instance.CheckWin())
            {
                winScreen.SetActive(true);
                Win();
            }
        }
        else
        {
            LifeCounter.instance.LostLife();
            int lifes = LifeCounter.instance.GetLifesRemaining();
            if (lifes <= 0)
            {
                lostScreen.SetActive(true);
                Lost();
            }

        }
    }

    private void PlaySound(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }


}
