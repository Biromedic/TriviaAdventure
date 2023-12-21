[System.Serializable]
public class Question
{
    public string fact;
    public int isTrue;

    // You can add a constructor to make it easier to create questions
    public Question(string fact, int isTrue)
    {
        this.fact = fact;
        this.isTrue = isTrue;
    }

    // Convert the integer to a bool
    public bool IsTrue
    {
        get
        {
            return isTrue == 1;
        }
    }
}