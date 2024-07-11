using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace d4160.Runtime.UGS.NetCode
{
    [Title("Set Connection Data")]
    [Description("Set transport IpAdress and Port to connect.")]

    [Category("UGS/Netcode/Set Connection Data")]

    [Keywords("Connection", "Data", "Multiplayer", "Networking", "Transport", "Multiplay")]

    [Image(typeof(IconLineStartEnd), ColorTheme.Type.Blue)]

    [Parameter("IpAddress", "The ip address of server")]
    [Parameter("Port", "The port of server")]
    [Parameter("ListenAddress", "The address to listen from. 0.0.0.0 means everyone.")]

    [Serializable]
    public class InstructionTransportSetConnectionData : Instruction
    {
        [SerializeField] private PropertyGetString m_IpAddress = new PropertyGetString();
        [SerializeField] private PropertyGetInteger m_Port = new PropertyGetInteger();
        [SerializeField] private PropertyGetString m_ListenAddress = new PropertyGetString();

        public override string Title => $"Set Connection Data {m_IpAddress}:{m_Port} > {m_ListenAddress}";

        protected override Task Run(Args args)
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(m_IpAddress.Get(args), (ushort)m_Port.Get(args), m_ListenAddress.Get(args));

            return DefaultResult;
        }
    }
}