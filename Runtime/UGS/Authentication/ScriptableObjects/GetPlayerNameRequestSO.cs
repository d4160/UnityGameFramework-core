using UnityEngine;
using d4160.Variables;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using Unity.Services.Core;
using System;
#if ENABLE_NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif

namespace d4160.UGS.Authentication
{
    [CreateAssetMenu(menuName = "d4160/UGS/Authentication/GetPlayerName")]
    public class GetPlayerNameRequestSO : ScriptableObject
    {
#if ENABLE_NAUGHTY_ATTRIBUTES
        [Expandable]
#endif
        [SerializeField] private StringVariableSO _playerName;

        public StringVariableSO PlayerName => _playerName;

        public async Task<string> GetPlayerNameAsync()
        {
            try
            {
                if (AuthenticationService.Instance.IsSignedIn)
                {
                    var response = await AuthenticationService.Instance.GetPlayerNameAsync();
                    _playerName.Value = response.Split('#')[0];
                    return _playerName.Value;
                }
                else
                {
                    Debug.LogError("Not IsSignedIn");
                    return string.Empty;
                }
            }
            catch (ServicesInitializationException)
            {
                return string.Empty;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return string.Empty;
            }
        }
    }
}