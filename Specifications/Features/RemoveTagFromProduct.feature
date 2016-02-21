Feature: Remove Tag From Product

Background: 
    Given Bart is an ethical consumer

Scenario: Remove existing tag
    Given Bart has saved the following products
    | Name                     | Notes                           | Tags    |
    | Green Oil Wet Chain Lube | It's good                       | bicycle |
    | Bulldog Shave Gel        | Top in ethical consumer ratings |         |
	When Bart removes the tag 'bicycle' from the 'Green Oil Wet Chain Lube' product
	Then the 'Green Oil Wet Chain Lube' product should not have the 'bicycle' tag

