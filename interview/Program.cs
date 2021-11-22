using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace interview
{
    class TNode {
        public TNode prev, next;
        public object item = new Object();
    }

    class TLRUCache {
        // fields
        private TNode _head, _tail;
        private int _max = 100;
        private Dictionary<object, TNode> _hash_table = new Dictionary<object, TNode>();

        // constructor
        public void TList(int max = 100) {
            _max = max;
        }

        public void Evict(object key) {
            if (_hash_table.Count >= _max) {
                _hash_table.Remove(_tail.item[0]);
                _tail = _tail.prev;
                _tail.next = null;
            }
        }
        public void Insert(object key, object item) {
            // evict if the list contains maximum item allowed.
            Evict();
            // when the list was null;
            if( _head == null) {
                _head = new TNode();
                _head.prev = _head_next = null;
                _head.item = new object[] {key, item};
                _tail = _head;
            } else {
                TNode temp = new TNode();
                temp.item = new object[] {key, item};
                temp.next = _head;
                _head.prev = temp;
                _head = temp;
            }
            _hash_table.Add(key, _head);
        }

        public object GetItem(object key) {

            if(_hash_table.TryGetValue(key, out var node) != false) {
                if(node != _head) {
                    if(node == _tail) {
                        var temp = _tail.prev;
                        temp.next = null;
                        _tail.next = _head;
                        Debug.Assert(_head.prev == null);
                        _head.prev = node;
                        _tail.prev = null;
                        _head = _tail;
                        _tail = temp;
                    } else {
                        var temp = node.prev;
                        temp.next = node.next;
                        temp = node.next;
                        temp.prev = node.prev;
                        node.next = _head;
                        _head.prev = node;
                        node.prev = null;
                        _head = node;
                    }

                }
                return node;
            }
            return null;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string city = "London";
            Console.WriteLine($"{city} is {city.Length} characters long.");
            Console.WriteLine($"First char is {city[0]} and third is {city[2]}");
       }
    }
}
