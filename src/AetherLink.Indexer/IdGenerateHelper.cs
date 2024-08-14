namespace AetherLink.Indexer;

public static class IdGenerateHelper
{
    public const string OcrPrefix = "ocr";
    public const string AIPrefix = "ai";
    public const string LogInfoPrefix = "loginfo";
    public const string ConfigSetPrefix = "configset";
    public const string TransmittedPrefix = "transmitted";
    public const string AIReportTransmittedPrefix = "aiReportTransmitted";
    public const string RequestCancelPrefix = "requestcancel";

    public static string GetId(params object[] inputs) => inputs.JoinAsString("-");
}