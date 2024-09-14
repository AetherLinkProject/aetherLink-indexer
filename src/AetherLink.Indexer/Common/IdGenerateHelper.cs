using AElf.Types;

namespace AetherLink.Indexer.Common;

public static class IdGenerateHelper
{
    private const string OcrPrefix = "ocr";
    private const string LogInfoPrefix = "loginfo";
    private const string ConfigSetPrefix = "configset";
    private const string TransmittedPrefix = "transmitted";
    private const string RequestCancelPrefix = "requestcancel";

    public static string GetId(params object[] inputs) => inputs.JoinAsString("-");
    public static string GetConfigSetId(string chainId) => GetId(ConfigSetPrefix, chainId);
    public static string GetOcrId(string chainId, string requestId) => GetId(OcrPrefix, chainId, requestId);


    public static string GetRequestCancelId(string chainId, string requestId)
        => GetId(RequestCancelPrefix, chainId, requestId);

    public static string GetTransmittedId(string chainId, string requestId, string configDigest, long epochAndRound)
        => GetId(TransmittedPrefix, chainId, requestId, configDigest, epochAndRound);

    public static string GetLogInfoId(string chainId, long blockHeight, string transactionId, string methodName,
        string eventName, string contractAddress, int index) => GetId(LogInfoPrefix, chainId, blockHeight,
        transactionId, methodName, eventName, contractAddress, index);
}