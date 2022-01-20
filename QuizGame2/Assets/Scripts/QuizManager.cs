using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{

    public List<QuestionAndAnswers> QnA; // A list of questions and answers (custom data type)
    public GameObject[] options; // Corresponding to the 4 buttons
    public int currentQuestionIndex; // Index of the current Question-Answers set

    public Text QuestionText; // The Text of the Question-Answers set
    public Text ScoreText; // Final score text
    int totalQuestions; // total number of questions
    public int score; // total score (correct answers made)

    public GameObject QuizPanel;
    public GameObject GameOverPanel;
    public GameObject MainMenuPanel;


    void Start()
    {
        MainMenuPanel.SetActive(true);
        GameOverPanel.SetActive(false);
        QuizPanel.SetActive(false);
         
    }

    void Update()
    {
        QuizPanel.transform.GetChild(3).GetComponent<Text>().text = "Score: " + score;
    }

    public void startGame()
    {
        totalQuestions = QnA.Count;

        MainMenuPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        QuizPanel.SetActive(true);
        

        generateQuestion();

    }

    public void correct()
    {
        score++;
        Debug.Log("Correct!");
        QnA.RemoveAt(currentQuestionIndex); // Remove the answered question;
        generateQuestion(); // generate the next question
    }
    public void wrong()
    {
        Debug.Log("Wrong!");
        QnA.RemoveAt(currentQuestionIndex); // Remove the answered question;
        generateQuestion(); // generate the next question
    }

    public void retry()
    {
        MainMenuPanel.SetActive(true);
        GameOverPanel.SetActive(false);
        QuizPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        QuizPanel.SetActive(false);
        GameOverPanel.SetActive(true);
        ScoreText.text = score + " / " + totalQuestions;
    }

    void generateQuestion()
    {
        if (QnA.Count > 0)
        {

            currentQuestionIndex = Random.Range(0, QnA.Count); // Get a random question using indexing
            QuestionText.text = QnA[currentQuestionIndex].Question; // Set the question text to the question
            SetAnswers(); // Set the answers to the button and find the correct answer;
        }
        else
        {
            Debug.Log("GG");
            GameOver();
        }
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false; // Set all answers to false first
            QuizPanel.transform.GetChild(0).GetChild(i).GetComponent<Text>().text = "Wrong!";

            // Set possible answers to each button
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestionIndex].Answers[i]; 

            // Find the correct answer
            if (QnA[currentQuestionIndex].CorrectAnswer == i)
            {
                QuizPanel.transform.GetChild(0).GetChild(i).GetComponent<Text>().text = "Correct!";
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }
}
