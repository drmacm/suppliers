Feature: CreateSupplier
	In order to have up-to-date list of suppliers
	As a supply manager
	I want to be able to add new suppliers

@create
Scenario: Create a new supplier
	Given I have entered 'testSupplier' as name 
	And I have entered 'testAddress' as address 
	And I have entered 'test@email.com' as email 
	And I have entered '123123123' as phone number
	And I have chosen 'Cleaners' from the 'Group' drop down list
	When I press 'Create' button
	Then I should have one more supplier
