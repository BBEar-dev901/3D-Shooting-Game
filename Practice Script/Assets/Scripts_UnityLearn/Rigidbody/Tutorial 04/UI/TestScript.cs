using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class TestScript : MonoBehaviour
    {
        DisplayObjectiveMessage[] m_displayObjectiveMessage = new DisplayObjectiveMessage[3];
        GameObject[] m_displayObjectiveLayout = new GameObject[3];
        int m_currentCountDisplayObjectiveLayout = 0;

        DisplayMessage m_displayMessage;

        string[] MessagesForTest =
        {
            "Test 1",
            "Test 2",
            "Test 3",
        };
        int iKieyDown = 0;

        private void Awake()
        {
            m_displayObjectiveMessage[0]._TitleText = "Mission 1";
            m_displayObjectiveMessage[0]._DescriptionText = "Eat Candy";
            m_displayObjectiveMessage[0]._CounterText = "(0 / 10)";

            m_displayObjectiveMessage[1]._TitleText = "Mission 2";
            m_displayObjectiveMessage[1]._DescriptionText = "Eat Chocolate";
            m_displayObjectiveMessage[1]._CounterText = "(0 / 20)";

            m_displayObjectiveMessage[2]._TitleText = "Mission 3";
            m_displayObjectiveMessage[2]._DescriptionText = "Eat Heal";
            m_displayObjectiveMessage[2]._CounterText = "(0 / 30)";
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                m_displayMessage.message = MessagesForTest[(iKieyDown++) % 3];
                DisplayMessageManager.Instance.RegisterMessage(m_displayMessage);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                if(m_currentCountDisplayObjectiveLayout == 3)
                {
                    return;
                }
                m_displayObjectiveLayout[m_currentCountDisplayObjectiveLayout] = DisplayObjectiveManager.Instance.RegisterObjective(
                                                                                    m_displayObjectiveMessage[m_currentCountDisplayObjectiveLayout]
                                                                                    );
                ++m_currentCountDisplayObjectiveLayout;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                DisplayObjectiveManager.Instance.UnregisterAllObjectives();

                m_currentCountDisplayObjectiveLayout = 0;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                if(m_currentCountDisplayObjectiveLayout == 0)
                {
                    return;
                }
                DisplayObjectiveManager.Instance.UpdateObjective(
                    m_displayObjectiveLayout[m_currentCountDisplayObjectiveLayout - 1],
                    "(10 / 10)", true);
                --m_currentCountDisplayObjectiveLayout;
            }

        }
    }
}