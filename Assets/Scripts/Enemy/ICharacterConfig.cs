namespace LearnGame.Enemy
{
    public interface IEnemyAIConfig
    {
        float ViewRadius { get; }
        float MinHpForEscapePercent { get; }
        float ProbabilityEscapePercent { get; }
        float SafeDistance { get; }
    }
}
