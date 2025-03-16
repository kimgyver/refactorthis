## Instructions

This repository is comprised of badly written code and some unit tests.

The overall objective is to refactor the code and keep the tests passing. There is no 'correct' solution to this task. We are interested in how you would approach this problem, what clean maintainable code looks like to you and your ability to explain your changes.

- Changing the current tests is ok.
- Approach this as if it were the beginning of a project that was likely to become a very large and long-lived solution.
- Structure dependencies and organise the solution as you see fit.
- Refactor the "ProcessPayment" method, and give it your best shot at making it readable and maintainable.
- Complete the refactoring in .NET 472 or if upgrading **do not** delete all the existing files and add new ones. (This makes it difficult to review.)

## Expectations

- We expect this will take you between 1 and 2 hours. We are not expecting you to spend significantly longer on this task.
- Do enough to show us what your approach will be and how you would organise things.
- We will likely ask you to explain/justify your solution in one of the interview rounds.

## Submission

- Clone this repository and push it to a private repository under your own account. (You are welcome to fork the repository, but that will be publically visible.)
- Branch, make your changes, and **create a pull request**.
- Invite _re-leased-hiring_ as a reviewer to the PR.
- Let your hiring rep know your refactoring task is ready for review.

## Clean Architecture

- Service layer (outermost) : InvoiceService
- Persistence (or Infrastructure) layer : InvoiceRepository
- Domain layer (innermost): Invoice, Payment, InvoiceType

## Refactoring directions

- Reduce Code Duplication
- Encapsulate Logic into private Methods with rule names (improve readability)
  Example: IsNoPaymentNeeded(), IsInvoiceFullyPaid(), IsPaymentExcessive()
- Use Guard Clauses (Replace deeply nested if conditions with early returns)
- Encapsulate State Changes
  . Implemented ApplyPayment() in Invoice, allowing the invoice to update its own state

## Dependency injection

Uses dependency injection (DI) to manage dependencies efficiently

| Interface            | Implementation      | Layer             |
| -------------------- | ------------------- | ----------------- |
| `IInvoiceService`    | `InvoiceService`    | Service Layer     |
| `IInvoiceRepository` | `InvoiceRepository` | Persistence Layer |

## Testing

- Preserve the existing test logic.
