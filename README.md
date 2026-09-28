# eShop Copilot

A modern, distributed e-commerce platform built entirely using **.NET 10**, **.NET Aspire 13**, **Blazor Server**, and the **Copilot CLI**. 

This project demonstrates how AI-driven development through custom Agentic Workflows (Sub-agents & Skills) can be used to scaffold, design, and architect a full-stack, enterprise-grade application.

## 🚀 Architecture

This solution follows a microservices-inspired architecture orchestrated by .NET Aspire:

* **`EShop.AppHost`**: The central Aspire orchestrator that wires up resources, sets up service discovery, and configures health checks.
* **`EShop.ApiService`**: A Minimal API backend handling all business logic, data persistence (in-memory singletons), and RESTful endpoints.
* **`EShop.Web`**: The frontend built with Blazor Server (Interactive Server mode). It features a premium, bespoke UI inspired by the Shopify Admin aesthetic.
* **`EShop.ServiceDefaults`**: Shared infrastructure for OpenTelemetry (OTel), health checks, resilience, and service discovery across all microservices.

## 🤖 AI-Driven Development (Copilot CLI)

A major highlight of this project is that the complete UI overhaul, new feature implementations, and code quality checks were executed using **Custom Copilot Agents and Skills**.

### Custom Agents (`.agents/agents/`)
Specialized AI sub-agents were created using declarative Markdown files to handle distinct domains of the application:
1. **`blazor-ui-designer.agent.md`**: Handled the premium UI/UX overhaul, CSS design system, and responsive layouts.
2. **`api-endpoint-developer.agent.md`**: Generated Minimal API endpoints following strict grouping and minimal routing conventions.
3. **`code-quality-reviewer.agent.md`**: Enforced modern C# 12/13 features (like collection expressions) and strict code quality standards.
4. **`accessibility-performance-auditor.agent.md`**: Ensured the Blazor frontend remained performant and accessible.

### Reusable Skills (`.github/skills/`)
Reusable AI macros were defined in YAML/Markdown to standardize scaffolding:
* **`blazor-detail-page`**: Scaffolds read-only Blazor Server detail pages with API clients.
* **`blazor-dashboard-widget`**: Generates reusable, animated stat widgets for the dashboard.
* **`api-search-filter`**: Adds advanced search and filter capabilities to existing Minimal APIs.

## ✨ Features

* **Modern Dashboard**: A data-dense, clean administrative dashboard showing live metrics and featured products.
* **Product Catalog**: A responsive product grid with detailed product view pages.
* **Shopping Cart & Checkout**: Interactive cart management with a seamless checkout flow.
* **Order Management**: Comprehensive order tracking, listing, and detail views with status badges.
* **Customer CRM**: Complete CRUD operations for customer management with inline editing.
* **Premium UI**: A custom-built CSS design system (no generic Bootstrap!) featuring warm palettes, clean typography, SVG icons, and micro-animations.

## 🛠️ Getting Started

### Prerequisites
* [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
* .NET Aspire Workload

### Running Locally

The preferred way to run the application is through the Aspire AppHost. This ensures all service discovery, health checks, and OpenTelemetry dashboards are booted up correctly.

```bash
# Clone the repository
git clone <your-repo-url>
cd eshop-copilot

# Run the distributed application via Aspire
aspire run --project src/EShop.AppHost
```

Alternatively, using standard dotnet commands:
```bash
dotnet run --project src/EShop.AppHost
```

Navigate to the Aspire Dashboard URL provided in the terminal to view your running services and access the `webfrontend`.

## 📜 License
This project is open-source and available under the MIT License.