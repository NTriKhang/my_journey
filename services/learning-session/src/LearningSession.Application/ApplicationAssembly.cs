using System.Reflection;

namespace LearningSession.Application
{
    // Marker type used for assembly scanning (MediatR registration)
    public static class ApplicationAssembly
    {
        public static Assembly Assembly => typeof(ApplicationAssembly).Assembly;
    }
}

