Feature: User

Background:
	Given I would call the base uri  "https://petstore.swagger.io/v2/"

Scenario: Create User
	When I Perform POST request for "user"
	Then I should see successful response with status code as 200 ok

Scenario: Update user
	When I perform PUT Operation for "user/{username}" with username "<username>"
		| id | username | firstName | Lastname | email            | password | phone      | userStatus |
		| 7  | tannu    | Tanvir    | S        | tanvir@gmail.com | tan#09   | 9876543221 | 1          |
	Then I should see response with status code as <Statuscode> ok

	Examples:
		| username | Statuscode |
		| tannu    | 200        |
		|          | 405        |