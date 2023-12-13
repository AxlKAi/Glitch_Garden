using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderBuildZone : MonoBehaviour
{
    Grid<DefenderGridElement> defendersArray;

    [SerializeField] private int gridWidth = 5;
    [SerializeField] private int gridHight = 5;
    [SerializeField] private float gridCellSize = 1f;
    [SerializeField] private Vector3 gridStartPoint = new Vector3( 1, 1, 0);

    [SerializeField] private GameObject selectedAreaGO;

    private Defender defenderPref;
    private GameState gameState;
    private bool isGameOnPause = false;

    // Start is called before the first frame update
    void Start()
    {
        defendersArray = new Grid<DefenderGridElement>
        (
            gridWidth, 
            gridHight, 
            gridCellSize, 
            gridStartPoint, 
            (int x, int y, Grid<DefenderGridElement> defGridElem) => { return new DefenderGridElement(x, y, defGridElem); }
        );

        gameState = GameObject.FindObjectOfType<GameState>();

        if (!selectedAreaGO) { Debug.LogError("Please, add Hightlight selected area gameObject ib BuilZone!"); }

        gameState.BuildModeOnEvent += ActivateHighlLightAreaGO;
        gameState.BuildModeOffEvent += DeactivateHighlLightAreaGO;
        gameState.SpellModeOnEvent += ActivateHighlLightAreaGO;
        gameState.SpellModeOffEvent += DeactivateHighlLightAreaGO;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isGameOnPause)
                return;

            if (gameState.GetBuildMode())
            {
                int x, y;
                Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                screenPos.z = 0;
                defendersArray.GetXY(screenPos, out x, out y);
                if (x >= 0 && y >= 0)
                {
                    defendersArray.GetValue(x, y).SpawnDefender(defenderPref);
                }
            }
            else if (gameState.GetSpellMode())
            {
                int x, y;
                Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                screenPos.z = 0;
                defendersArray.GetXY(screenPos, out x, out y);
                Vector3 squarePosition = defendersArray.GetWorldPosition(x, y) 
                    + new Vector3(defendersArray.GetCellSize()/2, defendersArray.GetCellSize()/2, 0);

                if (x>=0 && y>=0)
                {
                    DefenderGridElement def = defendersArray.GetValue(x, y);
                    Defender defndr = def.GetDefender();
                    
                    Attacker[] nearAttackers = FindNearEnemies(squarePosition, gameState.GetCurrentSpell().spellRange);

                    bool debug = false;
                    if (debug)
                    {
                        if (nearAttackers != null && nearAttackers.Length > 0)
                        {
                            foreach (Attacker attacker in nearAttackers)
                            {
                                Debug.Log(attacker.name + " at " + attacker.transform.position);
                            }
                        }
                    }
                    Debug.Log(squarePosition);

                    bool isCastMade = gameState.GetSpellAction().TryToCastSpell(squarePosition, defndr, nearAttackers);
                    if(isCastMade)
                    {
                        gameState.SpellModeOff();
                        gameState.GetInventory().DeleteSelectedItem();
                    }
                }
            }
            else
            {
                Vector3 startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector3.forward, 10f, gameState.GetPickableLayerNum()) ;
                Debug.DrawLine(startPosition, startPosition + Vector3.forward*10f, Color.red, 1f);
                if (hit)
                {
                    Spell_item spell_item = hit.transform.GetComponent<Spell_item>();
                    StarItem starsItem = hit.transform.GetComponent<StarItem>();

                    if (spell_item)
                    {
                        spell_item.TryToPickUpItem();
                        Debug.Log(hit.transform.gameObject.name);
                    }
                    else if(starsItem)
                    {
                        starsItem.CatchStar();
                    }                    
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (gameState.GetBuildMode())
            {
                gameState.BuildModeOff();
            }

            if (gameState.GetSpellMode())
            {
                gameState.SpellModeOff();
            }

            int x, y;
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            screenPos.z = 0;
            defendersArray.GetXY(screenPos, out x, out y);
            DefenderGridElement def = defendersArray.GetValue(x, y);
            if (def==null) { return; }
            Defender defndr = def.GetDefender();

            if (defndr != null)
            {
                int w, h;
                defendersArray.IndexesOfElement(def, out w, out h);
                Debug.Log(defndr.name + " w=" + w + " h=" + h);
            }
        }

        if (gameState.GetBuildMode() || gameState.GetSpellMode())
        {
            HightLightArea();
        }
    }

    public Attacker[] FindNearEnemies(Vector3 position, float distance)
    {
        Attacker[] allEnemies = FindObjectsOfType<Attacker>();
        List<Attacker> nearEnemies = new List<Attacker>();
        foreach (Attacker attacker in allEnemies)
        {
            float dist = Vector3.Distance(attacker.transform.position, position);
            if (dist <= distance)
            {
                nearEnemies.Add(attacker);
            }
        }

        Attacker[] resultArr;
        if (nearEnemies.Count > 0)
        {
            Debug.Log(nearEnemies.Count + " enemies near by ");
            resultArr = new Attacker[nearEnemies.Count];
            int indx = 0;
            foreach(Attacker attacker in nearEnemies)
            {
                resultArr[indx] = attacker;
                indx++;
            }
        }
        else
        {
            resultArr = null;
        }

        return resultArr;
    }

    public void ActivateHighlLightAreaGO(Defender def)
    {
        selectedAreaGO.SetActive(true);
    }

    public void ActivateHighlLightAreaGO(Spell spell)
    {
        selectedAreaGO.SetActive(true);
    }

    public void DeactivateHighlLightAreaGO()
    {
        selectedAreaGO.SetActive(false);
    }


    private void HightLightArea()
    {
        int x, y;
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenPos.z = 0;
        defendersArray.GetXY(screenPos, out x, out y);

        if (x >= 0 && x < defendersArray.GetWidth() && y >= 0 && y < defendersArray.GetHeight())
        {
            selectedAreaGO.transform.position = defendersArray.GetWorldPosition(x,y) 
                + new Vector3(defendersArray.GetCellSize()*.5f, defendersArray.GetCellSize() * .5f,0);
        }
    }

    public void SetSpawnableDefender(Defender newDefender)
    {
        defenderPref = newDefender;
    }

    public void SetGameOnPause()
    {
        isGameOnPause = true;
    }

    public void SetGameNormalSpeed()
    {
        isGameOnPause = false;
    }
}

public class DefenderGridElement 
{
    Grid<DefenderGridElement> defendersArray;

    private int x, y;
    private float xDelta, yDelta;

    Defender defender;

    public DefenderGridElement(int x, int y, Grid<DefenderGridElement> defsArr)
    {
        this.x = x;
        this.y = y;
        defendersArray = defsArr;
        xDelta = yDelta = defsArr.GetCellSize() / 2;
    }

    public void SpawnDefender(Defender defenderPrefab)
    {
        if ( CanBuild(defenderPrefab) && GameState._instance.IsEnoughStars(defenderPrefab.GetDefenderCost()) )
        {            
            Vector3 defenderPosition = defendersArray.GetWorldPosition(x, y) + new Vector3(xDelta,yDelta);
            Defender def = GameObject.Instantiate(defenderPrefab, defenderPosition, Quaternion.identity);
            this.defender = def;
            def.transform.parent = GameState._instance.GetDefendersRoot().transform;
            def.OnDestroyDefenderEvent += DeleteDefender;
            GameState._instance.SpendStars(defenderPrefab.GetDefenderCost());
            def.ShowBuildVFX();
        }
    }

    public bool CanBuild(Defender defenderPrefab)
    {
        if (defender == null)
        {
            Vector3 defenderPosition = defendersArray.GetWorldPosition(x, y) + new Vector3(xDelta, yDelta);
            int defenderY = (int)defenderPosition.y;
            bool res = defenderPrefab.isLineSpawnerSet(defenderY);

            return res;
        }
        else
        {
            return false;
        }
    }

    public override string ToString()
    {
        return x + ":" + y;
    }

    public void DeleteDefender()
    {
        defender = null;
    }

    public Defender GetDefender()
    {
        return defender;
    }

}
