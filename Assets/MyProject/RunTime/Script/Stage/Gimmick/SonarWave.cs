using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarWave : MonoBehaviour
{
    ParticleSystem m_particleSystem;
    List<ParticleSystem.Particle> m_enterList = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> m_exitList = new List<ParticleSystem.Particle>();
    void Start()
    {
        m_particleSystem = this.GetComponent<ParticleSystem>();
    }
    /// <summary>
    /// .
    /// </summary>
    void OnParticleTrigger()
    {
        // �����Ɉ�v����p�[�e�B�N���� ParticleSystem ����擾����.
        int numEnter = m_particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, m_enterList);
        int numExit = m_particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, m_exitList);

        // �擾�����p�[�e�B�N���̐F��ύX����.
        for (int idx = 0; idx < numEnter; idx++)
        {
            ParticleSystem.Particle p = m_enterList[idx];
            p.startColor = Color.red;   // �ԐF.
            m_enterList[idx] = p;
        }
        print(numEnter);
        for (int idx = 0; idx < numExit; idx++)
        {
            ParticleSystem.Particle p = m_exitList[idx];
            p.startColor = Color.yellow; // ���F.
            m_exitList[idx] = p;
        }
        // �ύX�����p�[�e�B�N���� ParticleSystem �ɍĐݒ�.
        m_particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, m_enterList);
        m_particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, m_exitList);
    }
}
