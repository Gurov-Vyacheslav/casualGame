namespace LearnGame
{
    public static class MathUtils
    {
        public static float ToPercentage(float currentValue, float maxValue)
        {
            return currentValue / maxValue * 100f;
        }
    }
}