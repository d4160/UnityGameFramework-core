using d4160.Variables;
#if ENABLE_NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using UnityEngine;

namespace d4160.UGS.Lobbies
{
    [CreateAssetMenu(menuName = "d4160/UGS/Lobbies/UpdatePlayer")]
    public class UpdatePlayerSO : ScriptableObject
    {
#if ENABLE_NAUGHTY_ATTRIBUTES
        [Expandable]
#endif
        public LobbySO lobby;
        public LobbyPlayerData[] playerData;

#if ENABLE_NAUGHTY_ATTRIBUTES
        [Button]
#endif
        public async void UpdatePlayer()
        {
            await UpdatePlayerAsync();
        }

        public async Task UpdatePlayerAsync()
        {
            if (lobby.Lobby == null) return;

            UpdatePlayerOptions options = new()
            {
                Data = LobbyPlayerData.GetPlayerData(playerData)
            };

            try
            {
                lobby.Lobby = await LobbyService.Instance.UpdatePlayerAsync(lobby.Lobby.Id, AuthenticationService.Instance.PlayerId, options);

                // TODO: Implement LoggerSO
                Debug.Log($"[UpdatePlayerAsync] Name: {lobby.Lobby.Name}; MaxPlayers: {lobby.Lobby.MaxPlayers}; Id: {lobby.Lobby.Id}: Code: {lobby.Lobby.LobbyCode}");

                lobby.PrintPlayers(lobby.Lobby);
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
    }
}