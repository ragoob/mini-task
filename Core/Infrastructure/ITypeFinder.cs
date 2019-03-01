using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Infrastructure
{
    public interface ITypeFinder
    {
       
        IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

        
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

     
        IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

      
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

       
        IList<Assembly> GetAssemblies();
    }
}
