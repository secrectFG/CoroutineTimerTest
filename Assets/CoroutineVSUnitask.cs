using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CoroutineVSUnitask : MonoBehaviour
{
    public int count = 10000;

    bool unitaskCanRun = false;
    public void StartCoroutine(){
        unitaskCanRun = false;
        for(int i = 0; i < count; i++){
            StartCoroutine(CoroutineLoop());
        }
    }

    IEnumerator CoroutineLoop(){
        while(true){
            yield return null;
        }
    }

    public async void StartUniTask_UniTaskVoid(){
        StopAllCoroutines();
        unitaskCanRun = false;
        await UniTask.Yield();
        unitaskCanRun = true;
        for(int i = 0; i < count; i++){
            UniTaskLoop().Forget();
        }
    }

    public async void StartUniTask_Void(){
        StopAllCoroutines();
        unitaskCanRun = false;
        await UniTask.Yield();
        unitaskCanRun = true;
        for(int i = 0; i < count; i++){
            UniTaskVoidLoop();
        }
    }

    async UniTaskVoid UniTaskLoop(){
        while(unitaskCanRun){
            await UniTask.Yield();
        }
    }

    async void UniTaskVoidLoop(){
        while(unitaskCanRun){
            await UniTask.Yield();
        }
    }

    public void StopAll(){
        StopAllCoroutines();
        unitaskCanRun = false;
    }
}
