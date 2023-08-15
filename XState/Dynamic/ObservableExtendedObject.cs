
namespace XState.Dynamic
{
    using System.Dynamic;

    public class ObservableExtendedObject : ObservableExposedObject
    {
        private readonly IDictionary<string, object> _dictionary;

        public ObservableExtendedObject(object instance)
            : base(instance)
        {
            _dictionary = new Dictionary<string, object>();
        }

        public ObservableExtendedObject(Type type)
            : base(type)
        {
            _dictionary = new Dictionary<string, object>();
        }

        public override bool TryDeleteMember(DeleteMemberBinder binder)
        {
            var resut = base.TryDeleteMember(binder);

            if (resut)
            {
                NotifyPropertyChanged(binder.Name);
            }

            return resut;
        }

        // Implement the TryGetMember method of the DynamicObject class for dynamic member calls.
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return base.TryGetMember(binder, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
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
    }
}
