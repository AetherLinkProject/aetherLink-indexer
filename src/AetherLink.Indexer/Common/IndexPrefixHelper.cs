namespace AetherLink.Indexer.Common;

public static class IndexPrefixHelper
{
    public static string GetOcrIndexId(string chainId, string requestId)
    {
        return IdGenerateHelper.GetId(IndexPrefix.OcrPrefix, chainId, requestId);
    }

    public static string GetCommitmentIndexId(string chainId, string requestId)
    {
        return IdGenerateHelper.GetId(IndexPrefix.CommitmentPrefix, chainId, requestId);
    }

    public static string GetTransmittedIndexId(string chainId, string requestId, string configDigest,
        long epochAndRound)
    {
        return IdGenerateHelper.GetId(IndexPrefix.TransmittedPrefix, chainId, requestId, configDigest, epochAndRound);
    }

    public static string GetLatestRoundIndexId(string chainId, string oracleContractAddress)
    {
        return IdGenerateHelper.GetId(IndexPrefix.LatestRoundPrefix, chainId, oracleContractAddress);
    }

    public static string GetConfigSetIndexId(string chainId)
    {
        return IdGenerateHelper.GetId(IndexPrefix.ConfigSetPrefix, chainId);
    }
    
    public static string GetRequestCancelIndexId(string chainId, string requestId)
    {
        return IdGenerateHelper.GetId(IndexPrefix.RequestCancelPrefix, chainId, requestId);
    }
}

public static class IndexPrefix
{
    public static readonly string OcrPrefix = "ocr";
    public static readonly string CommitmentPrefix = "commitment";
    public static readonly string TransmittedPrefix = "transmitted";
    public static readonly string LatestRoundPrefix = "latestround";
    public static readonly string ConfigSetPrefix = "configset";
    public static readonly string RequestCancelPrefix = "requestcancel";
}