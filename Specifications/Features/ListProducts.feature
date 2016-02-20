Feature: List Products

Background:
    Given Bart is an ethical consumer

Scenario: User has some products saved
    Given Bart has saved the following products
    | Name                     | Notes                           |
    | Green Oil Wet Chain Lube | It's good                       |
    | Bulldog Shave Gel        | Top in ethical consumer ratings |
	When Bart views their list of products
	Then Bart's list of products should be as follows:
    | Name                     | Notes                           |
    | Green Oil Wet Chain Lube | It's good                       |
    | Bulldog Shave Gel        | Top in ethical consumer ratings |
