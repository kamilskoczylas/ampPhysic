Feature: SphereKinematics
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@sphere
Scenario: I should be notified when sphereA has collision with a sphereB
	Given A sphere in position (0, 0, 0) and radius 1 and a sphere in position (10, 0, 0) and radius 0.99
	And The bodyA velocity is (2, 0, 0) and bodyB velocity is (1, 0, 0)
	And After 8 seconds nothing happens
	Then After 0.01 seconds I should recive collision event