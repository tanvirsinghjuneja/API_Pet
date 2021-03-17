Feature: PetStore
	Place an order for a pet

Background:
	Given I will call the base uri  "https://petstore.swagger.io/v2/"

Scenario: Creating order
	When I perform POST operation for "/store/order" on petstore
		| id | petId | quantity | status | complete |
		| 7  | 2     | 1        | placed | true     |
	Then I should see the response with status code as 200 ok

Scenario Outline: Getting Details
	When I perform GET operation for "store/order/{orderId}" with orderId <orderId>
	Then I should see the response with status code as <StatusCode> ok

	Examples:
		| orderId | StatusCode |
		| 7       | 200        |
		| 9       | 404        |
		|         | 405        |

Scenario Outline: Delete Operation
	When I perform DELETE operation for"store/order/{orderId}" with orderId <orderId>
	Then I should see the response with status code as <StatusCode> ok

	Examples:
		| orderId | StatusCode |
		| 7       | 200        |
		| 9       | 404        |
		|         | 405        |

Scenario Outline: Create Store by POST & Verify using GET operation
	When I perform POST Operation for "store/order" using body <id>, <petId>, <quantity>, "<status>", "<complete>"
	And I perform GET operation for "store/order/{orderId}" by giving id <id>
	Then I should see the response created during POST operation with status code as <statuscode>

	Examples:
		| id | petId | quantity | status | complete | statuscode |
		| 3  | 4     | 1        | placed | true     | 200        |