# 📘 TmsApi - Lab Session 1: Request Flow & Visibility

This pull request covers the implementation of **M4 Lab Session 1 – Request Flow & Visibility**.

## 🚀 Implemented Exercises

### 1. Exercise 1: The Blind Server (Middleware Ordering)
- **Problem:** The sensitive route `GET /api/assessments/results` returned sensitive JSON to anonymous callers because the authorization middleware was placed after the endpoint mapping.
- **Solution:** 
  - Registered the temporary `Training` authentication scheme and authorization services in `Program.cs`.
  - Added the `TrainingAuthHandler` which checks for a custom `X-Training-User` header.
  - Corrected the request pipeline order to ensure `app.UseRouting()`, `app.UseAuthentication()`, and `app.UseAuthorization()` run *before* mapping any endpoints.
  - Secured the endpoint by appending `.RequireAuthorization()` to the mapped route.

### 2. Exercise 1B: Custom Request Logging Middleware
- **Goal:** Enable request traceability using structured logging and Correlation IDs.
- **Solution:**
  - Implemented `RequestLoggingMiddleware` as the outer-most middleware.
  - Generates a unique short Correlation ID (`Guid.NewGuid().ToString("N")[..8]`) and sets it early in the response headers as `X-Correlation-Id`.
  - Uses `Stopwatch` to track and measure request elapsed execution time.
  - Emits queryable structured log entries on entry (method, path, correlation ID) and on exit (status code, elapsed milliseconds, correlation ID).
