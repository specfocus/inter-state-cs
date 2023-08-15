﻿namespace XState.Extensions
{
    using System.Reflection;

    public static class AssemblyExtensions
    {
        public static string GetShortName(this Assembly assembly) => assembly.GetName().Name;
    }
}
