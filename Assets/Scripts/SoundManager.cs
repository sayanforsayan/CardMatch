using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sayan.CardGame
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;
        [SerializeField] private AudioClip buttonClick, flip, match, wrong, gameOver;
        [SerializeField] private AudioSource audioSource;
        private void Awake() => Instance = this;

        public void PlaySound(SoundType type)
        {
            switch (type)
            {
                case SoundType.Click:
                    audioSource.clip = buttonClick;
                    break;
                case SoundType.Flip:
                    audioSource.clip = flip;
                    break;
                case SoundType.Match:
                    audioSource.clip = match;
                    break;
                case SoundType.Wrong:
                    audioSource.clip = wrong;
                    break;
                case SoundType.GameOver:
                    audioSource.clip = gameOver;
                    break;
            }
            if (audioSource.clip != null)
                audioSource.Play();
        }
    }
}

public enum SoundType
{
    Click, Match, Wrong, Flip, GameOver
}