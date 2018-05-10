  Project Notes
===================

### General Approach

I created interfaces for the static classes in BLL and DAL, and then created
wrapper classes that implement those interfaces. These wrapper classes simply
call the static classes, but this gives us the ability to use DI to inject the
Legacy BLL/DAL objects into our new Managers/Repositories. Also, it means the
ServiceLocator (more on that later) can resolve those dependencies.

Next, I created the ServiceLocator class. This class is **ONLY** for use by
external callers, and potentially the Legacy BLL components (if and only if they
need to call into the new managers or repositories). Using this ServiceLocator
from within the new managers - instead of using constructor injection - is an
anti-pattern and will make unit testing much more difficult.

### ServiceLocator details

The ServiceLocator works by building a private static UnityContainer with all
the types within the BLL and DAL project. Its Get<>() method then just calls the
UnityContainer's .Resolve<>() method, letting Unity handle the constructor
injection required to build up the objects.

### Issues with as-is ServiceLocator and IUnitOfWork

One thing to note is that this demo does not deal with injecting IUnitOfWork
objects. A similar approach to the UnityDI demo can be used for handling those.
However, care would need to be taken when calling Service.Locator.Get<>()
multiple times if the same UnitOfWork should be used between resolves. Alternate
approaches could be:

>   a) Add a non-generic Get() method that takes in a list of types to resolve.
      The ServiceLocator could first resolve an IUnitOfWork, and then use Unity's
      ParameterOverride syntax to force that UnitOfWork into its later resolves

>   b) Add a ServiceLocator.GetUnitOfWork() method. Then, change the signature of
      the Get<>() method to be Get<>(IUnitOfWork). Similar to approach 'a', this
      would allow use of Unity's ParameterOverride to force the provided UnitOfWork
      to be resolved.