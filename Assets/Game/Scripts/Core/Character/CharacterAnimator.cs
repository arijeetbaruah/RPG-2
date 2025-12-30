using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace RPG.Core.Character
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        private const int MAX_ANIMATION_LENGTH = 10;
        
        [ListDrawerSettings(Expanded = true),
         OnValueChanged(nameof(OnBlendChanged), IncludeChildren = true),
         SerializeField]
        private AnimationBlend[] _idleAnimations;
        
        private PlayableGraph _playableGraph;
        private AnimationMixerPlayable _mixer;

        private Dictionary<AnimationClip, int> _animationIndex = new();
        
        private void Start()
        {
            _playableGraph = PlayableGraph.Create("Animator");
            _playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            
            _mixer = AnimationMixerPlayable.Create(_playableGraph, MAX_ANIMATION_LENGTH);
            Animator animator = gameObject.GetComponent<Animator>();
            
            var output = AnimationPlayableOutput.Create(_playableGraph, "Animation", animator);
            output.SetSourcePlayable(_mixer);
            
            // Play the Graph.
            _playableGraph.Play();

            foreach (var _idleAnimation in _idleAnimations)
            {
                if (_idleAnimation.clip == null)
                {
                    Debug.LogWarning("Null animation clip found in _idleAnimations array, skipping.");
                    continue;
                }
                AddAnimation(_idleAnimation.clip);
            }
        }

        private void Update()
        {
            foreach (var animation in _idleAnimations)
            {
                SetAnimationWeight(animation.clip, animation.weight);
            }
        }

        private void OnDestroy()
        {
            if (_playableGraph.IsValid())
                _playableGraph.Destroy();
        }

        private void OnBlendChanged()
        {
            if (_idleAnimations == null || _idleAnimations.Length == 0)
                return;

            float total = 0f;

            for (int i = 0; i < _idleAnimations.Length; i++)
                total += _idleAnimations[i].weight;

            if (total <= 0f)
                return;

            for (int i = 0; i < _idleAnimations.Length; i++)
            {
                var b = _idleAnimations[i];
                b.weight /= total;
                _idleAnimations[i] = b;
            }
        }

        public void AddAnimation(AnimationClip clip)
        {
            if (clip == null)
            {
                Debug.LogError("Cannot add null animation clip");
                return;
            }
            
            if (_animationIndex.ContainsKey(clip))
            {
                Debug.LogWarning($"Animation Clip {clip.name} has already been added");
                return;
            }
            
            var clipPlayable = AnimationClipPlayable.Create(_playableGraph, clip);
            int index = _animationIndex.Count;
            if (index >= _mixer.GetInputCount())
            {
                Debug.LogError($"Cannot add animation clip {clip.name}: mixer input count limit reached");
                return;
            }
            _playableGraph.Connect(clipPlayable, 0, _mixer, index);
            _animationIndex.Add(clip, index);
        }

        public void SetAnimationWeight(AnimationClip clip, float weight)
        {
            if (clip == null)
            {
                Debug.LogError("Cannot set weight for null animation clip");
                return;
            }
            
            if (!_animationIndex.TryGetValue(clip, out int animationIndex))
            {
                Debug.LogError($"Animation Clip {clip.name} has not been added");
                return;
            }
            
            _mixer.SetInputWeight(animationIndex, weight);
        }

        [System.Serializable]
        public struct AnimationBlend
        {
            public AnimationClip clip;
            [Range(0, 1)] public float weight;
        }
    }
}
