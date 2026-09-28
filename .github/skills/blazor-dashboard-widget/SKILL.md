---
name: blazor-dashboard-widget
description: Scaffolds a reusable Blazor Server dashboard summary widget component that displays entity statistics such as count, totals, and trends.
version: 1.0.0
author: Frontend Architecture Team
compatibility: Requires blazor-server>=10.0
---

# Blazor Dashboard Widget Scaffolder

## Overview
This skill creates reusable dashboard widget components for the `EShop.Web` project. Each widget displays a summary metric (count, total value, etc.) for a domain entity, designed to be placed on a dashboard/home page alongside other widgets.

## Prerequisites & Inputs
* `domain_name`: The entity to summarize (e.g., `Product`, `Order`, `Customer`).
* `metric_type`: What to display — `count`, `total` (sum of a decimal field), or `latest` (most recent items).
* `api_route`: The base route for the backend API.
* `icon`: An SVG icon or emoji to display alongside the metric.

## Process Steps

1. **API Client Update:**
   Add a summary method to the existing API client (or use `GetAllAsync` and compute client-side).
   *Template:*
    ```csharp
    public async Task<int> GetCountAsync()
    {
        var items = await GetAllAsync();
        return items.Length;
    }
    ```

2. **Widget Component Generation:**
   Create `Components/Shared/{domain_name}Widget.razor`.
   *Requirement:* Reusable component with loading state and animated counter.
   *Template Structure:*
   ```html
   @inject {domain_name}ApiClient ApiClient

   <div class="dashboard-widget">
       <div class="widget-icon">
           @Icon
       </div>
       <div class="widget-content">
           @if (isLoading)
           {
               <div class="widget-skeleton"></div>
           }
           else
           {
               <span class="widget-value">@Value</span>
           }
           <span class="widget-label">@Label</span>
       </div>
   </div>

   @code {
       [Parameter] public string Label { get; set; } = "{domain_name}s";
       [Parameter] public string Icon { get; set; } = "📦";
       private string Value = "0";
       private bool isLoading = true;

       protected override async Task OnInitializedAsync()
       {
           var count = await ApiClient.GetCountAsync();
           Value = count.ToString("N0");
           isLoading = false;
       }
   }
   ```

3. **Scoped CSS Generation:**
   Create `Components/Shared/{domain_name}Widget.razor.css`.
   *Requirement:* Card with gradient accent bar, shadow, and hover effect.
   *Template:*
   ```css
   .dashboard-widget {
       display: flex;
       align-items: center;
       gap: 1rem;
       padding: 1.5rem;
       background: white;
       border-radius: 0.75rem;
       box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1);
       border-left: 4px solid var(--color-primary, #7c3aed);
       transition: transform 0.2s ease, box-shadow 0.2s ease;
   }

   .dashboard-widget:hover {
       transform: translateY(-2px);
       box-shadow: 0 10px 15px -3px rgba(0,0,0,0.1);
   }

   .widget-icon {
       font-size: 2rem;
       width: 3rem;
       height: 3rem;
       display: flex;
       align-items: center;
       justify-content: center;
   }

   .widget-value {
       font-size: 1.75rem;
       font-weight: 700;
       color: #0f172a;
       display: block;
   }

   .widget-label {
       font-size: 0.875rem;
       color: #64748b;
       text-transform: uppercase;
       letter-spacing: 0.05em;
   }

   .widget-skeleton {
       width: 4rem;
       height: 1.75rem;
       background: linear-gradient(90deg, #e2e8f0 25%, #f1f5f9 50%, #e2e8f0 75%);
       background-size: 200% 100%;
       animation: shimmer 1.5s infinite;
       border-radius: 0.375rem;
   }

   @keyframes shimmer {
       0% { background-position: -200% 0; }
       100% { background-position: 200% 0; }
   }
   ```

4. **Dashboard Integration:**
   Provide instructions for adding the widget to the home/dashboard page.
   *Template:*
   ```html
   <div class="dashboard-grid">
       <ProductWidget Label="Total Products" Icon="📦" />
       <OrderWidget Label="Orders" Icon="📋" />
       <CustomerWidget Label="Customers" Icon="👥" />
   </div>
   ```

## Verification Checklist
[ ] Widget shows loading skeleton during data fetch.
[ ] Widget is a reusable component with `[Parameter]` properties.
[ ] Scoped CSS includes hover animation and responsive sizing.
[ ] Widget can be placed on any page without layout issues.
[ ] Metric value is formatted (e.g., comma-separated numbers).
