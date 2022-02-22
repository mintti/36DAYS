using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Days.Util.Infra;

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
        private Dictionary<string, Del> _taskDict;

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
            _taskDict  = new Dictionary<string, Del>();
        }
        
        /// <summary>
        /// Scheduler Coroutine 활성화(실행)
        /// </summary>
        public void ActiveSchedule()   => StartCoroutine(_schedule);
        public void InactiveSchedule() => StopCoroutine(_schedule);
    
        /// <summary>
        /// SCHEDULE
        /// - 큐에 태스크가 쌓이면 순서대로 처리합니다.
        /// </summary>
        private IEnumerator Schedule()
        {
            yield return null;
            
            while (true)
            {
                yield return new WaitUntil(() => _queue.Count > 0); 
                _taskDict[_queue[0]]();
                //_osManager.ExecuteCommand(_taskDict[_queue[0]]);
                _queue.RemoveAt(0);
            }
        }
        
        /* ==============================================================
           Task State Managing
        ============================================================== */

        public void AddTask(string tkName, Del del)
        {
            if (!_taskDict.ContainsKey(tkName))
            {
                _taskDict.Add(tkName, del);
            }
        }
        public void CreateTask(string tkName, Del del)
        {
            AddTask(tkName, del);
            _queue.Add(tkName);
        }

        /* ==============================================================
           Alarm Managing
            - 태스크를 주기적으로 실행하는 알람입니다.
            - 설정한 주기마다 스케쥴러 큐에 올라갑니다.
        ============================================================== */
        /// <summary>
        /// 
        /// </summary>
        public void CreateAlarm(string tkName, Del del, string command, ushort interval)
        {
            // Add Task
            AddTask(tkName, del);
            
            // Add Alarm
            if (_alarmDict.ContainsKey(interval))
            {
                _alarmDict[interval].Add(tkName);
            }
            else
            {
                _alarmDict.Add(interval, new List<string>(){tkName});
            }
        }

        public void RemoveAlarm(string tkName)
        {
            _taskDict.Remove(tkName);
            var key = _alarmDict.FirstOrDefault(x => x.Value.Any(v => v == tkName) ).Key;
            _alarmDict[key].Remove(tkName);
        }
        
        
        /// <summary>
        /// The notification about increased 1 second in timer.
        ///  - 매 초 마다 알람 주기 확인, raise at the scheduler queue.
        /// </summary>
        public void TimerNotification()
        {
            _time++;

            foreach (var sec in _alarmDict.Keys.Where(sec => _time % sec == 0))
            {
                _alarmDict[sec].ForEach(tn => _queue.Add((tn)));
            }
        }
    }
}