using System;
using System.Collections.Generic;
using System.Linq;
using sdk_feed.Common.Extensions;
using UnityEngine;

namespace sdk_feed.Common.Events.DelayedEvents
{
    public enum SearchType
    {
        FirstObjectByName,
        ManyObjectByName,
        FirstObjectsByNameParent,
        ManyObjectByNameParent,
        FirstObjectByThisParent,
        ManyObjectsByThisParent
    }
    public class FeedDelayedEventsAdapter : MonoBehaviour
    {
        [SerializeField] private string _nameToSearch;
        [SerializeField] private SearchType _searchType = SearchType.FirstObjectByName;
        [SerializeField] private float _firstSearchDelay;
        [SerializeField] private bool _isSearchEveryInvoke;
        private List<FeedDelayedEvents> _feedDelayedEvents = new List<FeedDelayedEvents>();

        private void Start() => this.Invoke(Find, _firstSearchDelay);

        private void Find()
        {
            switch (_searchType)
            {
                case SearchType.FirstObjectByName:
                     _feedDelayedEvents.Add(FindObjectsOfType<FeedDelayedEvents>().First(obj => obj.gameObject.name.Equals(_nameToSearch)));
                    break;
                case SearchType.ManyObjectByName:
                    _feedDelayedEvents = FindObjectsOfType<FeedDelayedEvents>().Where(obj => obj.gameObject.name.Equals(_nameToSearch)).ToList();
                    break;
                case SearchType.FirstObjectsByNameParent:
                    _feedDelayedEvents.Add(GameObject.Find(_nameToSearch).GetComponentInChildren<FeedDelayedEvents>());
                    break;
                case SearchType.ManyObjectByNameParent:
                    _feedDelayedEvents = GameObject.Find(_nameToSearch).GetComponentsInChildren<FeedDelayedEvents>().ToList();
                    break;
                case SearchType.FirstObjectByThisParent:
                     _feedDelayedEvents.Add(GetComponentsInChildren<FeedDelayedEvents>().First(obj => obj.gameObject.name.Equals(_nameToSearch)));
                    break;
                case SearchType.ManyObjectsByThisParent:
                    _feedDelayedEvents = GetComponentsInChildren<FeedDelayedEvents>().ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateDelayedEvents() => Find();
        
        public void Invoke()
        {
            if (_isSearchEveryInvoke) Find();
            _feedDelayedEvents.ForEach(e =>
            {
                if (e != null) e.Invoke();
            });
        }
        
        public void Invoke(string customTag)
        {
            if (_isSearchEveryInvoke) Find();
            _feedDelayedEvents.ForEach(e =>
            {
                if (e != null) e.Invoke(customTag);
            });
        }
    }
}