# Assignment

Build a simple shopping cart that handles volume discounts as a class library with an accompanying test project.

Language: C#

Framework: .NET Core 3.1 or later

Expected time to complete: less than two hours.

## Introduction

Consider a store where items have prices per unit but also volume discount prices. For example, apples may be $1.00 each or 4 for $3.00.

Implement a shopping cart that accepts an arbitrary ordering of products (similar to what would happen at a checkout line) and then returns the correct total price for an entire shopping cart.

### Pricing List

Here are the products listed by code and the prices to use (there is no sales tax):

| Product Code | Price           |
| :----------- | --------------- |
| A | $2.00 each or 4 for $7.00  |
| B | $12.00                     |
| C | $1.25 or $6 for a six pack |
| D | $0.15                      |

## Requirements

There should be a top level point of sale object that implements the following `ITerminal` interface. You are free to design, implement, and add to the interface code however you wish. You are free to determine how you will specify the prices in the system:

```csharp
public interface ITerminal
{
    void Scan(string item);
    decimal Total();
}
```

## Rules

Here are the minimal inputs you should use for your test cases. These test cases must be shown to work in your program:

Scan these items in this order: ABCDABAA; Verify the total price is $32.40.

Scan these items in this order: CCCCCCC; Verify the total price is $7.25.

Scan these items in this order: ABCD; Verify the total price is $15.40.
