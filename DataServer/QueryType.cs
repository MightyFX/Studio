namespace MightyFX.Data
{
    /// <summary>
    /// The type of query data the field contains.
    /// </summary>
    public enum QueryType
    {
        Default,
        LatestSample,
        ////InterpolatedOverInterval,
        ////SampleClosestToInterval,
        ////AverageOverInterval,
        ////MinimumOverInterval,
        ////MaximumOverInterval,
        ////IntegralOverInterval,
        ////UniquePointsOnly,
        ////AllPoints
    }
}