using System;

namespace KoreanZed.QueueActions
{
    internal struct ActionQueueItem
    {
        public float Time;
        public Func<bool> PreConditionFunc;
        public Func<bool> ConditionToRemoveFunc;
        public Action ComboAction;
    }
}