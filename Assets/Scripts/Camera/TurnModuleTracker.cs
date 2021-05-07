using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetSpriteFadeMotion))]
public class TurnModuleTracker : MonoBehaviour
{
    [SerializeField] int targetFrame = 10;
    public ITurnModule module{get;private set;}
    TargetSpriteFadeMotion fadeMotion;
    SpriteRenderer moduleRenderer;
    Dimention dimention;

    public void Register(ITurnModule mod,Dimention dim){
        dimention = dim;
        module = mod;
        moduleRenderer = module.gameObject.GetComponent<SpriteRenderer>();
        fadeMotion = GetComponent<TargetSpriteFadeMotion>();
        StartCoroutine(Waitor());
    }

    IEnumerator Waitor(){
        while(!GameManager.instance.finished){
            yield return targetFrame;
            
            if(module.state == ModuleState.working && !dimention.active){
                yield return StartCoroutine(Tracker());
            }
        }

        gameObject.SetActive(false);
    }

    IEnumerator Tracker(){
        
        fadeMotion.sprite = moduleRenderer.sprite;
        transform.position = module.gameObject.transform.position;

        var activeDim = Dimention.dimentionList.Find(x => x.active);
        
        gameObject.layer = activeDim.gameObject.layer;

        fadeMotion.Enter();
        while(module.state != ModuleState.compleate){

            yield return targetFrame;

            fadeMotion.sprite = moduleRenderer.sprite;
            transform.position = module.gameObject.transform.position;
        }

        fadeMotion.Exit();
    }
}
