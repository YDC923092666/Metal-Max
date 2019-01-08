using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public class EventCenter
    {
        private static Dictionary<EventName, Delegate> m_EventTable = new Dictionary<EventName, Delegate>();

        private static void OnListenerAdding(EventName eventName, Delegate callBack)
        {
            if (!m_EventTable.ContainsKey(eventName))
            {
                m_EventTable.Add(eventName, null);
            }
            Delegate d = m_EventTable[eventName];
            if (d != null && d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1}，要添加的委托类型为{2}", eventName, d.GetType(), callBack.GetType()));
            }
        }
        private static void OnListenerRemoving(EventName eventName, Delegate callBack)
        {
            if (m_EventTable.ContainsKey(eventName))
            {
                Delegate d = m_EventTable[eventName];
                if (d == null)
                {
                    throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托", eventName));
                }
                else if (d.GetType() != callBack.GetType())
                {
                    throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除不同类型的委托，当前委托类型为{1}，要移除的委托类型为{2}", eventName, d.GetType(), callBack.GetType()));
                }
            }
            else
            {
                throw new Exception(string.Format("移除监听错误：没有事件码{0}", eventName));
            }
        }
        private static void OnListenerRemoved(EventName eventName)
        {
            if (m_EventTable[eventName] == null)
            {
                m_EventTable.Remove(eventName);
            }
        }
        //no parameters
        public static void AddListener(EventName eventName, CallBack callBack)
        {
            OnListenerAdding(eventName, callBack);
            m_EventTable[eventName] = (CallBack)m_EventTable[eventName] + callBack;
        }
        //Single parameters
        public static void AddListener<T>(EventName eventName, CallBack<T> callBack)
        {
            OnListenerAdding(eventName, callBack);
            m_EventTable[eventName] = (CallBack<T>)m_EventTable[eventName] + callBack;
        }
        //two parameters
        public static void AddListener<T, X>(EventName eventName, CallBack<T, X> callBack)
        {
            OnListenerAdding(eventName, callBack);
            m_EventTable[eventName] = (CallBack<T, X>)m_EventTable[eventName] + callBack;
        }
        //three parameters
        public static void AddListener<T, X, Y>(EventName eventName, CallBack<T, X, Y> callBack)
        {
            OnListenerAdding(eventName, callBack);
            m_EventTable[eventName] = (CallBack<T, X, Y>)m_EventTable[eventName] + callBack;
        }
        //four parameters
        public static void AddListener<T, X, Y, Z>(EventName eventName, CallBack<T, X, Y, Z> callBack)
        {
            OnListenerAdding(eventName, callBack);
            m_EventTable[eventName] = (CallBack<T, X, Y, Z>)m_EventTable[eventName] + callBack;
        }
        //five parameters
        public static void AddListener<T, X, Y, Z, W>(EventName eventName, CallBack<T, X, Y, Z, W> callBack)
        {
            OnListenerAdding(eventName, callBack);
            m_EventTable[eventName] = (CallBack<T, X, Y, Z, W>)m_EventTable[eventName] + callBack;
        }

        //no parameters
        public static void RemoveListener(EventName eventName, CallBack callBack)
        {
            OnListenerRemoving(eventName, callBack);
            m_EventTable[eventName] = (CallBack)m_EventTable[eventName] - callBack;
            OnListenerRemoved(eventName);
        }
        //single parameters
        public static void RemoveListener<T>(EventName eventName, CallBack<T> callBack)
        {
            OnListenerRemoving(eventName, callBack);
            m_EventTable[eventName] = (CallBack<T>)m_EventTable[eventName] - callBack;
            OnListenerRemoved(eventName);
        }
        //two parameters
        public static void RemoveListener<T, X>(EventName eventName, CallBack<T, X> callBack)
        {
            OnListenerRemoving(eventName, callBack);
            m_EventTable[eventName] = (CallBack<T, X>)m_EventTable[eventName] - callBack;
            OnListenerRemoved(eventName);
        }
        //three parameters
        public static void RemoveListener<T, X, Y>(EventName eventName, CallBack<T, X, Y> callBack)
        {
            OnListenerRemoving(eventName, callBack);
            m_EventTable[eventName] = (CallBack<T, X, Y>)m_EventTable[eventName] - callBack;
            OnListenerRemoved(eventName);
        }
        //four parameters
        public static void RemoveListener<T, X, Y, Z>(EventName eventName, CallBack<T, X, Y, Z> callBack)
        {
            OnListenerRemoving(eventName, callBack);
            m_EventTable[eventName] = (CallBack<T, X, Y, Z>)m_EventTable[eventName] - callBack;
            OnListenerRemoved(eventName);
        }
        //five parameters
        public static void RemoveListener<T, X, Y, Z, W>(EventName eventName, CallBack<T, X, Y, Z, W> callBack)
        {
            OnListenerRemoving(eventName, callBack);
            m_EventTable[eventName] = (CallBack<T, X, Y, Z, W>)m_EventTable[eventName] - callBack;
            OnListenerRemoved(eventName);
        }


        //no parameters
        public static void Broadcast(EventName eventName)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventName, out d))
            {
                CallBack callBack = d as CallBack;
                if (callBack != null)
                {
                    callBack();
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventName));
                }
            }
        }
        //single parameters
        public static void Broadcast<T>(EventName eventName, T arg)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventName, out d))
            {
                CallBack<T> callBack = d as CallBack<T>;
                if (callBack != null)
                {
                    callBack(arg);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventName));
                }
            }
        }
        //two parameters
        public static void Broadcast<T, X>(EventName eventName, T arg1, X arg2)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventName, out d))
            {
                CallBack<T, X> callBack = d as CallBack<T, X>;
                if (callBack != null)
                {
                    callBack(arg1, arg2);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventName));
                }
            }
        }
        //three parameters
        public static void Broadcast<T, X, Y>(EventName eventName, T arg1, X arg2, Y arg3)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventName, out d))
            {
                CallBack<T, X, Y> callBack = d as CallBack<T, X, Y>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventName));
                }
            }
        }
        //four parameters
        public static void Broadcast<T, X, Y, Z>(EventName eventName, T arg1, X arg2, Y arg3, Z arg4)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventName, out d))
            {
                CallBack<T, X, Y, Z> callBack = d as CallBack<T, X, Y, Z>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventName));
                }
            }
        }
        //five parameters
        public static void Broadcast<T, X, Y, Z, W>(EventName eventName, T arg1, X arg2, Y arg3, Z arg4, W arg5)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventName, out d))
            {
                CallBack<T, X, Y, Z, W> callBack = d as CallBack<T, X, Y, Z, W>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4, arg5);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventName));
                }
            }
        }
    }
}
