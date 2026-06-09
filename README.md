# 📘 TmsApi - Lab Session 3: API Surface & Production Readiness

This pull request covers the implementation of **M4 Lab Session 3 – API Surface & Production Readiness**.

## 🚀 Implemented Exercises

### 1. Exercise 5: The Enrollment API (Controllers with Real CRUD)
- **Goal:** Expose fully REST-compliant enrollment CRUD operations.
- **Solution:**
  - Created `EnrollmentsController.cs` under a new `Controllers/` folder.
  - Implemented the following HTTP verb endpoints mapped to `api/enrollments`:
    - `GET /api/enrollments`: Returns all enrollment records with `200 OK`.
    - `GET /api/enrollments/{id}`: Returns the single record with `200 OK` or `404 Not Found`.
    - `POST /api/enrollments`: Accepts `CreateEnrollmentRequest` payload, enrolls the student, and returns `201 Created` along with a `Location` header pointing to the created resource using `CreatedAtAction`.
    - `DELETE /api/enrollments/{id}`: Deletes the record returning `204 No Content` or `404 Not Found` if not existing.

### 2. Exercise 6: The Consistent Fault (Standardized Error Handling / ProblemDetails)
- **Goal:** Standardize application exceptions and response shapes.
- **Solution:**
  - Added the RFC 9457 `ProblemDetails` service in `Program.cs` via `builder.Services.AddProblemDetails()`.
  - Registered `app.UseExceptionHandler()` and `app.UseStatusCodePages()` early in the middleware pipeline to intercept exceptions and empty status-code responses, automatically transforming them into consistent ProblemDetails JSON.
  - Created a custom `TmsDatabaseException` and registered a testing route `/api/error` to verify RFC-compliant error responses are output as JSON (with fields like `type`, `title`, `status`, and `detail`) instead of raw HTML stack traces.

### 3. Exercise 7: The Environment Toggle (Dev vs Prod)
- **Goal:** Toggle API exploration surfaces depending on the environment posture.
- **Solution:**
  - Installed `Microsoft.AspNetCore.OpenApi` and `Scalar.AspNetCore`.
  - Configured conditional pipeline setup in `Program.cs`:
    - **In Development:** Registers OpenAPI document generation (`app.MapOpenApi()`) and exposes the interactive `Scalar` API Reference Explorer (`app.MapScalarApiReference()`) at `/scalar/v1`.
    - **In Production:** Excluded the OpenAPI metadata and Scalar reference mapping (returns `404 Not Found`) and keeps raw exception stack traces completely hidden from the public boundary.
