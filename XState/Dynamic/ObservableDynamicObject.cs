

namespace XState.Dynamic
{
    using System.ComponentModel;
    using System.Dynamic;
    using System.Runtime.CompilerServices;

    public class ObservableDynamicObject : DynamicObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            //check the base for the property
            var result = base.TrySetMember(binder, value);

            //if it wasn"t found then the user must have wanted a new key
            //we"ll expect implicit casting here, and an exception will be raised
            //if it cannot explicitly cast
            if (result)
            {
                NotifyPropertyChanged(binder.Name);
            }

            return result;
        }
    }
}
