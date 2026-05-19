# Aspect-Oriented Programming 
Aspect-Oriented Programming (AOP) in .NET/C# is a way to separate cross-cutting concerns — logic that appears in many places across an application — from your core business logic.

Typical cross-cutting concerns:
- Logging
- Caching
- Validation
- Authorization
- Transaction handling
- Retry policies

Instead of repeating logging code in every method, AOP lets you inject it automatically.

# Advantages of AOP
| Benefit              | Why It Matters               |
| -------------------- | ---------------------------- |
| Cleaner code         | Business logic stays focused |
| Reusability          | One logger used everywhere   |
| Centralized behavior | Easier maintenance           |
| Consistency          | Logging applied uniformly    |
| Less duplication     | DRY principle                |

# Downsides
| Issue            | Explanation                |
| ---------------- | -------------------------- |
| Harder debugging | Hidden behavior            |
| Magic feeling    | Flow not obvious           |
| Runtime overhead | Proxies/interception cost  |
| Complexity       | Can become over-engineered |

# Important Limitation
DispatchProxy only works with:
- interfaces
- virtual methods (depending on proxy style)

It cannot intercept:
- static methods
- non-virtual concrete methods directly