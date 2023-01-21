namespace Quiz
{
    public interface IQuizProcessService
    {
        void Begin();
        void End(bool isWin);
    }
}