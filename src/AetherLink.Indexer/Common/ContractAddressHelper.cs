using AetherLink.Indexer.Constants;

namespace AetherLink.Indexer.Common;

public static class ContractAddressHelper
{
    public static string GetContractAddress(string chainId) => chainId switch
    {
        ChainIdConstants.MainChainId => ContractAddressConstants.MainChainOracleContractAddress,
        ChainIdConstants.SideChainId => ContractAddressConstants.SideChainOracleContractAddress,
        _ => string.Empty
    };
    
    public static string GetRampContractAddress(string chainId) => chainId switch
    {
        ChainIdConstants.MainChainId => ContractAddressConstants.MainChainRampContractAddress,
        ChainIdConstants.SideChainId => ContractAddressConstants.SideChainRampContractAddress,
        _ => string.Empty
    };
}