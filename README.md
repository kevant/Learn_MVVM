# Why Use DTOs?
DTOs are transport objects between systems.
Your architecture becomes:
```
WPF Model <-> DTO <-> gRPC Service
```
Example:

|Layer|Object|
|--|--|
|WPF UI|ItemModel|
|gRPC Transport|ItemDto|
|Backend Domain|ItemEntity|

This separation is recommended in Prism/MVVM enterprise applications.

# Why Not Bind Directly to ItemDto?
You can, but Prism/MVVM usually avoids binding transport objects directly to UI.
Reasons:
- keeps UI independent of transport
- easier unit testing
- easier future migration
- cleaner domain separation
- prevents protobuf-generated types leaking into UI