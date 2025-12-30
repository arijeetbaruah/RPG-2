// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2018/07/13

#if true // MODULE_MARKER
using System;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Audio; // Required for AudioMixer

#pragma warning disable 1591
namespace DG.Tweening
{
	public static class DOTweenModuleAudio
    {
        #region Shortcuts

        #region Audio

        /// <summary>Tweens an AudioSource's volume to the given value.
        /// Also stores the AudioSource as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Tweens an AudioSource's volume to a specified value.
        /// </summary>
        /// <param name="target">The AudioSource whose volume will be animated.</param>
        /// <param name="endValue">Target volume level in the range 0 to 1; values outside that range are clamped.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>A Tweener that animates the target's volume to the specified value.</returns>
        public static TweenerCore<float, float, FloatOptions> DOFade(this AudioSource target, float endValue, float duration)
        {
            if (endValue < 0) endValue = 0;
            else if (endValue > 1) endValue = 1;
            TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.volume, x => target.volume = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        /// <summary>Tweens an AudioSource's pitch to the given value.
        /// Also stores the AudioSource as the tween's target so it can be used for filtered operations</summary>
        /// <summary>
        /// Tweens an AudioSource's pitch from its current value to the specified end value over the given duration.
        /// </summary>
        /// <param name="target">The AudioSource whose pitch will be tweened.</param>
        /// <param name="endValue">The target pitch value to reach.</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <returns>The TweenerCore controlling the pitch tween.</returns>
        public static TweenerCore<float, float, FloatOptions> DOPitch(this AudioSource target, float endValue, float duration)
        {
            TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.pitch, x => target.pitch = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        #endregion

        #region AudioMixer

        /// <summary>Tweens an AudioMixer's exposed float to the given value.
        /// Also stores the AudioMixer as the tween's target so it can be used for filtered operations.
        /// Note that you need to manually expose a float in an AudioMixerGroup in order to be able to tween it from an AudioMixer.</summary>
        /// <param name="floatName">Name given to the exposed float to set</param>
        /// <summary>
        /// Tweens an exposed float parameter on an AudioMixer to the specified value over time.
        /// </summary>
        /// <param name="floatName">The name of the exposed float parameter in the AudioMixer.</param>
        /// <param name="endValue">The target value to reach for the exposed float.</param>
        /// <param name="duration">The duration of the tween in seconds.</param>
        /// <returns>A TweenerCore that animates the specified exposed float.</returns>
        /// <remarks>The specified float parameter must be exposed in the AudioMixer for this to work.</remarks>
        public static TweenerCore<float, float, FloatOptions> DOSetFloat(this AudioMixer target, string floatName, float endValue, float duration)
        {
            TweenerCore<float, float, FloatOptions> t = DOTween.To(()=> {
                    float currVal;
                    target.GetFloat(floatName, out currVal);
                    return currVal;
                }, x=> target.SetFloat(floatName, x), endValue, duration);
            t.SetTarget(target);
            return t;
        }

        #region Operation Shortcuts

        /// <summary>
        /// Completes all tweens that have this target as a reference
        /// (meaning tweens that were started from this target, or that had this target added as an Id)
        /// and returns the total number of tweens completed
        /// (meaning the tweens that don't have infinite loops and were not already complete)
        /// </summary>
        /// <param name="withCallbacks">For Sequences only: if TRUE also internal Sequence callbacks will be fired,
        /// <summary>
        /// Complete all tweens that have the specified AudioMixer as their target.
        /// </summary>
        /// <param name="target">The AudioMixer whose tweens will be completed.</param>
        /// <param name="withCallbacks">If true, invoke tween callbacks while completing; otherwise callbacks are skipped.</param>
        /// <returns>The number of tweens that were completed.</returns>
        public static int DOComplete(this AudioMixer target, bool withCallbacks = false)
        {
            return DOTween.Complete(target, withCallbacks);
        }

        /// <summary>
        /// Kills all tweens that have this target as a reference
        /// (meaning tweens that were started from this target, or that had this target added as an Id)
        /// and returns the total number of tweens killed.
        /// </summary>
        /// <summary>
        /// Kills all tweens that target the specified AudioMixer.
        /// </summary>
        /// <param name="complete">If true, completes each tween before killing it.</param>
        /// <returns>The number of tweens that were killed.</returns>
        public static int DOKill(this AudioMixer target, bool complete = false)
        {
            return DOTween.Kill(target, complete);
        }

        /// <summary>
        /// Flips the direction (backwards if it was going forward or viceversa) of all tweens that have this target as a reference
        /// (meaning tweens that were started from this target, or that had this target added as an Id)
        /// and returns the total number of tweens flipped.
        /// <summary>
        /// Flip the play direction of all tweens targeting the specified AudioMixer.
        /// </summary>
        /// <param name="target">The AudioMixer whose tweens will be flipped.</param>
        /// <returns>The number of tweens that were flipped.</returns>
        public static int DOFlip(this AudioMixer target)
        {
            return DOTween.Flip(target);
        }

        /// <summary>
        /// Sends to the given position all tweens that have this target as a reference
        /// (meaning tweens that were started from this target, or that had this target added as an Id)
        /// and returns the total number of tweens involved.
        /// </summary>
        /// <param name="to">Time position to reach
        /// (if higher than the whole tween duration the tween will simply reach its end)</param>
        /// <summary>
        /// Moves all tweens that target the given AudioMixer to a specified time position.
        /// </summary>
        /// <param name="to">Time position, in seconds, to move the tweens to.</param>
        /// <param name="andPlay">If `true`, play the tweens after moving them; if `false`, leave them paused at that position.</param>
        /// <returns>The number of tweens that were moved.</returns>
        public static int DOGoto(this AudioMixer target, float to, bool andPlay = false)
        {
            return DOTween.Goto(target, to, andPlay);
        }

        /// <summary>
        /// Pauses all tweens that have this target as a reference
        /// (meaning tweens that were started from this target, or that had this target added as an Id)
        /// and returns the total number of tweens paused.
        /// <summary>
        /// Pauses all tweens that reference the given AudioMixer.
        /// </summary>
        /// <param name="target">The AudioMixer whose associated tweens will be paused.</param>
        /// <returns>The number of tweens that were paused.</returns>
        public static int DOPause(this AudioMixer target)
        {
            return DOTween.Pause(target);
        }

        /// <summary>
        /// Plays all tweens that have this target as a reference
        /// (meaning tweens that were started from this target, or that had this target added as an Id)
        /// and returns the total number of tweens played.
        /// <summary>
        /// Plays all tweens that reference the specified AudioMixer target.
        /// </summary>
        /// <param name="target">The AudioMixer whose associated tweens will be played.</param>
        /// <returns>The number of tweens that were played.</returns>
        public static int DOPlay(this AudioMixer target)
        {
            return DOTween.Play(target);
        }

        /// <summary>
        /// Plays backwards all tweens that have this target as a reference
        /// (meaning tweens that were started from this target, or that had this target added as an Id)
        /// and returns the total number of tweens played.
        /// <summary>
        /// Plays all tweens that reference the specified AudioMixer in reverse.
        /// </summary>
        /// <returns>The number of tweens that were played backwards.</returns>
        public static int DOPlayBackwards(this AudioMixer target)
        {
            return DOTween.PlayBackwards(target);
        }

        /// <summary>
        /// Plays forward all tweens that have this target as a reference
        /// (meaning tweens that were started from this target, or that had this target added as an Id)
        /// and returns the total number of tweens played.
        /// <summary>
        /// Plays all tweens referencing the specified AudioMixer forward.
        /// </summary>
        /// <param name="target">The AudioMixer whose associated tweens will be played forward.</param>
        /// <returns>The number of tweens that were played.</returns>
        public static int DOPlayForward(this AudioMixer target)
        {
            return DOTween.PlayForward(target);
        }

        /// <summary>
        /// Restarts all tweens that have this target as a reference
        /// (meaning tweens that were started from this target, or that had this target added as an Id)
        /// and returns the total number of tweens restarted.
        /// <summary>
        /// Restarts all tweens that target the specified AudioMixer.
        /// </summary>
        /// <returns>The number of tweens that were restarted.</returns>
        public static int DORestart(this AudioMixer target)
        {
            return DOTween.Restart(target);
        }

        /// <summary>
        /// Rewinds all tweens that have this target as a reference
        /// (meaning tweens that were started from this target, or that had this target added as an Id)
        /// and returns the total number of tweens rewinded.
        /// <summary>
        /// Rewinds all tweens that target the specified AudioMixer to their start position.
        /// </summary>
        /// <param name="target">The AudioMixer whose associated tweens will be rewound.</param>
        /// <returns>The number of tweens that were rewound.</returns>
        public static int DORewind(this AudioMixer target)
        {
            return DOTween.Rewind(target);
        }

        /// <summary>
        /// Smoothly rewinds all tweens that have this target as a reference
        /// (meaning tweens that were started from this target, or that had this target added as an Id)
        /// and returns the total number of tweens rewinded.
        /// <summary>
        /// Smoothly rewinds all tweens that target the specified AudioMixer.
        /// </summary>
        /// <param name="target">The AudioMixer whose associated tweens will be smoothly rewound.</param>
        /// <returns>The number of tweens that were rewound.</returns>
        public static int DOSmoothRewind(this AudioMixer target)
        {
            return DOTween.SmoothRewind(target);
        }

        /// <summary>
        /// Toggles the paused state (plays if it was paused, pauses if it was playing) of all tweens that have this target as a reference
        /// (meaning tweens that were started from this target, or that had this target added as an Id)
        /// and returns the total number of tweens involved.
        /// <summary>
        /// Toggles the pause state for all tweens that reference the specified AudioMixer.
        /// </summary>
        /// <param name="target">The AudioMixer whose associated tweens will have their pause state toggled.</param>
        /// <returns>The number of tweens that had their pause state toggled.</returns>
        public static int DOTogglePause(this AudioMixer target)
        {
            return DOTween.TogglePause(target);
        }

        #endregion

        #endregion

        #endregion
    }
}
#endif