using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XState
{
    internal interface DoneEvent : Event
    {
        object? Data { get; }
    }

    internal class DoneEventObject : EventObject, DoneEvent
    {
        public DoneEventObject(string type, object? data) : base(type) => Data = data;

        public object? Data { get; }

        public override bool Equals(object obj) => obj is DoneEvent other && Type == other.Type && Data == other.Data;

        public override int GetHashCode() => HashCode.Combine(Type, Data);

        public override string ToString() => $"{Type} ({Data})";
    }
}
