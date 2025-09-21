using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RandomQuestion : MonoBehaviour
{

    public TextMeshProUGUI UItext;
    public TextMeshProUGUI UItextAnswers;
    public int GenerateQuestion;
    public int num1 = 2;

    bool RunOnce = true;
    public bool SpawnEnemy = false;

    //Ask the AnswerFadeScript if the animation is finished
    public bool IsAnswerFadeAnimationFinished = false;

    public Animator anim;
    public Animator AnsAnim;
    public Transform MoveBackImage;
    public GameObject EnableAnswerFadeScript; // 210.4684 99.1603
    public GameObject Joysick1;

    public int[] AnswerKey50 = new int[50] {0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1};

    public string[] Questions1 = new string[50] { "What goes up but never ever comes down?" , "You spot a boat full of people but there isn�t a single person on board. How is that possible?",
        "If you have a bowl with six apples and you take away four, how many do you have?",
        "How do you make the number one disappear and make it in other term as missing?",
        "What is the answer to this question?", "What word becomes longer after you add two letters to it?",
        "What�s something that has a lot of holes in it but can still hold water?",
        "How many times can you cut a cake in half?",
        "What�s something can you hold without touching it?",
        "What�s something that belongs to you, but others seem to use it more than you?",
        "What Type Of Dress Can Never Be Worn?", "People Buy Me To Eat, But Never Eat Me. What Am I?",
        "What Has Four Wheels And Flies?", "What Goes Up As Soon As The Rain Comes Down?",
        "What Word Starts With IS, Ends With AND, And Has LA In The Middle?",
        "If I Smile, It Also Smiles. If I Cry, It Also Cries. If I Shout, It Does Nothing. What Is It?",
        "What Happens When You Throw A Blue Rock Into The Yellow Sea?", "He�s Small, But He Can Climb A Tower.",
        "I Am Nothing But Holes Tied To Holes, Yet I Am Strong As Iron.", "What Kind Of Room Doesn�t Have Physical Walls?",
        "I Have No Life, But I Can Die, What Am I?", "What Is Blank When It�s Clean And White When It�s Dirty?", "What Has Legs But Doesn�t Walk?",
        "What Has Words But Never Speaks?", "What Has A Head And A Tail But No Body?", "What Building Has The Most Stories?",
        "What Goes Through Cities And Fields But Never Moves?", "People Make Me, Save Me, Change Me, Raise Me. What Am I?",
        "What are the 5 senses?", "What percentage of the human body is made up of water?", "What is the capital of China?",
        "How many colors are there in a rainbow?", "Which direction does the sun rise?", "Which planet is the hottest in the solar system?",
        "How many hearts does an octopus have?", "How many eyes does a bee have?", "Who is considered the Father of Relativity?",
        "What is a group of cats called?", "What mammal has the most powerful bite?", "What is a group of parrots called?",
        "Where on the body are a crab�s taste buds?", "How many legs does a lobster have?", "What is a polar bear�s skin color?",
        "What is the only big cat that doesn�t roar?", "Which bird�s eye is bigger than its brain?",
        "What is a group of camels called?", "What color is a giraffe�s tongue?", "What animal has stripes on its skin as well as its fur?",
        "Which animal has rectangular pupils?", "Which among the choices is the most poisonous aquatic animal in the world?"};

    public string[] Answers11 = new string[50] { "Your Age / Cat / A tree", " They are hiding / Everyone on board is merried / Everyone is sleeping on the floor",
        " The 6 bowl / 10 / The four you took", "Add a �G� and it�s gone! / close your eyes / subtract it by itself",
        " Nothing / What / A knife", " ruler / snail / Long", "Sponge / Toothbrush / Cloud", " Eat the half / Once / Remove the two letters on it!",
        "Use gloves / Air / Your Breath", "Your name / Your wallet / Yourself", "Mermaid Dress / Address / Cocktail dress",
        "A Lipstick / A Candy / A plate ", "garbage truck / Jollibee / A car without wheels","Hair / Umbrella / Your head",
        "SAND / ISLAND / FISH", " Mime / Friend / Mirror", "It sinks / you are killing a green turtle / A rock music", "worm climbing a ladder / cotton candy / an ant",
        "Chains / Volcano / Canyon", "Golf field / A chat room / A gossip of neighbours", "A Ghost / A Dye / A baterry",
        "chalkboard / Clean white shirt / Chinese Pig", "Chalk / table /Pentrova", "A blank paper / A nerd / book",
        "Coin / An emo without friends / Snake", "Quantrium / Library / Engineering book",
        "Trash / Snail / Road ", "Money / Earth / Nile crocodile", "(Sight, scent, taste, smell, and touch) / (vission, hearing, taste, smell, and touch) / . . .",
        "73% / 70% / 60%", " Beijing / Wuhan Beijing / Zhua Zen Beijing", "6 / 7 / 8", "North / South / East",
        "Venus / Sun / Mercury", "8 / 3 / 4", "2 / 4 / 5", "Albert Einstein / John Rell / Orthon Norva",
        "Felines / Clowder / Pawners", "Wild Grizzly Bear Found in Australia / Pregnant Female Nile Crocodile / Hippopotamus with a bite force of 1,800psi!",
        "pandemonium / avifuana / wrablers", "eyes / Feet / Antenna", "12 including the wig feet / 12 / 10", "Black / Gray / During Night it is black and white when its day",
        "House cat / Cheetah / Baby jaguar", "Penguins / Owl / Ostrich", "Caravan / Cameroons / Cargoms", "Red / Purple / Gray", "House turnish cat in Ireland / Zebra / Tiger",
        "Goat / Cuttlefish / Leaf-tailed geckos", "Nefilus Stone fish / Pufferfish / PurpleFawn JellyFish"}; //Q1 lipat sa Q2 next

    public void OnEnable()
    {
        StartCoroutine(DelayTheTextQuestion());
    }

    void Update()
    {

        PickedQuestions(GenerateQuestion);

        //Background Move Up
        if (true && RunOnce == true) // ORIGINAL F.((IsAnswerFadeAnimationFinished == true && RunOnce == true))
        {
            Debug.Log("END ANIM");
            MoveBackImage.position += new Vector3(0, 0.01f, 0);
            if (MoveBackImage.transform.position.y >= 0.03) 
            {
                RunOnce = false;
                Joysick1.SetActive(true);
                SpawnEnemy = true;
            }
        }

    }

    //Set the Parameters to TRUE (Delay)
    public IEnumerator DelayTheTextQuestion()
    {
        GenerateQuestion = Random.Range(0, 49);
        Debug.Log("The Question is: " + GenerateQuestion);

        yield return new WaitForSeconds(2);
        anim.SetBool("QueFadeIn", true);

        yield return new WaitForSeconds(7);
        anim.SetBool("QueFadeOut", true);

        yield return new WaitForSeconds(3);
        EnableAnswerFadeScript.SetActive(true);

    }

    //Set the QUESTION AND ANSWER
    public void PickedQuestions(int Q)
    {
        UItext.text = Questions1[Q];
        UItextAnswers.text = Answers11[Q];
    }

}
