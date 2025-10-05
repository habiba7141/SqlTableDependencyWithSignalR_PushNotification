# 💬 SQLTableDependency with SignalR (Real-Time Notifications)

This project demonstrates how to **send real-time notifications** from SQL Server to connected clients using **SQLTableDependency** and **SignalR** in **ASP.NET Core**.

---

## 🚀 Overview

Whenever a specific table in the SQL Server database changes (INSERT, UPDATE, DELETE),  
the **SQLTableDependency** library detects the change and notifies the **SignalR Hub**,  
which then pushes a real-time message to all connected clients instantly — without refreshing the page or polling.

---

## 🧩 Technologies Used

- ⚙️ **ASP.NET Core 8 / 7**  
- 🧠 **SignalR** — for real-time communication  
- 🗄️ **SQLTableDependency** — for listening to SQL table changes  
- 💾 **Microsoft SQL Server**  


---

## ⚡ How It Works

1. **SignalR Hub** is created to broadcast messages to clients.  
2. **SQLTableDependency** listens to a database table (e.g., `Orders`).  
3. When a change occurs in the table (Insert / Update / Delete):
   - SQLTableDependency triggers an event.
   - The event calls the SignalR Hub.
   - The Hub pushes a message to all connected clients (e.g., admin dashboard or mobile app).

---

## 🧠 Example Flow

- A new order is inserted into the `Orders` table.
- `SQLTableDependency` detects the insert.
- It sends a message like:  
  >📰 New news article has been published successfully
- The message appears instantly on the client (e.g., Flutter app or web dashboard).

