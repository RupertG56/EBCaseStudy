# EB CaseStudy

## Introduction

I put this application together using GitHub Copilot and my own knowledge of C# to manipulate code and change it to accomplish my end goal.

Unfortunately even with the help of AI I was unable to get all pieces together within the constraint of 1 hour. I had the Prices and Orders page done within that hour.

There was debugging and work to get other pieces doing exaclty what I wanted that contributed to the longer time. The whole thing took about 3 hours, and it is still not as polished as I would like

### Running the app:

Everything should be self-contained within the project. Having Visual Studio Community with .NET 10 installed should allow the project to be loaded and run either with or without debugging. No additional libraries are necessary. That's just a quick local run, without deploying it.

## Notes

* Architecture
    * Blazor Web App using interactive server components
    * Typed HTTP client which allows for DI and easy testability
* Data Model Decisions
    * Kept API DTOs separate from application models for easier tracking of which models are used where
    * Orders store a price snapshot on each OrderItem (captures current price at time of ordering)
    * Kept the shape of the Order/OrderItem simple to avoid complex versioning
* What I built
    * Mobile-friendly responsive cards for Pricing and Orders
    * Product search + selection to drive weekly averages (though I couldn't find an interesting range to show some deviation in weeks)
    * Reorder page to look through existing orders and reorder what is on them
* Trade-offs / what I skipped or left minimal
    * Used EF Core EnsureCreated for simplicity instead of using migrations and deployment-ready DB operations
    * No resiliency is present such as retry/backoff for failed API calls
    * OrderRepo lack full CRUD. Only Create/Read





