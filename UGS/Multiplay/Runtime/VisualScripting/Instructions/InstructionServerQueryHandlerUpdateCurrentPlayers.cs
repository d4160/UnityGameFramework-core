using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using MiTschMR.Runtime.EasyUGS.Multiplay;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace d4160.Runtime.UGS.NetCode
{
    [Title("Update Server Current Players")]
    [Description("Update the current players count of Server Query Handler.")]

    [Category("UGS/Multiplay/Update Server Current Players")]

    [Keywords("Update", "Current", "Server", "Query", "Players", "Multiplay", "Handler")]

    [Image(typeof(IconPersonCircleSolid), ColorTheme.Type.Blue)]


    [Serializable]
    public class InstructionServerQueryHandlerUpdateCurrentPlayers : Instruction
    {
        public override string Title => $"Update Server Current Players";

        protected override Task Run(Args args)
        {
            ushort playersCount = (ushort)NetworkManager.Singleton.ConnectedClientsIds.Count;
            MultiplayManager.Instance.serverQueryHandler.CurrentPlayers = playersCount;

            return DefaultResult;
        }
    }
}