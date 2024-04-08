﻿using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Managers
{
    public class VibrateManager : MonoBehaviour
    {
        /// <summary>
        /// 单例模式
        /// </summary>
        private static VibrateManager instance;

        /// <summary>
        /// 单例模式
        /// </summary>
        public static VibrateManager Instance
        {
            get => instance;
            private set
            {
                if (instance == null)
                {
                    instance = value;
                }
            }
        }

        /// <summary>
        /// 单例模式
        /// On Awake, we initialize our iOS haptics.
        /// Of course, this only needs to be done when on iOS, or targeting iOS. 
        /// A test will be done and this method will do nothing if running on anything else
        /// </summary>
        private void Awake()
        {
            Instance = this;
            MMNViOS.iOSInitializeHaptics();
        }

        /// <summary>
        /// On Disable, we release our iOS haptics (to save memory and avoid garbage).
        /// Of course, this only needs to be done when on iOS, or targeting iOS. 
        /// A test will be done and this method will do nothing if running on anything else
        /// </summary>
        private void OnDisable()
        {
            MMNViOS.iOSReleaseHaptics();
        }

        /// <summary>
        /// The following methods are bound (via the inspector) to buttons in the demo scene, and will call the corresponding vibration methods
        /// </summary>
        /// <summary>
        /// Triggers the default Unity vibration, without any control over duration, pattern or amplitude
        /// </summary>
        public void TriggerDefault()
        {
            #if UNITY_IOS || UNITY_ANDROID
            Handheld.Vibrate();
            #endif
        }

        /// <summary>
        /// Triggers the default Vibrate method, which will result in a medium vibration on Android and a medium impact on iOS
        /// </summary>
        public void TriggerVibrate()
        {
            MMVibrationManager.Vibrate();
        }

        /// <summary>
        /// Triggers the selection haptic feedback, a light vibration on Android, and a light impact on iOS
        /// </summary>
        public void TriggerSelection()
        {
            MMVibrationManager.Haptic(HapticTypes.Selection);
        }

        /// <summary>
        /// Triggers the success haptic feedback, a light then heavy vibration on Android, and a success impact on iOS
        /// </summary>
        public void TriggerSuccess()
        {
            MMVibrationManager.Haptic(HapticTypes.Success);
        }

        /// <summary>
        /// Triggers the warning haptic feedback, a heavy then medium vibration on Android, and a warning impact on iOS
        /// </summary>
        public void TriggerWarning()
        {
            MMVibrationManager.Haptic(HapticTypes.Warning);
        }

        /// <summary>
        /// Triggers the failure haptic feedback, a medium / heavy / heavy / light vibration pattern on Android, and a failure impact on iOS
        /// </summary>
        public void TriggerFailure()
        {
            MMVibrationManager.Haptic(HapticTypes.Failure);
        }

        /// <summary>
        /// Triggers a light impact on iOS and a short and light vibration on Android.
        /// </summary>
        public void TriggerLightImpact()
        {
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }

        /// <summary>
        /// Triggers a medium impact on iOS and a medium and regular vibration on Android.
        /// </summary>
        public void TriggerMediumImpact()
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        }

        /// <summary>
        /// Triggers a heavy impact on iOS and a long and heavy vibration on Android.
        /// </summary>
        public void TriggerHeavyImpact()
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }
    }
}