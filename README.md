# CRM_PROJECT
A lightweight Customer Relationship Management (CRM) system built using .NET Core Web API, PostgreSQL, and a simple HTML/CSS + Bootstrap frontend.

?? Tech Stack
Backend

.NET Core Web API
RESTful API Architecture
JWT / Session-based Authentication
Swagger API Documentation

Frontend

HTML5
CSS3
Bootstrap
AJAX

Database

PostgreSQL

Version Control

GitHub (Branching strategy: main, dev, feature/*)


?? Features
 1. User Authentication & Authorization

User registration
Login/logout
Role-based access control (Admin, Sales Rep)
Password hashing (bcrypt or similar)


 2. Customer Management (CRUD)

Add new customers
View customers list
Update customer info
Delete customer
Search & filter
Fields: name, email, phone, address, company


3. Contact Management

Multiple contacts per customer
Add/update/delete contacts
Contact fields: name, position, email, phone


4. Notes & Interactions

Add notes for each customer
Edit/view interaction timeline
Optional reminders for follow-ups


 5. Dashboard

Total customers
Total contacts
Recent interactions
Activity feed


 6. Search & Filter

Search by name, email, company
Filter by status (active/inactive)
Filter by location


7. Data Validation

Mandatory field checks
Email format validation
API & frontend validation
Error message display


 Design Considerations

Fully responsive using Bootstrap
Clean UI layout
Proper backend exception handling


 Project Structure
/backend
   /Controllers
   /Models
   /Services
   /Repositories
   /DTOs
   /Config

/frontend
   index.html
   /pages
   /css
   /js
   /assets

/database
   schema.sql

README.md


 Project Plan (Week Sprint)
Day 1–4

Set up backend, frontend, and DB
Create GitHub repo & branching strategy
Add Swagger API documentation
Implement authentication
Customer CRUD

Day 5–6

Contact management
Notes & interaction logging

Day 7–8

Search, filtering, reporting
Dashboard metrics

Day 9

Testing (frontend + backend)
Form validation & error handling


setup instructions
Backend Setup
Shellcd backenddotnet restoredotnet builddotnet runShow more lines
Frontend Setup
Open index.html directly or use Live Server.
Database Setup
ShellCREATE DATABASE crm_app;-- Execute schema.sqlShow more lines

API Documentation
Swagger available at:
http://localhost:5000/swagger


 Contribution Workflow

Create feature branch:
git checkout -b feature/<feature-name>


Commit changes
Push & create Pull Request
Merge to dev, then to main on release

 License
Add MIT / Apache 2.0 / custom license here