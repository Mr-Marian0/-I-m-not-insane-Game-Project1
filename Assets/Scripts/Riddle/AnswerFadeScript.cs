using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerFadeScript : MonoBehaviour
{

    public Animator anim;

    //Set InheriRandom(Script) to true
    public RandomQuestion InheritRandomQuestionScript;

    void OnEnable()
    {
        StartCoroutine(DelayTheTextAnswer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DelayTheTextAnswer()
    {
        yield return new WaitForSeconds(8);
        anim.SetBool("AnswerFadeout", true);
        InheritRandomQuestionScript.IsAnswerFadeAnimationFinished = true;
    }

}
