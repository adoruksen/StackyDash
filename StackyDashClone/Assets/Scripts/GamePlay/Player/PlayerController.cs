using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static Transform dashParent;
    Transform player;
    float playerSpeed = 10;
    int stackedDashCount=0;
    bool portal = true;
    [System.NonSerialized] public List<Transform> bridgeParts = new List<Transform>();

    private void Start()
    {
        SetValues();
    }
    void SetValues()
    {
        player = transform.GetChild(1);
        dashParent = transform.GetChild(0);
    }

    Vector3 myDirection
    {
        get
        {
            return PlayerManager.Direction;
        }
    }

    private void FixedUpdate()
    {
        if (LevelManager.gameState == GameState.Normal || LevelManager.gameState == GameState.BeforeStart)
        {
            Ray ray = new Ray(dashParent.transform.position, myDirection);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, .65f))
            {
                Interactable interactable = hit.transform.GetComponent<Interactable>();
                if (interactable != null)
                {
                    if (interactable.type == InteractableTypes.Dash)
                    {
                        stackedDashCount++;
                        LevelPanel.instance.ScoreCalculator(stackedDashCount);
                        interactable.type = InteractableTypes.Stacked;
                        hit.transform.GetComponent<BoxCollider>().enabled = false;
                        hit.transform.parent = dashParent;
                        hit.transform.localPosition = new Vector3(0, 0.1f * (dashParent.childCount), 0);
                        player.localPosition = new Vector3(player.localPosition.x, 0.1f * (dashParent.childCount), player.localPosition.z);
                    }
                    else if (interactable.type == InteractableTypes.Obstacle)
                    {
                        PlayerManager.isMoved = false;
                        return;
                    }
                    else if (interactable.type == InteractableTypes.Bridge)
                    {
                        BridgeControl(hit.transform.parent);
                    }
                }
            }
            transform.position += new Vector3(myDirection.x, 0, myDirection.z) * Time.fixedDeltaTime * playerSpeed;


        }
    }

   

    void BridgeControl(Transform bridgeParent)
    {
        if (bridgeParts.Count == 0)
        {
            foreach (Transform bridgeChild in bridgeParent)
            {
                bridgeParts.Add(bridgeChild);
            }
        }
        
        if (dashParent.childCount - 1 > 0)
        {
            dashParent.GetChild(dashParent.childCount - 1).transform.parent = bridgeParts[0].transform;
            bridgeParts[0].transform.GetChild(0).transform.localPosition = Vector3.up;
            bridgeParts[0].GetComponent<Interactable>().type = InteractableTypes.Passed;
            player.localPosition = new Vector3(player.localPosition.x, 0.1f * (dashParent.childCount), player.localPosition.z);
            bridgeParts.RemoveAt(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            if (interactable.type == InteractableTypes.Portal)
            {
                if (Vector3.Distance(transform.position, other.transform.position) < .65f)
                {
                    interactable.type = InteractableTypes.PassedPortal;
                    PlayerManager.direction = Vector3.zero;

                    Transform parentPortal = other.transform.parent;
                    other.transform.parent = null;

                    Vector3 otherPortalPos = parentPortal.GetChild(0).transform.position;
                    parentPortal.GetChild(0).GetComponent<Interactable>().type = InteractableTypes.PassedPortal;
                    transform.DOMoveY(-3, 1f).OnComplete(() =>
                    transform.DOMove(new Vector3(otherPortalPos.x, -.3f * dashParent.childCount, otherPortalPos.z), .3f).OnComplete(() =>
                     transform.DOMoveY(2.1f, 1f).OnComplete(() =>
                     PlayerManager.isMoved = false)));

                    other.transform.parent = parentPortal;
                }
                
            }
            if (interactable.type==InteractableTypes.UpLeft)
            {
                if (Vector3.Distance(transform.position,other.transform.position)<.65f)
                {
                    if (PlayerManager.direction == Vector3.forward)
                    {
                        PlayerManager.direction = Vector3.left;
                    }
                    if (PlayerManager.direction == Vector3.right)
                    {
                        PlayerManager.direction = Vector3.back;
                    }
                }
                
            }
            if (interactable.type == InteractableTypes.UpRight)
            {
                if (Vector3.Distance(transform.position, other.transform.position) < .65f)
                {
                    if (PlayerManager.direction == Vector3.forward)
                    {
                        PlayerManager.direction = Vector3.right;
                    }
                    if (PlayerManager.direction == Vector3.left)
                    {
                        PlayerManager.direction = Vector3.back;
                    }
                }
               
            }
            if (interactable.type == InteractableTypes.DownLeft)
            {
                if (Vector3.Distance(transform.position, other.transform.position) < .65f)
                {
                    if (PlayerManager.direction == Vector3.back)
                    {
                        PlayerManager.direction = Vector3.left;
                    }
                    if (PlayerManager.direction == Vector3.right)
                    {
                        PlayerManager.direction = Vector3.forward;
                    }
                }
               
            }
            if (interactable.type == InteractableTypes.DownRight)
            {
                if (Vector3.Distance(transform.position, other.transform.position) < .65f)
                {
                    if (PlayerManager.direction == Vector3.back)
                    {
                        PlayerManager.direction = Vector3.right;
                    }
                    if (PlayerManager.direction == Vector3.left)
                    {
                        PlayerManager.direction = Vector3.forward;
                    }
                }
               
            }

            if (interactable.type==InteractableTypes.Multiplier)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (dashParent.childCount>0)
                    {
                        dashParent.GetChild(dashParent.childCount - 1).transform.DOLocalMove(new Vector3(-1, 0, i + 1), i * .15f);
                        dashParent.GetChild(dashParent.childCount - 1).transform.parent = other.transform;
                        player.localPosition = new Vector3(player.localPosition.x, 0.1f * (dashParent.childCount), player.localPosition.z);
                    }
                }
            }
            if (interactable.type == InteractableTypes.Finish)
            {
                LevelManager.gameState = GameState.Finish;
                player.transform.parent = null;
                CinemachineController.instance.ChangeCamPosInTime(new Vector3(1.5f, 2.15f, -4), .5f,false);
                player.DOMove(new Vector3(player.transform.localPosition.x, 2.25f, player.transform.position.z + 1), .1f).OnComplete(()=>
                {
                    PlayerAnimControl.instance.SetAnimState(PlayerAnimStates.Dance);
                });
                FinishSequenceForUI();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.transform.GetComponent<Interactable>();
        if (interactable!=null)
        {
            if (interactable.type == InteractableTypes.PassedPortal)
            {
                interactable.type = InteractableTypes.Portal;
            }
        }
        
    }

    void FinishSequenceForUI()
    {
        DuringGame.instance.transform.GetChild(0).gameObject.SetActive(false);
        LevelPanel.instance.transform.GetChild(0).gameObject.SetActive(false);
        LevelPanel.instance.transform.GetChild(1).gameObject.SetActive(false);
        CompletePanel.instance.Activator(true);
        CompletePanel.instance.SetFinalScoreText(stackedDashCount);
        CompletePanel.instance.UpdateCoin(stackedDashCount);
    }
}
