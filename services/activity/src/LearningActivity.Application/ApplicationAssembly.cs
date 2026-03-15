using System.Reflection;

namespace LearningActivity.Application
{
    // Marker type used for assembly scanning (AutoMapper / MediatR registration)
    public static class ApplicationAssembly
    {
        public static Assembly Assembly => typeof(ApplicationAssembly).Assembly;
    }
}

