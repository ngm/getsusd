Feature: AddProduct
	In order to keep track of my ethical choices
	As a user
	I want to record the products I buy 

Background: 
    Given Bart is an ethical consumer

@mytag
Scenario: Add product with name and notes
    When Bart adds the product:
    | Name                     | Notes                                                                            |
    | Green Oil Wet Chain Lube | I use this to oil my chain.  It's plant-based, bio-degradable, and made locally. |
    Then the product is added to Bart's list of products
