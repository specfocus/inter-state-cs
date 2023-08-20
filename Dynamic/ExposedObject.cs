﻿// https://github.dev/ivaylokenov/MyTested.AspNetCore.Mvc/blob/a57f3cec92e9c742c182eac4d1cfb24ebad720bc/src/MyTested.AspNetCore.Mvc.Abstractions/Utilities/ExposedObject.cs#L36#L57

namespace XState.Dynamic
{
    using Extensions;
    using System;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;

    public class ExposedObject : DynamicObject
    {
        private static readonly BindingFlags Flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        private readonly object? instance;
        private readonly Type type;

        public ExposedObject(object instance)
        {
            this.instance = instance;
            type = instance?.GetType()!;
        }

        public ExposedObject(Type type) => this.type = type;

        public object Object => instance ?? type;

        private static object Unwrap(dynamic obj)
        {
            if (obj is ExposedObject exposedObject)
            {
                return exposedObject.Object;
            }

            return obj;
        }

        public override bool TryConvert(ConvertBinder binder, out object? result)
        {
            result = binder.Type.GetTypeInfo().IsInstanceOfType(instance)
                ? instance
                : Convert.ChangeType(instance, binder.Type);

            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            var property = type.GetProperty(binder.Name, Flags);

            if (property != null)
            {
                result = property.GetValue(instance);

                return true;
            }

            var field = type.GetField(binder.Name, Flags);

            if (field != null)
            {
                result = field.GetValue(instance);

                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] arguments, out object? result)
        {
            arguments = arguments
                .Select(a => Unwrap(a))
                .ToArray();

            var argumentsTypes = arguments
                .Select(a => a?.GetType() ?? typeof(object))
                .ToArray();

            var method = type.GetMethod(binder.Name, argumentsTypes);

            if (method == null)
            {
                result = null;
                return false;
            }

            try
            {
                result = method
                    .Invoke(instance, arguments)
                    .Exposed();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }

                throw;
            }

            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            var property = type.GetProperty(binder.Name, Flags);

            if (property != null)
            {
                property.SetValue(instance, value);

                return true;
            }

            var field = type.GetField(binder.Name, Flags);

            if (field != null)
            {
                field.SetValue(instance, value);

                return true;
            }

            return base.TrySetMember(binder, value);
        }
    }
}
