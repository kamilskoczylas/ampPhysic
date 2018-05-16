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


Scenario: Point should not change the speed when forces are balanced
	Given A point in position 0, 0, 0
	And Its velocity is 1, 0, 0
	And There is a force affecting this body 20 N in direction (200, -900, 100)
	And There is a force affecting this body 20 N in direction (-200, 0, 100)
	And There is a force affecting this body 40 N in direction (0, 450, -100)
	When 100 second passes
	Then the point should be at position 100, 0, 0