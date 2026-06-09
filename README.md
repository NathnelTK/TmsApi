# 📘 TmsApi - Lab Session 2: Services Done Right

This pull request covers the implementation of **M4 Lab Session 2 – Services Done Right**.

## 🚀 Implemented Exercises

### 1. Exercise 2: The Memory Leak (Captive Dependencies)
- **Problem:** Captive dependencies occur when a Singleton service (e.g. `EnrollmentWorker`) holds a Scoped service (e.g. `IEnrollmentService`), leading to memory leaks and stale/cross-request data sharing.
- **Solution:**
  - Registered services with scopes and enabled container validation (`ValidateScopes = true` and `ValidateOnBuild = true`).
  - Injected `IServiceScopeFactory` into the singleton `EnrollmentWorker` instead of the scoped service directly.
  - Dynamically created a short-lived scope inside `ProcessBatch()` using the factory to safely resolve and dispose of the scoped `IEnrollmentService`.

### 2. Exercise 3: The Silent Crash (Options Pattern)
- **Problem:** Missing configuration causes runtime crashes when a specific feature is triggered by a user.
- **Solution:**
  - Created a strongly-typed `PaymentOptions` class with validation attributes (`[Required]`, `[Range(100, 100000)]`).
  - Registered and bound the Options pattern section `Payments` in `Program.cs` using `ValidateDataAnnotations()` and `ValidateOnStart()`.
  - Ensures the application crashes immediately upon startup with a clear validation exception if the configuration is missing or invalid, preventing silent runtime failures.

### 3. Exercise 4: The Unsearchable Logs (Structured Logging)
- **Problem:** Concatenated string logs make search and filtering difficult in log aggregators.
- **Solution:**
  - Audited and updated `EnrollmentService.cs` to use structured logging with named templates/placeholders (e.g., `{StudentId}`, `{CourseCode}`, `{EnrollmentId}`).
  - Applied appropriate log levels:
    - `LogInformation` for successful business actions (success enrollments, deletes).
    - `LogWarning` for expected but recoverable conditions (duplicate enrollment attempts, record not found).
