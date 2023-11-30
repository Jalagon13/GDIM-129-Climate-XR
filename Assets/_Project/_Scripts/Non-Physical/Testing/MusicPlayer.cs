using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MagnetFishing
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip _backgroundMusic;

        private void Start()
        {
            MMSoundManagerSoundPlayEvent.Trigger(_backgroundMusic, MMSoundManager.MMSoundManagerTracks.Music, transform.position, loop: true);
        }
    }
}
