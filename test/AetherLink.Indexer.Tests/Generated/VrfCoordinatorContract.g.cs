// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: vrf_coordinator_contract.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AetherLink.Contracts.VRF.Coordinator {

  /// <summary>Holder for reflection information generated from vrf_coordinator_contract.proto</summary>
  internal static partial class VrfCoordinatorContractReflection {

    #region Descriptor
    /// <summary>File descriptor for vrf_coordinator_contract.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static VrfCoordinatorContractReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ch52cmZfY29vcmRpbmF0b3JfY29udHJhY3QucHJvdG8SA3ZyZhoPYWVsZi9j",
            "b3JlLnByb3RvGhJhZWxmL29wdGlvbnMucHJvdG8aC2FjczEyLnByb3RvGhtv",
            "cmFjbGVfY29tbW9uX21lc3NhZ2UucHJvdG8aGmNvb3JkaW5hdG9yX2NvbnRy",
            "YWN0LnByb3RvGhtnb29nbGUvcHJvdG9idWYvZW1wdHkucHJvdG8aHmdvb2ds",
            "ZS9wcm90b2J1Zi93cmFwcGVycy5wcm90bxofZ29vZ2xlL3Byb3RvYnVmL3Rp",
            "bWVzdGFtcC5wcm90byKKAQoGQ29uZmlnEh8KF3JlcXVlc3RfdGltZW91dF9z",
            "ZWNvbmRzGAEgASgDEiUKHW1pbmltdW1fcmVxdWVzdF9jb25maXJtYXRpb25z",
            "GAIgASgDEiEKGW1heF9yZXF1ZXN0X2NvbmZpcm1hdGlvbnMYAyABKAMSFQoN",
            "bWF4X251bV93b3JkcxgEIAEoAyKSAQoMU3BlY2lmaWNEYXRhEhQKDGJsb2Nr",
            "X251bWJlchgBIAEoAxIRCgludW1fd29yZHMYAiABKAMSHAoIa2V5X2hhc2gY",
            "AyABKAsyCi5hZWxmLkhhc2gSHQoVcmVxdWVzdF9jb25maXJtYXRpb25zGAQg",
            "ASgDEhwKCHByZV9zZWVkGAUgASgLMgouYWVsZi5IYXNoIi4KCUNvbmZpZ1Nl",
            "dBIbCgZjb25maWcYASABKAsyCy52cmYuQ29uZmlnOgSguxgBMvsBChZWcmZD",
            "b29yZGluYXRvckNvbnRyYWN0EjIKCVNldENvbmZpZxILLnZyZi5Db25maWca",
            "Fi5nb29nbGUucHJvdG9idWYuRW1wdHkiABI3CglHZXRDb25maWcSFi5nb29n",
            "bGUucHJvdG9idWYuRW1wdHkaCy52cmYuQ29uZmlnIgWIifcBARp0ysr2AQth",
            "Y3MxMi5wcm90b8rK9gEaY29vcmRpbmF0b3JfY29udHJhY3QucHJvdG+yzPYB",
            "QEFldGhlckxpbmsuQ29udHJhY3RzLlZSRi5Db29yZGluYXRvci5WcmZDb29y",
            "ZGluYXRvckNvbnRyYWN0U3RhdGVCJ6oCJEFldGhlckxpbmsuQ29udHJhY3Rz",
            "LlZSRi5Db29yZGluYXRvcmIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AElf.Types.CoreReflection.Descriptor, global::AElf.OptionsReflection.Descriptor, global::AElf.Standards.ACS12.Acs12Reflection.Descriptor, global::Oracle.OracleCommonMessageReflection.Descriptor, global::Coordinator.CoordinatorContractReflection.Descriptor, global::Google.Protobuf.WellKnownTypes.EmptyReflection.Descriptor, global::Google.Protobuf.WellKnownTypes.WrappersReflection.Descriptor, global::Google.Protobuf.WellKnownTypes.TimestampReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AetherLink.Contracts.VRF.Coordinator.Config), global::AetherLink.Contracts.VRF.Coordinator.Config.Parser, new[]{ "RequestTimeoutSeconds", "MinimumRequestConfirmations", "MaxRequestConfirmations", "MaxNumWords" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::AetherLink.Contracts.VRF.Coordinator.SpecificData), global::AetherLink.Contracts.VRF.Coordinator.SpecificData.Parser, new[]{ "BlockNumber", "NumWords", "KeyHash", "RequestConfirmations", "PreSeed" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::AetherLink.Contracts.VRF.Coordinator.ConfigSet), global::AetherLink.Contracts.VRF.Coordinator.ConfigSet.Parser, new[]{ "Config" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  internal sealed partial class Config : pb::IMessage<Config>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Config> _parser = new pb::MessageParser<Config>(() => new Config());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Config> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AetherLink.Contracts.VRF.Coordinator.VrfCoordinatorContractReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Config() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Config(Config other) : this() {
      requestTimeoutSeconds_ = other.requestTimeoutSeconds_;
      minimumRequestConfirmations_ = other.minimumRequestConfirmations_;
      maxRequestConfirmations_ = other.maxRequestConfirmations_;
      maxNumWords_ = other.maxNumWords_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Config Clone() {
      return new Config(this);
    }

    /// <summary>Field number for the "request_timeout_seconds" field.</summary>
    public const int RequestTimeoutSecondsFieldNumber = 1;
    private long requestTimeoutSeconds_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long RequestTimeoutSeconds {
      get { return requestTimeoutSeconds_; }
      set {
        requestTimeoutSeconds_ = value;
      }
    }

    /// <summary>Field number for the "minimum_request_confirmations" field.</summary>
    public const int MinimumRequestConfirmationsFieldNumber = 2;
    private long minimumRequestConfirmations_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long MinimumRequestConfirmations {
      get { return minimumRequestConfirmations_; }
      set {
        minimumRequestConfirmations_ = value;
      }
    }

    /// <summary>Field number for the "max_request_confirmations" field.</summary>
    public const int MaxRequestConfirmationsFieldNumber = 3;
    private long maxRequestConfirmations_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long MaxRequestConfirmations {
      get { return maxRequestConfirmations_; }
      set {
        maxRequestConfirmations_ = value;
      }
    }

    /// <summary>Field number for the "max_num_words" field.</summary>
    public const int MaxNumWordsFieldNumber = 4;
    private long maxNumWords_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long MaxNumWords {
      get { return maxNumWords_; }
      set {
        maxNumWords_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Config);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Config other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (RequestTimeoutSeconds != other.RequestTimeoutSeconds) return false;
      if (MinimumRequestConfirmations != other.MinimumRequestConfirmations) return false;
      if (MaxRequestConfirmations != other.MaxRequestConfirmations) return false;
      if (MaxNumWords != other.MaxNumWords) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (RequestTimeoutSeconds != 0L) hash ^= RequestTimeoutSeconds.GetHashCode();
      if (MinimumRequestConfirmations != 0L) hash ^= MinimumRequestConfirmations.GetHashCode();
      if (MaxRequestConfirmations != 0L) hash ^= MaxRequestConfirmations.GetHashCode();
      if (MaxNumWords != 0L) hash ^= MaxNumWords.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (RequestTimeoutSeconds != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(RequestTimeoutSeconds);
      }
      if (MinimumRequestConfirmations != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(MinimumRequestConfirmations);
      }
      if (MaxRequestConfirmations != 0L) {
        output.WriteRawTag(24);
        output.WriteInt64(MaxRequestConfirmations);
      }
      if (MaxNumWords != 0L) {
        output.WriteRawTag(32);
        output.WriteInt64(MaxNumWords);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (RequestTimeoutSeconds != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(RequestTimeoutSeconds);
      }
      if (MinimumRequestConfirmations != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(MinimumRequestConfirmations);
      }
      if (MaxRequestConfirmations != 0L) {
        output.WriteRawTag(24);
        output.WriteInt64(MaxRequestConfirmations);
      }
      if (MaxNumWords != 0L) {
        output.WriteRawTag(32);
        output.WriteInt64(MaxNumWords);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (RequestTimeoutSeconds != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(RequestTimeoutSeconds);
      }
      if (MinimumRequestConfirmations != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(MinimumRequestConfirmations);
      }
      if (MaxRequestConfirmations != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(MaxRequestConfirmations);
      }
      if (MaxNumWords != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(MaxNumWords);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Config other) {
      if (other == null) {
        return;
      }
      if (other.RequestTimeoutSeconds != 0L) {
        RequestTimeoutSeconds = other.RequestTimeoutSeconds;
      }
      if (other.MinimumRequestConfirmations != 0L) {
        MinimumRequestConfirmations = other.MinimumRequestConfirmations;
      }
      if (other.MaxRequestConfirmations != 0L) {
        MaxRequestConfirmations = other.MaxRequestConfirmations;
      }
      if (other.MaxNumWords != 0L) {
        MaxNumWords = other.MaxNumWords;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            RequestTimeoutSeconds = input.ReadInt64();
            break;
          }
          case 16: {
            MinimumRequestConfirmations = input.ReadInt64();
            break;
          }
          case 24: {
            MaxRequestConfirmations = input.ReadInt64();
            break;
          }
          case 32: {
            MaxNumWords = input.ReadInt64();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            RequestTimeoutSeconds = input.ReadInt64();
            break;
          }
          case 16: {
            MinimumRequestConfirmations = input.ReadInt64();
            break;
          }
          case 24: {
            MaxRequestConfirmations = input.ReadInt64();
            break;
          }
          case 32: {
            MaxNumWords = input.ReadInt64();
            break;
          }
        }
      }
    }
    #endif

  }

  internal sealed partial class SpecificData : pb::IMessage<SpecificData>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SpecificData> _parser = new pb::MessageParser<SpecificData>(() => new SpecificData());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<SpecificData> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AetherLink.Contracts.VRF.Coordinator.VrfCoordinatorContractReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SpecificData() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SpecificData(SpecificData other) : this() {
      blockNumber_ = other.blockNumber_;
      numWords_ = other.numWords_;
      keyHash_ = other.keyHash_ != null ? other.keyHash_.Clone() : null;
      requestConfirmations_ = other.requestConfirmations_;
      preSeed_ = other.preSeed_ != null ? other.preSeed_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SpecificData Clone() {
      return new SpecificData(this);
    }

    /// <summary>Field number for the "block_number" field.</summary>
    public const int BlockNumberFieldNumber = 1;
    private long blockNumber_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long BlockNumber {
      get { return blockNumber_; }
      set {
        blockNumber_ = value;
      }
    }

    /// <summary>Field number for the "num_words" field.</summary>
    public const int NumWordsFieldNumber = 2;
    private long numWords_;
    /// <summary>
    /// amount of random values
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long NumWords {
      get { return numWords_; }
      set {
        numWords_ = value;
      }
    }

    /// <summary>Field number for the "key_hash" field.</summary>
    public const int KeyHashFieldNumber = 3;
    private global::AElf.Types.Hash keyHash_;
    /// <summary>
    /// hash of the public key
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AElf.Types.Hash KeyHash {
      get { return keyHash_; }
      set {
        keyHash_ = value;
      }
    }

    /// <summary>Field number for the "request_confirmations" field.</summary>
    public const int RequestConfirmationsFieldNumber = 4;
    private long requestConfirmations_;
    /// <summary>
    /// amount of blocks to wait
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long RequestConfirmations {
      get { return requestConfirmations_; }
      set {
        requestConfirmations_ = value;
      }
    }

    /// <summary>Field number for the "pre_seed" field.</summary>
    public const int PreSeedFieldNumber = 5;
    private global::AElf.Types.Hash preSeed_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AElf.Types.Hash PreSeed {
      get { return preSeed_; }
      set {
        preSeed_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as SpecificData);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(SpecificData other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (BlockNumber != other.BlockNumber) return false;
      if (NumWords != other.NumWords) return false;
      if (!object.Equals(KeyHash, other.KeyHash)) return false;
      if (RequestConfirmations != other.RequestConfirmations) return false;
      if (!object.Equals(PreSeed, other.PreSeed)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (BlockNumber != 0L) hash ^= BlockNumber.GetHashCode();
      if (NumWords != 0L) hash ^= NumWords.GetHashCode();
      if (keyHash_ != null) hash ^= KeyHash.GetHashCode();
      if (RequestConfirmations != 0L) hash ^= RequestConfirmations.GetHashCode();
      if (preSeed_ != null) hash ^= PreSeed.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (BlockNumber != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(BlockNumber);
      }
      if (NumWords != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(NumWords);
      }
      if (keyHash_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(KeyHash);
      }
      if (RequestConfirmations != 0L) {
        output.WriteRawTag(32);
        output.WriteInt64(RequestConfirmations);
      }
      if (preSeed_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(PreSeed);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (BlockNumber != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(BlockNumber);
      }
      if (NumWords != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(NumWords);
      }
      if (keyHash_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(KeyHash);
      }
      if (RequestConfirmations != 0L) {
        output.WriteRawTag(32);
        output.WriteInt64(RequestConfirmations);
      }
      if (preSeed_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(PreSeed);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (BlockNumber != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(BlockNumber);
      }
      if (NumWords != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(NumWords);
      }
      if (keyHash_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(KeyHash);
      }
      if (RequestConfirmations != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(RequestConfirmations);
      }
      if (preSeed_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(PreSeed);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(SpecificData other) {
      if (other == null) {
        return;
      }
      if (other.BlockNumber != 0L) {
        BlockNumber = other.BlockNumber;
      }
      if (other.NumWords != 0L) {
        NumWords = other.NumWords;
      }
      if (other.keyHash_ != null) {
        if (keyHash_ == null) {
          KeyHash = new global::AElf.Types.Hash();
        }
        KeyHash.MergeFrom(other.KeyHash);
      }
      if (other.RequestConfirmations != 0L) {
        RequestConfirmations = other.RequestConfirmations;
      }
      if (other.preSeed_ != null) {
        if (preSeed_ == null) {
          PreSeed = new global::AElf.Types.Hash();
        }
        PreSeed.MergeFrom(other.PreSeed);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            BlockNumber = input.ReadInt64();
            break;
          }
          case 16: {
            NumWords = input.ReadInt64();
            break;
          }
          case 26: {
            if (keyHash_ == null) {
              KeyHash = new global::AElf.Types.Hash();
            }
            input.ReadMessage(KeyHash);
            break;
          }
          case 32: {
            RequestConfirmations = input.ReadInt64();
            break;
          }
          case 42: {
            if (preSeed_ == null) {
              PreSeed = new global::AElf.Types.Hash();
            }
            input.ReadMessage(PreSeed);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            BlockNumber = input.ReadInt64();
            break;
          }
          case 16: {
            NumWords = input.ReadInt64();
            break;
          }
          case 26: {
            if (keyHash_ == null) {
              KeyHash = new global::AElf.Types.Hash();
            }
            input.ReadMessage(KeyHash);
            break;
          }
          case 32: {
            RequestConfirmations = input.ReadInt64();
            break;
          }
          case 42: {
            if (preSeed_ == null) {
              PreSeed = new global::AElf.Types.Hash();
            }
            input.ReadMessage(PreSeed);
            break;
          }
        }
      }
    }
    #endif

  }

  /// <summary>
  /// log event
  /// </summary>
  internal sealed partial class ConfigSet : pb::IMessage<ConfigSet>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ConfigSet> _parser = new pb::MessageParser<ConfigSet>(() => new ConfigSet());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ConfigSet> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AetherLink.Contracts.VRF.Coordinator.VrfCoordinatorContractReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ConfigSet() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ConfigSet(ConfigSet other) : this() {
      config_ = other.config_ != null ? other.config_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ConfigSet Clone() {
      return new ConfigSet(this);
    }

    /// <summary>Field number for the "config" field.</summary>
    public const int ConfigFieldNumber = 1;
    private global::AetherLink.Contracts.VRF.Coordinator.Config config_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AetherLink.Contracts.VRF.Coordinator.Config Config {
      get { return config_; }
      set {
        config_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ConfigSet);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ConfigSet other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Config, other.Config)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (config_ != null) hash ^= Config.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (config_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Config);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (config_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Config);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (config_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Config);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ConfigSet other) {
      if (other == null) {
        return;
      }
      if (other.config_ != null) {
        if (config_ == null) {
          Config = new global::AetherLink.Contracts.VRF.Coordinator.Config();
        }
        Config.MergeFrom(other.Config);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (config_ == null) {
              Config = new global::AetherLink.Contracts.VRF.Coordinator.Config();
            }
            input.ReadMessage(Config);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            if (config_ == null) {
              Config = new global::AetherLink.Contracts.VRF.Coordinator.Config();
            }
            input.ReadMessage(Config);
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
