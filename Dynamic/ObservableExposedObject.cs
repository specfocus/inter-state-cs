namespace XState.Dynamic
{
    using System.ComponentModel;
    using System.Dynamic;
    using System.Runtime.CompilerServices;

    public class ObservableExposedObject : ExposedObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableExposedObject(object instance) : base(instance) { }

        public ObservableExposedObject(Type type) : base(type) { }

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

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var resut = base.TrySetMember(binder, value);

            if (resut)
            {
                NotifyPropertyChanged(binder.Name);
            }

            return resut;
        }
    }
}
