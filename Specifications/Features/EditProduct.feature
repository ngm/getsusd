Feature: Edit Product

Background: 
    Given Bart is an ethical consumer

Scenario: Edit to product
    Given Bart has saved the following products
    | Name                     | Notes                           |
    | Green Oil Wet Chain Lube | It's good                       |
    | Bulldog Shave Gel        | Top in ethical consumer ratings |
	When Bart changes the name of 'Green Oil Wet Chain Lube' to 'Green Oil Chain Lube'
	Then the following products exist
    | Name                 | Notes                           |
    | Green Oil Chain Lube | It's good                       |
    | Bulldog Shave Gel    | Top in ethical consumer ratings |
