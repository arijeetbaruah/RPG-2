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
    
    public class CharacterAnimator : MonoBehaviour
    {
        [ListDrawerSettings(Expanded = true),
         OnValueChanged(nameof(OnBlendChanged), IncludeChildren = true),
         SerializeField]
        private AnimationBlend[] _idleAnimations;
        
        private PlayableGraph _playableGraph;
        private AnimationMixerPlayable _mixer;

        private Dictionary<AnimationClip, int> _animationIndex = new();
        
        /// <summary>
        /// Initializes the PlayableGraph, creates an AnimationMixerPlayable and output using the GameObject's Animator, starts the graph, and registers configured idle animations with the mixer.
        /// </summary>
        private void Start()
        {
            _playableGraph = PlayableGraph.Create("Animator");
            _playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            
            _mixer = AnimationMixerPlayable.Create(_playableGraph, 10);
            Animator animator = gameObject.GetComponent<Animator>();
            
            var output = AnimationPlayableOutput.Create(_playableGraph, "Animation", animator);
            output.SetSourcePlayable(_mixer);
            
            // Play the Graph.
            _playableGraph.Play();

            foreach (var _idleAnimation in _idleAnimations)
            {
                AddAnimation(_idleAnimation.clip);
            }
        }

        /// <summary>
        /// Apply the current weights from the _idleAnimations array to the animation mixer so each clip's input weight matches its AnimationBlend.weight.
        /// </summary>
        private void Update()
        {
            foreach (var animation in _idleAnimations)
            {
                SetAnimationWeight(animation.clip, animation.weight);
            }
        }

        /// <summary>
        /// Releases the PlayableGraph created by this component to clean up its native animation resources.
        /// </summary>
        private void OnDestroy()
        {
            _playableGraph.Destroy();
        }

        /// <summary>
        /// Normalizes the weights of the idle animation blends so their sum equals 1.
        /// </summary>
        /// <remarks>
        /// If the idle blend array is null, empty, or the total weight is less than or equal to zero, no changes are made. Each element's weight in the serialized array is updated in-place.
        /// </remarks>
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

        /// <summary>
        /// Adds the given animation clip to the animator's mixer and records its input index for future weight updates.
        /// </summary>
        /// <param name="clip">The animation clip to register and connect into the mixer's next available input.</param>
        /// <remarks>
        /// If the clip has already been added, the method logs a warning and does nothing.
        /// </remarks>
        public void AddAnimation(AnimationClip clip)
        {
            if (_animationIndex.ContainsKey(clip))
            {
                Debug.LogWarning($"Animation Clip {clip.name} has already been added");
                return;
            }
            
            var clipPlayable = AnimationClipPlayable.Create(_playableGraph, clip);
            int index = _animationIndex.Count;
            _playableGraph.Connect(clipPlayable, 0, _mixer, index);
            _animationIndex.Add(clip, index);
        }

        /// <summary>
        /// Sets the mixer input weight for a previously added animation clip.
        /// </summary>
        /// <param name="clip">The animation clip whose mixer input weight to set.</param>
        /// <param name="weight">The blend weight to assign to the clip's mixer input (typically between 0 and 1; values outside this range are applied as-is).</param>
        /// <remarks>If the clip has not been added to the animator, the method returns without changing any weights.</remarks>
        public void SetAnimationWeight(AnimationClip clip, float weight)
        {
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