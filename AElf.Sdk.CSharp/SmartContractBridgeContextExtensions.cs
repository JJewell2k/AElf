using AElf.Common;
using AElf.Kernel;
using AElf.Kernel.SmartContract.Sdk;
using AElf.Types.CSharp;
using Google.Protobuf;

namespace AElf.Sdk.CSharp
{
    public static class SmartContractBridgeContextExtensions
    {
        public static void FireEvent<TEvent>(this CSharpSmartContractContext context, TEvent e) where TEvent : Event
        {
            var logEvent = EventParser<TEvent>.ToLogEvent(e, context.Self);
            context.FireLogEvent(logEvent);
        }

        public static void Fire<T>(this CSharpSmartContractContext context, T eventData) where T : IEvent<T>
        {
            var le = new LogEvent()
            {
                Address = context.Self,
                Name = eventData.Descriptor.Name
            };

            foreach (var indexed in eventData.GetIndexed())
            {
                var byteString = indexed.ToByteString();
                if (byteString.Length == 0)
                {
                    continue;
                }
                le.Indexed.Add(byteString);
            }

            le.NonIndexed = eventData.GetNonIndexed().ToByteString();
            context.FireLogEvent(le);
        }
        //TODO: Add SmartContractBridgeContextExtensions test case [Case]

        public static T Call<T>(this ISmartContractBridgeContext context, IStateCache stateCache, Address address,
            string methodName, IMessage message)
        {
            return context.Call<T>(stateCache, address, methodName, ConvertToByteString(message));
        }

        public static void SendInline(this ISmartContractBridgeContext context, Address toAddress, string methodName,
            IMessage message)
        {
            context.SendInline(toAddress, methodName, ConvertToByteString(message));
        }

        public static void SendVirtualInline(this ISmartContractBridgeContext context, Hash fromVirtualAddress,
            Address toAddress, string methodName, IMessage message)
        {
            context.SendVirtualInline(fromVirtualAddress, toAddress, methodName,
                ConvertToByteString(message));
        }
        
        public static T Call<T>(this CSharpSmartContractContext context, IStateCache stateCache, Address address,
            string methodName, IMessage message)
        {
            return context.Call<T>(stateCache, address, methodName, ConvertToByteString(message));
        }

        public static void SendInline(this CSharpSmartContractContext context, Address toAddress, string methodName,
            IMessage message)
        {
            context.SendInline(toAddress, methodName, ConvertToByteString(message));
        }

        public static void SendVirtualInline(this CSharpSmartContractContext context, Hash fromVirtualAddress,
            Address toAddress, string methodName, IMessage message)
        {
            context.SendVirtualInline(fromVirtualAddress, toAddress, methodName,
                ConvertToByteString(message));
        }

        public static ByteString ConvertToByteString(IMessage message)
        {
            return message?.ToByteString() ?? ByteString.Empty;
            //return ByteString.CopyFrom(ParamsPacker.Pack(message));
        }
    }
}