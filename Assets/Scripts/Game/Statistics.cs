using Gameplay;
using UnityEngine;

namespace LetsLeap.Game
{
    public sealed class Statistics : MonoSingleton<Statistics>
    {
        private const string RecordKey = "Record";
        private const string LeapKey = "Leap";
        private const string MultiplyKey = "Multiply";
        private const string StageKey = "Stage";
        private const string SkinProgressKey = "SkinProgress";

        public int Record
        {
            get => PlayerPrefs.GetInt(RecordKey);
            set => PlayerPrefs.SetInt(RecordKey, value);
        }

        public int Leap
        {
            get => PlayerPrefs.GetInt(LeapKey);
            set => PlayerPrefs.SetInt(LeapKey, value);
        }

        public int Multiply
        {
            get => PlayerPrefs.GetInt(MultiplyKey);
            set => PlayerPrefs.SetInt(MultiplyKey, value);
        }

        public int Stage
        {
            get => PlayerPrefs.GetInt(StageKey);
            set => PlayerPrefs.SetInt(StageKey, value);
        }

        public int SkinProgress
        {
            get => PlayerPrefs.GetInt(SkinProgressKey);
            set => PlayerPrefs.SetInt(SkinProgressKey, value);
        }
        
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }
    }
}