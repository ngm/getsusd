Feature: TagProduct

Background: 
    Given Bart is an ethical consumer

Scenario: Tag product with new tag
    Given Bart has saved the following products
    | Name                     | Notes                           |
    | Green Oil Wet Chain Lube | It's good                       |
    | Bulldog Shave Gel        | Top in ethical consumer ratings |
	When Bart tags 'Green Oil Wet Chain Lube' with 'bicycle'
	Then the 'Green Oil Wet Chain Lube' product should have the 'bicycle' tag
