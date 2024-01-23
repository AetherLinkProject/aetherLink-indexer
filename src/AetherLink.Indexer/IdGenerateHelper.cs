namespace AetherLink.Indexer;

public static class IdGenerateHelper
{
    private const string OcrPrefix = "ocr";
    private const string ConfigSetPrefix = "configset";
    private const string TransmittedPrefix = "transmitted";
    private const string RequestCancelPrefix = "requestcancel";

    public static string GetOcrIndexId(string chainId, string requestId)
    {
        return GetId(OcrPrefix, chainId, requestId);
    }

    public static string GetTransmittedIndexId(string chainId, string requestId, string configDigest,
        long epochAndRound)
    {
        return GetId(TransmittedPrefix, chainId, requestId, configDigest, epochAndRound);
    }

    public static string GetConfigSetIndexId(string chainId)
    {
        return GetId(ConfigSetPrefix, chainId);
    }

    public static string GetRequestCancelIndexId(string chainId, string requestId)
    {
        return GetId(RequestCancelPrefix, chainId, requestId);
    }

    private static string GetId(params object[] inputs)
    {
        return inputs.JoinAsString("-");
    }
}