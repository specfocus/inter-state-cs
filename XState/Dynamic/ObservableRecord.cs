namespace XState.Dynamic
{
    using System.Dynamic;

    public class ObservableRecord : ObservableDynamicObject
    {
        private readonly IDictionary<string, object?> _dictionary;

        public bool IsReadOnly { get; private set; }

        public ObservableRecord()
        {
            _dictionary = new Dictionary<string, object?>();
        }

        public override bool TryDeleteMember(DeleteMemberBinder binder)
        {
            if (_dictionary.ContainsKey(binder.Name))
            {
                _dictionary.Remove(binder.Name);

                NotifyPropertyChanged(binder.Name);

                return true;
            }

            var resut = base.TryDeleteMember(binder);

            if (resut)
            {
                NotifyPropertyChanged(binder.Name);
            }

            return resut;
        }

        // Implement the TryGetMember method of the DynamicObject class for dynamic member calls.
        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            //check if the dictionary already has this key
            if (_dictionary.ContainsKey(binder.Name))
            {
                //it did so we can return the value
                result = _dictionary[binder.Name];

                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            if (IsReadOnly)
            {
                throw new InvalidOperationException("This dictionary instance is read-only, you cannot modify the data it contains");
            }

            //check if the dictionary already has this key
            if (_dictionary.ContainsKey(binder.Name))
            {
                //it did so we can assign it to the new value
                _dictionary[binder.Name] = value;

                NotifyPropertyChanged(binder.Name);

                return true;
            }

            //check the base for the property
            var found = base.TrySetMember(binder, value);

            //if it wasn"t found then the user must have wanted a new key
            //we"ll expect implicit casting here, and an exception will be raised
            //if it cannot explicitly cast
            if (!found)
            {
                _dictionary.Add(binder.Name, value);
            }

            NotifyPropertyChanged(binder.Name);

            return true;
        }

        // Implement the TryInvokeMember method of the DynamicObject class for
        // dynamic member calls that have arguments.
        public override bool TryInvokeMember(InvokeMemberBinder binder,
                                             object[] args,
                                             out object result)
        {
            return base.TryInvokeMember(binder, args, out result);
        }
    }
}
