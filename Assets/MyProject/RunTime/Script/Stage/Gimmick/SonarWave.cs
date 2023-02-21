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
        // 条件に一致するパーティクルを ParticleSystem から取得する.
        int numEnter = m_particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, m_enterList);
        int numExit = m_particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, m_exitList);

        // 取得したパーティクルの色を変更する.
        for (int idx = 0; idx < numEnter; idx++)
        {
            ParticleSystem.Particle p = m_enterList[idx];
            p.startColor = Color.red;   // 赤色.
            m_enterList[idx] = p;
        }
        print(numEnter);
        for (int idx = 0; idx < numExit; idx++)
        {
            ParticleSystem.Particle p = m_exitList[idx];
            p.startColor = Color.yellow; // 黄色.
            m_exitList[idx] = p;
        }
        // 変更したパーティクルを ParticleSystem に再設定.
        m_particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, m_enterList);
        m_particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, m_exitList);
    }
}
