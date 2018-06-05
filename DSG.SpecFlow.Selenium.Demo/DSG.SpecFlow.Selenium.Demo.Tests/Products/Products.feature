Feature: Products page
	In order to...
	As a..
	I want to...
	
################################################################
#   Category -> Sub-Category chaining
################################################################

Scenario: Selecting a category with sub-categories will show the sub-category dropdown
	Given I am on the products page
	When I select the "Category with sub-categories" category
	Then the sub-category dropdown should be shown


Scenario Outline: Selecting an empty category will NOT show the sub-category dropdown
	Given I am on the products page
	When I select the "<category-name>" category
	Then the sub-category dropdown should not be shown

	Examples: 
	| category-name        |
	| Empty category one   |
	| Empty category two   |
	| Empty category three |
	
################################################################
#   Category -> Sub-Category -> Product & Sub-Product chaining
################################################################

Scenario: Selecting a sub-category with products will show the product and sub-product dropdowns
	Given I am on the products page
	When I select the "Category with sub-categories" category
	 And I select the "Sub-category with products and sub-products" sub-category
	Then the product dropdown should be shown
	 And the sub-product dropdown should be shown
	 

Scenario Outline: Selecting an empty sub-category will NOT show the product and sub-product dropdowns
	Given I am on the products page
	When I select the "Category with sub-categories" category
	 And I select the "<sub-category-name>" sub-category
	Then the product dropdown should not be shown
	 And the sub-product dropdown should not be shown

	Examples: 
	| sub-category-name        |
	| Empty sub-category one   |
	| Empty sub-category two   |
	| Empty sub-category three |
	
################################################################
#   Resetting parent dropdowns should hide chained dropdowns
################################################################

Scenario: After selecting a category with sub-categories, selecting an empty category will hide the sub-category dropdown
   Given I am on the products page
	When I select the "Category with sub-categories" category
	 And I select the "Empty category one" category
	Then the sub-category dropdown should not be shown

Scenario: After selecting a sub-category with products, selecting an empty sub-category will hide the product and sub-product dropdown
   Given I am on the products page
	When I select the "Category with sub-categories" category
	 And I select the "Sub-category with products and sub-products" sub-category
	 And I select the "Empty sub-category one" sub-category
	Then the product dropdown should not be shown
	 And the sub-product dropdown should not be shown