﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cosmos.Event;
using Cosmos.UI;
using UnityEngine.UI;
namespace Cosmos
{
    public class EventDispatcher : MonoBehaviour, IEventDispatcher
    {
        [SerializeField]
        string eventKey = "defaultEventKey";
        public string EventKey { get { return eventKey; } }
        public string DispatcherName { get { return gameObject.name; } }
        UIEventArgs uch;


        [SerializeField]
        string message;
        private void Start()
        {
            uch = new UIEventArgs();
            slider = GetComponentInChildren<Slider>();
        }
        Slider slider;
        public void DispatchEvent()
        {
            uch.SliderMaxValue = slider.maxValue;
            uch.Message = message;
            uch.SliderValue = slider.value;
            Facade.Instance.DispatchEvent(eventKey, this, uch);
        }



        public void DeregisterEvent()
        {
            Facade.Instance.DeregisterEvent(eventKey);
        }
    }
}