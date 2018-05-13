Feature: StaticPhysicPoint
	Check I Newton law on a point

@mytag
Scenario: Point should not move when no force affects it
	Given A point in position 0, 0, 0
	And Its velocity is 0, 0, 0
	When 1 second passes
	Then the point should be at position 0, 0, 0


Scenario: Point should move lineary when has velocity
	Given A point in position 0, 0, 0
	And Its velocity is 1, 0, 0
	When 1 second passes
	Then the point should be at position 1, 0, 0