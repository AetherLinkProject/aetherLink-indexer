// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: ramp_contract.proto
// </auto-generated>
// Original file comments:
// the version of the language, use proto3 for contracts
#pragma warning disable 0414, 1591
#region Designer generated code

using System.Collections.Generic;
using aelf = global::AElf.CSharp.Core;

namespace AetherLink.Contracts.Ramp {

  #region Events
  public partial class ConfigSet : aelf::IEvent<ConfigSet>
  {
    public global::System.Collections.Generic.IEnumerable<ConfigSet> GetIndexed()
    {
      return new List<ConfigSet>
      {
      };
    }

    public ConfigSet GetNonIndexed()
    {
      return new ConfigSet
      {
        Config = Config,
      };
    }
  }

  public partial class RampSenderAdded : aelf::IEvent<RampSenderAdded>
  {
    public global::System.Collections.Generic.IEnumerable<RampSenderAdded> GetIndexed()
    {
      return new List<RampSenderAdded>
      {
      };
    }

    public RampSenderAdded GetNonIndexed()
    {
      return new RampSenderAdded
      {
        SenderAddress = SenderAddress,
      };
    }
  }

  public partial class RampSenderRemoved : aelf::IEvent<RampSenderRemoved>
  {
    public global::System.Collections.Generic.IEnumerable<RampSenderRemoved> GetIndexed()
    {
      return new List<RampSenderRemoved>
      {
      };
    }

    public RampSenderRemoved GetNonIndexed()
    {
      return new RampSenderRemoved
      {
        SenderAddress = SenderAddress,
      };
    }
  }

  public partial class CommitReportAccepted : aelf::IEvent<CommitReportAccepted>
  {
    public global::System.Collections.Generic.IEnumerable<CommitReportAccepted> GetIndexed()
    {
      return new List<CommitReportAccepted>
      {
      };
    }

    public CommitReportAccepted GetNonIndexed()
    {
      return new CommitReportAccepted
      {
        MessageId = MessageId,
        SourceChainId = SourceChainId,
        TargetChainId = TargetChainId,
        Sender = Sender,
        Receiver = Receiver,
        Report = Report,
      };
    }
  }

  public partial class SendRequested : aelf::IEvent<SendRequested>
  {
    public global::System.Collections.Generic.IEnumerable<SendRequested> GetIndexed()
    {
      return new List<SendRequested>
      {
      };
    }

    public SendRequested GetNonIndexed()
    {
      return new SendRequested
      {
        MessageId = MessageId,
        TargetChainId = TargetChainId,
        Receiver = Receiver,
        Sender = Sender,
        Data = Data,
      };
    }
  }

  #endregion
  public static partial class RampContractContainer
  {
    static readonly string __ServiceName = "ramp.RampContract";

    #region Marshallers
    static readonly aelf::Marshaller<global::AetherLink.Contracts.Ramp.InitializeInput> __Marshaller_ramp_InitializeInput = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::AetherLink.Contracts.Ramp.InitializeInput.Parser.ParseFrom);
    static readonly aelf::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_google_protobuf_Empty = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Protobuf.WellKnownTypes.Empty.Parser.ParseFrom);
    static readonly aelf::Marshaller<global::AetherLink.Contracts.Ramp.Config> __Marshaller_ramp_Config = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::AetherLink.Contracts.Ramp.Config.Parser.ParseFrom);
    static readonly aelf::Marshaller<global::AElf.Types.Address> __Marshaller_aelf_Address = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::AElf.Types.Address.Parser.ParseFrom);
    static readonly aelf::Marshaller<global::AetherLink.Contracts.Ramp.AddRampSenderInput> __Marshaller_ramp_AddRampSenderInput = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::AetherLink.Contracts.Ramp.AddRampSenderInput.Parser.ParseFrom);
    static readonly aelf::Marshaller<global::AetherLink.Contracts.Ramp.RampSenderInfo> __Marshaller_ramp_RampSenderInfo = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::AetherLink.Contracts.Ramp.RampSenderInfo.Parser.ParseFrom);
    static readonly aelf::Marshaller<global::AetherLink.Contracts.Ramp.SendInput> __Marshaller_ramp_SendInput = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::AetherLink.Contracts.Ramp.SendInput.Parser.ParseFrom);
    static readonly aelf::Marshaller<global::AetherLink.Contracts.Ramp.CommitInput> __Marshaller_ramp_CommitInput = aelf::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::AetherLink.Contracts.Ramp.CommitInput.Parser.ParseFrom);
    #endregion

    #region Methods
    static readonly aelf::Method<global::AetherLink.Contracts.Ramp.InitializeInput, global::Google.Protobuf.WellKnownTypes.Empty> __Method_Initialize = new aelf::Method<global::AetherLink.Contracts.Ramp.InitializeInput, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "Initialize",
        __Marshaller_ramp_InitializeInput,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::AetherLink.Contracts.Ramp.Config, global::Google.Protobuf.WellKnownTypes.Empty> __Method_SetConfig = new aelf::Method<global::AetherLink.Contracts.Ramp.Config, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "SetConfig",
        __Marshaller_ramp_Config,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::AetherLink.Contracts.Ramp.Config> __Method_GetConfig = new aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::AetherLink.Contracts.Ramp.Config>(
        aelf::MethodType.View,
        __ServiceName,
        "GetConfig",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_ramp_Config);

    static readonly aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::AElf.Types.Address> __Method_GetAdmin = new aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::AElf.Types.Address>(
        aelf::MethodType.View,
        __ServiceName,
        "GetAdmin",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_aelf_Address);

    static readonly aelf::Method<global::AElf.Types.Address, global::Google.Protobuf.WellKnownTypes.Empty> __Method_SetOracleContractAddress = new aelf::Method<global::AElf.Types.Address, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "SetOracleContractAddress",
        __Marshaller_aelf_Address,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::AElf.Types.Address> __Method_GetOracleContractAddress = new aelf::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::AElf.Types.Address>(
        aelf::MethodType.View,
        __ServiceName,
        "GetOracleContractAddress",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_aelf_Address);

    static readonly aelf::Method<global::AetherLink.Contracts.Ramp.AddRampSenderInput, global::Google.Protobuf.WellKnownTypes.Empty> __Method_AddRampSender = new aelf::Method<global::AetherLink.Contracts.Ramp.AddRampSenderInput, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "AddRampSender",
        __Marshaller_ramp_AddRampSenderInput,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::AElf.Types.Address, global::Google.Protobuf.WellKnownTypes.Empty> __Method_RemoveRampSender = new aelf::Method<global::AElf.Types.Address, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "RemoveRampSender",
        __Marshaller_aelf_Address,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::AElf.Types.Address, global::AetherLink.Contracts.Ramp.RampSenderInfo> __Method_GetRampSender = new aelf::Method<global::AElf.Types.Address, global::AetherLink.Contracts.Ramp.RampSenderInfo>(
        aelf::MethodType.View,
        __ServiceName,
        "GetRampSender",
        __Marshaller_aelf_Address,
        __Marshaller_ramp_RampSenderInfo);

    static readonly aelf::Method<global::AetherLink.Contracts.Ramp.SendInput, global::Google.Protobuf.WellKnownTypes.Empty> __Method_Send = new aelf::Method<global::AetherLink.Contracts.Ramp.SendInput, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "Send",
        __Marshaller_ramp_SendInput,
        __Marshaller_google_protobuf_Empty);

    static readonly aelf::Method<global::AetherLink.Contracts.Ramp.CommitInput, global::Google.Protobuf.WellKnownTypes.Empty> __Method_Commit = new aelf::Method<global::AetherLink.Contracts.Ramp.CommitInput, global::Google.Protobuf.WellKnownTypes.Empty>(
        aelf::MethodType.Action,
        __ServiceName,
        "Commit",
        __Marshaller_ramp_CommitInput,
        __Marshaller_google_protobuf_Empty);

    #endregion

    #region Descriptors
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::AetherLink.Contracts.Ramp.RampContractReflection.Descriptor.Services[0]; }
    }

    public static global::System.Collections.Generic.IReadOnlyList<global::Google.Protobuf.Reflection.ServiceDescriptor> Descriptors
    {
      get
      {
        return new global::System.Collections.Generic.List<global::Google.Protobuf.Reflection.ServiceDescriptor>()
        {
          global::AElf.Standards.ACS12.Acs12Reflection.Descriptor.Services[0],
          global::AetherLink.Contracts.Ramp.RampContractReflection.Descriptor.Services[0],
        };
      }
    }
    #endregion

    /// <summary>Base class for the contract of RampContract</summary>
    // public abstract partial class RampContractBase : AElf.Sdk.CSharp.CSharpSmartContract<AetherLink.Contracts.Ramp.RampContractState>
    // {
    //   public virtual global::Google.Protobuf.WellKnownTypes.Empty Initialize(global::AetherLink.Contracts.Ramp.InitializeInput input)
    //   {
    //     throw new global::System.NotImplementedException();
    //   }
    //
    //   public virtual global::Google.Protobuf.WellKnownTypes.Empty SetConfig(global::AetherLink.Contracts.Ramp.Config input)
    //   {
    //     throw new global::System.NotImplementedException();
    //   }
    //
    //   public virtual global::AetherLink.Contracts.Ramp.Config GetConfig(global::Google.Protobuf.WellKnownTypes.Empty input)
    //   {
    //     throw new global::System.NotImplementedException();
    //   }
    //
    //   public virtual global::AElf.Types.Address GetAdmin(global::Google.Protobuf.WellKnownTypes.Empty input)
    //   {
    //     throw new global::System.NotImplementedException();
    //   }
    //
    //   public virtual global::Google.Protobuf.WellKnownTypes.Empty SetOracleContractAddress(global::AElf.Types.Address input)
    //   {
    //     throw new global::System.NotImplementedException();
    //   }
    //
    //   public virtual global::AElf.Types.Address GetOracleContractAddress(global::Google.Protobuf.WellKnownTypes.Empty input)
    //   {
    //     throw new global::System.NotImplementedException();
    //   }
    //
    //   public virtual global::Google.Protobuf.WellKnownTypes.Empty AddRampSender(global::AetherLink.Contracts.Ramp.AddRampSenderInput input)
    //   {
    //     throw new global::System.NotImplementedException();
    //   }
    //
    //   public virtual global::Google.Protobuf.WellKnownTypes.Empty RemoveRampSender(global::AElf.Types.Address input)
    //   {
    //     throw new global::System.NotImplementedException();
    //   }
    //
    //   public virtual global::AetherLink.Contracts.Ramp.RampSenderInfo GetRampSender(global::AElf.Types.Address input)
    //   {
    //     throw new global::System.NotImplementedException();
    //   }
    //
    //   public virtual global::Google.Protobuf.WellKnownTypes.Empty Send(global::AetherLink.Contracts.Ramp.SendInput input)
    //   {
    //     throw new global::System.NotImplementedException();
    //   }
    //
    //   public virtual global::Google.Protobuf.WellKnownTypes.Empty Commit(global::AetherLink.Contracts.Ramp.CommitInput input)
    //   {
    //     throw new global::System.NotImplementedException();
    //   }
    //
    // }
    //
    // public static aelf::ServerServiceDefinition BindService(RampContractBase serviceImpl)
    // {
    //   return aelf::ServerServiceDefinition.CreateBuilder()
    //       .AddDescriptors(Descriptors)
    //       .AddMethod(__Method_Initialize, serviceImpl.Initialize)
    //       .AddMethod(__Method_SetConfig, serviceImpl.SetConfig)
    //       .AddMethod(__Method_GetConfig, serviceImpl.GetConfig)
    //       .AddMethod(__Method_GetAdmin, serviceImpl.GetAdmin)
    //       .AddMethod(__Method_SetOracleContractAddress, serviceImpl.SetOracleContractAddress)
    //       .AddMethod(__Method_GetOracleContractAddress, serviceImpl.GetOracleContractAddress)
    //       .AddMethod(__Method_AddRampSender, serviceImpl.AddRampSender)
    //       .AddMethod(__Method_RemoveRampSender, serviceImpl.RemoveRampSender)
    //       .AddMethod(__Method_GetRampSender, serviceImpl.GetRampSender)
    //       .AddMethod(__Method_Send, serviceImpl.Send)
    //       .AddMethod(__Method_Commit, serviceImpl.Commit).Build();
    // }

  }
}
#endregion

