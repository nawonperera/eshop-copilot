---
name: api-search-filter
description: Adds search and filter query parameter support to an existing Minimal API entity endpoint, including updated service logic and OpenAPI documentation.
version: 1.0.0
author: Enterprise Architecture Team
compatibility: Requires dotnet>=10.0
---

# API Search & Filter Builder

## Overview
This skill extends an existing Minimal API entity with search and filter capabilities. It adds query parameter support to the `GET /api/{entities}` endpoint, updates the service layer with filter logic, and documents the new parameters for OpenAPI.

## Prerequisites & Inputs
* `domain_name`: The entity to extend (e.g., `Product`, `Customer`).
* `searchable_fields`: Properties to search across (e.g., `Name`, `Description`).
* `filterable_fields`: Properties to filter by exact match (e.g., `Status`, `Category`).
* `sortable_fields`: Properties to sort by (e.g., `Price`, `CreatedAt`).

## Process Steps

1. **Service Update:**
   Add a filtered query method to `{domain_name}Service.cs`.
   *Requirement:* Accept optional parameters and chain LINQ filters.
   *Template:*
    ```csharp
    public IEnumerable<{domain_name}> Search(
        string? searchTerm = null,
        string? filterField = null,
        string? sortBy = null,
        bool descending = false)
    {
        var query = _items.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(x =>
                x.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                x.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filterField))
        {
            // Apply exact match filter
        }

        query = sortBy?.ToLower() switch
        {
            "price" => descending ? query.OrderByDescending(x => x.Price) : query.OrderBy(x => x.Price),
            "name" => descending ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name),
            _ => query
        };

        return query;
    }
    ```

2. **Endpoint Update:**
   Modify the `GET /` handler in `{domain_name}Endpoints.cs` to accept query parameters.
   *Requirement:* Use `[AsParameters]` or individual `string?` parameters.
   *Template:*
   ```csharp
   group.MapGet("/", (string? search, string? sort, bool? desc, {domain_name}Service service) =>
       Results.Ok(service.Search(search, sortBy: sort, descending: desc ?? false)));
   ```

3. **Pagination Support (Optional):**
   If the entity has many items, add `skip` and `take` parameters.
   *Template:*
   ```csharp
   group.MapGet("/", (string? search, int? skip, int? take, {domain_name}Service service) =>
   {
       var results = service.Search(search);
       var paged = results.Skip(skip ?? 0).Take(take ?? 20);
       return Results.Ok(new { items = paged, total = results.Count() });
   });
   ```

## Verification Checklist
[ ] Existing `GET /` endpoint still works without any query parameters (backward compatible).
[ ] Search is case-insensitive.
[ ] Filter parameters are optional — omitting them returns all items.
[ ] Sort direction defaults to ascending when `desc` is not provided.
[ ] OpenAPI tags are preserved on the route group.
