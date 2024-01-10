// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: coordinator_contract.proto
// </auto-generated>
// Original file comments:
// the version of the language, use proto3 for contracts
#pragma warning disable 0414, 1591
#region Designer generated code

using System.Collections.Generic;
using aelf = global::AElf.CSharp.Core;

namespace Coordinator {

  #region Events
  internal partial class AdminTransferRequested : aelf::IEvent<AdminTransferRequested>
  {
    public global::System.Collections.Generic.IEnumerable<AdminTransferRequested> GetIndexed()
    {
      return new List<AdminTransferRequested>
      {
      };
    }

    public AdminTransferRequested GetNonIndexed()
    {
      return new AdminTransferRequested
      {
        From = From,
        To = To,
      };
    }
  }

  internal partial class AdminTransferred : aelf::IEvent<AdminTransferred>
  {
    public global::System.Collections.Generic.IEnumerable<AdminTransferred> GetIndexed()
    {
      return new List<AdminTransferred>
      {
      };
    }

    public AdminTransferred GetNonIndexed()
    {
      return new AdminTransferred
      {
        From = From,
        To = To,
      };
    }
  }

  internal partial class Paused : aelf::IEvent<Paused>
  {
    public global::System.Collections.Generic.IEnumerable<Paused> GetIndexed()
    {
      return new List<Paused>
      {
      };
    }

    public Paused GetNonIndexed()
    {
      return new Paused
      {
        Account = Account,
      };
    }
  }

  internal partial class Unpaused : aelf::IEvent<Unpaused>
  {
    public global::System.Collections.Generic.IEnumerable<Unpaused> GetIndexed()
    {
      return new List<Unpaused>
      {
      };
    }

    public Unpaused GetNonIndexed()
    {
      return new Unpaused
      {
        Account = Account,
      };
    }
  }

  internal partial class RequestSent : aelf::IEvent<RequestSent>
  {
    public global::System.Collections.Generic.IEnumerable<RequestSent> GetIndexed()
    {
      return new List<RequestSent>
      {
      };
    }

    public RequestSent GetNonIndexed()
    {
      return new RequestSent
      {
        RequestId = RequestId,
        RequestingContract = RequestingContract,
        RequestingInitiator = RequestingInitiator,
        Commitment = Commitment,
      };
    }
  }

  internal partial class Reported : aelf::IEvent<Reported>
  {
    public global::System.Collections.Generic.IEnumerable<Reported> GetIndexed()
    {
      return new List<Reported>
      {
      };
    }

    public Reported GetNonIndexed()
    {
      return new Reported
      {
        RequestId = RequestId,
        Transmitter = Transmitter,
      };
    }
  }

  internal partial class RequestTypeIndexSet : aelf::IEvent<RequestTypeIndexSet>
  {
    public global::System.Collections.Generic.IEnumerable<RequestTypeIndexSet> GetIndexed()
    {
      return new List<RequestTypeIndexSet>
      {
      };
    }

    public RequestTypeIndexSet GetNonIndexed()
    {
      return new RequestTypeIndexSet
      {
        RequestTypeIndex = RequestTypeIndex,
      };
    }
  }

  internal partial class CommitmentDeleted : aelf::IEvent<CommitmentDeleted>
  {
    public global::System.Collections.Generic.IEnumerable<CommitmentDeleted> GetIndexed()
    {
      return new List<CommitmentDeleted>
      {
      };
    }

    public CommitmentDeleted GetNonIndexed()
    {
      return new CommitmentDeleted
      {
        RequestId = RequestId,
      };
    }
  }

  #endregion
  /// <summary>
  /// the contract definition: a gRPC service definition.
  /// </summary>
  internal static partial class CoordinatorInterfaceContainer
  {
    static readonly string __ServiceName = "coordinator.CoordinatorInterface";

    #region Marshallers
    static readonly aelf::Marshaller<global::Coordinator.InitializeInput> __Marshaller_coordinator_InitializeInput = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Coordinator.InitializeInput.Parser.ParseFrom);
    static readonly aelf::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_google_protobuf_Empty = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Protobuf.WellKnownTypes.Empty.Parser.ParseFrom);
    static readonly aelf::Marshaller<global::AElf.Types.Address> __Marshaller_aelf_Address = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::AElf.Types.Address.Parser.ParseFrom);
    static readonly aelf::Marshaller<global::Google.Protobuf.WellKnownTypes.Int32Value> __Marshaller_google_protobuf_Int32Value = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Protobuf.WellKnownTypes.Int32Value.Parser.ParseFrom);
    static readonly aelf::Marshaller<global::Coordinator.Request> __Marshaller_coordinator_Request = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Coordinator.Request.Parser.ParseFrom);
    static readonly aelf::Marshaller<global::Coordinator.ReportInput> __Marshaller_coordinator_ReportInput = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Coordinator.ReportInput.Parser.ParseFrom);
    static readonly aelf::Marshaller<global::AElf.Types.Hash> __Marshaller_aelf_Hash = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::AElf.Types.Hash.Parser.ParseFrom);
    static readonly aelf::Marshaller<global::Google.Protobuf.WellKnownTypes.BoolValue> __Marshaller_google_protobuf_BoolValue = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Protobuf.WellKnownTypes.BoolValue.Parser.ParseFrom);
    #endregion

    #region Methods
    static readonly aelf::Method<global::Coordinator.InitializeInput, global::Google.Protobuf.WellKnownTypes.Empty> __Method_Initialize = new aelf::Method<global::Coordinator.InitializeInput, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "Initialize",
        __Marshaller_coordinator_InitializeInput,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::AElf.Types.Address, global::Google.Protobuf.WellKnownTypes.Empty> __Method_TransferAdmin = new aelf::Method<global::AElf.Types.Address, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "TransferAdmin",
        __Marshaller_aelf_Address,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Google.Protobuf.WellKnownTypes.Empty> __Method_AcceptAdmin = new aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "AcceptAdmin",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Google.Protobuf.WellKnownTypes.Empty> __Method_Pause = new aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "Pause",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Google.Protobuf.WellKnownTypes.Empty> __Method_Unpause = new aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "Unpause",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::AElf.Types.Address, global::Google.Protobuf.WellKnownTypes.Empty> __Method_SetOracleContractAddress = new aelf::Method<global::AElf.Types.Address, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "SetOracleContractAddress",
        __Marshaller_aelf_Address,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::Google.Protobuf.WellKnownTypes.Int32Value, global::Google.Protobuf.WellKnownTypes.Empty> __Method_SetRequestTypeIndex = new aelf::Method<global::Google.Protobuf.WellKnownTypes.Int32Value, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "SetRequestTypeIndex",
        __Marshaller_google_protobuf_Int32Value,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::Coordinator.Request, global::Google.Protobuf.WellKnownTypes.Empty> __Method_SendRequest = new aelf::Method<global::Coordinator.Request, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "SendRequest",
        __Marshaller_coordinator_Request,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::Coordinator.ReportInput, global::Google.Protobuf.WellKnownTypes.Empty> __Method_Report = new aelf::Method<global::Coordinator.ReportInput, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "Report",
        __Marshaller_coordinator_ReportInput,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::AElf.Types.Hash, global::Google.Protobuf.WellKnownTypes.Empty> __Method_DeleteCommitment = new aelf::Method<global::AElf.Types.Hash, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "DeleteCommitment",
        __Marshaller_aelf_Hash,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::AElf.Types.Address> __Method_GetAdmin = new aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::AElf.Types.Address>(
        aelf::MethodType.View,
        __ServiceName,
        "GetAdmin",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_aelf_Address);

    static readonly aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Google.Protobuf.WellKnownTypes.BoolValue> __Method_IsPaused = new aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Google.Protobuf.WellKnownTypes.BoolValue>(
        aelf::MethodType.View,
        __ServiceName,
        "IsPaused",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_google_protobuf_BoolValue);

    static readonly aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::AElf.Types.Address> __Method_GetOracleContractAddress = new aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::AElf.Types.Address>(
        aelf::MethodType.View,
        __ServiceName,
        "GetOracleContractAddress",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_aelf_Address);

    static readonly aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Google.Protobuf.WellKnownTypes.Int32Value> __Method_GetRequestTypeIndex = new aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Google.Protobuf.WellKnownTypes.Int32Value>(
        aelf::MethodType.View,
        __ServiceName,
        "GetRequestTypeIndex",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_google_protobuf_Int32Value);

    static readonly aelf::Method<global::AElf.Types.Hash, global::AElf.Types.Hash> __Method_GetCommitmentHash = new aelf::Method<global::AElf.Types.Hash, global::AElf.Types.Hash>(
        aelf::MethodType.View,
        __ServiceName,
        "GetCommitmentHash",
        __Marshaller_aelf_Hash,
        __Marshaller_aelf_Hash);

    #endregion

    #region Descriptors
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Coordinator.CoordinatorContractReflection.Descriptor.Services[0]; }
    }

    public static global::System.Collections.Generic.IReadOnlyList<global::Google.Protobuf.Reflection.ServiceDescriptor> Descriptors
    {
      get
      {
        return new global::System.Collections.Generic.List<global::Google.Protobuf.Reflection.ServiceDescriptor>()
        {
          global::Coordinator.CoordinatorContractReflection.Descriptor.Services[0],
        };
      }
    }
    #endregion

    // public class CoordinatorInterfaceReferenceState : global::AElf.Sdk.CSharp.State.ContractReferenceState
    // {
    //   internal global::AElf.Sdk.CSharp.State.MethodReference<global::Coordinator.InitializeInput, global::Google.Protobuf.WellKnownTypes.Empty> Initialize { get; set; }
    //   internal global::AElf.Sdk.CSharp.State.MethodReference<global::AElf.Types.Address, global::Google.Protobuf.WellKnownTypes.Empty> TransferAdmin { get; set; }
    //   internal global::AElf.Sdk.CSharp.State.MethodReference<global::Google.Protobuf.WellKnownTypes.Empty, global::Google.Protobuf.WellKnownTypes.Empty> AcceptAdmin { get; set; }
    //   internal global::AElf.Sdk.CSharp.State.MethodReference<global::Google.Protobuf.WellKnownTypes.Empty, global::Google.Protobuf.WellKnownTypes.Empty> Pause { get; set; }
    //   internal global::AElf.Sdk.CSharp.State.MethodReference<global::Google.Protobuf.WellKnownTypes.Empty, global::Google.Protobuf.WellKnownTypes.Empty> Unpause { get; set; }
    //   internal global::AElf.Sdk.CSharp.State.MethodReference<global::AElf.Types.Address, global::Google.Protobuf.WellKnownTypes.Empty> SetOracleContractAddress { get; set; }
    //   internal global::AElf.Sdk.CSharp.State.MethodReference<global::Google.Protobuf.WellKnownTypes.Int32Value, global::Google.Protobuf.WellKnownTypes.Empty> SetRequestTypeIndex { get; set; }
    //   internal global::AElf.Sdk.CSharp.State.MethodReference<global::Coordinator.Request, global::Google.Protobuf.WellKnownTypes.Empty> SendRequest { get; set; }
    //   internal global::AElf.Sdk.CSharp.State.MethodReference<global::Coordinator.ReportInput, global::Google.Protobuf.WellKnownTypes.Empty> Report { get; set; }
    //   internal global::AElf.Sdk.CSharp.State.MethodReference<global::AElf.Types.Hash, global::Google.Protobuf.WellKnownTypes.Empty> DeleteCommitment { get; set; }
    //   internal global::AElf.Sdk.CSharp.State.MethodReference<global::Google.Protobuf.WellKnownTypes.Empty, global::AElf.Types.Address> GetAdmin { get; set; }
    //   internal global::AElf.Sdk.CSharp.State.MethodReference<global::Google.Protobuf.WellKnownTypes.Empty, global::Google.Protobuf.WellKnownTypes.BoolValue> IsPaused { get; set; }
    //   internal global::AElf.Sdk.CSharp.State.MethodReference<global::Google.Protobuf.WellKnownTypes.Empty, global::AElf.Types.Address> GetOracleContractAddress { get; set; }
    //   internal global::AElf.Sdk.CSharp.State.MethodReference<global::Google.Protobuf.WellKnownTypes.Empty, global::Google.Protobuf.WellKnownTypes.Int32Value> GetRequestTypeIndex { get; set; }
    //   internal global::AElf.Sdk.CSharp.State.MethodReference<global::AElf.Types.Hash, global::AElf.Types.Hash> GetCommitmentHash { get; set; }
    // }
  }
}
#endregion

