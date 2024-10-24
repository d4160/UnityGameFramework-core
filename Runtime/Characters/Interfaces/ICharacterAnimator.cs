
namespace d4160.Characters
{
    public interface ICharacterAnimator
    {
        bool PrevIsSpeaking { get; set; }

        void PlayState(int index);
        void PlayTransition(int index, bool randomizeBlend = false, int maxBlendIndex = 1, float delay = 10f);

        bool IsInState(int index);
    }
}