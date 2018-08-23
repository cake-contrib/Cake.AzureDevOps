# Build Script

To use the Cake Tfs addin in your Cake file simply import it. Then define a task.

```csharp
#addin "Cake.Tfs"

Task("run-generator").Does(() =>
{
    RunYeomanGenerator("my-generator");
}
```