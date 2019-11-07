using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;

namespace AElf.Kernel.Consensus.Application
{
    public interface ITriggerInformationProvider
    {
        BytesValue GetTriggerInformationForConsensusCommand(BytesValue consensusCommandBytes);
        BytesValue GetTriggerInformationForConsensusTransactions(BytesValue consensusCommandBytes);
        BytesValue GetTriggerInformationForConsensusTransactions(BytesValue consensusCommandBytes);
    }
}