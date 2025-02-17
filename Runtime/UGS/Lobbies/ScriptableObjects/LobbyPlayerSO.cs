using System.Collections;
using System.Collections.Generic;
using d4160.Collections;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace d4160.UGS.Lobbies
{
    [CreateAssetMenu(menuName = "d4160/UGS/Lobbies/LobbyPlayer")]
    public class LobbyPlayerSO : ScriptableObject
    {
        public LobbyPlayerData[] playerData;

        public Player GetPlayer() => new() { Data = LobbyPlayerData.GetPlayerData(playerData) };

        public Player GetPlayer(string id) => new(id) { Data = LobbyPlayerData.GetPlayerData(playerData) };

        public string GetPlayerData(int index)
        {
            if (playerData.IsValidIndex(index))
            {
                return playerData[index].value;
            }

            return string.Empty;
        }
    }
}