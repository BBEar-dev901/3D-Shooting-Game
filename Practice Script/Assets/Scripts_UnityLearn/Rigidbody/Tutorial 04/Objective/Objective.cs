using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public enum ObjectiveType
    {
        KillEnemy,
        PickupItems,
    }

    public abstract class Objective : MonoBehaviour
    {
        public uint uid;
        public ObjectiveType objectiveType;
        public string Title;
        public string Description;

        protected DisplayObjectiveMessage m_displayObjective;
        protected GameObject m_displayObjectiveLayout;

        protected DisplayMessage m_displayMessage;

        public abstract bool IsAchieved();
        public abstract bool Completed();
        public abstract void Updated();
    }
}