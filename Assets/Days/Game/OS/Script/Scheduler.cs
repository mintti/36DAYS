using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Days.Game.OS.Script
{
    public class Scheduler : MonoBehaviour
    {
        private OsManager _osManager;
            
        /// <summary>
        /// Schedule Enumerator
        /// </summary>
        private IEnumerator _schedule;

        /// <summary>
        /// Check time
        /// </summary>
        private int _time;
        
        
        /// <summary>
        /// Scheduler Queue
        /// </summary>
        private List<string> _queue;
        
        /// <summary>
        /// Dictionary about alarm
        /// key     : interval
        /// value   : task name
        /// </summary>
        private Dictionary<int, List<string>> _alarmDict;
        
        /// <summary>
        /// Dictionary about task
        /// key     : name
        /// value   : command
        /// </summary>
        private Dictionary<string, string> _taskDict;

        /* ==============================================================
           Scheduler
        ============================================================== */

        public void Init(OsManager osManager)
        {
            _osManager = osManager;
            
            _schedule = Schedule();
            _time = 0;

            _queue     = new List<string>();
            _alarmDict = new Dictionary<int, List<string>>();
            _taskDict  = new Dictionary<string, string>();
        }
        
        /// <summary>
        /// Scheduler Coroutine 활성화(실행)
        /// </summary>
        private void ActiveSchedule()   => StartCoroutine(_schedule);
        private void InactiveSchedule() => StopCoroutine(_schedule);

        /// <summary>
        /// SCHEDULE
        /// - 큐에 태스크가 쌓이면 순서대로 처리합니다.
        /// </summary>
        private IEnumerator Schedule()
        {
            while (true)
            {
                yield return new WaitUntil(() => _queue.Count > 0);
                _osManager.ExecuteCommand(_taskDict[_queue[0]]);
                _queue.RemoveAt(0);
            }
        }
        
        /* ==============================================================
           Task State Managing
        ============================================================== */
        
        /// <summary>
        /// The notification about increased 1 second in timer.
        /// </summary>
        public void TimerNotification()
        {
            _time++;

            foreach (var sec in _alarmDict.Keys.Where(sec => _time % sec == 0))
            {
                _alarmDict[sec].ForEach(tn => _queue.Add((tn)));
            }
        }
        

        /* ==============================================================
           Alarm Managing
        ============================================================== */
        public void CreateAlarm(string name, string command, ushort interval)
        {
            // Add Task
            _taskDict.Add(name, command);
            
            // Add Alarm
            if (_alarmDict.ContainsKey(interval))
            {
                _alarmDict.Add(interval, new List<string>(){name});
            }
            else
            {
                _alarmDict[interval].Add(name);
            }
        }

        public void RemoveAlarm(string name)
        {
            _taskDict.Remove(name);
            var key = _alarmDict.FirstOrDefault(x => x.Value.Any(v => v ==name) ).Key;
            _alarmDict[key].Remove(name);
        }
    }
}