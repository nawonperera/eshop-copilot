---
name: blazor-detail-page
description: Scaffolds a read-only Blazor Server detail/view page for a specific domain entity, including the API client method and styled card layout.
version: 1.0.0
author: Frontend Architecture Team
compatibility: Requires blazor-server>=10.0
---

# Blazor Detail Page Scaffolder

## Overview
This skill generates a detail/view page for a domain entity in the `EShop.Web` project. It creates a dedicated page at `/{entities}/{id}` that fetches and displays a single entity with a polished card layout, loading state, and not-found handling.

## Prerequisites & Inputs
* `domain_name`: The singular name of the entity (e.g., `Product`, `Order`).
* `api_route`: The base route for the backend API (e.g., `/api/products`).
* `target_project`: Must be `EShop.Web`.
* `properties`: List of entity properties to display.

## Process Steps

1. **API Client Update:**
   Add a `GetByIdAsync` method to the existing `{domain_name}ApiClient.cs`.
   *Requirement:* Use `GetFromJsonAsync` with the entity's ID.
   *Template:*
    ```csharp
    public async Task<{domain_name}?> GetByIdAsync(int id)
        => await httpClient.GetFromJsonAsync<{domain_name}>($"{api_route}/{id}");
    ```

2. **Detail Page Generation:**
   Create `Components/Pages/{domain_name}Detail.razor`.
   *Requirement:* Handle loading, not-found, and display states.
   *Template Structure:*
   ```html
   @page "/{domain_name}s/{id:int}"
   @rendermode InteractiveServer
   @inject {domain_name}ApiClient ApiClient
   @inject NavigationManager Navigation

   <PageTitle>@(item?.Name ?? "{domain_name} Detail")</PageTitle>

   @if (isLoading)
   {
       <div class="detail-skeleton">
           <div class="skeleton-line skeleton-title"></div>
           <div class="skeleton-line"></div>
           <div class="skeleton-line"></div>
       </div>
   }
   else if (item is null)
   {
       <div class="empty-state">
           <h3>{domain_name} not found</h3>
           <p>The requested {domain_name} does not exist.</p>
           <a href="/{domain_name}s" class="btn-back">← Back to list</a>
       </div>
   }
   else
   {
       <section class="detail-card">
           <header class="detail-header">
               <a href="/{domain_name}s" class="btn-back">← Back</a>
               <h1>@item.Name</h1>
           </header>
           <div class="detail-body">
               <!-- Render each property as a labeled field -->
               @foreach property in properties
               {
                   <div class="detail-field">
                       <span class="field-label">{PropertyName}</span>
                       <span class="field-value">@item.{PropertyName}</span>
                   </div>
               }
           </div>
       </section>
   }

   @code {
       [Parameter] public int Id { get; set; }
       private {domain_name}? item;
       private bool isLoading = true;

       protected override async Task OnInitializedAsync()
       {
           item = await ApiClient.GetByIdAsync(Id);
           isLoading = false;
       }
   }
   ```

3. **Scoped CSS Generation:**
   Create `Components/Pages/{domain_name}Detail.razor.css` with:
   - Card layout with shadow and rounded corners
   - Loading skeleton animations
   - Empty state centered layout
   - Responsive design
   - Field label/value grid layout

## Verification Checklist
[ ] Page has `@page` directive with ID parameter and route constraint.
[ ] Loading state shows skeleton, not just "Loading...".
[ ] Not-found state has a back link.
[ ] API client method handles nullable return.
[ ] Scoped CSS file exists with responsive styles.
