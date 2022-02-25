using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pattern;
public enum PlayerState 
{
    walk = 1,
    stop
}

public class PlayerController : SingletonMono<PlayerController>
{
    public MapCardSave mapCardSave;
    public AK.Wwise.Event footStep;

    public GameObject switchEffectPrefab;
    public PlayerState playerState = PlayerState.walk;
    public MapManager currentMap;
    public MapManager map1 , map2 , map3;
    public Vector2 targetPoint;
    public Vector2 dir = Vector2.zero;

    public float speed = 10f;
    public bool arrival = true;
    public GirdCell currentGridCell;
    public GirdCell nextGridCell;
    
    public Vector2 currentGridCellIndex;

    public int originStepCount = 20 , currentStepCount , stepCost = 0;

    bool moving = false;


    public Action<int> OnStepCountChange;

    private Animator anim;
    private SpriteRenderer sprite;

    public bool ReceiveInput = true;

    protected override void Awake()
    {
        base.Awake();

        //currentMap = map3;
        currentStepCount = originStepCount;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
            



        if(Input.GetKeyDown(KeyCode.Alpha1))
            SetMapManager(map1);

        if(Input.GetKeyDown(KeyCode.Alpha2))
            SetMapManager(map2);
        
        if(Input.GetKeyDown(KeyCode.Alpha3))
            SetMapManager(map3);

        // if(Input.GetKeyDown(KeyCode.E))
        //     SavePos();

        if (Input.GetKeyDown(KeyCode.R) && playerState != PlayerState.stop)
        {
            dir = Vector2.zero;
            LoadPos();
        }

        if (playerState == PlayerState.walk)
        {
            anim.speed = 1;
            Move();
        }
        else anim.speed = 0;
    }

#region Move

    private void Move()
    {
        if(dir == Vector2.zero)
        {
            if(Input.GetKey(KeyCode.A))
            {
                sprite.flipX = true;
                dir = Vector2.left;
                moving = true;
            }               
            else if(Input.GetKey(KeyCode.D))
            {
                sprite.flipX = false;
                dir = Vector2.right;
                moving = true;
            } 
            else if(Input.GetKey(KeyCode.W))
            {
                dir = Vector2.up;
                moving = true;
            }
            else if(Input.GetKey(KeyCode.S))
            {
                dir = Vector2.down;
                moving = true;
            }
            else
            {
                moving = false;
            }
            anim.SetBool("moving", moving);
        }
        else
        {
            //检测边界
            if(currentMap.CanMoveTo(currentGridCell.pos , dir))
            {
                nextGridCell = GetNextGrid(dir);
            }
            else
            {
                dir = Vector2.zero;
                return;
            }

            
            //到达时
            if(arrival)
                GetTargetPoint();
            else
            {
                //未到达时
                if(new Vector2(transform.position.x , transform.position.y) == targetPoint)
                {
                    AddStepCount(-1);
                    currentGridCell = nextGridCell;
                    arrival = true;
                    currentGridCellIndex += dir;
                    dir = Vector2.zero;
                }
                else
                {
                    //步数不为负
                    if(currentStepCount == 0)
                    {
                        playerState = PlayerState.stop;
                        anim.Play("walk");
                        Invoke("ShowStepOut" , 1f);
                        return;
                    }

                    transform.position = Vector2.MoveTowards(transform.position , targetPoint , speed * Time.deltaTime);
                }
            }
        }
    }
    public void Normalize()
    {
        if(dir != Vector2.zero)
            currentGridCell = nextGridCell;
        arrival = true;
        currentGridCellIndex += dir;
        dir = Vector2.zero;
        anim.Play("idle");
        transform.position = currentGridCell.pos;
    }
    public void GetTargetPoint()
    {
        targetPoint = nextGridCell.pos;
        arrival = false;
    }

    public GirdCell GetNextGrid(Vector2 dir)
    {
        if(dir.x != 0)
            return currentMap.Grid.Get((int)currentGridCellIndex.x + (int)dir.x , (int)currentGridCellIndex.y).GetComponent<GirdCell>();
        else
            return currentMap.Grid.Get((int)currentGridCellIndex.x , (int)currentGridCellIndex.y + (int)dir.y).GetComponent<GirdCell>();
    }

#endregion

    public void AddStepCount(int num)
    {
        // 更新UI
        
        //步数无上限
        currentStepCount += num;
        stepCost -= num;

        if (OnStepCountChange != null)
        {
            OnStepCountChange(currentStepCount);
        }
    }

    public GameObject stepOutTip;
    public void ShowStepOut()
    {
        stepOutTip.SetActive(true);
    }
    public void PlayFootStep()
    {
        footStep.Post(gameObject);
    }

    MapManager tempManager;
    public void SetMapManager(MapManager m)
    {

        Instantiate(switchEffectPrefab).transform.position = transform.position;
        tempManager = m;
        Invoke("DelaySetMapManager", 0.8f);

    }
    void DelaySetMapManager()
    {
        MapManager m = tempManager;


        if (m == currentMap)
            return;

        if (m == map1)
        {
            StartCoroutine(IChangeVolume(100, 0, 2));
        }
        else if (currentMap == map1)
        {
            StartCoroutine(IChangeVolume(0, 100, 2));
        }

        if (m == map1)
        {
            EventCenter.Broadcast(EventDefine.SwitchToMap1);
        }
        else if (m == map2)
        {
            EventCenter.Broadcast(EventDefine.SwitchToMap2);
        }
        else if (m == map3)
        {
            EventCenter.Broadcast(EventDefine.SwitchToMap3);
        }

        currentMap.gameObject.SetActive(false);
        m.gameObject.SetActive(true);
        currentMap = m;
        currentGridCell = currentMap.Grid.Get((int)currentGridCellIndex.x, (int)currentGridCellIndex.y).GetComponent<GirdCell>();
        nextGridCell = null;
    }
    IEnumerator IChangeVolume(float origin, float target, float transitionDuration)
    {
        float timer = 0f;

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float value = Mathf.Lerp(origin, target, timer / transitionDuration);
            AkSoundEngine.SetRTPCValue("Lofi", value);
            yield return null;
        }
        AkSoundEngine.SetRTPCValue("Lofi", target);
    }

    public Stack<RevertPoint> revertPoints = new Stack<RevertPoint>();

    public void SavePos()
    {

        RevertPoint r = new RevertPoint();
        //r.step = stepCost;
        r.step = 0;
        r.map = currentMap;
        r.savedGridCellIndex = currentGridCellIndex;
        if(revertPoints.Count != 0)
        {
            revertPoints.Peek().step = stepCost;
        }
        revertPoints.Push(r);

        //savedGridCellIndex = currentGridCellIndex;
        
        stepCost = 0;
        Debug.Log("Save success!");
    }
    public Stack<RevertPoint> certainRevertPoints = new Stack<RevertPoint>();
    public void SaveCertainPos()
    {

        RevertPoint r = new RevertPoint();
        //r.step = stepCost;
        r.step = 0;
        r.map = currentMap;
        r.savedGridCellIndex = currentGridCellIndex;
        if (certainRevertPoints.Count != 0)
        {
            certainRevertPoints.Peek().step = stepCost;
        }
        certainRevertPoints.Push(r);

        //savedGridCellIndex = currentGridCellIndex;

        stepCost = 0;
        Debug.Log("Save success!");
    }
    public bool CheckIsIn2000()
    {
        return currentMap == map1;
    }

    public void LoadPos()
    {
        nextGridCell = null;

        if(revertPoints.Count == 0)
        {
            Debug.Log("No element in stack!");
            return;
        }


        var r = revertPoints.Pop(); 
        
        if (r.Compare(certainRevertPoints.Peek()))
        {
            certainRevertPoints.Pop();
        }
        foreach (var item in certainRevertPoints)
        {
            if (r.Compare(item))
            {
                item.hasMeaning = false;
            }
        }
        currentMap.gameObject.SetActive(false);
        r.map.gameObject.SetActive(true);
        currentMap = r.map;
        if (r.step != 0)
        {
            AddStepCount(r.step);
        }
        else
        {
            AddStepCount(stepCost);
        }
        stepCost = 0;

        currentGridCellIndex = r.savedGridCellIndex;

        //currentGridCellIndex = savedGridCellIndex;

        currentGridCell = currentMap.Grid.Get((int)currentGridCellIndex.x , (int)currentGridCellIndex.y).GetComponent<GirdCell>();

        targetPoint = currentGridCell.pos;
        arrival = true;
        transform.position = currentGridCell.pos;
        Debug.Log("Load success!");
    }

    public List<Sprite> CalculateCard()
    {
        List<Sprite> cardsSprites = new List<Sprite>();

        foreach (var item in certainRevertPoints)
        {
            if (!item.hasMeaning)
                continue;
            MapManager m = item.map;
            int mapIndex = 0;
            Sprite sprite;
            if (m == map1) mapIndex = 1;
            else if (m == map2) mapIndex = 2;
            else if (m == map3) mapIndex = 3;
            if (item.savedGridCellIndex == new Vector2(1, 3))
                sprite = mapCardSave.GetMapSprites(mapIndex)[0];
            else if (item.savedGridCellIndex == new Vector2(6, 3))
                sprite = mapCardSave.GetMapSprites(mapIndex)[1];
            else if (item.savedGridCellIndex == new Vector2(3, 6))
                sprite = mapCardSave.GetMapSprites(mapIndex)[2];
            else if (item.savedGridCellIndex == new Vector2(5, 5))
                sprite = mapCardSave.GetMapSprites(mapIndex)[3];
            else if (item.savedGridCellIndex == new Vector2(7, 6))
                sprite = mapCardSave.GetMapSprites(mapIndex)[4];
            else
                continue;
            cardsSprites.Add(sprite);
            Debug.Log("ADD mapINDEX : " + mapIndex);
        }


        return cardsSprites;
    }


    private List<Vector2> changePoints = new List<Vector2>() {new Vector2(1,3) ,new Vector2(6,3) ,new Vector2(3,6) ,new Vector2(5,5) ,new Vector2(7,6)};
    private List<bool> visited = new List<bool>();
    public List<MapManager> mapForCard = new List<MapManager>();
    public List<Vector2> posForCard = new List<Vector2>();

    public bool CheckAllPointsDone()
    {
        visited.Clear();
        mapForCard.Clear();
        posForCard.Clear();

        for (var i = 0; i < 5; i++)
        {
            visited.Add(false);
        }

        int cnt = 0;

        foreach(var point in revertPoints)
            for (var i = 0; i < 5; i++)
            {
                var cp = changePoints[i];
                if(!visited[i] && point.step == 0 && point.savedGridCellIndex == cp)
                {
                    visited[i] = true;
                    mapForCard.Add(point.map);
                    posForCard.Add(point.savedGridCellIndex);
                    cnt++;
                    break;
                }
            }

        //CardUI.Instance.RefreshCardUI();

        if(cnt == 5)
        {
            Debug.Log("AllPointsDone");
            return true;
        }

        Debug.Log("Points Not Done");
        return false;
    }




    

}
;