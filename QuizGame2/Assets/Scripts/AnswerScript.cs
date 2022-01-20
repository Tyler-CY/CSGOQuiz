using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;


    //[SerializeField]
    //public Animator animator;


    void Start()
    {
        quizManager = FindObjectOfType<QuizManager>();
        //animator = FindObjectOfType<Animator>();
    }
    public void Answer()
    {
        //animator.SetTrigger("New Bool");
        if (isCorrect)
        {
            quizManager.correct();
        }
        else
        {
            quizManager.wrong();
        }
    }
}
