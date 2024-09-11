using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingshotHandler : MonoBehaviour
{   
    [Header("Line Renderers")]
    [SerializeField] private LineRenderer leftlr;
    [SerializeField] private LineRenderer rightlr;

    [Header("Transform Referencesss")]
    [SerializeField] private Transform leftstartpos;
    [SerializeField] private Transform rightstartpos;
    [SerializeField] private Transform centrepos;
    [SerializeField] private Transform idlepos;

    [Header("Slingshot Stats")]
    [SerializeField] private float maxdist=3.5f;
    [SerializeField] private float launchforce=7f;
    private Vector2 linepos;
    private bool clickedWithinArea;
    [Header("Scripts")]
    [SerializeField] private SlingShotArea slingShotArea;
    [Header("Bird")]
    [SerializeField] private float RedOffset=0.2f;
    [SerializeField] private redbird redPrefab;
    private Vector2 direction;
    private Vector2 directionNormalized;
    private redbird SpawnedRed;
    private bool birdOnSlingshot;
    private void Awake() {
        leftlr.enabled=true;
        rightlr.enabled=true;
        SpawnRed();
    }
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && slingShotArea.WithinArea()){
            clickedWithinArea=true;
        }
        if (Mouse.current.leftButton.isPressed && clickedWithinArea && birdOnSlingshot){
            DrawSlingShot();
            PosAndRotRed();
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame && birdOnSlingshot){
            clickedWithinArea=false;
            SpawnedRed.Launch(direction,launchforce);
            birdOnSlingshot=false;
        }
    }
    #region Slingshot Methods
    private void DrawSlingShot(){
        
        Vector3 mousepos=Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        linepos=centrepos.position+Vector3.ClampMagnitude(mousepos-centrepos.position,maxdist);
        moveSling(linepos);
        direction=(Vector2)centrepos.position-linepos;
        directionNormalized=direction.normalized;

    }
    private void moveSling(Vector3 pos){
        if (!leftlr.enabled && !rightlr.enabled){
            leftlr.enabled=true;
            rightlr.enabled=false;
        }
        leftlr.SetPosition(0,pos);
        leftlr.SetPosition(1,leftstartpos.position);
        rightlr.SetPosition(0,pos);
        rightlr.SetPosition(1,rightstartpos.position);
    }
    private void resetSling(){
        leftlr.SetPosition(0,centrepos.position);
        leftlr.SetPosition(1,leftstartpos.position);
        rightlr.SetPosition(0,centrepos.position);
        rightlr.SetPosition(1,rightstartpos.position);

    }
    #endregion

    #region Red Methods

    private void SpawnRed(){
        
        Vector2 dir=(centrepos.position-idlepos.position).normalized;
        Vector2 spawnpos=(Vector2)idlepos.position+dir*RedOffset;
        moveSling(idlepos.position);
        SpawnedRed=Instantiate(redPrefab,spawnpos,Quaternion.identity);
        SpawnedRed.transform.right=dir;
        birdOnSlingshot=true;
    }
    private void PosAndRotRed(){
        SpawnedRed.transform.position=linepos+directionNormalized*RedOffset;
        SpawnedRed.transform.right=directionNormalized;
    }
    #endregion
    //new concept just make it try to go into specific areas instead of kill stuff 

}
