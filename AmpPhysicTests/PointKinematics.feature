Feature: PointKinematics
	Test behavior when point has collision with other objects

@ignore
Scenario: Bugatti Veyron acceleration
	Given A car with engine power 1001 HP and mass of 1888 kg
	And Its engine efficency is typical as usually 40%
	And Its starting position is (0, 0, 0)
	And Its direction is (1, 0, 0)
	And Its air resistance factor is 0.4 Surface of the front of car is 2 sqare meters
	When 1 second passes
	Then Car velocity should be 100 km/h and position (70, 0, 0)

@ignore
Scenario: Renault Scenic acceleration
	Given A car with engine power 113 HP and mass of 1340 kg
	And Its engine efficency is typical as usually 40%
	And Its starting position is (0, 0, 0)
	And Its direction is (1, 0, 0)
	And Its air resistance factor is 0.4 Surface of the front of car is 2 sqare meters
	When 15 second passes
	Then Car velocity should be 100 km/h and position (230, 0, 0)


Scenario: I should be notified when pointA has collision with another object
	Given A bodyA in position (0, 0, 0) and bodyB in position (10, 0, 0)
	And The bodyA velocity is (2, 0, 0) and bodyB velocity is (1, 0, 0)
	And After 7 seconds nothing happens	
	Then After 2 seconds I should recive collision event


Scenario: A point moves behind and very closely to another point with the same speed without collision