Feature: DisplacementSimplifier
	Testing feature which simplifies calculations

@mytag
Scenario: Bodies moves with the same velocity and direction
	Given I have bodyA and bodyB
	And bodyA velocity is (20, 900, 10)
	And bodyB velocity is (20, 900, 10)	
	When bodies are in different position
	Then Collision simplifier should remove their displacements
