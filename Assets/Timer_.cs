namespace DefaultNamespace
{
    public class Timer_
    {
        bool ended = true;
        public bool Ended => ended;
        float timeLeft = 0 ;

        public float TimeLeft
        {
            get => timeLeft;
            set => timeLeft = value;
        }

        public void start(float time)
        {
            timeLeft = time;
            ended = false;
        }
        public void update(float dt = 0)
        {
            if (timeLeft > 0)
            {
                ended = false;
                timeLeft -= dt;
            }
            else
            {
                ended = true;
                timeLeft = 0;
            }
        }
    }
}