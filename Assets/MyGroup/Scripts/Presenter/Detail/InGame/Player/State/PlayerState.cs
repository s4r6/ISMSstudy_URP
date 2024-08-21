using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public partial class PlayerState : MonoBehaviour
{
    private static readonly LoadingState Loadingstate = new LoadingState();
    private static readonly WaitState Waitstate = new WaitState();
    private static readonly ExploreState Explorestate = new ExploreState();
    private static readonly DetailInfoState DetailInfostate = new DetailInfoState();
    private static readonly DiscoverState Discoverstate = new DiscoverState();
    private static readonly ArchivesState Archivesstate = new ArchivesState();
    private static readonly ResultState Resultstate = new ResultState();
    private static readonly DocumentState Documentstate = new DocumentState();


    private Animator anim; 
    private PlayerStateBase currentState = Loadingstate;
    private DocsWindow myDocsWindow;
    [SerializeField]
    private DetailInfoWindow myDetailInfoWindow;
    [SerializeField]
    private StageLoader myStageLoader;
    private GameData _gameData;

    private void Start()
    {
        Debug.Log("PlayerState‹N“®");
        myDocsWindow = GameObject.Find("DocsWindow").GetComponent<DocsWindow>();
        myDocsWindow.gameObject.SetActive(false);
        myDetailInfoWindow = GameObject.Find("DetailDataWindow").GetComponent<DetailInfoWindow>();
        myDetailInfoWindow.gameObject.SetActive(false);
        anim = GameObject.Find("WaitPhaseWindow").GetComponent<Animator>();
        OnStart();
    }

    private void Update()
    {
        OnUpdate();
    }
    private void OnStart()
    {
        currentState.OnEnter(this, null);
    }

    private void OnUpdate()
    {
        currentState.OnUpdate(this);
    }

    private void ChangeState(PlayerStateBase nextState)
    {
        currentState.OnExit(this, nextState);
        nextState.OnEnter(this, currentState);
        currentState = nextState;
        Debug.Log(currentState);
    }
}
