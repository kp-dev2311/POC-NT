# **POC for User Activity Tracking**

## **Project Structure**

This project follows an **N-Tier Architecture** with a clear separation of concerns:

### **Solution Layers**

Each layer is responsible for a specific part of the application:

| Layer                     | Description                                                  |
| ------------------------- | ------------------------------------------------------------ |
| **1. POC.Api**            | Entry Point (Controllers, API Endpoints) (Startup Prj)                     |
| **2. POC.Application**    | Contains DTOs, Mappings, and Business Logic Services         |
| **3. POC.ConsoleUI**      | User Interface (Console-Based UI) (Startup Prj)                           |
| **4. POC.Domain**         | Defines Models and Interfaces (Core Business Entities)       |
| **5. POC.Infrastructure** | Handles Database, Migrations, and Repository Implementations |

---

## **System Architecture**

The project is organized into **four primary layers**:

### **1. Presentation Layer**

- Handles **user interaction** (e.g., Web App or Console UI).
- Contains **Controllers** (for API) or **UI Logic** (for Console UI).

### **2. Service Layer**

- Implements **Business Logic** and **Rules for Processing Data**.
- Uses **Repository Layer** for Data Fetching and Transformation.

### **3. Repository Layer**

- Handles **Data Access** via **Entity Framework Core (EF Core)**.
- Responsible for **CRUD operations** (Create, Read, Update, Delete).

### **4. Data Layer**

- Contains the **actual database or storage system**.

---

## **Key Concepts**

### **1. Repository Pattern**

- `` → Defines methods that every repository must implement.
  - Example: `GetAllAsync`, `GetByIdAsync`, `CreateAsync`, `DeleteAsync`, `UpdateAsync`.
  - Acts as a **contract** for database interactions.
- `` → Implements `IRepository` and contains actual database interaction logic.

### **2. Service Layer**

- Uses the **Repository Layer** to fetch and save data.

- Applies **business logic** before returning results.

- `` → Defines methods that service classes must implement.

  - Example: `GetAllUsersAsync`, `GetUserActivityAsync`.

- **Creating a Service for Each Entity**

  - Example:
    - `UserService` → Handles User-specific logic.
    - `UserActivityService` → Manages User Activities.

---

## **Workflow (How Data Flows)**

```
1. User sends an HTTP Request → Presentation Layer (Controller receives request).
2. Controller calls → Service Layer (`UserService` or `UserActivityService`).
3. Service Layer calls → Repository Layer (`IUserRepository.GetAllAsync()`).
4. Repository Layer fetches data from → Database.
5. Data is returned to the Service Layer (Transforms it, maps to DTOs).
6. Controller receives transformed data and sends a JSON response back to the user.
```

---

## **Architectural Principles Used**

✅ Repository Pattern (For Data Access)\
✅ N-Tier Architecture (Layered Structure)\
✅ SOLID Principles (Maintainability & Scalability)\
✅ Separation of Concerns (Independent Responsibilities)\
✅ Class Libraries (Modular & Organized Codebase)

---

## **Code-First Approach (EF Core)**

### **Step 1: Install EF Core Tools**

If required, install EF Core tools:

```bash
dotnet tool install --global dotnet-ef
```

### **Step 2: Database Migration**

After updating the **Connection String**, run:

```bash
dotnet ef database update -s ..\POC.Api\
```

### **Step 3: Indexing for Performance Optimization**

Ensure an **index is created** on frequently queried columns:

```sql
CREATE INDEX idx_user_session_activity
ON UserActivities (UserID, SessionID, ActivityTimeStamp);
```

This improves **query performance** when filtering by `UserID`, `SessionID`, and `ActivityTimeStamp`.

---

## **Future Enhancements**

✅ **Enhance UI** → Web-based dashboard with analytics.\
✅ **Machine Learning** → Predict user behavior.\


---

## **Conclusion**

This **POC for User Activity Tracking** ensures:

- ✅ **Scalability** with **N-Tier Architecture**.
- ✅ **Efficient Data Access** using **Repository Pattern**.
- ✅ **Optimized Queries** with **Indexing**.
- ✅ **Separation of Concerns** for **maintainability**.

---

### **Contributors**

- **Project Owner:** KP
- **Developers:** Kamatchi Pandian J	
- **Last Updated:** `2025-03-15`

---
