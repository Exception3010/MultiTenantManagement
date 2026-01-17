# Centralized Multi-Tenant Management System

**Based on Multi-Tenant Architecture and Cloud Computing (AWS)**

## 📌 Overview

This project is a **centralized multi-tenant web-based management system** designed to serve multiple independent organizations (tenants) using a single shared application and database, while ensuring **strict data isolation** and **secure access control**.

The system follows modern **Multi-Tenant Architecture** principles commonly used in SaaS platforms and is deployed on **cloud infrastructure (AWS)** to ensure scalability, availability, and cost efficiency.

---

## 🎯 Project Objectives

* Design and implement a centralized system using **Multi-Tenant Architecture**
* Allow multiple organizations/stores to operate independently on the same system
* Ensure complete **data isolation** using `tenant_id`
* Apply **Role-Based Access Control (RBAC)** per tenant
* Provide a **Super Admin** layer for global management
* Utilize cloud services to enhance scalability and security

---

## 🧩 System Roles

* **Tenant User**: Accesses and manages data related only to their organization
* **Tenant Admin**: Manages users, roles, and configurations within their tenant
* **Super Admin**: Manages tenants and system configuration without accessing tenant data

---

## 🏗️ System Architecture

* Centralized Web API
* Multi-Tenant data isolation using `tenant_id`
* JWT-based authentication with tenant context
* Role-based authorization
* Cloud deployment on AWS

---

## 🛠️ Technologies Used

### Backend

* **ASP.NET Core Web API**
* **C#**
* **Entity Framework Core**
* **JWT Authentication**
* **RBAC (Role-Based Access Control)**

### Frontend

* **React.js**

### Database

* **PostgreSQL**
* Logical data isolation using `tenant_id`

### Cloud & Infrastructure (AWS)

* Amazon RDS (PostgreSQL)
* Amazon S3 (file storage)
* CloudFront
* Application Load Balancer
* CloudWatch (monitoring & logging)

### Utilities & Libraries

* BCrypt.Net (password hashing)
* FluentValidation
* AutoMapper
* appsettings.json

---

## 🔐 Security Features

* JWT-based authentication
* Tenant-aware authorization
* Role-based access control
* Enforced tenant isolation at API and database levels

---

## 🔄 Applied Use Case Workflow

1. User selects a tenant (store/organization)
2. User authenticates and receives a JWT containing `tenant_id`
3. All API requests are filtered by `tenant_id`
4. Tenant admins manage products and orders
5. Super admin manages tenants and monitors the system globally

---

## 🚀 Expected Outcomes

* Fully functional centralized multi-tenant system
* Secure and isolated tenant data
* Scalable cloud-based architecture
* Practical implementation aligned with real-world SaaS systems
* Strong academic and professional project foundation

---

## 👨‍🎓 Team Members

* Abdalkareem Alsahhar
* Mousa Alsultan
* Salem Thabit
* Mazen Rajab
* Hazem Oukal

**Supervised by:**
Eng. Belal Elfarra

---

## 📅 Academic Year

2025 – Graduation Project

---

## ▶️ How to Run the Project (Basic)

1. Clone the repository
2. Configure database connection in `appsettings.json`
3. Apply migrations
4. Run the ASP.NET Core Web API
5. Start the React frontend
6. Access the system via browser

---

## Apply EF Core migrations

```powershell
dotnet ef database update --project .\MultiTenantManagement.Data\MultiTenantManagement.Data.csproj --startup-project .\MultiTenantManagement.API\MultiTenantManagement.API.csproj
```

---

This repository represents a **practical, scalable, and secure Multi-Tenant SaaS-ready architecture**, suitable for academic evaluation and real-world extension.
